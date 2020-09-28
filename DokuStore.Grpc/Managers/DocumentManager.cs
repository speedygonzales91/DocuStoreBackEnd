using DocuStore.DAL.Interfaces;
using DocuStore.DAL.Models;
using DokuStore.Grpc.Helpers;
using DokuStore.Grpc.Interfaces.Managers;
using DokuStore.Grpc.Protos;
using Google.Protobuf;
using Grpc.Core;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DokuStore.Grpc.Managers
{
    public class DocumentManager : IDocumentManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly int providerValue;

        public DocumentManager(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            this._unitOfWork = unitOfWork;
            this._configuration = configuration;

            var provider = _configuration.GetSection("Provider").Value;
            switch (provider)
            {
                case "Google":
                    providerValue = (int)ProvidersEnum.Google;
                    break;
                default:
                    break;
            }
        }

        public ItemResponse CreateItem(CreateItemRequest request)
        {
            var item = new Item();
            item.TypeId = request.TypeId;
            item.Name = request.Name;
            item.CreatedAt = DateTime.Now;
            item.CreatedBy = 1;
            item.ProjectId = request.ProjectId;
            item.Provider = providerValue;
            item.ParentItemId = request?.ParentId;

            _unitOfWork.ItemRepository.Insert(item);
            _unitOfWork.ItemRepository.Save();

            return new ItemResponse { Id = item.Id, Name = item.Name };

        }

        public ItemResponse GetItemDetails(long id)
        {
            var item = _unitOfWork.ItemRepository.GetItem(id);
            if (item != null)
            {
                new ItemResponse { Id = item.Id, Name = item.Name, ParentId = item.ParentItemId, ProjectId = item.ProjectId, Size = item.Size, TypeId = item.TypeId };
            }

            return new ItemResponse();
        }

        public ItemResponse UpdateItem(UpdateItemRequest request)
        {
            var item = _unitOfWork.ItemRepository.GetItem(request.Id);
            if (item != null)
            {
                item.Name = request.Name;
                item.ModifiedAt = DateTime.Now;
                item.ModifiedBy = 1;
            }
            _unitOfWork.ItemRepository.Save();

            return new ItemResponse { Id = item.Id, Name = item.Name, ParentId = item.ParentItemId, ProjectId = item.ProjectId, Size = item.Size, TypeId = item.TypeId };
        }

        public List<ItemResponse> GetItems(GetItemsRequest request)
        {
            List<ItemResponse> result = new List<ItemResponse>();
            List<Item> items = _unitOfWork.ItemRepository.GetActiveItemsByProvider(providerValue);

            foreach (var item in items)
            {
                result.Add(new ItemResponse { Id = item.Id, Name = item.Name, ParentId = item.ParentItemId, ProjectId = item.ProjectId, Size = item.Size, TypeId = item.TypeId });
            }
            return result;
        }

        public DeleteItemResponse DeleteItem(long id)
        {
            var idsToDelete = GetAllIdsToDelete(id);

            //var addmore = GetParentId(id).ToList();
            foreach (var idToDelete in idsToDelete)
            {
                var itemToDelete = _unitOfWork.ItemRepository.GetItem(idToDelete);
                itemToDelete.IsDeleted = true;
                itemToDelete.DeletedBy = 1;
                itemToDelete.DeletedAt = DateTime.Now;

                _unitOfWork.ItemRepository.Save();
            }

            return new DeleteItemResponse();
        }

        public List<long> GetAllIdsToDelete(long rootId)
        {
            List<long> list = new List<long>();
            Traverse(rootId);
            return list;

            void Traverse(long categoryId)
            {
                Item c = _unitOfWork.ItemRepository.GetItem(categoryId);
                list.Add(categoryId);

                if (c.ParentItemId != null)
                {
                    Traverse(c.ParentItemId.Value);
                }
            }
        }

        /// <summary>
        /// https://stackoverflow.com/questions/60661438/c-sharp-grpc-file-streaming-original-file-smaller-than-the-streamed-one
        /// </summary>
        /// <param name="id"></param>
        /// <param name="responseStream"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task DownloadItem(long id, IServerStreamWriter<DataChunkResponse> responseStream, ServerCallContext context)
        {
            try
            {

                String filePath = @"C:\Users\molnar.zsolt\DokuStore\Root\test.txt";

                FileInfo fileInfo = new FileInfo(filePath);
                DataChunkResponse chunk = new DataChunkResponse();

                chunk.FileName = Path.GetFileName(filePath);
                chunk.FileSize = fileInfo.Length;

                int fileChunkSize = 64 * 1024;

                byte[] fileByteArray = File.ReadAllBytes(filePath);
                byte[] fileChunk = new byte[fileChunkSize];
                int fileOffset = 0;

                while (fileOffset < fileByteArray.Length && !context.CancellationToken.IsCancellationRequested)
                {
                    int length = Math.Min(fileChunkSize, fileByteArray.Length - fileOffset);
                    Buffer.BlockCopy(fileByteArray, fileOffset, fileChunk, 0, length);
                    fileOffset += length;
                    ByteString byteString = ByteString.CopyFrom(fileChunk);

                    chunk.Chunk = byteString;

                    await responseStream.WriteAsync(chunk).ConfigureAwait(false);
                }
            }
            catch
            {

            }
        }
    }

}

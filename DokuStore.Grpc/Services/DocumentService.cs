using DokuStore.Grpc.Interfaces.Managers;
using DokuStore.Grpc.Protos;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DokuStore.Grpc.Services
{
    public class DocumentService : DocumentServiceDefinition.DocumentServiceDefinitionBase
    {
        private readonly IDocumentManager _documentManager;

        public DocumentService(IDocumentManager documentManager)
        {
            this._documentManager = documentManager;
        }

        public override Task<ItemResponse> CreateItem(CreateItemRequest request, ServerCallContext context)
        {
            return Task.FromResult(_documentManager.CreateItem(request));
        }

        public override Task<ItemResponse> GetItemDetails(GetItemDetailsRequest request, ServerCallContext context)
        {
            return Task.FromResult(_documentManager.GetItemDetails(request.Id));
        }

        public override Task<ItemResponse> UpdateItem(UpdateItemRequest request, ServerCallContext context)
        {
            return Task.FromResult(_documentManager.UpdateItem(request));
        }

        public override async Task GetItems(GetItemsRequest request, IServerStreamWriter<ItemResponse> responseStream, ServerCallContext context)
        {
            List<ItemResponse> items = _documentManager.GetItems(request);
            foreach (var item in items)
            {
                await responseStream.WriteAsync(item);
            }
        }

        public override Task<DeleteItemResponse> DeleteItem(DeleteItemRequest request, ServerCallContext context)
        {
            return Task.FromResult(_documentManager.DeleteItem(request.Id));
        }

        public override Task DownloadItem(DownloadItemRequest request, IServerStreamWriter<DataChunkResponse> responseStream, ServerCallContext context)
        {
            return Task.FromResult(_documentManager.DownloadItem(request.Id, responseStream, context));
        }
    }
}

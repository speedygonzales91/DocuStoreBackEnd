using DokuStore.Grpc.Protos;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DokuStore.Grpc.Interfaces.Managers
{
    public interface IDocumentManager
    {
        ItemResponse CreateItem(CreateItemRequest request);
        ItemResponse GetItemDetails(long id);
        ItemResponse UpdateItem(UpdateItemRequest request);
        List<ItemResponse> GetItems(GetItemsRequest request);
        DeleteItemResponse DeleteItem(long id);
        Task DownloadItem(long id, IServerStreamWriter<DataChunkResponse> responseStream, ServerCallContext context);
    }
}

using DocuStore.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocuStore.DAL.Interfaces.Repositories
{
    public interface IItemRepository : IGenericRepository<Item>
    {
        Item GetItem(long id);
        List<Item> GetActiveItemsByProvider(int providerValue);
    }
}

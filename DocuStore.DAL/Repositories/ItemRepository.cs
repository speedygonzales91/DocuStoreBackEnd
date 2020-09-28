using DocuStore.DAL.Interfaces.Repositories;
using DocuStore.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocuStore.DAL.Repositories
{
    public class ItemRepository : GenericRepository<Item>, IItemRepository
    {
        public ItemRepository(DocuStoreContext context) : base(context)
        {

        }

        public Item GetItem(long id)
        {
            return dbSet.FirstOrDefault(x => x.Id == id);
        }

        public List<Item> GetActiveItemsByProvider(int providerValue)
        {
            return dbSet.Where(x => !x.IsDeleted && x.Provider == providerValue).ToList();
        }
    }
}

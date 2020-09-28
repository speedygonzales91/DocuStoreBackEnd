using DocuStore.DAL.Interfaces.Repositories;
using DocuStore.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocuStore.DAL.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(DocuStoreContext context) : base(context)
        {

        }

        public List<Customer> GetActiveCustomers()
        {
            return dbSet.Where(x => !x.IsDeleted).ToList();
        }
    }
}

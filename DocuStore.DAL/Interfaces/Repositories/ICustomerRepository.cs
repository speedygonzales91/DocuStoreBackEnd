using DocuStore.DAL.Models;
using DocuStore.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocuStore.DAL.Interfaces.Repositories
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
        List<Customer> GetActiveCustomers();
    }
}

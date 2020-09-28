using DocuStore.DAL.Models;
using DokuStore.Grpc.Protos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DokuStore.Grpc.Interfaces.Managers
{
    public interface ICustomerManager
    {
        CustomerResponse CreateCustomer(CreateCustomerRequest customerRequest);
        UpdateCustomerResponse UpdateCustomer(UpdateCustomerRequest request);
        IEnumerable<CustomerResponse> GetCustomers(GetCustomersRequest request);
        DeleteCustomerResponse DeleteCustomer(int id);
    }
}

using DocuStore.DAL.Models;
using DokuStore.Grpc.Interfaces.Managers;
using DokuStore.Grpc.Protos;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DokuStore.Grpc.Services
{
    public class CustomerService : CustomerServiceDefinition.CustomerServiceDefinitionBase
    {
        private readonly ICustomerManager _customerManager;

        public CustomerService(ICustomerManager customerManager)
        {
            this._customerManager = customerManager;
        }

        
        public override Task<CustomerResponse> CreateCustomer(CreateCustomerRequest request, ServerCallContext context)
        {
            var newCustomer = _customerManager.CreateCustomer(request);
            return Task.FromResult(newCustomer);
        }

        public override Task<DeleteCustomerResponse> DeleteCustomer(DeleteCustomerRequest request, ServerCallContext context)
        {
            return Task.FromResult(_customerManager.DeleteCustomer(request.Id));
        }

        public override Task<UpdateCustomerResponse> UpdateCustomer(UpdateCustomerRequest request, ServerCallContext context)
        {
            return Task.FromResult(_customerManager.UpdateCustomer(request));
        }

        public override async Task GetCustomers(GetCustomersRequest request, IServerStreamWriter<CustomerResponse> responseStream, ServerCallContext context)
        {
            var customers = _customerManager.GetCustomers(request).ToList();
            foreach (var customer in customers)
            {
                await responseStream.WriteAsync(customer);
            }
        }
    }
}

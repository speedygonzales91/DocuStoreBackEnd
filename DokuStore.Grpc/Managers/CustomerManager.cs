using DocuStore.DAL.Interfaces;
using DocuStore.DAL.Models;
using DokuStore.Grpc.Interfaces.Managers;
using DokuStore.Grpc.Protos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DokuStore.Grpc.Managers
{
    public class CustomerManager : ICustomerManager
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerManager(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        
        /// <summary>
        /// Create new Customre
        /// </summary>
        /// <param name="customerRequest"></param>
        /// <returns></returns>
        public CustomerResponse CreateCustomer(CreateCustomerRequest customerRequest)
        {
            var customer = new Customer();
            customer.CreatedAt = DateTime.Now;
            customer.CreatedBy = 1;
            customer.Name = customerRequest.Name;

            _unitOfWork.CustomerRepository.Insert(customer);

            // Innen a Save()-be nem kell Guid
            _unitOfWork.CustomerRepository.Save();

            return new CustomerResponse { Id = customer.Id, Name = customer.Name };
        }

        public DeleteCustomerResponse DeleteCustomer(int id)
        {
            var customerToDelete = _unitOfWork.CustomerRepository.GetByID(id);
            customerToDelete.IsDeleted = true;
            customerToDelete.DeletedAt = DateTime.Now;
            customerToDelete.DeletedBy = 1;

            _unitOfWork.CustomerRepository.Save();

            return new DeleteCustomerResponse();
        }

        public UpdateCustomerResponse UpdateCustomer(UpdateCustomerRequest request)
        {
            var customer = _unitOfWork.CustomerRepository.GetByID(request.Id);
            customer.ModifiedAt = DateTime.Now;
            customer.ModifiedBy = 1;
            customer.Name = request.Name;

            _unitOfWork.CustomerRepository.Save();

            return new UpdateCustomerResponse { Id = customer.Id, Name = customer.Name };
        }

        public IEnumerable<CustomerResponse> GetCustomers(GetCustomersRequest request)
        {
            var customers = _unitOfWork.CustomerRepository.GetActiveCustomers();
            List<CustomerResponse> customersResponse = new List<CustomerResponse>();

            foreach (var customer in customers)
            {
                customersResponse.Add(new CustomerResponse { Id = customer.Id, Name = customer.Name });
            }

            return customersResponse;
        }

    }
}

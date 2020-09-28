using DocuStore.DAL.Interfaces;
using DocuStore.DAL.Interfaces.Repositories;
using DocuStore.DAL.Models;
using DokuStore.Grpc.Interfaces.Managers;
using DokuStore.Grpc.Protos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DokuStore.Grpc.Managers
{
    public class RoleManager : IRoleManager
    {
        private readonly IUnitOfWork _unitOfWork;

        public RoleManager(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public RoleResponse CreateRole(string name)
        {
            var role = new Role();
            role.Name = name;

            _unitOfWork.RoleRepository.Insert(role);
            _unitOfWork.RoleRepository.Save();

            return new RoleResponse { Id = role.Id, Name = role.Name };
        }

        public List<RoleResponse> GetRoles()
        {
            var result = new List<RoleResponse>();
            var roles = _unitOfWork.RoleRepository.GetAll();

            foreach (var role in roles)
            {
                result.Add(new RoleResponse { Id = role.Id, Name = role.Name });
            }

            return result;
        }
    }
}

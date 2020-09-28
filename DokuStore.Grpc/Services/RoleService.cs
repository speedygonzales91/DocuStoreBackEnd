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
    public class RoleService : RoleServiceDefinition.RoleServiceDefinitionBase
    {
        private readonly IRoleManager _roleManager;

        public RoleService(IRoleManager roleManager)
        {
            this._roleManager = roleManager;
        }

        public override Task<RoleResponse> CreateRole(CreateRoleRequest request, ServerCallContext context)
        {
            return Task.FromResult(_roleManager.CreateRole(request.Name));
        }

        public override async Task GetRoles(GetRolesRequest request, IServerStreamWriter<RoleResponse> responseStream, ServerCallContext context)
        {
            List<RoleResponse> roles = _roleManager.GetRoles();
            foreach (var role in roles)
            {
                await responseStream.WriteAsync(role);
            }
        }
    }
}

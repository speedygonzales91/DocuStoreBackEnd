using DocuStore.DAL.Models;
using DokuStore.Grpc.Protos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DokuStore.Grpc.Interfaces.Managers
{
    public interface IRoleManager
    {
        RoleResponse CreateRole(string name);
        List<RoleResponse> GetRoles();
    }
}

using DocuStore.DAL.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocuStore.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        ICustomerRepository CustomerRepository { get; }
        IProjectRepository ProjectRepository { get; }
        IRoleRepository RoleRepository { get; }
        IProjectRoleRepository ProjectRoleRepository { get; }
        IItemRepository ItemRepository { get; }
    }
}

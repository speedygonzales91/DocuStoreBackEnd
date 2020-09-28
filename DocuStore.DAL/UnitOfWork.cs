using DocuStore.DAL.Interfaces;
using DocuStore.DAL.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocuStore.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        public ICustomerRepository CustomerRepository { get; private set; }
        public IProjectRepository ProjectRepository { get; private set; }
        public IRoleRepository RoleRepository { get; private set; }
        public IProjectRoleRepository ProjectRoleRepository { get; private set; }
        public IItemRepository ItemRepository { get; private set; }

        public UnitOfWork(
            ICustomerRepository customerRepository,
            IProjectRepository projectRepository,
            IRoleRepository roleRepository,
            IProjectRoleRepository projectRoleRepository,
            IItemRepository itemRepository
            )
        {
            this.CustomerRepository = customerRepository;
            this.ProjectRepository = projectRepository;
            this.RoleRepository = roleRepository;
            this.ProjectRoleRepository = projectRoleRepository;
            this.ItemRepository = itemRepository;
        }
    }
}

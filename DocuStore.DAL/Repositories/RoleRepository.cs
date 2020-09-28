using DocuStore.DAL.Interfaces.Repositories;
using DocuStore.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocuStore.DAL.Repositories
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(DocuStoreContext context) : base(context)
        {

        }
    }
}

using DocuStore.DAL.Interfaces.Repositories;
using DocuStore.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocuStore.DAL.Repositories
{
    public class ProjectRepository : GenericRepository<Project>, IProjectRepository
    {
        public ProjectRepository(DocuStoreContext context) : base(context)
        {

        }

        public List<Project> GetActiveProjects()
        {
            return dbSet.Where(x => !x.IsDeleted).Include(x=>x.ProjectRoles).ToList();
        }

        public List<Project> GetActiveProjectsByUser(int identityId, int identityType)
        {
            var query = dbSet.Where(x => x.ProjectRoles.Any(x => x.IdentityId == identityId && x.IdentityType == identityType));

            query = query.Include(x => x.ProjectRoles);

            return query.ToList();
        }
    }
}

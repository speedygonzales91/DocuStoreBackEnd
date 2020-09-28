using DocuStore.DAL.Interfaces.Repositories;
using DocuStore.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocuStore.DAL.Repositories
{
    public class ProjectRoleRepository : GenericRepository<ProjectRoles>, IProjectRoleRepository
    {
        public ProjectRoleRepository(DocuStoreContext context) :  base(context)
        {

        }

        public ProjectRoles GetActiveRight(int identityId, int projectId, int roleId)
        {
            return dbSet.FirstOrDefault(x => !x.IsDeleted && x.IdentityId == identityId && x.RoleId == roleId);
        }

        public Dictionary<int, List<int>> GetRolesOfProject(int projectId)
        {
            Dictionary<int, List<int>> result = new Dictionary<int, List<int>>();
            var projectRoles = dbSet.Where(x => !x.IsDeleted && x.ProjectId == projectId);

            foreach (var projectRole in projectRoles)
            {
                if (result.Any(x=>x.Key == projectRole.IdentityType))
                {
                    result[projectRole.IdentityType].Add(projectRole.IdentityId);
                }
                else
                {
                    result.Add(projectRole.IdentityType, new List<int> { projectRole.IdentityId });
                }
            }

            return result;
        }
    }
}

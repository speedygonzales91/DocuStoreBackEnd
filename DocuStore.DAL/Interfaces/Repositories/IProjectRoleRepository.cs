using DocuStore.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocuStore.DAL.Interfaces.Repositories
{
    public interface IProjectRoleRepository : IGenericRepository<ProjectRoles>
    {
        ProjectRoles GetActiveRight(int identityId, int projectId, int roleId);
        Dictionary<int, List<int>> GetRolesOfProject(int projectId);
    }
}

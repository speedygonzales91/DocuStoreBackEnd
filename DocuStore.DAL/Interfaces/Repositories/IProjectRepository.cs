using DocuStore.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocuStore.DAL.Interfaces.Repositories
{
    public interface IProjectRepository : IGenericRepository<Project>
    {
        List<Project> GetActiveProjects();
        List<Project> GetActiveProjectsByUser(int identityId, int identityType);
    }
}

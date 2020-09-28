using DocuStore.DAL.Models;
using DokuStore.Grpc.Protos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DokuStore.Grpc.Interfaces.Managers
{
    public interface IProjectManager
    {
        ProjectResponse CreateProject(CreateProjectRequest request);
        ProjectResponse UpdateProjet(UpdateProjectRequest request);
        DeleteProjectResponse DeleteProject(int id);
        List<ProjectResponse> GetProjectsByUser(int identityType, int identityId);
        AddUserToProjectResponse AddUserToProject(AddUserToProjectRequest request);
        RemoveUserRoleFromProjectResponse RemoveUserRoleFromProject(RemoveUserRoleFromProjectRequest request);
        List<UserResponse> GetUsersByProject(int projectId);
    }
}

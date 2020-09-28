using DokuStore.Grpc.Interfaces.Managers;
using DokuStore.Grpc.Protos;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DokuStore.Grpc.Services
{
    public class ProjectService : ProjectServiceDefinition.ProjectServiceDefinitionBase
    {
        private readonly IProjectManager _projectManager;

        public ProjectService(IProjectManager projectManager)
        {
            this._projectManager = projectManager;
        }

        public override Task<ProjectResponse> CreateProject(CreateProjectRequest request, ServerCallContext context)
        {
            return Task.FromResult(_projectManager.CreateProject(request));
        }

        public override Task<ProjectResponse> UpdateProject(UpdateProjectRequest request, ServerCallContext context)
        {
            return Task.FromResult(_projectManager.UpdateProjet(request));
        }

        public override Task<DeleteProjectResponse> DeleteProject(DeleteProjectRequest request, ServerCallContext context)
        {
            return Task.FromResult(_projectManager.DeleteProject(request.Id));
        }

        
        public override async Task GetProjectsByUser(GetProjectsByUserRequest request, IServerStreamWriter<ProjectResponse> responseStream, ServerCallContext context)
        {
            var projects = _projectManager.GetProjectsByUser(request.IdentityType, request.IdentityId);

            foreach (var project in projects)
            {
                 await responseStream.WriteAsync(project);
            }
        }

        public override Task<AddUserToProjectResponse> AddUserToProject(AddUserToProjectRequest request, ServerCallContext context)
        {
            return Task.FromResult(_projectManager.AddUserToProject(request));
        }

        public override Task<RemoveUserRoleFromProjectResponse> RemoveUserRoleFromProject(RemoveUserRoleFromProjectRequest request, ServerCallContext context)
        {
            return Task.FromResult(_projectManager.RemoveUserRoleFromProject(request));
        }

        public override async Task GetUsersByProject(GetUsersByProjectRequest request, IServerStreamWriter<UserResponse> responseStream, ServerCallContext context)
        {
            List<UserResponse> users = _projectManager.GetUsersByProject(request.ProjectId);
            foreach (var user in users)
            {
                await responseStream.WriteAsync(user);
            }
        }
    }
}

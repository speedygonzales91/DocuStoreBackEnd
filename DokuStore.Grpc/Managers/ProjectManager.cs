using DocuStore.DAL.Interfaces;
using DocuStore.DAL.Models;
using DokuStore.Grpc.Interfaces.Managers;
using DokuStore.Grpc.Protos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DokuStore.Grpc.Managers
{
    public class ProjectManager : IProjectManager
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProjectManager(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public ProjectResponse CreateProject(CreateProjectRequest request)
        {
            var project = new Project();
            project.Name = request.Name;
            project.CustomerId = request.CustomerId;
            project.CreatedAt = DateTime.Now;
            project.CreatedBy = 1;

            _unitOfWork.ProjectRepository.Insert(project);
            _unitOfWork.ProjectRepository.Save();

            return new ProjectResponse { Id = project.Id, Name = project.Name };
        }

        public ProjectResponse UpdateProjet(UpdateProjectRequest request)
        {
            var projectToUpdate = _unitOfWork.ProjectRepository.GetByID(request.Id);
            projectToUpdate.Name = request.Name;
            projectToUpdate.CustomerId = request.CustomerId;
            projectToUpdate.ModifiedAt = DateTime.Now;
            projectToUpdate.ModifiedBy = 1;

            _unitOfWork.ProjectRepository.Save();

            return new ProjectResponse { Id = projectToUpdate.Id, Name = projectToUpdate.Name, CustomerId = projectToUpdate.CustomerId };
        }

        public DeleteProjectResponse DeleteProject(int id)
        {
            var projectToDelete = _unitOfWork.ProjectRepository.GetByID(id);
            projectToDelete.IsDeleted = true;
            projectToDelete.DeletedAt = DateTime.Now;
            projectToDelete.DeletedBy = 1;

            _unitOfWork.ProjectRepository.Save();

            return new DeleteProjectResponse();
        }

        public List<ProjectResponse> GetProjectsByUser(int identityType, int identityId)
        {
            List<ProjectResponse> results = new List<ProjectResponse>();
            var projects = _unitOfWork.ProjectRepository.GetActiveProjectsByUser(identityId, identityType);
            foreach (var project in projects)
            {
                results.Add(new ProjectResponse { Id = project.Id, Name = project.Name, CustomerId = project.CustomerId });
            }
            return results;
        }

        public AddUserToProjectResponse AddUserToProject(AddUserToProjectRequest request)
        {
            var projectRoles = new ProjectRoles();
            projectRoles.CreatedAt = DateTime.Now;
            projectRoles.CreatedBy = 1;
            projectRoles.IdentityId = request.IdentityId;
            projectRoles.IdentityType = request.IdentityType;
            projectRoles.ProjectId = request.ProjectId;
            projectRoles.RoleId = request.RoleId;

            _unitOfWork.ProjectRoleRepository.Insert(projectRoles);
            _unitOfWork.ProjectRepository.Save();

            return new AddUserToProjectResponse();

        }

        public RemoveUserRoleFromProjectResponse RemoveUserRoleFromProject(RemoveUserRoleFromProjectRequest request)
        {
            var projectrole = _unitOfWork.ProjectRoleRepository.GetActiveRight(request.IdentityId, request.ProjectId, request.RoleId);

            if (projectrole != null)
            {
                projectrole.IsDeleted = true;
                projectrole.DeletedAt = DateTime.Now;
                projectrole.DeletedBy = 1;
            }
            _unitOfWork.ProjectRoleRepository.Save();

            return new RemoveUserRoleFromProjectResponse();
        }

        public List<UserResponse> GetUsersByProject(int projectId)
        {
            List<UserResponse> result = new List<UserResponse>();
            var roles = _unitOfWork.ProjectRoleRepository.GetRolesOfProject(projectId);

            foreach (var identityType in roles)
            {
                foreach (var identity in identityType.Value)
                {
                    result.Add(new UserResponse { IdentityId = identity, IdentityType = identityType.Key });
                }
            }
            return result;
        }
    }
}

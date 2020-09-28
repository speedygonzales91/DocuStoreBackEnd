using DokuStore.Grpc.Protos;
using Google.Protobuf;
using Grpc.Core;
using Grpc.Net.Client;
using System;
using System.IO;
using System.Threading.Tasks;

namespace DokuStore.GrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {

            var channel = GrpcChannel.ForAddress("https://localhost:5001");


            await TestCustomerService(channel);
            await TestProjectService(channel);
            await TestRoleService(channel);
            await TestDocumentService(channel);

            Console.ReadLine();
        }

        private async static Task TestCustomerService(GrpcChannel channel)
        {
            //var customerClient = new CustomerServiceDefinition.CustomerServiceDefinitionClient(channel);
            //await CreateCustomer(customerClient);

            //await DeleteCustomer(customerClient);

            //await UpdateCustomer(customerClient);

            //await GetCustomers(customerClient);
        }

        private async static Task TestProjectService(GrpcChannel channel)
        {
            //var projectClient = new ProjectServiceDefinition.ProjectServiceDefinitionClient(channel);

            //await CreateProject(projectClient);
            //await UpdateProject(projectClient);
            //await DeleteProject(projectClient);
            //await AddUserToProject(projectClient);
        }
        private async static Task TestRoleService(GrpcChannel channel)
        {
            //var roleClient = new RoleServiceDefinition.RoleServiceDefinitionClient(channel);
            //await CreateRole(roleClient);
        }

        private async static Task TestDocumentService(GrpcChannel channel)
        {
            var documentClient = new DocumentServiceDefinition.DocumentServiceDefinitionClient(channel);
            // await CreateItem(documentClient);
            //await DeleteItem(documentClient);
            await DownloadItem(documentClient);

        }

        private async static Task DownloadItem(DocumentServiceDefinition.DocumentServiceDefinitionClient documentClient)
        {
            var input = new DownloadItemRequest { Id = 9 };

            string tempFileName = $@"C:\Users\molnar.zsolt\DokuStore\temp_{DateTime.UtcNow.ToString("yyyyMMdd_HHmmss")}.tmp";
            string finalFileName = tempFileName;

            using (var reply = documentClient.DownloadItem(input))
            {
                await using (Stream fs = File.OpenWrite(tempFileName))
                {
                    await foreach (DataChunkResponse chunkMsg in reply.ResponseStream.ReadAllAsync().ConfigureAwait(false))
                    {
                        //Int64 totalSize = chunkMsg.FileSize;
                        string tempFinalFilePath = chunkMsg.FileName;

                        if (!string.IsNullOrEmpty(tempFinalFilePath))
                        {
                            finalFileName = chunkMsg.FileName;
                        }

                        fs.Write(chunkMsg.Chunk.ToByteArray());
                    }
                }
            }
            if (finalFileName != tempFileName)
            {
                File.Move(tempFileName, finalFileName);
            }

        }

        private async static Task DeleteItem(DocumentServiceDefinition.DocumentServiceDefinitionClient documentClient)
        {
            var input = new DeleteItemRequest { Id = 9 };
            var reply = await documentClient.DeleteItemAsync(input);
        }

        private async static Task CreateItem(DocumentServiceDefinition.DocumentServiceDefinitionClient documentClient)
        {
            var input = new CreateItemRequest { Name = "ChildOfRoot", TypeId = 1, ProjectId = 1, ParentId = 9 };
            var reply = await documentClient.CreateItemAsync(input);
        }



        private async static Task CreateRole(RoleServiceDefinition.RoleServiceDefinitionClient roleClient)
        {
            var input = new CreateRoleRequest { Name = "Deleter" };
            var reply = await roleClient.CreateRoleAsync(input);
        }

        #region Project Helpers
        private static async Task AddUserToProject(ProjectServiceDefinition.ProjectServiceDefinitionClient projectClient)
        {
            var input = new AddUserToProjectRequest { IdentityId = 2, IdentityType = 1, ProjectId = 1, RoleId = 1 };
            var reply = await projectClient.AddUserToProjectAsync(input);
        }

        private async static Task DeleteProject(ProjectServiceDefinition.ProjectServiceDefinitionClient projectClient)
        {
            var input = new DeleteProjectRequest { Id = 2 };
            var reply = await projectClient.DeleteProjectAsync(input);
        }

        private async static Task UpdateProject(ProjectServiceDefinition.ProjectServiceDefinitionClient projectClient)
        {
            var input = new UpdateProjectRequest { Id = 1, Name = "UPDATED - CIB Fejlesztés sok pénzért", CustomerId = 2 };
            var reply = await projectClient.UpdateProjectAsync(input);
        }

        private async static Task CreateProject(ProjectServiceDefinition.ProjectServiceDefinitionClient projectClient)
        {
            var input = new CreateProjectRequest { Name = "CIB Fejlesztés sok pénzért", CustomerId = 2 };
            var reply = await projectClient.CreateProjectAsync(input);
        }
        #endregion


        #region Customer Helpers
        private static async Task CreateCustomer(CustomerServiceDefinition.CustomerServiceDefinitionClient customerClient)
        {
            var input = new CreateCustomerRequest { Name = "CIB" };
            var reply = await customerClient.CreateCustomerAsync(input);
        }

        private static async Task DeleteCustomer(CustomerServiceDefinition.CustomerServiceDefinitionClient customerClient)
        {
            var input = new DeleteCustomerRequest { Id = 3 };
            var reply = await customerClient.DeleteCustomerAsync(input);
        }

        private static async Task UpdateCustomer(CustomerServiceDefinition.CustomerServiceDefinitionClient customerClient)
        {
            var input = new UpdateCustomerRequest { Id = 2, Name = "CIB2" };
            var reply = await customerClient.UpdateCustomerAsync(input);
        }

        private static async Task GetCustomers(CustomerServiceDefinition.CustomerServiceDefinitionClient customerClient)
        {
            var input = new GetCustomersRequest();

            using (var call = customerClient.GetCustomers(input))
            {
                while (await call.ResponseStream.MoveNext())
                {
                    var currentCustomer = call.ResponseStream.Current;
                    Console.WriteLine($"{currentCustomer.Id} {currentCustomer.Name}");
                }
                Console.WriteLine($"Kilép");
            }
        }
        #endregion
    }
}

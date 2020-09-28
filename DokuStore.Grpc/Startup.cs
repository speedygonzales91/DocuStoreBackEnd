using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocuStore.DAL;
using DocuStore.DAL.Interfaces;
using DocuStore.DAL.Interfaces.Repositories;
using DocuStore.DAL.Models;
using DocuStore.DAL.Repositories;
using DokuStore.Grpc.Interfaces.Managers;
using DokuStore.Grpc.Managers;
using DokuStore.Grpc.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DokuStore.Grpc
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGrpc();

            #region Manager registration
            services.AddScoped<ICustomerManager, CustomerManager>();
            services.AddScoped<IProjectManager,ProjectManager>();
            services.AddScoped<IRoleManager, RoleManager>();
            services.AddScoped<IDocumentManager, DocumentManager>();
            #endregion

            #region Repository Registration
            services.AddDbContext<DocuStoreContext>(opt => opt.UseNpgsql(configuration.GetConnectionString("PgsqlConnection")), ServiceLifetime.Scoped);

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IProjectRoleRepository, ProjectRoleRepository>();
            services.AddScoped<IItemRepository, ItemRepository>();

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<GreeterService>();
                endpoints.MapGrpcService<CustomerService>();
                endpoints.MapGrpcService<ProjectService>();
                endpoints.MapGrpcService<RoleService>();
                endpoints.MapGrpcService<DocumentService>();

                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                });
            });

            SetupDatabase(app);
        }


        /// <summary>
        /// Updates the database to the current migration
        /// </summary>
        /// <param name="app"></param>
        public virtual void SetupDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetService<DocuStoreContext>().Database.Migrate();
            }
        }
    }
}

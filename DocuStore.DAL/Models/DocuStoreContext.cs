using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocuStore.DAL.Models
{
    public class DocuStoreContext : DbContext
    {
        public DocuStoreContext()
        {

        }

        public DocuStoreContext(DbContextOptions<DocuStoreContext> dbContextOptions) : base(dbContextOptions) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<SystemLog> SystemLogs { get; set; }
        public DbSet<Type> Types { get; set; }
        public DbSet<ProjectRoles> ProjectRoles { get; set; }

        /*
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("User Id=admin;Password=admin; Server=localhost;Port=5432; Database=DocuStore; Integrated Security=true; Pooling=true");*/

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("User Id=admin; Password=admin; Server=localhost; Port=5432; Database=DocuStore; Integrated Security=true; Pooling=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProjectRoles>()
                .HasOne(pr => pr.Project)
                .WithMany(p => p.ProjectRoles)
                .HasForeignKey(pr => pr.ProjectId);
            modelBuilder.Entity<ProjectRoles>()
                .HasOne(pr => pr.Role)
                .WithMany(r => r.ProjectRoles)
                .HasForeignKey(pr => pr.RoleId);

        }
    }
}

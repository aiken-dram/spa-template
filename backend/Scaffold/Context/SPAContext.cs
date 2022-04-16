using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Scaffold.Context
{
    public partial class SPAContext : DbContext
    {
        public SPAContext()
        {
        }

        public SPAContext(DbContextOptions<SPAContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AuthAction> AuthActions { get; set; } = null!;
        public virtual DbSet<Group> Groups { get; set; } = null!;
        public virtual DbSet<GroupRole> GroupRoles { get; set; } = null!;
        public virtual DbSet<Module> Modules { get; set; } = null!;
        public virtual DbSet<Request> Requests { get; set; } = null!;
        public virtual DbSet<RequestState> RequestStates { get; set; } = null!;
        public virtual DbSet<RequestType> RequestTypes { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<RoleModule> RoleModules { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserAuth> UserAuths { get; set; } = null!;
        public virtual DbSet<UserGroup> UserGroups { get; set; } = null!;
        public virtual DbSet<UserRole> UserRoles { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseNpgsql("Host=localhost;Database=SPA;Username=spa_app;Password=db2admin");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuthAction>(entity =>
            {

            });

            modelBuilder.Entity<Group>(entity =>
            {

            });

            modelBuilder.Entity<GroupRole>(entity =>
            {

            });

            modelBuilder.Entity<Module>(entity =>
            {

            });

            modelBuilder.Entity<Request>(entity =>
            {

            });

            modelBuilder.Entity<RequestState>(entity =>
            {

            });

            modelBuilder.Entity<RequestType>(entity =>
            {

            });

            modelBuilder.Entity<Role>(entity =>
            {

            });

            modelBuilder.Entity<RoleModule>(entity =>
            {

            });

            modelBuilder.Entity<User>(entity =>
            {

            });

            modelBuilder.Entity<UserAuth>(entity =>
            {

            });

            modelBuilder.Entity<UserGroup>(entity =>
            {

            });

            modelBuilder.Entity<UserRole>(entity =>
            {

            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

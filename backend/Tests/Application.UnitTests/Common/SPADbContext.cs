using System;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Application.UnitTests.Common;

public class SPADbContextFactory
{
    public static SPADbContext CreateInMemory()
    {
        var options = new DbContextOptionsBuilder<SPADbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

        var context = new SPADbContext(options);

        context.Database.EnsureCreated();

        Seed(context);

        return context;
    }

    protected static void Seed(SPADbContext context)
    {
        #region ACCOUNT
        context.Users.AddRange(new[]
        {
                new User { IdUser = 1, Login = "admin", Pass = "21232f297a57a5a743894a0e4a801fc3", PassDate = new DateTime(2050,1,1), IsActive = "T", Name = "Application admin", Description = "Application admin description" },
                new User { IdUser = 2, Login = "user", Pass = "098f6bcd4621d373cade4e832627b4f6", PassDate = new DateTime(2050,1,1), IsActive = "T", Name = "Application user", Description = "Application user description" },
                new User { IdUser = 3, Login = "viewer", Pass = "098f6bcd4621d373cade4e832627b4f6", PassDate = new DateTime(2050,1,1), IsActive = "T", Name = "Read only user", Description = "Read only user description" },
            });

        context.UserAuth.AddRange(new[]
        {
                new UserAuth { IdAuth = 1, IdUser = 1, Stamp = DateTime.Now, IdAction = 1, System = "localhost", Message = "" },
                new UserAuth { IdAuth = 2, IdUser = 1, Stamp = DateTime.Now, IdAction = 2, System = "localhost", Message = "" },
                new UserAuth { IdAuth = 3, IdUser = 2, Stamp = DateTime.Now, IdAction = 2, System = "localhost", Message = "" },
            });

        context.Modules.AddRange(new[]
        {
                new Module { IdModule = 1, Name = "SECADM", Description = "Access management module" },
                new Module { IdModule = 2, Name = "DICTADM", Description = "Dictionary management module" },
                new Module { IdModule = 3, Name = "CFGADM", Description = "Configuration management module" },
                new Module { IdModule = 4, Name = "SUPERVISE", Description = "Supervisor data access module" },
                new Module { IdModule = 5, Name = "READONLY", Description = "Read only restriction module" },
            });

        context.Roles.AddRange(new[]
        {
                new Role { IdRole = 1, Name = "Access admins", Description = "Access management role" },
                new Role { IdRole = 2, Name = "Application admins", Description = "Application management role" },
                new Role { IdRole = 3, Name = "Supervisor", Description = "Supervisor role" },
                new Role { IdRole = 4, Name = "Read only", Description = "Read only restriction role" },
            });

        context.Groups.AddRange(new[]
        {
                new Group { IdGroup = 1, Name = "Admins", Description = "Group of administrators" },
                new Group { IdGroup = 2, Name = "Users", Description = "Group of users" },
                new Group { IdGroup = 3, Name = "Viewers", Description = "Group of read only users" },
            });

        context.UserGroups.AddRange(new[]
        {
                new UserGroup { Id = 1, IdUser = 1, IdGroup = 1 },
                new UserGroup { Id = 2, IdUser = 2, IdGroup = 2 },
                new UserGroup { Id = 3, IdUser = 3, IdGroup = 3 },
            });

        context.RoleModules.AddRange(new[]
        {
                new RoleModule { Id = 1, IdRole = 1, IdModule = 1 },
                new RoleModule { Id = 2, IdRole = 2, IdModule = 2 },
                new RoleModule { Id = 3, IdRole = 2, IdModule = 3 },
                new RoleModule { Id = 4, IdRole = 3, IdModule = 4 },
                new RoleModule { Id = 5, IdRole = 4, IdModule = 5 },
            });

        context.GroupRoles.AddRange(new[]
        {
                new GroupRole { Id = 1, IdGroup = 1, IdRole = 1 },
                new GroupRole { Id = 2, IdGroup = 1, IdRole = 2 },
                new GroupRole { Id = 3, IdGroup = 1, IdRole = 3 },
                new GroupRole { Id = 4, IdGroup = 3, IdRole = 4 },
            });
        #endregion

        #region DICTIONARY

        #endregion
        context.SaveChanges();
    }

    public static void Destroy(SPADbContext context)
    {
        context.Database.EnsureDeleted();

        context.Dispose();
    }
}

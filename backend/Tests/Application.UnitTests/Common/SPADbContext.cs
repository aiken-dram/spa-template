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

        var _context = new SPADbContext(options);

        _context.Database.EnsureCreated();

        Seed(_context);

        return _context;
    }

    protected static void Seed(SPADbContext _context)
    {
        #region ACCOUNT
        _context.Users.AddRange(new[]
        {
            new User { IdUser = 1, Login = "admin", Pass = "21232f297a57a5a743894a0e4a801fc3", PassDate = new DateTime(2050,1,1), IsActive = "T", Name = "Application admin", Description = "Application admin description" },
            new User { IdUser = 2, Login = "secadm", Pass = "098f6bcd4621d373cade4e832627b4f6", PassDate = new DateTime(2050,1,1), IsActive = "T", Name = "Security admin", Description = "Security admin description" },
            new User { IdUser = 3, Login = "supervisor", Pass = "098f6bcd4621d373cade4e832627b4f6", PassDate = new DateTime(2050,1,1), IsActive = "T", Name = "Supervisor user", Description = "Supervisor user description" },
            new User { IdUser = 4, Login = "user1", Pass = "098f6bcd4621d373cade4e832627b4f6", PassDate = new DateTime(2050,1,1), IsActive = "T", Name = "Application user 1", Description = "Application user 1 description" },
            new User { IdUser = 5, Login = "user2", Pass = "098f6bcd4621d373cade4e832627b4f6", PassDate = new DateTime(2050,1,1), IsActive = "T", Name = "Application user 2", Description = "Application user 2 description" },
            new User { IdUser = 6, Login = "viewer", Pass = "098f6bcd4621d373cade4e832627b4f6", PassDate = new DateTime(2050,1,1), IsActive = "T", Name = "Read only user", Description = "Read only user description" },
        });

        _context.Modules.AddRange(new[]
        {
            new Module { IdModule = 1, Name = "SECADM", Description = "Access management module" },
            new Module { IdModule = 2, Name = "DICTADM", Description = "Dictionary management module" },
            new Module { IdModule = 3, Name = "CFGADM", Description = "Configuration management module" },
            new Module { IdModule = 4, Name = "SUPERVISE", Description = "Supervisor data access module" },
            new Module { IdModule = 5, Name = "READONLY", Description = "Read only restriction module" },
        });

        _context.Roles.AddRange(new[]
        {
            new Role { IdRole = 1, Name = "Access admins", Description = "Access management role" },
            new Role { IdRole = 2, Name = "Application admins", Description = "Application management role" },
            new Role { IdRole = 3, Name = "Supervisor", Description = "Supervisor role" },
            new Role { IdRole = 4, Name = "Read only", Description = "Read only restriction role" },
        });

        _context.Groups.AddRange(new[]
        {
            new Group { IdGroup = 1, Name = "Admins", Description = "Group of administrators" },
            new Group { IdGroup = 2, Name = "Supervisors", Description = "Group of supervisors" },
            new Group { IdGroup = 3, Name = "Users", Description = "Group of users" },
            new Group { IdGroup = 4, Name = "Viewers", Description = "Group of read only users" },
        });

        _context.UserGroups.AddRange(new[]
        {
            new UserGroup { Id = 1, IdUser = 1, IdGroup = 1 },
            new UserGroup { Id = 2, IdUser = 3, IdGroup = 2 },
            new UserGroup { Id = 4, IdUser = 4, IdGroup = 3 },
            new UserGroup { Id = 5, IdUser = 5, IdGroup = 3 },
            new UserGroup { Id = 6, IdUser = 6, IdGroup = 4 },
        });

        _context.RoleModules.AddRange(new[]
        {
            new RoleModule { Id = 1, IdRole = 1, IdModule = 1 },
            new RoleModule { Id = 2, IdRole = 2, IdModule = 2 },
            new RoleModule { Id = 3, IdRole = 2, IdModule = 3 },
            new RoleModule { Id = 4, IdRole = 3, IdModule = 4 },
            new RoleModule { Id = 5, IdRole = 4, IdModule = 5 },
        });

        _context.GroupRoles.AddRange(new[]
        {
            new GroupRole { Id = 1, IdGroup = 1, IdRole = 1 },
            new GroupRole { Id = 2, IdGroup = 1, IdRole = 2 },
            new GroupRole { Id = 3, IdGroup = 1, IdRole = 3 },
            new GroupRole { Id = 4, IdGroup = 2, IdRole = 3 },
            new GroupRole { Id = 5, IdGroup = 4, IdRole = 4 },
        });

        _context.UserRoles.AddRange(new[]
        {
            new UserRole { Id = 1, IdUser = 2, IdRole = 1 },
        });
        #endregion

        #region DICTIONARY
        _context.AuthActions.AddRange(new[]
        {
            new AuthAction { IdAction = 1, Action = "LOGIN", Description = "Login description" },
            new AuthAction { IdAction = 2, Action = "WRONGPASS", Description = "Wrong pass description" },
            new AuthAction { IdAction = 3, Action = "EXPIRED", Description = "Expired description" },
            new AuthAction { IdAction = 4, Action = "LOCK", Description = "Lock description" },
        });

        _context.RequestStates.AddRange(new[]
        {
            new RequestState { IdState = 1, State = "QUEUE", Description = "Queue description" },
            new RequestState { IdState = 2, State = "PROCESSING", Description = "Processing description" },
            new RequestState { IdState = 3, State = "READY", Description = "Ready description" },
            new RequestState { IdState = 4, State = "DELIVERED", Description = "Delivered description" },
            new RequestState { IdState = 5, State = "ERROR", Description = "Error description" },
        });

        _context.RequestTypes.AddRange(new[]
        {
            new RequestType { IdType = 1, Type = "USER_EXPORT", Description = "User export description" },
        });
        #endregion

        _context.SaveChanges();
    }

    public static void Destroy(SPADbContext _context)
    {
        _context.Database.EnsureDeleted();

        _context.Dispose();
    }
}

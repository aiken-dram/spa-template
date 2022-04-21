using System;
using System.Linq;
using System.Threading.Tasks;
using Domain.Common;
using Domain.Entities;
using Infrastructure.Common.Models;
using Shared.Application.Exceptions;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace Infrastructure.UnitTests.Identity;

public class LoginTests : AuthServiceTestBase
{
    public LoginTests(ITestOutputHelper output) : base(output)
    {
    }

    [Fact]
    public async Task Handle_GivenInvalidLogin_ThrowsBadRequestException()
    {
        // Given
        var request = new AuthRequest
        {
            Login = "wrong",
            Password = "test"
        };
        string? system = "localhost";

        // Then
        await Should.ThrowAsync<BadRequestException>(() => _auth.LoginAsync(request, system));
    }

    [Fact]
    public async Task Handle_GivenLockedUserLogin_ThrowsBadRequestException()
    {
        // Given
        var request = new AuthRequest
        {
            Login = "user2",
            Password = "test"
        };
        string? system = "localhost";

        // Then
        await Should.ThrowAsync<BadRequestException>(() => _auth.LoginAsync(request, system));
    }

    [Fact]
    public async Task Handle_GivenExpiredPasswordUserLogin_ThrowsBadRequestException()
    {
        // Given
        var now = DateTime.Now;
        var request = new AuthRequest
        {
            Login = "viewer",
            Password = "test"
        };
        string? system = "Handle_GivenExpiredPasswordUserLogin_ThrowsBadRequestException";

        // Then
        await Should.ThrowAsync<BadRequestException>(() => _auth.LoginAsync(request, system));
        // UserAuth entry in _context
        var auth = _context.UserAuth
            .First(p => p.IdUser == 6 && p.System == system);
        auth.ShouldNotBeNull();
        auth.IdAction.ShouldBe(3); //EXPIRED
        auth.Stamp.ShouldBeInRange(now, DateTime.Now);
    }

    [Fact]
    public async Task Handle_GivenWrongPassExceedingLocked_ThrowsBadRequestExceptionAndLocksUser()
    {
        // Context
        _context.UserAuth.AddRange(new[]
        {
            new UserAuth { IdAuth = 1, IdUser = 3, Stamp = DateTime.Now.AddMinutes(-10), IdAction = 2, System = "localhost", Message = "" },
            new UserAuth { IdAuth = 2, IdUser = 3, Stamp = DateTime.Now.AddMinutes(-9), IdAction = 2, System = "localhost", Message = "" },
            new UserAuth { IdAuth = 3, IdUser = 3, Stamp = DateTime.Now.AddMinutes(-8), IdAction = 2, System = "localhost", Message = "" },
            new UserAuth { IdAuth = 4, IdUser = 3, Stamp = DateTime.Now.AddMinutes(-7), IdAction = 2, System = "localhost", Message = "" },
            new UserAuth { IdAuth = 5, IdUser = 3, Stamp = DateTime.Now.AddMinutes(-6), IdAction = 2, System = "localhost", Message = "" },
            new UserAuth { IdAuth = 6, IdUser = 3, Stamp = DateTime.Now.AddMinutes(-5), IdAction = 2, System = "localhost", Message = "" },
            new UserAuth { IdAuth = 7, IdUser = 3, Stamp = DateTime.Now.AddMinutes(-4), IdAction = 2, System = "localhost", Message = "" },
            new UserAuth { IdAuth = 8, IdUser = 3, Stamp = DateTime.Now.AddMinutes(-3), IdAction = 2, System = "localhost", Message = "" },
            new UserAuth { IdAuth = 9, IdUser = 3, Stamp = DateTime.Now.AddMinutes(-2), IdAction = 2, System = "localhost", Message = "" },
            new UserAuth { IdAuth = 10, IdUser = 3, Stamp = DateTime.Now.AddMinutes(-1), IdAction = 2, System = "localhost", Message = "" },
        });
        _context.SaveChanges();
        // Given
        var now = DateTime.Now;
        var request = new AuthRequest
        {
            Login = "supervisor",
            Password = "wrong"
        };
        string? system = "Handle_GivenWrongPassExceedingLocked_ThrowsBadRequestExceptionAndLocksUser";

        // Then
        await Should.ThrowAsync<BadRequestException>(() => _auth.LoginAsync(request, system));
        // check User in _context
        var user = _context.Users.Find((long)3);
        user.ShouldNotBeNull();
        user.IsActive.ShouldBe(CharBoolean.False);
        // check UserAuth in _context
        var auths = _context.UserAuth
            .Where(p => p.IdUser == 3 && p.System == system)
            .ToList();
        auths.ShouldNotBeNull();
        auths.ShouldNotBeEmpty();
        auths.ShouldContain(p =>
            p.IdAction == 2 && //WRONGPASS
            p.Stamp >= now && p.Stamp <= DateTime.Now);
        auths.ShouldContain(p =>
            p.IdAction == 4 && //LOCK
            p.Stamp >= now && p.Stamp <= DateTime.Now);
    }

    [Fact]
    public async Task Handle_GivenValidLoginDuringTimeout_ThrowsBadRequestException()
    {
        // Context
        _context.UserAuth.AddRange(new[]
        {
            new UserAuth { IdAuth = 11, IdUser = 4, Stamp = DateTime.Now.AddMinutes(-2), IdAction = 2, System = "localhost", Message = "" },
            new UserAuth { IdAuth = 12, IdUser = 4, Stamp = DateTime.Now.AddMinutes(-1), IdAction = 2, System = "localhost", Message = "" },
            new UserAuth { IdAuth = 13, IdUser = 4, Stamp = DateTime.Now, IdAction = 2, System = "localhost", Message = "" },
        });
        _context.SaveChanges();
        // Given
        var now = DateTime.Now;
        var request = new AuthRequest
        {
            Login = "user1",
            Password = "test"
        };
        string? system = "localhost";

        // Then
        await Should.ThrowAsync<BadRequestException>(() => _auth.LoginAsync(request, system));
    }


    [Fact]
    public async Task Handle_GivenWrongPassword_ThrowsBadRequestException()
    {
        // Given
        var now = DateTime.Now;
        var request = new AuthRequest
        {
            Login = "admin",
            Password = "wrong"
        };
        string? system = "Handle_GivenWrongPassword_ThrowsBadRequestException";

        // Then
        await Should.ThrowAsync<BadRequestException>(() => _auth.LoginAsync(request, system));
        // UserAuth entry in _context
        var auth = _context.UserAuth
            .First(p => p.IdUser == 1 && p.System == system);
        auth.ShouldNotBeNull();
        auth.IdAction.ShouldBe(2); //WRONGPASS
        auth.Stamp.ShouldBeInRange(now, DateTime.Now);
    }

    [Fact]
    public async Task Handle_GivenValidLogin_LoginsUser()
    {
        // Given
        var now = DateTime.Now;
        var request = new AuthRequest
        {
            Login = "admin",
            Password = "admin"
        };
        string? system = "Handle_GivenValidLogin_LoginsUser";

        // When
        var res = await _auth.LoginAsync(request, system);

        // Then
        //1. response
        res.ShouldNotBeNull();
        res.User.UserID.ShouldBe(1);
        res.User.UserName.ShouldBe("Application admin");

        res.User.UserGroups.ShouldNotBeEmpty();
        res.User.UserGroups.Length.ShouldBe(1);
        res.User.UserGroups.ShouldContain("Group of administrators");

        res.User.UserModules.ShouldNotBeEmpty();
        res.User.UserModules.Length.ShouldBe(4);
        res.User.UserModules.ShouldContain("SECADM");
        res.User.UserModules.ShouldContain("CFGADM");
        res.User.UserModules.ShouldContain("DICTADM");
        res.User.UserModules.ShouldContain("SUPERVISE");

        //2. UserAuth entry in _context
        var auth = _context.UserAuth
            .First(p => p.IdUser == 1 && p.System == system);
        auth.ShouldNotBeNull();
        auth.IdAction.ShouldBe(1); //LOGIN
        auth.Stamp.ShouldBeInRange(now, DateTime.Now);
    }
}

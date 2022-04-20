using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Account.User.Commands.UpsertUser;
using Application.UnitTests.Common;
using Shared.Application.Exceptions;
using Shouldly;
using Xunit;
using Xunit.Abstractions;
using static Application.Account.User.Commands.UpsertUser.UpsertUserCommand;

namespace Application.UnitTests.Account.User.Commands.UpsertUser;

public class UpsertUserCommandTests : TestBase
{
    private readonly UpsertUserCommandHandler _sut;
    private XunitLogger<UpsertUserCommand> _logger;

    public UpsertUserCommandTests(ITestOutputHelper output)
        : base()
    {
        _logger = new XunitLogger<UpsertUserCommand>(output);
        _sut = new UpsertUserCommandHandler(_context, _logger);
    }

    [Fact]
    public async Task Handle_GivenInvalidId_ThrowsNotFoundException()
    {
        //Given
        var command = new UpsertUserCommand
        {
            IdUser = -1,
            IsActive = "T",
            Login = "admin",
            Password = "admin",
            Name = "User name",
            Description = "User description",
            PassDate = new DateTime(2021, 5, 28),
            Groups = new long[] { 1 },
            Roles = new long[] { }
        };

        //Then
        await Assert.ThrowsAsync<NotFoundException>(() => _sut.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_GivenValidExistingUser_UpdatesUser()
    {
        //Given
        var command = new UpsertUserCommand
        {
            IdUser = 1,
            IsActive = "T",
            Login = "admin",
            Password = "admin",
            Name = "User name",
            Description = "User description",
            PassDate = new DateTime(2021, 5, 28),
            Groups = new long[] { 1 },
            Roles = new long[] { }
        };

        //When
        var result = await _sut.Handle(command, CancellationToken.None);

        //Then
        result.ShouldBeOfType<long>();
        result.ShouldBe((long)1);
        var user = await _context.Users.FindAsync(result);
        user.ShouldNotBeNull();
        user.IsActive.ShouldBe("T");
        user.Login.ShouldBe("admin");
        user.Pass.ShouldBe("21232f297a57a5a743894a0e4a801fc3");
        user.Name.ShouldBe("User name");
        user.Description.ShouldBe("User description");
        user.PassDate.ShouldBe(new DateTime(2021, 5, 28));
        user.UserGroups.Count.ShouldBe(1);
        user.UserGroups.ShouldContain(p => p.IdGroup == 1);
        user.UserRoles.Count.ShouldBe(0);
    }

    [Fact]
    public async Task Handle_GivenValidNewUser_CreatesUser()
    {
        //Given
        var command = new UpsertUserCommand
        {
            IdUser = (long?)null,
            IsActive = "T",
            Login = "new_user",
            Password = "admin",
            Name = "User name",
            Description = "User description",
            PassDate = new DateTime(2021, 5, 28),
            Groups = new long[] { 1 },
            Roles = new long[] { }
        };

        //When
        var result = await _sut.Handle(command, CancellationToken.None);

        //Then
        result.ShouldBeOfType<long>();
        var user = await _context.Users.FindAsync(result);
        user.ShouldNotBeNull();
        user.IsActive.ShouldBe("T");
        user.Login.ShouldBe("new_user");
        user.Pass.ShouldBe("21232f297a57a5a743894a0e4a801fc3");
        user.Name.ShouldBe("User name");
        user.Description.ShouldBe("User description");
        user.PassDate.ShouldBe(new DateTime(2021, 5, 28));
        user.UserGroups.Count.ShouldBe(1);
        user.UserGroups.ShouldContain(p => p.IdGroup == 1);
        user.UserRoles.Count.ShouldBe(0);
    }

    /**
    could use more extended testing here, like it correctly handles exceptions and changes 
    for lists UserGroups and UserRoles correctly
    */

    [Fact]
    public async Task Handle_GivenDuplicateLogin_ThrowsException()
    {
        //Given
        var command = new UpsertUserCommand
        {
            IdUser = (long?)null,
            IsActive = "T",
            Login = "admin",
            Password = "admin",
            Name = "User name",
            Description = "User description",
            PassDate = new DateTime(2021, 5, 28),
            Groups = new long[] { 1 },
            Roles = new long[] { }
        };

        //Then
        await Assert.ThrowsAsync<ValidationException>(() => _sut.Handle(command, CancellationToken.None));
    }
}

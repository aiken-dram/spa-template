using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Account.User.Commands.UpsertUser;
using Application.UnitTests.Common;
using Shared.Application.Exceptions;
using FluentAssertions;
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
        await FluentActions.Invoking(() =>
            _sut.Handle(command, CancellationToken.None)).Should().ThrowAsync<NotFoundException>();
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
            Roles = new long[] { },
            Districts = new long[] { 3 },
        };

        //When
        var result = await _sut.Handle(command, CancellationToken.None);

        //Then
        result.Should().BeOfType(typeof(long));
        result.Should().Be((long)1);
        var user = _context.Users.Find(result);
        user.Should().NotBeNull();
        user!.IsActive.Should().Be("T");
        user.Login.Should().Be("admin");
        user.Pass.Should().Be("21232f297a57a5a743894a0e4a801fc3");
        user.Name.Should().Be("User name");
        user.Description.Should().Be("User description");
        user.PassDate.Should().Be(new DateTime(2021, 5, 28));
        user.UserGroups.Count.Should().Be(1);
        user.UserGroups.Should().Contain(p => p.IdGroup == 1);
        user.UserRoles.Count.Should().Be(0);
        user.UserDistricts.Count.Should().Be(1);
        user.UserDistricts.Should().Contain(p => p.IdDistrict == 3);
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
            Roles = new long[] { },
            Districts = new long[] { 3 },
        };

        //When
        var result = await _sut.Handle(command, CancellationToken.None);

        //Then
        result.Should().BeOfType(typeof(long));
        var user = _context.Users.Find(result);
        user.Should().NotBeNull();
        user!.IsActive.Should().Be("T");
        user.Login.Should().Be("new_user");
        user.Pass.Should().Be("21232f297a57a5a743894a0e4a801fc3");
        user.Name.Should().Be("User name");
        user.Description.Should().Be("User description");
        user.PassDate.Should().Be(new DateTime(2021, 5, 28));
        user.UserGroups.Count.Should().Be(1);
        user.UserGroups.Should().Contain(p => p.IdGroup == 1);
        user.UserRoles.Count.Should().Be(0);
        user.UserDistricts.Count.Should().Be(1);
        user.UserDistricts.Should().Contain(p => p.IdDistrict == 3);
    }

    /**
    could use more extensive testing here, like it correctly handles exceptions and changes 
    for lists (UserGroups, UserRoles) correctly
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
        await FluentActions.Invoking(() =>
            _sut.Handle(command, CancellationToken.None)).Should().ThrowAsync<ValidationException>();
    }
}

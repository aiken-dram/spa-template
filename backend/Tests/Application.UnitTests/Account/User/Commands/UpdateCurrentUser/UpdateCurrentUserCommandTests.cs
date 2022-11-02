using System.Threading.Tasks;
using Xunit;
using Application.Account.User.Commands.UpdateCurrentUser;
using System.Threading;
using Shared.Application.Exceptions;
using FluentAssertions;
using Moq;
using Application.Common.Interfaces;
using Application.UnitTests.Common;

namespace Application.UnitTests.Account.User.Commands.UpdateCurrentUser;

public class UpdateCurrentUserCommandTests : TestBase
{
    private readonly UpdateCurrentUserCommandHandler _sut;
    private readonly Mock<IUserService> _user;

    public UpdateCurrentUserCommandTests()
        : base()
    {
        _user = UserServiceFactory.Create();

        _sut = new UpdateCurrentUserCommandHandler(_context, _user.Object);
    }

    [Fact]
    public async Task Handle_GivenNotMatchedId_ThrowsAccessDeniedException()
    {
        //Given
        var command = new UpdateCurrentUserCommand
        {
            IdUser = 2,
            Name = "New name",
            Description = "New description"
        };

        //Then
        await FluentActions.Invoking(() =>
            _sut.Handle(command, CancellationToken.None)).Should().ThrowAsync<AccessDeniedException>();
    }

    [Fact]
    public async Task Handle_GivenValidCommand_UpdatesCurrentUser()
    {
        //Given
        var command = new UpdateCurrentUserCommand
        {
            IdUser = 1,
            Name = "New name",
            Description = "New description"
        };

        //When
        await _sut.Handle(command, CancellationToken.None);

        //Then
        var user = _context.Users.Find((long)1);
        user.Should().NotBeNull();
        user!.Name.Should().Be("New name");
        user.Description.Should().Be("New description");
    }
}

using System.Threading;
using System.Threading.Tasks;
using Application.Account.User.Commands.DeleteUser;
using Application.UnitTests.Common;
using Shared.Application.Exceptions;
using Shouldly;
using Xunit;
using Xunit.Abstractions;
using static Application.Account.User.Commands.DeleteUser.DeleteUserCommand;

namespace Application.UnitTests.Account.User.Commands.DeleteUser;

public class DeleteUserCommandTests : TestBase
{
    private readonly DeleteUserCommandHandler _sut;

    public DeleteUserCommandTests(ITestOutputHelper output)
        : base()
    {
        _sut = new DeleteUserCommandHandler(_context);
    }

    [Fact]
    public async Task Handle_GivenInvalidId_ThrowsNotFoundException()
    {
        //Given
        var command = new DeleteUserCommand { Id = -1 };

        //Then
        await Should.ThrowAsync<NotFoundException>(() => _sut.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_GivenValidId_DeletesUser()
    {
        // Given
        var command = new DeleteUserCommand { Id = 2 };

        // When
        await _sut.Handle(command, CancellationToken.None);

        // Then
        var user = _context.Users.Find((long)2);
        user.ShouldBeNull();
    }
}

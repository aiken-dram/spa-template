using System.Threading.Tasks;
using Shared.Application.Exceptions;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace Infrastructure.UnitTests.Identity;

public class ValidateTests : AuthServiceTestBase
{
    public ValidateTests(ITestOutputHelper output) : base(output)
    {
    }

    [Fact]
    public async Task Handle_GivenEmptyUserId_ThrowsBadRequestException()
    {
        // Given
        string userId = string.Empty;

        // Then
        await FluentActions.Invoking(() =>
            _auth.Validate(userId)).Should().ThrowAsync<BadRequestException>();
    }

    [Fact]
    public async Task Handle_GivenInvalidUserId_ThrowsBadRequestException()
    {
        // Given
        string userId = "-1";

        // Then
        await FluentActions.Invoking(() =>
            _auth.Validate(userId)).Should().ThrowAsync<BadRequestException>();
    }

    [Fact]
    public async Task Handle_GivenLockedUserId_ThrowsBadRequestException()
    {
        // Given
        string userId = "5";

        // Then
        await FluentActions.Invoking(() =>
            _auth.Validate(userId)).Should().ThrowAsync<BadRequestException>();
    }

    [Fact]
    public async Task Handle_GivenValidUser_DoesntThrowException()
    {
        // Given
        string userId = "1";

        // Then
        await FluentActions.Invoking(() =>
            _auth.Validate(userId)).Should().NotThrowAsync();
    }
}

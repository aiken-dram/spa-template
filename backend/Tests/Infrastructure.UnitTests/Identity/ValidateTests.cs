using System.Threading.Tasks;
using Shared.Application.Exceptions;
using Shouldly;
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
        await Should.ThrowAsync<BadRequestException>(() => _auth.Validate(userId));
    }

    [Fact]
    public async Task Handle_GivenInvalidUserId_ThrowsBadRequestException()
    {
        // Given
        string userId = "-1";

        // Then
        await Should.ThrowAsync<BadRequestException>(() => _auth.Validate(userId));
    }

    [Fact]
    public async Task Handle_GivenLockedUserId_ThrowsBadRequestException()
    {
        // Given
        string userId = "5";

        // Then
        await Should.ThrowAsync<BadRequestException>(() => _auth.Validate(userId));
    }

    [Fact]
    public async Task Handle_GivenValidUser_DoesntThrowException()
    {
        // Given
        string userId = "1";

        // Then
        await Should.NotThrowAsync(() => _auth.Validate(userId));
    }
}

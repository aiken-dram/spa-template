using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace Infrastructure.UnitTests.Identity;

public class CurrentUserTests : AuthServiceTestBase
{
    public CurrentUserTests(ITestOutputHelper output) : base(output)
    {
    }

    [Fact]
    public async Task Handle_GivenValidUserId_ReturnsCurrentUser()
    {
        // Given
        var userId = "1";

        // When
        var res = await _auth.CurrentUserAsync(userId);

        // Then
        res.User.Should().NotBeNull();
        res.User.UserID.Should().Be(1);
        res.User.UserName.Should().Be("Application admin");


        res.User.UserGroups.Should().NotBeEmpty();
        res.User.UserGroups.Length.Should().Be(1);
        res.User.UserGroups.Should().Contain("Group of administrators");

        res.User.UserDistricts.Should().BeEmpty();

        res.User.UserModules.Should().NotBeEmpty();
        res.User.UserModules.Length.Should().Be(4);
        res.User.UserModules.Should().Contain("SECADM");
        res.User.UserModules.Should().Contain("CFGADM");
        res.User.UserModules.Should().Contain("DICTADM");
        res.User.UserModules.Should().Contain("SUPERVISE");
    }
}

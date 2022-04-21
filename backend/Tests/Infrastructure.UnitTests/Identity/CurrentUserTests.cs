using System.Threading.Tasks;
using Shouldly;
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
        res.User.ShouldNotBeNull();
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
    }
}

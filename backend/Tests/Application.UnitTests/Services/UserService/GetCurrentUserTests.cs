using System.Threading;
using System.Threading.Tasks;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace Application.UnitTests.Services;

public class GetCurrentUserTests : ServiceTestBase
{
    public GetCurrentUserTests(ITestOutputHelper output) : base(output)
    {
    }

    [Fact]
    public async Task GetCurrentUserTest()
    {
        //Given
        User.Setup(p => p.UserId).Returns("1");

        //When
        var res = await _user.GetCurrentUserAsync(CancellationToken.None);

        //Then
        res.IdUser.ShouldBe(1);
        res.Modules.ShouldNotBeEmpty();
        res.Modules.ShouldContain("SECADM");
        res.Modules.ShouldContain("DICTADM");
        res.Modules.ShouldContain("CFGADM");
        res.Modules.ShouldContain("SUPERVISE");
    }
}

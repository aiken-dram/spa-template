using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
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
        res.IdUser.Should().Be(1);
        res.Modules.Should().NotBeEmpty();
        res.Modules.Should().Contain("SECADM");
        res.Modules.Should().Contain("DICTADM");
        res.Modules.Should().Contain("CFGADM");
        res.Modules.Should().Contain("SUPERVISE");
    }
}

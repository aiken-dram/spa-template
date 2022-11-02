using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace Application.UnitTests.Services;

public class CurrentUserIdTests : ServiceTestBase
{
    public CurrentUserIdTests(ITestOutputHelper output) : base(output)
    {
    }

    [Fact]
    public void CurrentUserIdTest()
    {
        //Given
        User.Setup(m => m.UserId).Returns("1");

        //When
        var res = _user.CurrentUserId;

        //Then
        res.Should().Be(1);
    }
}

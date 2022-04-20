using Xunit.Abstractions;

namespace Infrastructure.UnitTests.Identity;

public class CurrentUserTests : AuthServiceTestBase
{
    public CurrentUserTests(ITestOutputHelper output) : base(output)
    {
    }
}

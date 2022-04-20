using Xunit.Abstractions;

namespace Infrastructure.UnitTests.Identity;

public class LoginTests : AuthServiceTestBase
{
    public LoginTests(ITestOutputHelper output) : base(output)
    {
    }
}

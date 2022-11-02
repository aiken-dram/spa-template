using System;

namespace Domain.UnitTests.Entities.Account;

public class UserTests
{
    [Fact]
    public void UserUpdatePasswordTest()
    {
        // Given
        var _date = DateTime.Now.Date;
        var entity = new User
        {
            IdUser = 1,
            PassDate = _date
        };

        // When
        entity.UpdatePassword("hash", 30);

        // Then
        entity.Pass.Should().Be("hash");
        entity.PassDate.Should().Be(_date.AddDays(30));
    }
}
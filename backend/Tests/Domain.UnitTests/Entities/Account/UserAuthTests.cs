using System;
using Domain.Entities;
using Shouldly;
using Xunit;

namespace Tests.Domain.UnitTests.Entities.Account;

public class UserAuthTests
{
    [Fact]
    public void UserAuthConstructorTest()
    {
        // Given
        var _stamp = DateTime.Now;

        // When
        var entity = new UserAuth(1, 2, null);

        // Then
        entity.IdUser.ShouldBe(1);
        entity.IdAction.ShouldBe(2);
        entity.System.ShouldBe(string.Empty);
        entity.Stamp.ShouldBeInRange(_stamp, DateTime.Now);
    }
}
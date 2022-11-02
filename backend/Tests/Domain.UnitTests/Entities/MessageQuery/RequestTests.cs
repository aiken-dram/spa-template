using System;

namespace Domain.UnitTests.Entities.MessageQuery;

public class RequestTests
{
    [Fact]
    public void RequestStartTest()
    {
        // Given
        var entity = new Request();

        // When
        var guid = entity.Start();

        // Then
        entity.Guid.Should().NotBeEmpty();
        entity.Guid.Should().Be(guid);
        entity.IdState.Should().Be(eRequestState.Processing);
    }

    [Fact]
    public void RequestErrorTest()
    {
        // Given
        var entity = new Request();
        var err = new Exception("test exception");

        // When
        entity.Error(err);

        // Then
        entity.Message.Should().Be("test exception");
        entity.IdState.Should().Be(eRequestState.Error);
    }

    [Fact]
    public void RequestSuccessTest()
    {
        // Given
        var entity = new Request();

        // When
        entity.Success();

        // Then
        entity.IdState.Should().Be(eRequestState.Ready);
        entity.Processed.Should().NotBeNull();
        entity.Processed!.Value.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(1));
    }
}

using System.Threading.Tasks;
using Xunit;
using System.Threading;
using FluentAssertions;
using Moq;
using Xunit.Abstractions;
using Application.UnitTests.Common;
using Application.Account.User.Commands.ProcessFile;
using System.Collections.Generic;
using System;
using MediatR;
using Application.Notification;

namespace Application.UnitTests.Account.User.Commands.ProcessFile;

public class ProcessFileCommandTests : TestBase
{
    private Mock<IMediator> _mediator;
    private readonly ProcessFileCommandHandler _sut;
    private XunitLogger<ProcessFileCommand> _logger;

    public ProcessFileCommandTests(ITestOutputHelper output)
        : base()
    {
        _mediator = new Mock<IMediator>();
        _logger = new XunitLogger<ProcessFileCommand>(output);
        _sut = new ProcessFileCommandHandler(_mediator.Object, _context, _logger);
    }

    [Fact]
    public async Task ProcessFileTests()
    {
        //Given
        var txt = new List<string?>();
        txt.Add("admin test");
        txt.Add("wrong wrong");
        txt.Add("wrong_string_format");
        var command = new ProcessFileCommand { IdConnection = "connection", FileContent = txt };

        //When
        var result = await _sut.Handle(command, CancellationToken.None);

        //Then
        //1. check result
        result.Should().BeOfType<ProcessFileVm>();
        result.Items.Should().NotBeEmpty();
        result.Items!.Count.Should().Be(3);

        result.Items[0].State.Should().Be("success");
        result.Items[0].Body.Should().Contain("admin");

        result.Items[1].State.Should().Be("error");
        result.Items[1].Body.Should().Contain("0");
        result.Items[1].Body.Should().Contain("wrong");

        result.Items[2].State.Should().Be("error");
        result.Items[2].Body.Should().Contain("wrong_string_format");

        //2. check mediator
        _mediator.Verify(x => x.Publish(It.Is<SignalRNotification>(p =>
            p.IdConnection == "connection"),
            It.IsAny<CancellationToken>()), Times.Exactly(3));

        //3. check context
        var user = _context.Users.Find((long)1);
        user.Should().NotBeNull();
        user!.Pass.Should().Be("098f6bcd4621d373cade4e832627b4f6");
        user.PassDate.Should().NotBeNull();
        user.PassDate.HasValue.Should().Be(true);
        user.PassDate!.Value.Should().BeCloseTo(DateTime.Now.AddDays(90), TimeSpan.FromMinutes(10));
    }
}

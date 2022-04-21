using System.Threading.Tasks;
using Xunit;
using System.Threading;
using Shouldly;
using Moq;
using static Application.Account.User.Commands.ProcessFile.ProcessFileCommand;
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
        result.ShouldBeOfType<ProcessFileVm>();
        result.Items.ShouldNotBeEmpty();
        result.Items.Count.ShouldBe(3);

        result.Items[0].State.ShouldBe("success");
        result.Items[0].Body.ShouldContain("admin");

        result.Items[1].State.ShouldBe("error");
        result.Items[1].Body.ShouldContain("0");
        result.Items[1].Body.ShouldContain("wrong");

        result.Items[2].State.ShouldBe("error");
        result.Items[2].Body.ShouldContain("wrong_string_format");

        //2. check mediator
        _mediator.Verify(x => x.Publish(It.Is<SignalRNotification>(p =>
            p.IdConnection == "connection"),
            It.IsAny<CancellationToken>()), Times.Exactly(3));

        //3. check context
        var user = _context.Users.Find((long)1);
        user.ShouldNotBeNull();
        user.Pass.ShouldBe("098f6bcd4621d373cade4e832627b4f6");
        user.PassDate.ShouldNotBeNull();
        user.PassDate.HasValue.ShouldBe(true);
        user.PassDate.Value.ShouldBe(DateTime.Now.AddDays(90), TimeSpan.FromMinutes(10));
    }
}

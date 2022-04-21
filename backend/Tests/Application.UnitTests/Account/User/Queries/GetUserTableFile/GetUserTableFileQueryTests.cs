using Moq;
using Xunit;
using Shouldly;
using Application.UnitTests.Common;
using Application.Common.Interfaces;
using Application.Account.User.Queries.GetUserTableFile;
using static Application.Account.User.Queries.GetUserTableFile.GetUserTableFileQuery;
using Xunit.Abstractions;
using MediatR;
using Application.Account.User.Queries.GetUserTable;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace Application.UnitTests.Account.User.Queries.GetUserTableFile;

[Collection("AccountQueryCollection")]
public class GetUserTableFileQueryTests
{
    private XunitLogger<GetUserTableFileQuery> _logger;
    private Mock<IMediator> _mediator;
    private Mock<IFileBuilder> _file;

    private GetUserTableFileQueryHandler _sut;

    public GetUserTableFileQueryTests(AccountQueryTestFixture fixture, ITestOutputHelper output)
    {
        _mediator = new Mock<IMediator>();
        _logger = new XunitLogger<GetUserTableFileQuery>(output);
        _file = new Mock<IFileBuilder>();
        _mediator
            .Setup(m => m.Send(It.IsAny<GetUserTableQuery>(), It.IsAny<CancellationToken>()))
            .Returns(async () => await Task.FromResult(new UserTableVm { Items = new List<UserTableDto>() }));
        _sut = new GetUserTableFileQueryHandler(_mediator.Object, _file.Object, _logger);
    }

    [Fact]
    public async Task GetUserTableFileTests()
    {
        //Given
        var command = new GetUserTableFileQuery
        {
            Page = 1,
            ItemsPerPage = 100,
            SortBy = "sortField",
            SortDesc = false,
            Filters = new[] { "filterField|filterOperation|filterValue" },
            Search = new[] { "searchField|searchOperation|searchValue" },
        };

        //When
        var result = await _sut.Handle(command, CancellationToken.None);

        //Then
        result.ShouldBeOfType<UserTableFileVm>();
        result.ContentType.ShouldBe("text/csv");
        result.FileName.ShouldContain($"{DateTime.Now:yyyy-MM-dd}.csv");
        _mediator.Verify(m =>
            m.Send(It.Is<GetUserTableQuery>(cc =>
                    cc.Page == 1 &&
                    cc.ItemsPerPage == 100 &&
                    cc.SortBy == "sortField" &&
                    cc.SortDesc == false &&
                    cc.Filters![0] == "filterField|filterOperation|filterValue" &&
                    cc.Search![0] == "searchField|searchOperation|searchValue"),
                It.IsAny<CancellationToken>()),
            Times.Once);
        _file.Verify(m => m.BuildUserTableFile(It.Is<List<UserTableDto>>(c => true)), Times.Once);
    }
}

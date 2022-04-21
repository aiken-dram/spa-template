using System.Threading;
using System.Threading.Tasks;
using Application.Account.User.Queries.GetUserAuthTable;
using Application.UnitTests.Common;
using AutoMapper;
using Shared.Application.Exceptions;
using Infrastructure.Persistence;
using Shouldly;
using Xunit;
using Xunit.Abstractions;
using static Application.Account.User.Queries.GetUserAuthTable.GetUserAuthTableQuery;

namespace Application.UnitTests.Account.User.Queries.GetUserAuthTable;

[Collection("AccountQueryCollection")]
public class GetUserAuthTableQueryTests
{
    private readonly SPADbContext _context;
    private readonly IMapper _mapper;
    private XunitLogger<GetUserAuthTableQuery> _logger;

    private GetUserAuthTableQueryHandler _sut;

    public GetUserAuthTableQueryTests(AccountQueryTestFixture fixture, ITestOutputHelper output)
    {
        _context = fixture._context;
        _mapper = fixture.Mapper;
        _logger = new XunitLogger<GetUserAuthTableQuery>(output);
        _sut = new GetUserAuthTableQueryHandler(_context, _mapper);
    }

    [Fact]
    public async Task Handle_GivenInvalidId_ThrowsNotFoundException()
    {
        //Given
        var command = new GetUserAuthTableQuery
        {
            Id = -1,
            Page = 1,
            ItemsPerPage = 10,
            SortBy = "stamp",
            SortDesc = true,
            Filters = null,
        };

        //Then
        await Should.ThrowAsync<NotFoundException>(() => _sut.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task GetUserAuthTableTests()
    {
        // Given
        var command = new GetUserAuthTableQuery
        {
            Id = 1,
            Page = 1,
            ItemsPerPage = 10,
            SortBy = "stamp",
            SortDesc = true,
            Filters = null,
        };

        // When
        var result = await _sut.Handle(command, CancellationToken.None);

        //Then
        result.Total.ShouldBe(2);
        result.Items.ShouldNotBeEmpty();
        result.Items.Count.ShouldBe(2);
        result.Items.ShouldContain(p => p.idAuth == 1);
        result.Items.ShouldContain(p => p.idAuth == 2);
    }
}

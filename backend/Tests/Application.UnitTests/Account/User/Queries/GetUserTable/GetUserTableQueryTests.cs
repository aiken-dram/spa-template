using AutoMapper;
using Infrastructure.Persistence;
using Xunit;
using FluentAssertions;
using Application.UnitTests.Common;
using Application.Account.User.Queries.GetUserTable;
using Xunit.Abstractions;
using System.Threading.Tasks;
using System.Threading;

namespace Application.UnitTests.Account.Queries.GetUserTable;

[Collection("AccountQueryCollection")]
public class GetUserTableQueryTests
{
    private readonly SPADbContext _context;
    private readonly IMapper _mapper;
    private XunitLogger<GetUserTableQuery> _logger;

    private GetUserTableQueryHandler _sut;

    public GetUserTableQueryTests(AccountQueryTestFixture fixture, ITestOutputHelper output)
    {
        _context = fixture._context;
        _mapper = fixture.Mapper;
        _logger = new XunitLogger<GetUserTableQuery>(output);
        _sut = new GetUserTableQueryHandler(_context, _mapper, _logger);
    }

    /*
    * hmm not really testing stuff like order, filters and pagination here,
    * probably should add testing to spa-shared project? 
    * or maybe not, since it's almost like testing ef core stuff
    */

    [Fact]
    public async Task GetUserTableTests()
    {
        //Given
        var command = new GetUserTableQuery
        {
            Page = 1,
            ItemsPerPage = 10,
            SortBy = "idUser",
            SortDesc = false,
            Filters = null,
            Search = null,
            FullSearch = null
        };

        //When
        var result = await _sut.Handle(command, CancellationToken.None);

        //Then
        result.Total.Should().Be(6);
        result.Items.Should().NotBeEmpty();
        result.Items!.Count.Should().Be(6);
        result.Items.Should().Contain(p => p.idUser == 1);
        result.Items.Should().Contain(p => p.idUser == 2);
        result.Items.Should().Contain(p => p.idUser == 3);
        result.Items.Should().Contain(p => p.idUser == 4);
        result.Items.Should().Contain(p => p.idUser == 5);
        result.Items.Should().Contain(p => p.idUser == 6);
    }

    [Fact]
    public async Task Handle_GivenFullSearch_FiltersResult()
    {
        // Given
        var command = new GetUserTableQuery
        {
            Page = 1,
            ItemsPerPage = 10,
            SortBy = "idUser",
            SortDesc = false,
            Filters = null,
            Search = null,
            FullSearch = "user"
        };

        // When
        var result = await _sut.Handle(command, CancellationToken.None);

        // Then
        result.Total.Should().Be(4);
        result.Items.Should().NotBeEmpty();
        result.Items!.Count.Should().Be(4);
        result.Items.Should().Contain(p => p.idUser == 3);
        result.Items.Should().Contain(p => p.idUser == 4);
        result.Items.Should().Contain(p => p.idUser == 5);
        result.Items.Should().Contain(p => p.idUser == 6);
    }
}

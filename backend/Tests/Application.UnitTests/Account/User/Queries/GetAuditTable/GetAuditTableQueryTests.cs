using System.Threading;
using System.Threading.Tasks;
using Application.Account.User.Queries.GetAuditTable;
using Application.UnitTests.Common;
using AutoMapper;
using Shared.Application.Exceptions;
using Infrastructure.Persistence;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;
using Moq;
using Application.Common.Interfaces;

namespace Application.UnitTests.Account.User.Queries.GetAuditTable;

[Collection("AccountQueryCollection")]
public class GetUserAuthTableQueryTests
{
    private readonly SPADbContext _context;
    private readonly IMapper _mapper;
    private XunitLogger<GetAuditTableQuery> _logger;
    private Mock<IUserService> _user;

    private GetAuditTableQueryHandler _sut;

    public GetUserAuthTableQueryTests(AccountQueryTestFixture fixture, ITestOutputHelper output)
    {
        _context = fixture._context;
        _user = new Mock<IUserService>();
        _mapper = fixture.Mapper;
        _logger = new XunitLogger<GetAuditTableQuery>(output);
        _sut = new GetAuditTableQueryHandler(_context, _user.Object, _mapper, _logger);
    }

    [Fact]
    public async Task Handle_GivenInvalidId_ThrowsNotFoundException()
    {
        //Given
        var command = new GetAuditTableQuery
        {
            IdUser = -1,
            Page = 1,
            ItemsPerPage = 10,
            SortBy = "stamp",
            SortDesc = true,
            Filters = null,
        };

        //Then
        await FluentActions.Invoking(() =>
            _sut.Handle(command, CancellationToken.None)).Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task GetUserAuthTableTests()
    {
        // Given
        var command = new GetAuditTableQuery
        {
            IdUser = 1,
            Page = 1,
            ItemsPerPage = 10,
            SortBy = "stamp",
            SortDesc = true,
            Filters = null,
        };

        // When
        var result = await _sut.Handle(command, CancellationToken.None);

        //Then
        result.Total.Should().Be(2);
        result.Items.Should().NotBeEmpty();
        result.Items!.Count.Should().Be(2);
        result.Items.Should().Contain(p => p.idAudit == 1);
        result.Items.Should().Contain(p => p.idAudit == 2);
    }
}

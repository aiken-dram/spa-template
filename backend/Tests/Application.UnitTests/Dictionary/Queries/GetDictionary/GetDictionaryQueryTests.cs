using AutoMapper;
using Infrastructure.Persistence;
using Moq;
using Xunit;
using FluentAssertions;
using Application.UnitTests.Common;
using Application.Common.Interfaces;
using System.Threading.Tasks;
using Application.Dictionary.Queries.GetDictionary;
using System.Threading;
using System.Collections.Generic;
using Shared.Application.Exceptions;

namespace Application.UnitTests.Dictionary.Queries.GetDictionary;

[Collection("QueryCollection")]
public class GetDictionaryQueryTests
{
    private readonly SPADbContext _context;
    private readonly IMapper _mapper;
    private Mock<IUserService> _user;

    private GetDictionaryQueryHandler _sut;

    public GetDictionaryQueryTests(QueryTestFixture fixture)
    {
        _context = fixture._context;
        _mapper = fixture.Mapper;
        _user = UserServiceFactory.Create();
        _sut = new GetDictionaryQueryHandler(_context, _mapper, _user.Object);
    }

    [Fact]
    public async Task Handle_GivenUnsupportedDictionary_ThrowsNotFoundException()
    {
        // Given
        var command = new GetDictionaryQuery { Dictionary = "wrong" };

        // Then
        await FluentActions.Invoking(() =>
            _sut.Handle(command, CancellationToken.None)).Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task GetDictionaryAccessGroupsTests()
    {
        // Given
        var command = new GetDictionaryQuery { Dictionary = "AccessGroups" };

        // When
        var result = await _sut.Handle(command, CancellationToken.None);

        // Then
        result.Should().BeOfType<List<DictionaryDto>>();
        result.Should().NotBeEmpty();
        result.Count.Should().Be(4);
        result.Should().Contain(p => p.Value == 1 && p.Text == "Admins");
        result.Should().Contain(p => p.Value == 2 && p.Text == "Supervisors");
        result.Should().Contain(p => p.Value == 3 && p.Text == "Users");
        result.Should().Contain(p => p.Value == 4 && p.Text == "Viewers");
    }

    [Fact]
    public async Task GetDictionaryAccessRolesTests()
    {
        // Given
        var command = new GetDictionaryQuery { Dictionary = "AccessRoles" };

        // When
        var result = await _sut.Handle(command, CancellationToken.None);

        // Then
        result.Should().BeOfType<List<DictionaryDto>>();
        result.Should().NotBeEmpty();
        result.Count.Should().Be(4);
        result.Should().Contain(p => p.Value == 1 && p.Text == "Access admins");
        result.Should().Contain(p => p.Value == 2 && p.Text == "Application admins");
        result.Should().Contain(p => p.Value == 3 && p.Text == "Supervisor");
        result.Should().Contain(p => p.Value == 4 && p.Text == "Read only");
    }

    [Fact]
    public async Task GetDictionaryDistrictsTests()
    {
        // Given
        var command = new GetDictionaryQuery { Dictionary = "Districts" };

        // When
        var result = await _sut.Handle(command, CancellationToken.None);

        // Then
        result.Should().BeOfType<List<DictionaryDto>>();
        result.Should().NotBeEmpty();
        result.Count.Should().Be(3);
        result.Should().Contain(p => p.Value == 1 && p.Text == "1 Анапа");
        result.Should().Contain(p => p.Value == 2 && p.Text == "2 Армавир");
        result.Should().Contain(p => p.Value == 3 && p.Text == "3 Белореченск");
    }

    [Fact]
    public async Task GetDictionaryUserDistrictsSupervisorTests()
    {
        // Given
        UserServiceFactory.Setup(ref _user, eMockUser.Supervisor);
        _sut = new GetDictionaryQueryHandler(_context, _mapper, _user.Object);
        var command = new GetDictionaryQuery { Dictionary = "UserDistricts" };

        // When
        var result = await _sut.Handle(command, CancellationToken.None);

        // Then
        result.Should().BeOfType<List<DictionaryDto>>();
        result.Should().NotBeEmpty();
        result.Count.Should().Be(3);
        result.Should().Contain(p => p.Value == 1 && p.Text == "1 Анапа");
        result.Should().Contain(p => p.Value == 2 && p.Text == "2 Армавир");
        result.Should().Contain(p => p.Value == 3 && p.Text == "3 Белореченск");
    }

    [Fact]
    public async Task GetDictionaryUserDistrictsTests()
    {
        // Given
        UserServiceFactory.Setup(ref _user, eMockUser.User1);
        _sut = new GetDictionaryQueryHandler(_context, _mapper, _user.Object);
        var command = new GetDictionaryQuery { Dictionary = "UserDistricts" };

        // When
        var result = await _sut.Handle(command, CancellationToken.None);

        // Then
        result.Should().BeOfType<List<DictionaryDto>>();
        result.Should().NotBeEmpty();
        result.Count.Should().Be(1);
        result.Should().Contain(p => p.Value == 1 && p.Text == "1 Анапа");
    }

    [Fact]
    public async Task GetDictionaryAuthActionsTests()
    {
        // Given
        var command = new GetDictionaryQuery { Dictionary = "AuthActions" };

        // When
        var result = await _sut.Handle(command, CancellationToken.None);

        // Then
        result.Should().BeOfType<List<DictionaryDto>>();
        result.Should().NotBeEmpty();
        result.Count.Should().Be(4);
        result.Should().Contain(p => p.Value == 1 && p.Text == "Login description");
        result.Should().Contain(p => p.Value == 2 && p.Text == "Wrong pass description");
        result.Should().Contain(p => p.Value == 3 && p.Text == "Expired description");
        result.Should().Contain(p => p.Value == 4 && p.Text == "Lock description");
    }
}
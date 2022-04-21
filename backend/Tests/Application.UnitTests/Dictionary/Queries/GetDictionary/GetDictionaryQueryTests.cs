using AutoMapper;
using Infrastructure.Persistence;
using Moq;
using Xunit;
using Shouldly;
using Application.UnitTests.Common;
using Application.Common.Interfaces;
using static Application.Dictionary.Queries.GetDictionary.GetDictionaryQuery;
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
        await Should.ThrowAsync<NotFoundException>(() => _sut.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task GetDictionaryAccessGroupsTests()
    {
        // Given
        var command = new GetDictionaryQuery { Dictionary = "AccessGroups" };

        // When
        var result = await _sut.Handle(command, CancellationToken.None);

        // Then
        result.ShouldBeOfType<List<DictionaryDto>>();
        result.ShouldNotBeEmpty();
        result.Count.ShouldBe(4);
        result.ShouldContain(p => p.Value == 1 && p.Text == "Admins");
        result.ShouldContain(p => p.Value == 2 && p.Text == "Supervisors");
        result.ShouldContain(p => p.Value == 3 && p.Text == "Users");
        result.ShouldContain(p => p.Value == 4 && p.Text == "Viewers");
    }

    [Fact]
    public async Task GetDictionaryAccessRolesTests()
    {
        // Given
        var command = new GetDictionaryQuery { Dictionary = "AccessRoles" };

        // When
        var result = await _sut.Handle(command, CancellationToken.None);

        // Then
        result.ShouldBeOfType<List<DictionaryDto>>();
        result.ShouldNotBeEmpty();
        result.Count.ShouldBe(4);
        result.ShouldContain(p => p.Value == 1 && p.Text == "Access admins");
        result.ShouldContain(p => p.Value == 2 && p.Text == "Application admins");
        result.ShouldContain(p => p.Value == 3 && p.Text == "Supervisor");
        result.ShouldContain(p => p.Value == 4 && p.Text == "Read only");
    }

    [Fact]
    public async Task GetDictionaryAuthActionsTests()
    {
        // Given
        var command = new GetDictionaryQuery { Dictionary = "AuthActions" };

        // When
        var result = await _sut.Handle(command, CancellationToken.None);

        // Then
        result.ShouldBeOfType<List<DictionaryDto>>();
        result.ShouldNotBeEmpty();
        result.Count.ShouldBe(4);
        result.ShouldContain(p => p.Value == 1 && p.Text == "Login description");
        result.ShouldContain(p => p.Value == 2 && p.Text == "Wrong pass description");
        result.ShouldContain(p => p.Value == 3 && p.Text == "Expired description");
        result.ShouldContain(p => p.Value == 4 && p.Text == "Lock description");
    }
}
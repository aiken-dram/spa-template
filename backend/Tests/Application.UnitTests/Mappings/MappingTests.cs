using Application.Account.User.Queries.GetCurrentUser;
using Application.Account.User.Queries.GetAuditTable;
using Application.Account.User.Queries.GetUser;
using Application.Account.User.Queries.GetUserTable;
using Application.Dictionary.Queries.GetDictionary;
using AutoMapper;
using Domain.Entities;
using FluentAssertions;
using Xunit;
using Application.Common.Models;

namespace Application.UnitTests.Mappings;

public class MappingTests : IClassFixture<MappingTestsFixture>
{
    private readonly IConfigurationProvider _configuration;
    private readonly IMapper _mapper;

    public MappingTests(MappingTestsFixture fixture)
    {
        _configuration = fixture.ConfigurationProvider;
        _mapper = fixture.Mapper;
    }

    [Fact]
    public void ShouldHaveValidConfiguration()
    {
        _configuration.AssertConfigurationIsValid();
    }

    #region ACCOUNT
    [Fact]
    public void ShouldMapUserToCurrentUserVm()
    {
        var entity = new Domain.Entities.User();

        var result = _mapper.Map<CurrentUserVm>(entity);

        result.Should().NotBeNull();
        result.Should().BeOfType<CurrentUserVm>();

    }

    [Fact]
    public void ShouldMapUserToUserVm()
    {
        var entity = new Domain.Entities.User();

        var result = _mapper.Map<UserVm>(entity);

        result.Should().NotBeNull();
        result.Should().BeOfType<UserVm>();

    }

    [Fact]
    public void ShouldMapUserToUserTableDto()
    {
        var entity = new Domain.Entities.User();

        var result = _mapper.Map<UserTableDto>(entity);

        result.Should().NotBeNull();
        result.Should().BeOfType<UserTableDto>();
    }

    [Fact]
    public void ShouldMapUserAuditToAuditTableDto()
    {
        // Given
        var entity = new Domain.Entities.VAudit();

        // When
        var result = _mapper.Map<AuditTable>(entity);

        // Then
        result.Should().NotBeNull();
        result.Should().BeOfType<AuditTable>();
    }

    [Fact]
    public void ShouldMapUserAuditDataToAuditDataTableDto()
    {
        // Given
        var entity = new Domain.Entities.VAuditData();

        // When
        var result = _mapper.Map<AuditDataTable>(entity);

        // Then
        result.Should().NotBeNull();
        result.Should().BeOfType<AuditDataTable>();
    }
    #endregion

    #region DICTIONARY
    [Fact]
    public void ShouldMapGroupToDictionaryDto()
    {
        var entity = new Group();

        var result = _mapper.Map<DictionaryDto>(entity);

        result.Should().NotBeNull();
        result.Should().BeOfType<DictionaryDto>();
    }

    [Fact]
    public void ShouldMapRoleToDictionaryDto()
    {
        var entity = new Role();

        var result = _mapper.Map<DictionaryDto>(entity);

        result.Should().NotBeNull();
        result.Should().BeOfType<DictionaryDto>();
    }

    [Fact]
    public void ShouldMapDistrictToDictionaryDto()
    {
        var entity = new District();

        var result = _mapper.Map<DictionaryDto>(entity);

        result.Should().NotBeNull();
        result.Should().BeOfType<DictionaryDto>();
    }
    #endregion
}

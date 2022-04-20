using Application.Account.User.Queries.GetCurrentUserDetail;
using Application.Account.User.Queries.GetUserDetail;
using Application.Account.User.Queries.GetUserTable;
using Application.Dictionary.Queries.GetDictionary;
using AutoMapper;
using Domain.Entities;
using Shouldly;
using Xunit;

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
    public void ShouldMapUserToCurrentUserDetailVm()
    {
        var entity = new Domain.Entities.User();

        var result = _mapper.Map<CurrentUserDetailVm>(entity);

        result.ShouldNotBeNull();
        result.ShouldBeOfType<CurrentUserDetailVm>();

    }

    [Fact]
    public void ShouldMapUserToUserDetailVm()
    {
        var entity = new Domain.Entities.User();

        var result = _mapper.Map<UserDetailVm>(entity);

        result.ShouldNotBeNull();
        result.ShouldBeOfType<UserDetailVm>();

    }

    [Fact]
    public void ShouldMapUserToUserTableDto()
    {
        var entity = new Domain.Entities.User();

        var result = _mapper.Map<UserTableDto>(entity);

        result.ShouldNotBeNull();
        result.ShouldBeOfType<UserTableDto>();
    }
    #endregion

    #region DICTIONARY
    [Fact]
    public void ShouldMapGroupsToDictionaryDto()
    {
        var entity = new Group();

        var result = _mapper.Map<DictionaryDto>(entity);

        result.ShouldNotBeNull();
        result.ShouldBeOfType<DictionaryDto>();
    }

    [Fact]
    public void ShouldMapRolesToDictionaryDto()
    {
        var entity = new Role();

        var result = _mapper.Map<DictionaryDto>(entity);

        result.ShouldNotBeNull();
        result.ShouldBeOfType<DictionaryDto>();
    }
    #endregion
}

using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Account.User.Queries.GetUserDetail;
using Application.UnitTests.Common;
using AutoMapper;
using Shared.Application.Exceptions;
using Infrastructure.Persistence;
using Shouldly;
using Xunit;
using static Application.Account.User.Queries.GetUserDetail.GetUserDetailQuery;

namespace Application.UnitTests.Account.User.Queries.GetUserDetail;

[Collection("QueryCollection")]
public class GetUserDetailQueryTests
{
    private readonly SPADbContext _context;
    private readonly IMapper _mapper;

    private readonly GetUserDetailQueryHandler _sut;


    public GetUserDetailQueryTests(QueryTestFixture fixture)
    {
        _context = fixture.Context;
        _mapper = fixture.Mapper;

        _sut = new GetUserDetailQueryHandler(_context, _mapper);
    }

    [Fact]
    public async Task Handle_GivenValidId_ReturnsUserDetailVm()
    {
        //Given
        var command = new GetUserDetailQuery { Id = 1 };

        //When
        var result = await _sut.Handle(command, CancellationToken.None);

        //Then
        result.ShouldBeOfType<UserDetailVm>();

        result.IdUser.ShouldBe(1);
        result.Login.ShouldBe("admin");
        result.Name.ShouldBe("Application admin");
        result.Description.ShouldBe("Application admin description");
        result.IsActive.ShouldBe("T");
        result.PassDate.ShouldBe(new DateTime(2050, 1, 1));
        result.Groups.ShouldNotBeNull();
        result.Groups.Length.ShouldBe(1);
        result.Groups.ShouldContain(1);
        result.Roles.ShouldNotBeNull();
        result.Roles.Length.ShouldBe(0);
    }

    [Fact]
    public async Task Handle_GivenInvalidId_ThrowsNotFoundException()
    {
        //Given
        var command = new GetUserDetailQuery { Id = -1 };

        //Then
        await Assert.ThrowsAsync<NotFoundException>(() => _sut.Handle(command, CancellationToken.None));
    }
}

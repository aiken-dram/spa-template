using System.Threading;
using System.Threading.Tasks;
using Application.Account.User.Queries.GetCurrentUserDetail;
using Application.Common.Interfaces;
using Application.UnitTests.Common;
using AutoMapper;
using Shared.Application.Exceptions;
using Infrastructure.Persistence;
using Moq;
using Shouldly;
using Xunit;
using static Application.Account.User.Queries.GetCurrentUserDetail.GetCurrentUserDetailQuery;

namespace Application.UnitTests.Account.User.Queries.GetCurrentUserDetail;

[Collection("QueryCollection")]
public class GetCurrentUserDetailQueryTests
{
    private readonly SPADbContext _context;
    private readonly IMapper _mapper;
    private Mock<IUserService> _user = null!;

    public GetCurrentUserDetailQueryTests(QueryTestFixture fixture)
    {
        _context = fixture.Context;
        _mapper = fixture.Mapper;
    }

    [Fact]
    public async Task Handle_GivenValidUserId_ReturnsUserDetailVm()
    {
        //Given
        _user = UserServiceFactory.Create();

        var sut = new GetCurrentUserDetailQueryHandler(_context, _mapper, _user.Object);

        //When
        var result = await sut.Handle(new GetCurrentUserDetailQuery(), CancellationToken.None);

        //Then
        result.ShouldBeOfType<CurrentUserDetailVm>();

        result.IdUser.ShouldBe(1);
        result.Login.ShouldBe("admin");
        result.Name.ShouldBe("Application admin");
        result.Description.ShouldBe("Application admin description");
    }

    [Fact]
    public async Task Handle_GivenInvalidCurrentUser_ThrowsNotFoundException()
    {
        //Given
        _user = UserServiceFactory.Create(eMockUser.Invalid);

        var sut = new GetCurrentUserDetailQueryHandler(_context, _mapper, _user.Object);
        var command = new GetCurrentUserDetailQuery();

        //Then
        await Assert.ThrowsAsync<NotFoundException>(() => sut.Handle(command, CancellationToken.None));
    }
}

using System.Threading;
using System.Threading.Tasks;
using Application.Account.User.Queries.GetCurrentUser;
using Application.Common.Interfaces;
using Application.UnitTests.Common;
using AutoMapper;
using Shared.Application.Exceptions;
using Infrastructure.Persistence;
using Moq;
using FluentAssertions;
using Xunit;

namespace Application.UnitTests.Account.User.Queries.GetCurrentUserDetail;

[Collection("AccountQueryCollection")]
public class GetCurrentUserQueryTests
{
    private readonly SPADbContext _context;
    private readonly IMapper _mapper;
    private Mock<IUserService> _user = null!;

    public GetCurrentUserQueryTests(AccountQueryTestFixture fixture)
    {
        _user = UserServiceFactory.Create();

        _context = fixture._context;
        _mapper = fixture.Mapper;
    }

    [Fact]
    public async Task Handle_GivenValidUserId_ReturnsUserDetailVm()
    {
        //Given
        var _sut = new GetCurrentUserQueryHandler(_context, _mapper, _user.Object);

        //When
        var result = await _sut.Handle(new GetCurrentUserQuery(), CancellationToken.None);

        //Then
        result.Should().BeOfType<CurrentUserVm>();

        result.IdUser.Should().Be(1);
        result.Login.Should().Be("admin");
        result.Name.Should().Be("Application admin");
        result.Description.Should().Be("Application admin description");
    }

    [Fact]
    public async Task Handle_GivenInvalidCurrentUser_ThrowsNotFoundException()
    {
        //Given
        _user = UserServiceFactory.Create(eMockUser.Invalid);

        var _sut = new GetCurrentUserQueryHandler(_context, _mapper, _user.Object);
        var command = new GetCurrentUserQuery();

        //Then
        await FluentActions.Invoking(() =>
            _sut.Handle(command, CancellationToken.None)).Should().ThrowAsync<NotFoundException>();
    }
}

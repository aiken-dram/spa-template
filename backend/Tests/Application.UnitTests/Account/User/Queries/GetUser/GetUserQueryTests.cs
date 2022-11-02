using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Account.User.Queries.GetUser;
using Application.UnitTests.Common;
using AutoMapper;
using Shared.Application.Exceptions;
using Infrastructure.Persistence;
using FluentAssertions;
using Xunit;

namespace Application.UnitTests.Account.User.Queries.GetUserDetail;

[Collection("AccountQueryCollection")]
public class GetUserQueryTests
{
    private readonly SPADbContext _context;
    private readonly IMapper _mapper;

    private readonly GetUserQueryHandler _sut;


    public GetUserQueryTests(AccountQueryTestFixture fixture)
    {
        _context = fixture._context;
        _mapper = fixture.Mapper;

        _sut = new GetUserQueryHandler(_context, _mapper);
    }

    [Fact]
    public async Task Handle_GivenValidId_ReturnsUserDetailVm()
    {
        //Given
        var command = new GetUserQuery { Id = 1 };

        //When
        var result = await _sut.Handle(command, CancellationToken.None);

        //Then
        result.Should().BeOfType<UserVm>();

        result.IdUser.Should().Be(1);
        result.Login.Should().Be("admin");
        result.Name.Should().Be("Application admin");
        result.Description.Should().Be("Application admin description");
        result.IsActive.Should().Be("T");
        result.PassDate.Should().Be(new DateTime(2050, 1, 1));
        result.Groups.Should().NotBeNull();
        result.Groups!.Length.Should().Be(1);
        result.Groups.Should().Contain(1);
        result.Roles.Should().NotBeNull();
        result.Roles!.Length.Should().Be(0);
        result.Districts.Should().NotBeNull();
        result.Districts!.Length.Should().Be(0);
    }

    [Fact]
    public async Task Handle_GivenInvalidId_ThrowsNotFoundException()
    {
        //Given
        var command = new GetUserQuery { Id = -1 };

        //Then
        await FluentActions.Invoking(() =>
            _sut.Handle(command, CancellationToken.None)).Should().ThrowAsync<NotFoundException>();
    }
}

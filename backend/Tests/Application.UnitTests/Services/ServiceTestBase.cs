using System;
using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Common.Services;
using Application.UnitTests.Common;
using AutoMapper;
using Infrastructure.Persistence;
using Moq;
using Xunit.Abstractions;

namespace Application.UnitTests.Services;

public class ServiceTestBase : IDisposable
{
    public SPADbContext _context { get; private set; }
    public IMapper Mapper { get; private set; }
    public Mock<ICurrentUserService> User { get; private set; }

    public UserService _user { get; private set; }

    private XunitLogger<UserService> _logger_user;

    public ServiceTestBase(ITestOutputHelper output)
    {
        _context = SPADbContextFactory.CreateInMemory();

        var configurationProvider = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });

        Mapper = configurationProvider.CreateMapper();

        _logger_user = new XunitLogger<UserService>(output);

        User = new Mock<ICurrentUserService>();

        _user = new UserService(_context, Mapper, _logger_user, User.Object);
    }

    public void Dispose()
    {
        SPADbContextFactory.Destroy(_context);
    }
}

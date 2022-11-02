using System;
using Infrastructure.UnitTests.Common;
using Infrastructure.Identity;
using Infrastructure.Persistence;
using Moq;
using Xunit.Abstractions;
using System.Collections.Generic;
using Application.Common.Interfaces;

namespace Infrastructure.UnitTests.Identity;

public class AuthServiceTestBase : IDisposable
{
    public SPADbContext _context { get; private set; }

    public AuthService _auth { get; private set; }

    public Mock<IDomainEventService> _domainEventService;
    public Mock<ICurrentUserService> _currentUserService;
    public Mock<Infrastructure.Common.Interfaces.IConfigurationService> _configuration;

    private XunitLogger<AuthService> _logger;

    public AuthServiceTestBase(ITestOutputHelper output)
    {
        _domainEventService = new Mock<IDomainEventService>();
        _currentUserService = new Mock<ICurrentUserService>();
        _configuration = new Mock<Infrastructure.Common.Interfaces.IConfigurationService>();

        _context = SPADbContextFactory.CreateInMemory(_domainEventService.Object, _currentUserService.Object);

        _logger = new XunitLogger<AuthService>(output);

        //configuration setup for testing
        var inMemorySettings = new Dictionary<string, string> {
            {"AuthSettings:Lock", "10"},
            {"AuthSettings:Timeout", "3"},
        };

        _auth = new AuthService(_context, _logger, _configuration.Object);
    }

    public void Dispose()
    {
        SPADbContextFactory.Destroy(_context);
    }
}

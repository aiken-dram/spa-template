using System;
using Infrastructure.UnitTests.Common;
using Infrastructure.Identity;
using Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit.Abstractions;
using System.Collections.Generic;

namespace Infrastructure.UnitTests.Identity;

public class AuthServiceTestBase : IDisposable
{
    public SPADbContext _context { get; private set; }

    public AuthService _auth { get; private set; }

    private XunitLogger<AuthService> _logger;
    private IConfiguration _config;

    public AuthServiceTestBase(ITestOutputHelper output)
    {
        _context = SPADbContextFactory.CreateInMemory();

        _logger = new XunitLogger<AuthService>(output);

        //configuration setup for testing
        var inMemorySettings = new Dictionary<string, string> {
            {"AuthSettings:Lock", "10"},
            {"AuthSettings:Timeout", "3"},
        };
        _config = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        _auth = new AuthService(_context, _logger, _config);
    }

    public void Dispose()
    {
        SPADbContextFactory.Destroy(_context);
    }
}

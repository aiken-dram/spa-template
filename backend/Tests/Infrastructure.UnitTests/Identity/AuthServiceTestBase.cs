using System;
using Infrastructure.UnitTests.Common;
using Infrastructure.Identity;
using Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit.Abstractions;

namespace Infrastructure.UnitTests.Identity
{
    public class AuthServiceTestBase : IDisposable
    {
        public SPADbContext Context { get; private set; }

        public AuthService _auth { get; private set; }

        private XunitLogger<AuthService> _logger;
        private Mock<IConfiguration> _config;

        public AuthServiceTestBase(ITestOutputHelper output)
        {
            Context = SPADbContextFactory.CreateInMemory();

            _logger = new XunitLogger<AuthService>(output);
            _config = new Mock<IConfiguration>(); //2D setup here

            _auth = new AuthService(Context, _logger, _config.Object);
        }

        public void Dispose()
        {
            SPADbContextFactory.Destroy(Context);
        }
    }
}
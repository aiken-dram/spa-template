using System;
using Application.Common.Interfaces;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistence;
using Moq;
using Xunit;

namespace Application.UnitTests.Common;

public class QueryTestFixture : IDisposable
{
    public SPADbContext _context { get; private set; }
    public IMapper Mapper { get; private set; }
    public Mock<ICurrentUserService> User { get; private set; }

    public QueryTestFixture()
    {
        _context = SPADbContextFactory.CreateInMemory();

        Seed();

        var configurationProvider = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });

        Mapper = configurationProvider.CreateMapper();

        User = new Mock<ICurrentUserService>();
    }

    protected virtual void Seed()
    {

    }

    public void Dispose()
    {
        SPADbContextFactory.Destroy(_context);
    }
}

public class AccountQueryTestFixture : QueryTestFixture
{
    protected override void Seed()
    {
        base.Seed();

        _context.UserAuth.AddRange(new[]
        {
            new UserAuth { IdAuth = 1, IdUser = 1, Stamp = DateTime.Now, IdAction = 1, System = "localhost", Message = "" },
            new UserAuth { IdAuth = 2, IdUser = 1, Stamp = DateTime.Now, IdAction = 2, System = "localhost", Message = "" },
            new UserAuth { IdAuth = 3, IdUser = 2, Stamp = DateTime.Now, IdAction = 2, System = "localhost", Message = "" },
        });

        _context.SaveChanges();
    }
}

[CollectionDefinition("QueryCollection")]
public class QueryCollection : ICollectionFixture<QueryTestFixture> { }

[CollectionDefinition("AccountQueryCollection")]
public class AccountQueryCollection : ICollectionFixture<AccountQueryTestFixture> { }

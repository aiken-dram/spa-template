using System;
using Application.Common.Interfaces;
using Application.Common.Mappings;
using AutoMapper;
using Infrastructure.Persistence;
using Moq;
using Xunit;

namespace Application.UnitTests.Common;

public class QueryTestFixture : IDisposable
{
    public SPADbContext Context { get; private set; }
    public IMapper Mapper { get; private set; }
    public Mock<ICurrentUserService> User { get; private set; }

    public QueryTestFixture()
    {
        Context = SPADbContextFactory.CreateInMemory();

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
        SPADbContextFactory.Destroy(Context);
    }
}

[CollectionDefinition("QueryCollection")]
public class QueryCollection : ICollectionFixture<QueryTestFixture> { }

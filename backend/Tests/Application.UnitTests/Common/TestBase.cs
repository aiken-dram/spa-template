using System;
using Infrastructure.Persistence;

namespace Application.UnitTests.Common;

public class TestBase : IDisposable
{
    protected readonly SPADbContext _context;

    public TestBase()
    {
        _context = SPADbContextFactory.CreateInMemory();
    }

    public void Dispose()
    {
        SPADbContextFactory.Destroy(_context);
    }
}

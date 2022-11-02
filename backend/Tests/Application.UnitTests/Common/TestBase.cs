using System;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Infrastructure.Persistence;
using MediatR;
using Moq;

namespace Application.UnitTests.Common;

public class TestBase : IDisposable
{
    protected readonly SPADbContext _context;
    public Mock<IDomainEventService> _domainEventService;
    public Mock<ICurrentUserService> _currentUserService;
    public Mock<IAuditService> _audit;

    public TestBase()
    {
        _domainEventService = new Mock<IDomainEventService>();
        _currentUserService = new Mock<ICurrentUserService>();
        _audit = new Mock<IAuditService>();
        _context = SPADbContextFactory.CreateInMemory(_domainEventService.Object, _currentUserService.Object);
    }

    public void Dispose()
    {
        SPADbContextFactory.Destroy(_context);
    }
}

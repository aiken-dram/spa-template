namespace Application.MessageQuery.Commands.DeleteRequest;

public class DeleteRequestCommand : IRequest
{
    /// <summary>
    /// Id of request in database
    /// </summary>
    /// <example>1</example>
    public long Id { get; set; }
}

public class DeleteRequestCommandHandler : IRequestHandler<DeleteRequestCommand>
{
    private readonly ISPADbContext _context;
    private readonly IUserService _user;
    private readonly IFileService _file;

    public DeleteRequestCommandHandler(
        ISPADbContext context,
        IUserService user,
        IFileService file)
    {
        _context = context;
        _user = user;
        _file = file;
    }

    public async Task<Unit> Handle(DeleteRequestCommand request, CancellationToken cancellationToken)
    {
        //check access                
        var entity = await _context.Requests
            .GetAsync(request.Id, cancellationToken);

        //only supervisors or users who created request can delete it
        var user = await _user.GetCurrentUserAsync(cancellationToken);
        if (!user.Modules.Contains(eAccountModule.Supervise))
            if (entity.IdUser != user.IdUser)
                throw new AccessDeniedException(Messages.NoAccessToDeleteRequest);

        //delete request
        _context.Requests.Remove(entity);

        //delete request file
        if (entity.Guid != null)
            _file.DeleteRequestResponseFile(entity.Guid);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}

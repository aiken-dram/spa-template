using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Account.User.Commands.ProcessFile;

/// <summary>
/// Update users with provided passwords from text file
/// </summary>
[Authorize(Modules = eAccountModule.SecurityAdmin)]
public class ProcessFileCommand : SignalRCommand, IRequest<ProcessFileVm>
{
    /// <summary>
    /// Parsed content of text file
    /// </summary>
    public IList<string?>? FileContent { get; set; }
}

public class ProcessFileCommandHandler : SignalRCommandHandler, IRequestHandler<ProcessFileCommand, ProcessFileVm>
{
    private readonly ISPADbContext _context;
    private readonly ILogger _logger;
    private IAuditBuilder _audit => _context.AuditBuilder;

    public ProcessFileCommandHandler(
        IMediator mediator,
        ISPADbContext context,
        ILogger<ProcessFileCommand> logger)
        : base(mediator, eSignalRSubject.UserProcessFile)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// Audit for updating password from file
    /// </summary>
    /// <param name="entity">User entity</param>
    /// <returns>Audit</returns>
    public async Task<Audit> Audit(Domain.Entities.User entity)
    {
        var res = new Audit(
            entity,
            (int)eUserAuditAction.UpdatePassword,
            null);

        // AuditData
        res.Add(await _audit.PropertyValueAsync(entity, p => p.PassDate));

        return res;
    }

    /// <summary>
    /// Processing line from file for updating password
    /// </summary>
    /// <param name="line">Line from file</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Message for processing line</returns>
    private async Task<(Message, long?)> ProcessLine(string line, CancellationToken cancellationToken)
    {
        //already have roles declared on api controller
        //no further access restriction necessary

        var a = line.Split(' ');
        if (a.Length != 2)
            return (Message.Error(Messages.InvalidFormatLine(line)), null);

        var login = a[0];
        var pass = a[1];


        int cnt;
        if ((cnt = await _context.Users.CountAsync(p => p.Login == login, cancellationToken)) != 1)
            return (Message.Error(Messages.FoundMultipleUsersWithLogin(login, cnt)), null);

        var entity = await _context.Users.SingleAsync(p => p.Login == login, cancellationToken);
        entity.UpdatePassword(pass.MD5Hash());

        //log audit event for password update
        entity.Log(await Audit(entity));

        return (Message.Success(Messages.UserPasswordUpdated(login)), entity.IdUser);
    }

    public async Task<ProcessFileVm> Handle(ProcessFileCommand request, CancellationToken cancellationToken)
    {
        //setup connection
        this.IdConnection = request.IdConnection;

        _logger.JsonLogDebug("FileContent", request.FileContent);

        var res = new List<Message>();

        if (request.FileContent != null && request.FileContent.Count(p => !string.IsNullOrWhiteSpace(p)) > 0)
        {
            var lines = request.FileContent.Where(p => !string.IsNullOrWhiteSpace(p));
            SetIteration(lines.Count());
            foreach (var l in lines)
            {
                (var m, var i) = await ProcessLine(l!, cancellationToken);
                await ReportNext(m, i);
                res.Add(m);

                //Thread.Sleep(TimeSpan.FromSeconds(1)); //was checking animation
            }

            await _context.SaveChangesAsync(cancellationToken);
        }

        return new ProcessFileVm(res);
    }
}


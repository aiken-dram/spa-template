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
    private readonly IAuditService _audit;

    public ProcessFileCommandHandler(
        IMediator mediator,
        ISPADbContext context,
        ILogger<ProcessFileCommand> logger,
        IAuditService audit)
        : base(mediator, "Account.User.Commands.ProcessFile")
    {
        _context = context;
        _logger = logger;
        _audit = audit;
    }

    /// <summary>
    /// Processing line from file for updating password
    /// </summary>
    /// <param name="line">Line from file</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Message for processing line</returns>
    private async Task<Message> ProcessLine(string line, CancellationToken cancellationToken)
    {
        //already have roles declared on api controller
        //no further access restriction necessary

        var a = line.Split(' ');
        if (a.Length == 2)
        {
            var login = a[0];
            var pass = a[1];

            var cnt = await _context.Users.CountAsync(p => p.Login == login, cancellationToken);
            if (cnt == 1)
            {
                var entity = await _context.Users.SingleAsync(p => p.Login == login, cancellationToken);
                entity.UpdatePassword(EncryptorHelper.MD5Hash(pass));

                //log audit event for password update
                entity.Log(await _audit.UserUpdatePassword(entity));

                return Message.Success(Messages.UserPasswordUpdated(login));
            }
            else
                return Message.Error(Messages.FoundMultipleUsersWithLogin(login, cnt));
        }
        else
            return Message.Error(Messages.InvalidFormatLine(line));
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
                var m = await ProcessLine(l!, cancellationToken);
                await ReportNext(m);
                res.Add(m);

                //Thread.Sleep(TimeSpan.FromSeconds(1)); //was checking animation
            }

            await _context.SaveChangesAsync(cancellationToken);
        }

        var vm = new ProcessFileVm()
        {
            Items = res
        };
        return vm;
    }
}


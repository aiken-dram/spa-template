using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shared.Application.Extensions;
using Shared.Application.Helpers;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common;
using Shared.Application.Models;
using Shared.Domain.Models;
using Domain.Enums;
using Shared.Domain.Enums;

namespace Application.Account.User.Commands.ProcessFile;

/// <summary>
/// Update users with provided passwords from text file
/// </summary>
public class ProcessFileCommand : SignalRCommand, IRequest<ProcessFileVm>
{
    /// <summary>
    /// Parsed content of text file
    /// </summary>
    public IList<string?>? FileContent { get; set; }

    public class ProcessFileCommandHandler : SignalRCommandHandler, IRequestHandler<ProcessFileCommand, ProcessFileVm>
    {
        private readonly ISPADbContext _context;
        private readonly IAppAuditService _audit;
        private readonly ILogger _logger;

        public ProcessFileCommandHandler(
            IMediator mediator,
            ISPADbContext context,
            IAppAuditService audit,
            ILogger<ProcessFileCommand> logger)
            : base(mediator, "Account.User.Commands.ProcessFile")
        {
            _context = context;
            _audit = audit;
            _logger = logger;
        }

        /// <summary>
        /// Audit for updating password from file
        /// </summary>
        /// <param name="entity">User entity</param>
        /// <returns>AuditEvent</returns>
        private async Task<AuditEvent> Audit(Domain.Entities.User entity)
        {
            var res = new AuditEvent(
                entity,
                (int)eUserEventAction.UpdatePassword,
                null);

            // EventData
            res.Add(await _audit.PropertyValue(entity, p => p.PassDate));

            return res;
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
                    entity.Pass = EncryptorHelper.MD5Hash(pass) ?? String.Empty;
                    entity.IsActive = CharBoolean.True;
                    entity.PassDate = DateTime.Now.AddDays(90); //2D - change to configuration

                    //log audit event for password update
                    entity.Log(await Audit(entity));

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

                    //Thread.Sleep(TimeSpan.FromSeconds(1)); //testing animation
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
}

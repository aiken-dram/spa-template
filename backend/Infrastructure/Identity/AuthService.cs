using Microsoft.EntityFrameworkCore;
using Application.Common.Interfaces;
using Shared.Application.Exceptions;
using Microsoft.Extensions.Logging;
using Shared.Application.Extensions;

namespace Infrastructure.Identity;

public class AuthService : IAuthService
{
    private readonly ISPADbContext _context;
    private readonly ILogger<AuthService> _logger;
    private readonly Infrastructure.Common.Interfaces.IConfigurationService _configuration;

    public AuthService(
        ISPADbContext context,
        ILogger<AuthService> logger,
        Infrastructure.Common.Interfaces.IConfigurationService configuration)
    {
        _logger = logger;
        _context = context;
        _configuration = configuration;
    }

    private async Task<AuthUserVm> GetUserVm(User user)
    {
        var res = new AuthUserVm();
        res.UserID = user.IdUser;
        res.UserName = user.Name;

        res.UserDistricts = await _context.UserDistricts.Where(p => p.IdUser == user.IdUser)
                .Include(j => j.IdDistrictNavigation)
                .Select(q => q.IdDistrictNavigation.IdDistrict)
                .ToArrayAsync();

        res.UserGroups = await _context.UserGroups.Where(p => p.IdUser == user.IdUser)
            .Include(j => j.IdGroupNavigation)
            .Select(q => q.IdGroupNavigation.Description)
            .ToArrayAsync();

        var ugm = from u in _context.UserGroups.Where(p => p.IdUser == user.IdUser)
                  join g in _context.GroupRoles on u.IdGroup equals g.IdGroup
                  join r in _context.RoleModules on g.IdRole equals r.IdRole
                  select r.IdModuleNavigation.Name;

        var urm = from u in _context.UserRoles.Where(p => p.IdUser == user.IdUser)
                  join r in _context.RoleModules on u.IdRole equals r.IdRole
                  select r.IdModuleNavigation.Name;

        res.UserModules = await ugm.Union(urm).Distinct().ToArrayAsync();

        return res;
    }

    public async Task<AuthResponse> CurrentUserAsync(string userId)
    {
        await this.Validate(userId);

        long uid = Convert.ToInt64(userId);
        var user = await _context.Users.FirstOrDefaultAsync(p => p.IdUser == uid);

        var uservm = await GetUserVm(user!);
        var vm = new AuthResponse()
        {
            User = uservm
        };
        return vm;
    }

    public async Task<AuthResponse> LoginAsync(AuthRequest request, string? System)
    {
        //need to convert password to hash
        var hashpass = request.Password.MD5Hash() ?? string.Empty;

        var user = await _context.Users.FirstOrDefaultAsync(x => x.Login == request.Login);

        //user not found
        if (user == null)
            throw new BadRequestException(Messages.UserWithProvidedLoginNotFound);

        //not active
        if (user.IsActive != CharBoolean.True)
            throw new BadRequestException(Messages.UserLocked);

        //password expired
        if (user.PassDate <= DateTime.Now)
        {
            user.Log(AuthAudit.Expired(user, System));
            //add expired audit
            await _context.SaveChangesAsync(default(CancellationToken));
            throw new BadRequestException(Messages.PasswordExpired);
        }


        //timeout
        var period = DateTime.Now.AddMinutes(-_configuration.AuthPeriod);
        var prev_auth = await _context.UserAudits
            .Where(p => p.IdUser == user.IdUser)
            .Where(p => p.IdTarget == (int)eAuditTarget.Auth)
            .Where(p => p.Stamp >= period)
            .OrderByDescending(q => q.Stamp)
            .Skip(0).Take(_configuration.AuthLock)
            .ToListAsync();
        var cnt_wrong = prev_auth.TakeWhile(p => p.IdAction == (int)eAuthAuditAction.WrongPassword).Count();
        if (cnt_wrong >= _configuration.AuthTimeout)
        {
            var last_wrong = prev_auth.First().Stamp;
            var now = DateTime.Now;
            var timeout = _configuration.AuthBaseTimeout;
            var cur_timeout = (cnt_wrong - 2) * timeout;
            var d_timeout = last_wrong.AddMinutes(timeout);
            if (d_timeout >= now)
                throw new BadRequestException(Messages.UserLockTimeout(d_timeout - now));
        }

        //wrong password
        if (user.Pass != hashpass)
        {
            user.Log(AuthAudit.WrongPassword(user, System));
            await _context.SaveChangesAsync(default(CancellationToken));
            //check if password lock now

            if (cnt_wrong + 1 >= _configuration.AuthLock)
            {
                //lock user
                user.IsActive = CharBoolean.False;
                //add lock event
                user.Log(AuthAudit.Lock(user, System));
                await _context.SaveChangesAsync(default(CancellationToken));
                throw new BadRequestException(Messages.WrongPassLock(_configuration.AuthLock));
            }
            else if (cnt_wrong + 1 >= _configuration.AuthTimeout)
                throw new BadRequestException(Messages.WrongPassTimeout(_configuration.AuthTimeout));
            else
                throw new BadRequestException(Messages.WrongPass);
        }

        var uservm = await GetUserVm(user);

        var vm = new AuthResponse()
        {
            User = uservm,
        };

        //save login in UserAuth
        user.Log(AuthAudit.Login(user, System));
        await _context.SaveChangesAsync(default(CancellationToken));
        return vm;
    }

    public async Task Validate(string userId)
    {
        if (string.IsNullOrEmpty(userId))
            throw new BadRequestException(Messages.UserIdMustNotBeEmpty);

        long uid = Convert.ToInt64(userId);
        var user = await _context.Users.FirstOrDefaultAsync(p => p.IdUser == uid);

        // return null if user not found
        if (user == null)
            throw new BadRequestException(Messages.UserNotFound);

        if (user.IsActive == CharBoolean.False)
            throw new BadRequestException(Messages.UserNotActive);

        return;
    }
}

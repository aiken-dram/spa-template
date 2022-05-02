using Application.Common.Interfaces;
using Application.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public partial class UserService : IUserService
{
    public async Task<CurrentUser> GetCurrentUserAsync(CancellationToken cancellationToken)
    {
        long uid = Convert.ToInt64(_user.UserId);
        var user = await _context.Users.FirstOrDefaultAsync(p => p.IdUser == uid, cancellationToken);

        var res = new CurrentUser();

        res.IdUser = uid;

        res.Districts = await _context.UserDistricts
                .Where(p => p.IdUser == user!.IdUser)
                .Select(q => q.IdDistrict)
                .ToArrayAsync(cancellationToken);

        //user groups modules
        var ugm = from u in _context.UserGroups.Where(p => p.IdUser == user!.IdUser)
                  join g in _context.GroupRoles on u.IdGroup equals g.IdGroup
                  join r in _context.RoleModules on g.IdRole equals r.IdRole
                  select r.IdModuleNavigation.Name;

        //user roles modules
        var urm = from u in _context.UserRoles.Where(p => p.IdUser == user!.IdUser)
                  join r in _context.RoleModules on u.IdRole equals r.IdRole
                  select r.IdModuleNavigation.Name;

        //distinct of union
        res.Modules = await ugm.Union(urm).Distinct().ToArrayAsync(cancellationToken);

        return res;
    }
}

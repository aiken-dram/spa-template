using Application.Common.Interfaces;

namespace Application.Common.Services;

public partial class UserService : IUserService
{
    public long CurrentUserId
    {
        get
        {
            return Convert.ToInt64(_user.UserId);
        }
    }
}

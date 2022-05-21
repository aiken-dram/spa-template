namespace Application.Services;

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

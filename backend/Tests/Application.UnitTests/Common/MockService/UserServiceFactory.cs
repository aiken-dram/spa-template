using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Models;
using Moq;

namespace Application.UnitTests.Common;

public enum eMockUser
{
    Admin,
    User,
    Viewer,
    Invalid
}

public class UserServiceFactory
{
    private readonly static CurrentUser _admin = new CurrentUser
    {
        IdUser = 1,
        Modules = new string[] { "SECADM", "DICTADM", "CFGADM", "SUPERVISE" }
    };

    private readonly static CurrentUser _user = new CurrentUser
    {
        IdUser = 2,
        Modules = new string[] { }
    };

    private readonly static CurrentUser _viewer = new CurrentUser
    {
        IdUser = 3,
        Modules = new string[] { "READONLY" }
    };

    private readonly static CurrentUser _invalid = new CurrentUser
    {
        IdUser = -1,
        Modules = new string[] { }
    };

    public static Mock<IUserService> Create(eMockUser user = eMockUser.Admin)
    {
        var res = new Mock<IUserService>();
        Setup(ref res, user);
        return res;
    }

    /// <summary>
    /// Setup mocking of UserService for testing
    /// </summary>
    /// <param name="res">link to Mock of UserService</param>
    /// <param name="user">enumerable user to setup</param>
    public static void Setup(
        ref Mock<IUserService> res,
        eMockUser user)
    {
        CurrentUser _res = _admin;
        switch (user)
        {
            case eMockUser.Admin:
                break;
            case eMockUser.User:
                _res = _user;
                break;
            case eMockUser.Viewer:
                _res = _viewer;
                break;
            case eMockUser.Invalid:
                _res = _invalid;
                break;
        }

        res.Setup(m => m.CurrentUserId)
            .Returns(_res.IdUser);

        res.Setup(m => m.GetCurrentUserAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(_res));
    }
}

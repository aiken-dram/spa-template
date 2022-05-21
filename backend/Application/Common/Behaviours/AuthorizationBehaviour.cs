using System.Reflection;

namespace Application.Common.Behaviours;

public class AuthorizationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull, IRequest<TResponse>
{
    private readonly IUserService _user;

    public AuthorizationBehaviour(IUserService user)
    {
        _user = user;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        var authorizeAttributes = request.GetType().GetCustomAttributes<AuthorizeAttribute>();

        if (authorizeAttributes.Any())
        {
            // Module-based authorization
            var attrModules = authorizeAttributes.Where(a => !string.IsNullOrWhiteSpace(a.Modules));

            if (attrModules.Any())
            {
                var authorized = false;
                var userModules = await _user.GetCurrentUserAsync(cancellationToken);

                foreach (var modules in attrModules.Select(a => a.Modules.Split(',')))
                {
                    foreach (var module in modules)
                    {
                        if (userModules.Modules.Contains(module))
                        {
                            authorized = true;
                            break;
                        }
                    }
                }

                // Must be a member of at least one role in roles
                if (!authorized)
                {
                    throw new AccessDeniedException(string.Empty);
                }
            }
        }

        // User is authorized / authorization not required
        return await next();
    }
}

using AutoMapper;
using Application.Common.Mappings;

namespace Application.Account.User.Queries.GetUserDetail;

/// <summary>
/// View model for detailed user information
/// </summary>
public class UserDetailVm : IMapFrom<Domain.Entities.User>
{
    /// <summary>
    /// Id of user in database
    /// </summary>
    /// <example>1</example>
    public long IdUser { get; set; }

    /// <summary>
    /// User is active (T = yes, F = no)
    /// </summary>
    /// <example>T</example>
    public string IsActive { get; set; }

    /// <summary>
    /// Login
    /// </summary>
    /// <example>test17</example>
    public string Login { get; set; }

    /// <summary>
    /// User name
    /// </summary>
    /// <example>Test user 17</example>
    public string Name { get; set; }

    /// <summary>
    /// Expiration date of password
    /// </summary>
    /// <example>2010-01-01T00:00:00</example>
    public DateTime? PassDate { get; set; }

    /// <summary>
    /// User description
    /// </summary>
    /// <example>Test user #17</example>
    public string? Description { get; set; }

    /// <summary>
    /// Array of group id's
    /// </summary>
    public long[]? Groups { get; set; }

    /// <summary>
    /// Array of role id's
    /// </summary>
    public long[]? Roles { get; set; }


    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Entities.User, UserDetailVm>()
        .ForMember(p => p.Groups, o => o.MapFrom(q => q.UserGroups.Select(r => r.IdGroup)))
        .ForMember(p => p.Roles, o => o.MapFrom(q => q.UserRoles.Select(r => r.IdRole)));
    }
}

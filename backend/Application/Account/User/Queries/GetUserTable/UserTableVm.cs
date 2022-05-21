using AutoMapper;

namespace Application.Account.User.Queries.GetUserTable;

/// <summary>
/// User table data transfer object
/// </summary>
public class UserTableDto : IMapFrom<Domain.Entities.User>
{
    /// <summary>
    /// Id of user in database
    /// </summary>
    /// <example>1</example>
    public long idUser { get; set; }

    /// <summary>
    /// Is user active (T = yes, F = no)
    /// </summary>
    /// <example>T</example>
    public string isActive { get; set; } = null!;

    /// <summary>
    /// Login
    /// </summary>
    /// <example>test17</example>
    public string login { get; set; } = null!;

    /// <summary>
    /// User name
    /// </summary>
    /// <example>Test user 17</example>
    public string name { get; set; } = null!;

    /// <summary>
    /// User description
    /// </summary>
    /// <example>Test user #17</example>
    public string? description { get; set; }

    /// <summary>
    /// Password expiration date
    /// </summary>
    /// <example>2010-01-01T00:00:00</example>
    public DateTime? passDate { get; set; }

    /// <summary>
    /// Array of group names
    /// </summary>
    public string[]? groups { get; set; }

    /// <summary>
    /// Array of district numbers
    /// </summary>
    public int[]? districts { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Entities.User, UserTableDto>()
            .ForMember(p => p.groups, o => o.MapFrom(q => q.UserGroups.Select(r => r.IdGroupNavigation.Name)))
            .ForMember(p => p.districts, o => o.MapFrom(q => q.UserDistricts.Select(r => r.IdDistrict)));
    }
}

/// <summary>
/// View model for user table data
/// </summary>
public class UserTableVm : TableVm<UserTableDto>
{

}

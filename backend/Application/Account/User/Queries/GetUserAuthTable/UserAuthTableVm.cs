using Application.Common.Mappings;
using AutoMapper;
using Shared.Application.Models;
using Domain.Entities;

namespace Application.Account.User.Queries.GetUserAuthTable;

/// <summary>
/// Data transfer object for user authorization event
/// </summary>
public class UserAuthTableDto : IMapFrom<UserAuth>
{
    /// <summary>
    /// Id of authorization event in database
    /// </summary>
    public long idAuth { get; set; }

    /// <summary>
    /// Date and time of activity
    /// </summary>
    /// <example>2010-01-01T00:00:00</example>
    public DateTime stamp { get; set; }

    /// <summary>
    /// Source of activity:
    /// AUTH
    /// </summary>
    /// <example>AUTH</example>
    public string source { get; set; } = null!;

    /// <summary>
    /// Id of auth action in dictionary
    /// </summary>
    /// <example>1</example>
    public int idAction { get; set; }

    /// <summary>
    /// Description of action in dictionary
    /// </summary>
    /// <example>Wrong password</example>
    public string? action { get; set; }

    /// <summary>
    /// Subject of activity:
    /// Name of system for AUTH
    /// </summary>
    /// <example>192.168.0.1</example>
    public string subject { get; set; } = null!;

    /// <summary>
    /// Message
    /// </summary>
    /// <example></example>
    public string? message { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UserAuth, UserAuthTableDto>()
            .ForMember(p => p.subject, o => o.MapFrom(m => m.System))
            .ForMember(p => p.message, o => o.MapFrom(m => m.Message))
            .ForMember(p => p.action, o => o.MapFrom(m => m.IdActionNavigation.Description))
            .ForMember(p => p.source, o => o.MapFrom(m => "AUTH"));
    }
}

/// <summary>
/// View model for table of user authorization events
/// </summary>
public class UserAuthTableVm : TableVm<UserAuthTableDto>
{

}

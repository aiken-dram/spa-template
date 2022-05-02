using Application.Common.Mappings;
using AutoMapper;
using Shared.Application.Models;
using Domain.Entities;

namespace Application.Account.User.Queries.GetAuditTable;

public class AuditDataTableDto : IMapFrom<UserEventData>
{
    /// <summary>
    /// Id of event data
    /// </summary>
    public long id { get; set; }

    /// <summary>
    /// Id of data type in dictionary
    /// </summary>
    public int idType { get; set; }

    /// <summary>
    /// Name of data type from dictionary
    /// </summary>
    public string type { get; set; } = null!;

    /// <summary>
    /// Json with data
    /// </summary>
    public string? json { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UserEventData, AuditDataTableDto>()
            .ForMember(p => p.type, o => o.MapFrom(m => m.IdTypeNavigation.Type));
    }
}

/// <summary>
/// Data transfer object for user authorization event
/// </summary>
public class AuditTableDto : IMapFrom<UserEvent>
{
    /// <summary>
    /// Id of user activity event
    /// </summary>
    public long idEvent { get; set; }

    /// <summary>
    /// Id of user
    /// </summary>
    public long idUser { get; set; }

    /// <summary>
    /// User login
    /// </summary>
    public string login { get; set; } = null!;

    /// <summary>
    /// Id of target in dictionary
    /// </summary>
    public int idTarget { get; set; }

    /// <summary>
    /// Name of target from dictionary
    /// </summary>
    public string target { get; set; } = null!;

    /// <summary>
    /// Description of target from dictionary
    /// </summary>
    public string? targetDesc { get; set; }

    /// <summary>
    /// Id of action
    /// </summary>
    public int idAction { get; set; }

    /// <summary>
    /// Description of action
    /// </summary>
    public string? action { get; set; }

    /// <summary>
    /// Date and time of user activity event
    /// </summary>
    public DateTime stamp { get; set; }

    /// <summary>
    /// Id of target entity
    /// </summary>
    public long? targetId { get; set; }

    /// <summary>
    /// Name of target entity
    /// </summary>
    public string? targetName { get; set; }

    /// <summary>
    /// Event message
    /// </summary>
    public string? message { get; set; }

    /// <summary>
    /// Collection of event data
    /// </summary>
    public ICollection<AuditDataTableDto>? eventData { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UserEvent, AuditTableDto>()
            .ForMember(p => p.login, o => o.MapFrom(m => m.IdUserNavigation.Login))
            .ForMember(p => p.target, o => o.MapFrom(m => m.IdTargetNavigation.Target))
            .ForMember(p => p.targetDesc, o => o.MapFrom(m => m.IdTargetNavigation.Description))
            .ForMember(p => p.action, o => o.MapFrom(m => m.IdActionNavigation.Description))
            .ForMember(p => p.eventData, o => o.MapFrom(m => m.UserEventData));
    }
}

/// <summary>
/// View model for table of user authorization events
/// </summary>
public class AuditTableVm : TableVm<AuditTableDto>
{

}
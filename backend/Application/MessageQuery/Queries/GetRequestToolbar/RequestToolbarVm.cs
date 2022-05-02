using Application.Common.Mappings;
using AutoMapper;
using Shared.Application.Models;

namespace Application.MessageQuery.Queries.GetRequestToolbar;

public class RequestToolbarDto : IMapFrom<Domain.Entities.Request>
{
    /// <summary>
    /// Id of request in database
    /// </summary>
    public long idRequest { get; set; }

    /// <summary>
    /// Id of request type
    /// </summary>
    public int idType { get; set; }

    /// <summary>
    /// Description of type
    /// </summary>
    public string? typeDesc { get; set; }

    /// <summary>
    /// Id of request state
    /// </summary>
    public int idState { get; set; }

    /// <summary>
    /// Name of state
    /// </summary>
    public string state { get; set; } = null!;

    /// <summary>
    /// Timestamp of creating request
    /// </summary>
    public DateTime Created { get; set; }

    /// <summary>
    /// Timestamp of processing request
    /// </summary>
    public DateTime? Processed { get; set; }

    /// <summary>
    /// Message
    /// </summary>
    public string? Message { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Entities.Request, RequestToolbarDto>()
            .ForMember(p => p.typeDesc, o => o.MapFrom(m => m.IdTypeNavigation.Description))
            .ForMember(p => p.state, o => o.MapFrom(m => m.IdStateNavigation.State));
    }
}

public class RequestToolbarVm : ListVm<RequestToolbarDto>
{
    public int? Count { get; set; }
}

using AutoMapper;

namespace Application.R.RScript.Queries.GetRScriptForm;

public class RScriptFormFieldDto : IMapFrom<Domain.Entities.RScriptParam>
{
    /// <summary>
    /// Id of form field
    /// </summary>
    public long id { get; set; }

    /// <summary>
    /// Field type
    /// </summary>
    public string type { get; set; } = null!;

    /// <summary>
    /// Name of field
    /// </summary>
    public string name { get; set; } = null!;

    /// <summary>
    /// Hint for field
    /// </summary>
    public string? hint { get; set; }

    /// <summary>
    /// Description of field
    /// </summary>
    public string? description { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<RScriptParam, RScriptFormFieldDto>()
            .ForMember(p => p.type, o => o.MapFrom(m => m.IdTypeNavigation.Type));
    }
}

public class RScriptFormVm : IMapFrom<Domain.Entities.RScript>
{
    /// <summary>
    /// Id of R script in database
    /// </summary>
    public long idRScript { get; set; }

    /// <summary>
    /// Name of R script
    /// </summary>
    public string name { get; set; } = null!;

    /// <summary>
    /// Content type of result file
    /// </summary>
    public string contentType { get; set; } = null!;

    /// <summary>
    /// Description of R script
    /// </summary>
    public string? description { get; set; }

    /// <summary>
    /// List of form fields
    /// </summary>
    public IEnumerable<RScriptFormFieldDto> formFields { get; set; } = null!;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Entities.RScript, RScriptFormVm>()
            .ForMember(p => p.formFields, o => o.MapFrom(m => m.RScriptParams));
    }
}

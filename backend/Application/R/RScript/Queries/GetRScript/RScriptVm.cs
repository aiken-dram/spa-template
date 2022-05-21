using AutoMapper;

namespace Application.R.RScript.Queries.GetRScript;

public class RScriptParamDto : IMapFrom<Domain.Entities.RScriptParam>
{
    /// <summary>
    /// Id of parameter in database
    /// </summary>
    public long id { get; set; }

    /// <summary>
    /// Id of parameter type
    /// </summary>
    public int idType { get; set; }

    /// <summary>
    /// Name of parameter
    /// </summary>
    public string name { get; set; } = null!;

    /// <summary>
    /// Hint displayed for parameter in form
    /// </summary>
    public string? hint { get; set; }

    /// <summary>
    /// Description of parameter
    /// </summary>
    public string? description { get; set; }
}

public class RScriptVm : IMapFrom<Domain.Entities.RScript>
{
    /// <summary>
    /// Id of R script in database
    /// </summary>
    public long idRScript { get; set; }

    /// <summary>
    /// Name of file with R script
    /// </summary>
    public string scriptFile { get; set; } = null!;

    /// <summary>
    /// Name of R script
    /// </summary>
    public string name { get; set; } = null!;

    /// <summary>
    /// Content type of result file
    /// </summary>
    public string contentType { get; set; } = null!;

    /// <summary>
    /// Result file name (timestamp will be added at the end)
    /// </summary>
    public string resultFile { get; set; } = null!;

    /// <summary>
    /// Description of R script
    /// </summary>
    public string? description { get; set; }

    /// <summary>
    /// List of R script parameters
    /// </summary>
    public IEnumerable<RScriptParam> scriptParams { get; set; } = null!;

    /// <summary>
    /// Content of R script file
    /// </summary>
    public IEnumerable<string> scriptContent { get; set; } = null!;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Entities.RScript, RScriptVm>()
            .ForMember(p => p.scriptParams, o => o.MapFrom(m => m.RScriptParams))
            .ForMember(p => p.scriptContent, o => o.Ignore());
    }
}

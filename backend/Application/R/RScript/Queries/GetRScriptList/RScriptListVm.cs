namespace Application.R.RScript.Queries.GetRScriptList;

public class RScriptListDto : IMapFrom<Domain.Entities.RScript>
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
}

public class RScriptListVm : ListVm<RScriptListDto>
{

}

namespace Application.Dictionary.SampleDict.Queries.GetSampleDictList;

#warning SAMPLE, remove entire file in actual application
public class SampleDictListDto : IMapFrom<Domain.Entities.SampleDict>
{
    /// <summary>
    /// Id of dictionary in database
    /// </summary>
    public long idDict { get; set; }

    /// <summary>
    /// Name of dictionary
    /// </summary>
    public string dict { get; set; } = null!;

    /// <summary>
    /// Description of dictionary
    /// </summary>
    public string? description { get; set; }
}

public class SampleDictListVm : ListVm<SampleDictListDto>
{

}

namespace Application.Sample.Queries.GetSampleTable;

#warning SAMPLE, remove entire file in actual application
public class SampleTableDto : IMapFrom<Domain.Entities.Sample>
{
    /// <summary>
    /// Id of sample in database
    /// </summary>
    public long idSample { get; set; }

    /// <summary>
    /// Id of district
    /// </summary>
    public int? idDistrict { get; set; }

    /// <summary>
    /// Id of sample type
    /// </summary>
    public int idType { get; set; }

    /// <summary>
    /// Id of sample dictionary
    /// </summary>
    public long idDict { get; set; }

    /// <summary>
    /// Text field
    /// </summary>
    public string? text { get; set; }

    /// <summary>
    /// Number field
    /// </summary>
    public long? number { get; set; }

    /// <summary>
    /// Date field
    /// </summary>
    public DateTime? date { get; set; }
}

public class SampleTableVm : TableVm<SampleTableDto>
{

}

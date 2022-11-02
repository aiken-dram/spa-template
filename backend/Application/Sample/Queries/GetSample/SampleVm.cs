namespace Application.Sample.Queries.GetSample;

#warning SAMPLE, remove entire file in actual application
public class SampleChildDto : IMapFrom<Domain.Entities.SampleChild>
{
    /// <summary>
    /// Id of sample child in database
    /// </summary>
    public long idChild { get; set; }

    /// <summary>
    /// Id of sample
    /// </summary>
    public long idSample { get; set; }

    /// <summary>
    /// Text field
    /// </summary>
    public string? text { get; set; }
}

public class SampleVm : IMapFrom<Domain.Entities.Sample>
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

    /// <summary>
    /// Time stamp field
    /// </summary>
    public DateTime? timeStamp { get; set; }

    /// <summary>
    /// Decimal field
    /// </summary>
    public decimal? sum { get; set; }

    /// <summary>
    /// List of sample children
    /// </summary>
    public IEnumerable<SampleChildDto> sampleChildren { get; set; } = null!;
}

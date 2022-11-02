namespace Application.Dictionary.District.Queries.GetDistrict;

public class DistrictVm : IMapFrom<Domain.Entities.District>
{
    /// <summary>
    /// District number
    /// </summary>
    /// <example>1</example>
    public int idDistrict { get; set; }

    /// <summary>
    /// District name
    /// </summary>
    /// <example>District name</example>
    public string name { get; set; } = null!;
}

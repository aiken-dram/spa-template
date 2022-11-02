namespace Application.Dictionary.District.Queries.GetDistrictList;

public class DistrictListDto : IMapFrom<Domain.Entities.District>
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

public class DistrictListVm : ListVm<DistrictListDto>
{

}

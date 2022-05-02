using Application.Common.Mappings;
using Shared.Application.Models;

namespace Application.Dictionary.District.Queries.GetDistrictList;

public class DistrictListDto : IMapFrom<Domain.Entities.District>
{
    /// <summary>
    /// District number
    /// </summary>
    /// <example>1</example>
    public int IdDistrict { get; set; }

    /// <summary>
    /// District name
    /// </summary>
    /// <example>District name</example>
    public string Name { get; set; } = null!;
}

public class DistrictListVm : ListVm<DistrictListDto>
{

}

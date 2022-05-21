using Application.Common.Mappings;
using Shared.Application.Models;

namespace Application.Sample.Queries.GetSampleTable;

#warning This is example, remove entire file in actual application
public class SampleTableDto : IMapFrom<Domain.Entities.Sample>
{

}

public class SampleTableVm : TableVm<SampleTableDto>
{

}

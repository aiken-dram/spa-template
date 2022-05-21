namespace Application.Sample.Queries.GetSampleAuditTable;

public class SampleAuditTableDto : IMapFrom<SampleAudit>
{

}

public class SampleAuditTableVm : TableVm<SampleAuditTableDto>
{

}

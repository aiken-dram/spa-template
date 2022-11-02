using AutoMapper;

namespace Application.Sample.Queries.GetSampleAuditTable;

#warning SAMPLE, remove entire file in actual application
/// <summary>
/// Data transfer object for sample audit data
/// </summary>
public class SampleAuditDataTableDto : AuditDataTable, IMapFrom<SampleAuditData>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SampleAuditData, SampleAuditDataTableDto>()
            .ForMember(p => p.type, o => o.MapFrom(m => m.IdTypeNavigation.Type));
    }
}

/// <summary>
/// Data transfer object for sample audit
/// </summary>
public class SampleAuditTableDto : AuditTable, IMapFrom<SampleAudit>
{
    /// <summary>
    /// Collection of audit data
    /// </summary>
    public new ICollection<SampleAuditDataTableDto>? auditData { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<SampleAudit, SampleAuditTableDto>()
            .ForMember(p => p.login, o => o.MapFrom(m => m.IdUserNavigation.Login))
            .ForMember(p => p.target, o => o.MapFrom(m => m.IdTargetNavigation.Target))
            .ForMember(p => p.targetDesc, o => o.MapFrom(m => m.IdTargetNavigation.Description))
            .ForMember(p => p.action, o => o.MapFrom(m => m.IdActionNavigation.Description))
            .ForMember(p => p.auditData, o => o.MapFrom(m => m.SampleAuditData));
    }
}

public class SampleAuditTableVm : TableVm<SampleAuditTableDto>
{

}

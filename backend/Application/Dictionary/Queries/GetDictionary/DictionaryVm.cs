using AutoMapper;

namespace Application.Dictionary.Queries.GetDictionary;

public class DictionaryDto :
    IMapFrom<Group>,
    IMapFrom<Role>,
    IMapFrom<AuditAction>,
    IMapFrom<AuditTarget>,
    IMapFrom<Domain.Entities.District>,

#warning This is example, remove next 2 lines in actual application
    IMapFrom<SampleType>,
    IMapFrom<Domain.Entities.SampleDict>
{
    /// <summary>
    /// Dictionary value
    /// </summary>
    /// <example>1</example>
    public long Value { get; set; }

    /// <summary>
    /// Dictionary text
    /// </summary>
    /// <example>Description</example>
    public string? Text { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Group, DictionaryDto>()
            .ForMember(p => p.Value, o => o.MapFrom(q => q.IdGroup))
            .ForMember(p => p.Text, o => o.MapFrom(q => q.Name));

        profile.CreateMap<Role, DictionaryDto>()
            .ForMember(p => p.Value, o => o.MapFrom(q => q.IdRole))
            .ForMember(p => p.Text, o => o.MapFrom(q => q.Name));

        profile.CreateMap<AuditAction, DictionaryDto>()
            .ForMember(p => p.Value, o => o.MapFrom(q => Convert.ToInt64(q.IdAction)))
            .ForMember(p => p.Text, o => o.MapFrom(q => q.Description));

        profile.CreateMap<AuditTarget, DictionaryDto>()
            .ForMember(p => p.Value, o => o.MapFrom(q => Convert.ToInt64(q.IdTarget)))
            .ForMember(p => p.Text, o => o.MapFrom(q => q.Description));

        profile.CreateMap<Domain.Entities.District, DictionaryDto>()
            .ForMember(p => p.Value, o => o.MapFrom(q => Convert.ToInt64(q.IdDistrict)))
            .ForMember(p => p.Text, o => o.MapFrom(q => q.IdDistrict.ToString() + " " + q.Name));

#warning This is example, remove in actual application
        profile.CreateMap<SampleType, DictionaryDto>()
            .ForMember(p => p.Value, o => o.MapFrom(q => Convert.ToInt64(q.IdType)))
            .ForMember(p => p.Text, o => o.MapFrom(q => q.Description));

#warning This is example, remove in actual application
        profile.CreateMap<Domain.Entities.SampleDict, DictionaryDto>()
            .ForMember(p => p.Value, o => o.MapFrom(q => Convert.ToInt64(q.IdDict)))
            .ForMember(p => p.Text, o => o.MapFrom(q => q.Description));
    }
}

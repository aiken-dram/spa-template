using AutoMapper;
using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Dictionary.Queries.GetDictionary;

public class DictionaryDto :
    IMapFrom<Group>,
    IMapFrom<Role>,
    IMapFrom<AuthAction>
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

        profile.CreateMap<AuthAction, DictionaryDto>()
            .ForMember(p => p.Value, o => o.MapFrom(q => Convert.ToInt64(q.IdAction)))
            .ForMember(p => p.Text, o => o.MapFrom(q => q.Description));
    }
}

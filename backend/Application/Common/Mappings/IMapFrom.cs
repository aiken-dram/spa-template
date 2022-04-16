using AutoMapper;

namespace Application.Common.Mappings;

// Source: Clean Architecture example (https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
/// <summary>
/// Interface for mapping DTO from domain entities
/// </summary>
public interface IMapFrom<T>
{
    void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
}

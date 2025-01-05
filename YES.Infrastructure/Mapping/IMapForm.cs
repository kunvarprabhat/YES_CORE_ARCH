using AutoMapper;

namespace YES.Infrastructure.Mapping
{
    public interface IMapForm<T>
    {
        void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
    }
}

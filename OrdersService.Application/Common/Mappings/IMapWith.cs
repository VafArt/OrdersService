using AutoMapper;

namespace OrdersService.Application.Common.Mappings
{
    public interface IMapWith<T>
    {
        void Mapping(Profile profile) =>
            profile.CreateMap(GetType(), typeof(T)).ReverseMap();
    }
}

using AutoMapper;
using BurgerRaterApi.Models;
using BurgerRaterApi.Models.Dto.Restaurant;

namespace BurgerRaterApi.Mappers
{
    public class RestaurantAutoMapperProfile : Profile
    {
        public RestaurantAutoMapperProfile()
        {
            CreateMap<RestaurantCreateDto, Restaurant>();

            CreateMap<RestaurantUpdateDto, Restaurant>();

            CreateMap<Restaurant, RestaurantResponseDto>();
        }
    }
}

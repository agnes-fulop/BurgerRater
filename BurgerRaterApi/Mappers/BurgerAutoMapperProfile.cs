using AutoMapper;
using BurgerRaterApi.Models;
using BurgerRaterApi.Models.Dto.Burger;


namespace BurgerRaterApi.Mappers
{
    public class BurgerAutoMapperProfile : Profile
    {
        public BurgerAutoMapperProfile()
        {
            CreateMap<BurgerCreateDto, Burger>();

            CreateMap<Burger, BurgerResponseDto>();
        }
    }
}

using AutoMapper;
using BurgerRaterApi.Models;
using BurgerRaterApi.Models.Dto.Menu;

namespace BurgerRaterApi.Mappers
{
    public class MenuAutoMapperProfile : Profile
    {
        public MenuAutoMapperProfile()
        {
            CreateMap<MenuCreateDto, Menu>();

            CreateMap<Menu, MenuResponseDto>();
        }
    }
}

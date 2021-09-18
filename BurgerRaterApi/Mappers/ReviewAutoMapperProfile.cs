using AutoMapper;
using BurgerRaterApi.Models;
using BurgerRaterApi.Models.Dto.Review;

namespace BurgerRaterApi.Mappers
{
    public class ReviewAutoMapperProfile : Profile
    {
        public ReviewAutoMapperProfile()
        {
            CreateMap<ReviewCreateDto, Review>();

            CreateMap<Review, ReviewResponseDto>()
                .ForMember(dest => dest.Image, opt => opt.Ignore());
        }
    }
}

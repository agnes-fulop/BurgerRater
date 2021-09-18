using BurgerRaterApi.Models.Dto.Restaurant;
using FluentValidation;

namespace BurgerRaterApi.Validators.Restaurant
{
    public class RestaurantCreateDtoValidator : AbstractValidator<RestaurantCreateDto>
    {
        private readonly string TimestampFormat = "^([0-1]?\\d|2[0-3])(?::([0-5]?\\d))?(?::([0-5]?\\d))?$";

        public RestaurantCreateDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Address)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.District)
                .NotEmpty()
                .MaximumLength(15);

            RuleFor(x => x.ZipCode)
                .NotEmpty()
                .MaximumLength(15);

            RuleFor(x => x.OpeningTime)
                .NotEmpty()
                .Matches(TimestampFormat);

            RuleFor(x => x.ClosingTime)
                .NotEmpty()
                .Matches(TimestampFormat);
        }
    }
}

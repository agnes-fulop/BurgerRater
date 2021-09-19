using BurgerRaterApi.Models.Dto.Burger;
using FluentValidation;

namespace BurgerRaterApi.Validators.Burger
{
    public class BurgerCreateDtoValidator : AbstractValidator<BurgerCreateDto>
    {
        public BurgerCreateDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(500);

            RuleFor(x => x.Price)
                .NotNull()
                .GreaterThan(0);

            RuleFor(x => x.Currency)
                .NotNull()
                .MaximumLength(10);
        }
    }
}

using BurgerRaterApi.Models.Dto.Review;
using FluentValidation;

namespace BurgerRaterApi.Validators.Review
{
    public class ReviewCreateDtoValidator : AbstractValidator<ReviewCreateDto>
    {
        public ReviewCreateDtoValidator()
        {
            RuleFor(x => x.TasteScore)
                .NotNull()
                .InclusiveBetween(1, 10);

            RuleFor(x => x.TextureScore)
                .NotNull()
                .InclusiveBetween(1, 10);

            RuleFor(x => x.VisualPresentationScore)
                .NotNull()
                .InclusiveBetween(1, 10);
        }
    }
}

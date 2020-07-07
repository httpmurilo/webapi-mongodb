using FluentValidation;
using Source.Domain.Model;

namespace Source.Api.Validators
{
    public class AuthorInputModelValidator : AbstractValidator<Author>
    {
        public AuthorInputModelValidator()
        {
            RuleFor(x => x.Name)
                .MinimumLength(5)
                .WithMessage("the author's name must be at least 5 characters");
        }
    }
}
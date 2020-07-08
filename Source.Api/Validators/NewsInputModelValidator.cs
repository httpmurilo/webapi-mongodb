using FluentValidation;
using Source.Domain.Model;

namespace Source.Api.Validators
{
    public class NewsInputModelValidator : AbstractValidator<News>
    {
        public NewsInputModelValidator()
        {
            RuleFor(x => x.Title)
                .MinimumLength(10)
                .WithMessage("The news title must be 10 characters long");

            RuleFor(x => x.Body)
                .MinimumLength(100)
                .WithMessage("The title must contain at least 100 characters");

            RuleFor(x => x.AuthorId)
                .NotNull()
                .WithMessage("the id of the author consulted in GET /api/author HTTP/ 1.1 must be informed");
        }
    }
}
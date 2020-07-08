using FluentValidation;
using Source.Domain.Model;

namespace Source.Api.Validators
{
    public class NewsInputModelValidator : AbstractValidator<News>
    {
        public NewsInputModelValidator()
        {
            
        }
    }
}
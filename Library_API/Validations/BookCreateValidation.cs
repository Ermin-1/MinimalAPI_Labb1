using FluentValidation;
using Library_API.Models.DTOs;

namespace Library_API.Models.Validations
{
    public class BookCreateValidation : AbstractValidator<CreateBookDTO>
    {
        public BookCreateValidation()
        {
            RuleFor( model => model.Title ).NotEmpty().MaximumLength(30);
            RuleFor( model => model.Author ).NotEmpty().MaximumLength(30);
            RuleFor(model => model.IsAvalible).NotNull();
            RuleFor( model => model.Description ).NotEmpty().MaximumLength(80);
            RuleFor( model => model.Genre ).NotEmpty().MaximumLength(10);
        }
    }
}

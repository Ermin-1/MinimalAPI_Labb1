using FluentValidation;
using Library_API.Data;
using Library_API.Models.DTOs;

namespace Library_API.Models.Validations
{
    public class BookUpdateValidation : AbstractValidator<UpdateBookDTO>
    {

        public BookUpdateValidation(AppDbContext context)
        {
            RuleFor(model => model.Title).NotEmpty().MaximumLength(20);
            RuleFor(model => model.Author).NotEmpty().MaximumLength(20);
            RuleFor(model => model.Genre).NotEmpty().MaximumLength(10);
            RuleFor(model => model.IsAvalible).NotNull();
            RuleFor(model => model.Description).NotEmpty().MaximumLength(80);
        }

  
    }

}

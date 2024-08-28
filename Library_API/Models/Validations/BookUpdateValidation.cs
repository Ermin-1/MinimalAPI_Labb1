using FluentValidation;
using Library_API.Data;
using Library_API.Models.DTOs;

namespace Library_API.Models.Validations
{
    public class BookUpdateValidation : AbstractValidator<UpdateBookDTO>
    {

        //För att komma åt databasens uppgfiter. Behhövs ej vid create. 
        private AppDbContext _dbContext;
        public BookUpdateValidation(AppDbContext context)
        {
            this._dbContext = context;
            RuleFor(models => models.Id).Must(UniqeId).WithMessage("Id already exist. Please choose a different one.");
            RuleFor(model => model.Title).NotEmpty().MaximumLength(20);
            RuleFor(model => model.Author).NotEmpty().MaximumLength(20);
            RuleFor(model => model.Genre).NotEmpty().MaximumLength(10);
            RuleFor(model => model.IsAvalible).NotNull();
            RuleFor(model => model.Description).NotEmpty().MaximumLength(80);
        }

        private bool UniqeId(int id)
        {
            return !_dbContext.book.Any(b => b.Id == id);

        }
    }

}

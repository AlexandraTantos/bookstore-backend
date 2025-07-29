using BookStore.Domain;
using FluentValidation;

namespace BookStore.Application.CreateBook
{
  public class CreateBookValidator : AbstractValidator<CreateBookRequest>
  {
    public CreateBookValidator()
    {
      this.RuleFor(request => request.BookDto).NotEmpty();
      this.RuleFor(request => request.BookDto.Title).MinimumLength(4);
      this.RuleFor(request => request.BookDto.Genres).NotEmpty();
      this.RuleFor(request => request.BookDto.AuthorId.IsValidObjectId()).Equal(true).WithMessage("Invalid Object Id");
    }
  }
}

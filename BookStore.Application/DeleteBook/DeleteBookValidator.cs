using FluentValidation;
using MongoDB.Bson;

namespace BookStore.Application.DeleteBook;

public class DeleteBookValidator : AbstractValidator<DeleteBookRequest>
{
    public DeleteBookValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Book Id is required.");

        RuleFor(x => x.Id)
            .Must(id => ObjectId.TryParse(id, out _))
            .WithMessage("Invalid Object Id");
    }
}


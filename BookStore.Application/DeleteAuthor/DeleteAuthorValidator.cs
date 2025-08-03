using FluentValidation;
using MongoDB.Bson;

namespace BookStore.Application.DeleteAuthor;

public class DeleteAuthorValidator : AbstractValidator<DeleteAuthorRequest>
{
    public DeleteAuthorValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Book Id is required.");

        RuleFor(x => x.Id)
            .Must(id => ObjectId.TryParse(id, out _))
            .WithMessage("Invalid Object Id");
    }
}
using BookStore.Domain;
using FluentValidation;

namespace BookStore.Application.GetAuthorById;

public class GetAuthorByIdValidator : AbstractValidator<GetAuthorByIdRequest>
{
    public GetAuthorByIdValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Author Id is required.")
            .Must(id => id.IsValidObjectId()).WithMessage("Invalid Author Id.");
    }
}
using FluentValidation;
using MongoDB.Bson;

namespace BookStore.Application.DeletePublisher;

public class DeletePublisherValidator : AbstractValidator<DeletePublisherRequest>
{
    public DeletePublisherValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Book Id is required.");

        RuleFor(x => x.Id)
            .Must(id => ObjectId.TryParse(id, out _))
            .WithMessage("Invalid Object Id");
    }
}
using FluentValidation;
using MongoDB.Bson;

namespace BookStore.Application.GetPublisherById;

public class GetPublisherByIdValidator :AbstractValidator<GetPublisherByIdRequest>
{
    public GetPublisherByIdValidator()
    {
        
    RuleFor(x => x.Id)
        .NotEmpty().WithMessage("Publisher Id is required.");

    RuleFor(x => x.Id)
        .Must(id => ObjectId.TryParse(id, out _))
    .WithMessage("Invalid Object Id");
    }
}
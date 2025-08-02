using BookStore.Domain;
using FluentValidation;

namespace BookStore.Application.UpdatePublisher;

public class UpdatePublisherValidator : AbstractValidator<UpdatePublisherRequest>
{
    public UpdatePublisherValidator()
    {
        RuleFor(request => request.Id).NotEmpty().WithMessage("Id is required.").Must(id=>id.IsValidObjectId().Equals(true))
            .WithMessage("Invalid Object Id");
        
        RuleFor(request => request.Name)
            .MaximumLength(25).WithMessage("Nme cannot be longer than 25 characters.")
            .When(request => !string.IsNullOrWhiteSpace(request.Name));
        RuleFor(request => request.Address)
            .MaximumLength(25).WithMessage("Title cannot be longer than 25 characters.")
            .When(request => !string.IsNullOrWhiteSpace(request.Address));
        RuleFor(request => request.Email)
            .MaximumLength(25).WithMessage("Email cannot be longer than 25 characters.")
            .When(request => !string.IsNullOrWhiteSpace(request.Email))
            .EmailAddress().WithMessage("Invalid Email format.");
    }
}
using BookStore.Domain;
using FluentValidation;

namespace BookStore.Application.UpdatePublisher;

public class UpdatePublisherValidator : AbstractValidator<UpdatePublisherRequest>
{
    public UpdatePublisherValidator()
    {
        
        RuleFor(request => request.Name)
            .MaximumLength(25).WithMessage("Name cannot be longer than 25 characters.")
            .When(request => !string.IsNullOrWhiteSpace(request.Name));

        RuleFor(request => request.Address)
            .MaximumLength(25).WithMessage("Address cannot be longer than 25 characters.")
            .When(request => !string.IsNullOrWhiteSpace(request.Address));

        RuleFor(request => request.Email)
            .MaximumLength(25).WithMessage("Email cannot be longer than 25 characters.")
            .EmailAddress().WithMessage("Invalid Email format.")
            .When(request => !string.IsNullOrWhiteSpace(request.Email));

    }
}
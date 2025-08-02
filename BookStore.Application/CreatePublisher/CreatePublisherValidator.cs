using BookStore.Domain;
using FluentValidation;

namespace BookStore.Application.CreatePublisher;

public class CreatePublisherValidator : AbstractValidator<CreatePublisherRequest>
{
    public CreatePublisherValidator()
    {
        this.RuleFor(request => request.PublisherDto).NotEmpty();
        this.RuleFor(request => request.PublisherDto.Email).MinimumLength(4);
        this.RuleFor(request => request.PublisherDto.Name).MinimumLength(4);
        this.RuleFor(request => request.PublisherDto.Address).NotEmpty();
    }
}
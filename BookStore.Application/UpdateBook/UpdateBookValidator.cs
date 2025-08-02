using BookStore.Domain;
using FluentValidation;

namespace BookStore.Application.UpdateBook;

public class UpdateBookValidator:AbstractValidator<UpdateBookRequest>
{
    public UpdateBookValidator()
    {
        RuleFor(request => request.AuthorId)
            .Must(id => id.IsValidObjectId())
            .WithMessage("Invalid Author Id.")
            .When(request => !string.IsNullOrWhiteSpace(request.AuthorId));

        RuleFor(request => request.PublisherId)
            .Must(id => id.IsValidObjectId())
            .WithMessage("Invalid Publisher Id.")
            .When(request => !string.IsNullOrWhiteSpace(request.PublisherId));
        
        RuleFor(request => request.Title)
            .MaximumLength(25).WithMessage("Title cannot be longer than 25 characters.")
            .When(request => !string.IsNullOrWhiteSpace(request.Title));

        RuleFor(request => request.YearOfPublication)
            .LessThanOrEqualTo(DateTime.Now)
            .When(request => request.YearOfPublication.HasValue)
            .WithMessage("Year of publication cannot be in the future.");

        RuleFor(request => request.Genres)
            .Must(genres => genres == null || genres.All(g => !string.IsNullOrWhiteSpace(g)))
            .WithMessage("Genres cannot contain empty or null values.");
    }
}
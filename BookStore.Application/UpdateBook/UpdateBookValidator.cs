using BookStore.Domain;
using FluentValidation;

namespace BookStore.Application.UpdateBook;

public class UpdateBookValidator:AbstractValidator<UpdateBookRequest>
{
    public UpdateBookValidator()
    {
        RuleFor(request => request.Id).NotEmpty().WithMessage("Id is required.").Must(id=>id.IsValidObjectId().Equals(true))
            .WithMessage("Invalid Object Id");
        
        RuleFor(request => request.AuthorId).NotEmpty().WithMessage("Id is required.").Must(id=>id.IsValidObjectId().Equals(true))
            .WithMessage("Invalid Object Id");
        
        RuleFor(request => request.PublisherId).NotEmpty().WithMessage("Id is required.").Must(id=>id.IsValidObjectId().Equals(true))
            .WithMessage("Invalid Object Id");
        
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
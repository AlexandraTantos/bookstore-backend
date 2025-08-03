using FluentValidation;

namespace BookStore.Application.CreateAuthor;

public class CreateAuthorValidator : AbstractValidator<CreateAuthorRequest>
{
    public CreateAuthorValidator()
    {
        this.RuleFor(request => request.AuthorDto).NotEmpty();
        this.RuleFor(request => request.AuthorDto.FirstName).MinimumLength(4);
        this.RuleFor(request => request.AuthorDto.LastName).MinimumLength(4);
        this.RuleFor(request => request.AuthorDto.Nationality).MinimumLength(4);
        this.RuleFor(request => request.AuthorDto.SpokenLanguages).NotEmpty();
    }
}
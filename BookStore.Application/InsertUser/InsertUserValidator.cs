using FluentValidation;

namespace BookStore.Application.InsertUser;

public class InsertUserValidator : AbstractValidator<InsertUserRequest>
{
    public InsertUserValidator()
    {
        RuleFor(x => x.UserDto).NotNull();
        RuleFor(x => x.UserDto.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.UserDto.Password).NotEmpty().MinimumLength(4);
        RuleFor(x => x.UserDto.Role).IsInEnum();
    }
}
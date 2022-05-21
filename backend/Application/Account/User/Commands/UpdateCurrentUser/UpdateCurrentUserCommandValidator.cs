using FluentValidation;

namespace Application.Account.User.Commands.UpdateCurrentUser;

public class UpdateUserCommandValidator : AbstractValidator<UpdateCurrentUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(x => x.Name).MaximumLength(120).WithMessage(Messages.MaximumLength(120));
        RuleFor(x => x.Description).MaximumLength(255).WithMessage(Messages.MaximumLength(255));
    }
}

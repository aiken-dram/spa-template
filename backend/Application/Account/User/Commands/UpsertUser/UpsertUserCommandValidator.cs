using FluentValidation;

namespace Application.Account.User.Commands.UpsertUser;

public class UpsertUserCommandValidator : AbstractValidator<UpsertUserCommand>
{
    public UpsertUserCommandValidator()
    {
        RuleFor(x => x.Login).MinimumLength(1).NotEmpty().WithMessage(Messages.MustNotBeEmpty(Messages.Login));
        RuleFor(x => x.Password).MinimumLength(1).NotEmpty().WithMessage(Messages.MustNotBeEmpty(Messages.Password)).When(p => !p.IdUser.HasValue);
        RuleFor(x => x.Name).MinimumLength(1).NotEmpty().WithMessage(Messages.MustNotBeEmpty(Messages.UserName));
        RuleFor(x => x.Login).MaximumLength(20).WithMessage(Messages.MaximumLength(20));
        RuleFor(x => x.Name).MaximumLength(120).WithMessage(Messages.MaximumLength(120));
        RuleFor(x => x.Description).MaximumLength(255).WithMessage(Messages.MaximumLength(255));
        //2D: add database limitations for role and group values?
    }
}

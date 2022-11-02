using FluentValidation;

namespace Application.R.RScript.Commands.UpsertRScript;

public class UpsertRScriptCommandValidator : AbstractValidator<UpsertRScriptCommand>
{
    public UpsertRScriptCommandValidator()
    {
        RuleFor(x => x.ScriptFile).NotEmpty().MinimumLength(1).WithMessage(Messages.MustNotBeEmpty(Messages.RequestScriptFile));
        RuleFor(x => x.ScriptFile).MaximumLength(255).WithMessage(Messages.MaximumLength(255));
        RuleFor(x => x.Name).NotEmpty().MinimumLength(1).WithMessage(Messages.MustNotBeEmpty(Messages.RequestName));
        RuleFor(x => x.Name).MaximumLength(120).WithMessage(Messages.MaximumLength(120));
        RuleFor(x => x.ContentType).NotEmpty().MinimumLength(1).WithMessage(Messages.MustNotBeEmpty(Messages.RequestContentType));
        RuleFor(x => x.ContentType).MaximumLength(50).WithMessage(Messages.MaximumLength(50));
        RuleFor(x => x.ResultFile).NotEmpty().MinimumLength(1).WithMessage(Messages.MustNotBeEmpty(Messages.RequestResultFile));
        RuleFor(x => x.ResultFile).MaximumLength(200).WithMessage(Messages.MaximumLength(200));
        RuleFor(x => x.Description).MaximumLength(255).WithMessage(Messages.MaximumLength(255));

        //2D - rules for parameters
    }
}

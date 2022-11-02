using FluentValidation;

namespace Application.Sample.Commands.UpsertSample;

#warning SAMPLE, remove entire file in actual application
public class UpsertSampleCommandValidator : AbstractValidator<UpsertSampleCommand>
{
    public UpsertSampleCommandValidator()
    {
        RuleFor(x => x.Text).MaximumLength(120).WithMessage(Messages.MaximumLength(120));
        RuleForEach(x => x.SampleChildren).ChildRules(c =>
        {
            c.RuleFor(x => x.Text).MaximumLength(120).WithMessage(Messages.MaximumLength(120));
        });

    }
}

//2D: might only need this, but change IEnum to List in command?
public class UpsertSampleChildDtoValidator : AbstractValidator<UpsertSampleChildDto>
{
    public UpsertSampleChildDtoValidator()
    {
        RuleFor(x => x.Text).MaximumLength(120).WithMessage(Messages.MaximumLength(120));
    }
}
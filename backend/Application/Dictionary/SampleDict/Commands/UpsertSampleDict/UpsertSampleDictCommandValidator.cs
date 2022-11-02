using FluentValidation;

namespace Application.Dictionary.SampleDict.Commands.UpsertSampleDict;

#warning SAMPLE, remove entire file in actual application
public class UpsertSampleDictCommandValidator : AbstractValidator<UpsertSampleDictCommand>
{
    public UpsertSampleDictCommandValidator()
    {
        RuleFor(x => x.dict).NotEmpty().MinimumLength(1).WithMessage(Messages.MustNotBeEmpty(Messages.Name));
        RuleFor(x => x.dict).MaximumLength(120).WithMessage(Messages.MaximumLength(120));
        RuleFor(x => x.description).MaximumLength(255).WithMessage(Messages.MaximumLength(255));
    }
}

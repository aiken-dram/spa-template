using FluentValidation;

namespace Application.Dictionary.District.Commands.UpsertDistrict;

public class UpsertDistrictCommandValidator : AbstractValidator<UpsertDistrictCommand>
{
    public UpsertDistrictCommandValidator()
    {
        RuleFor(x => x.name).MinimumLength(1).NotEmpty().WithMessage(Messages.MustNotBeEmpty(Messages.Name));
        RuleFor(x => x.name).MaximumLength(200).WithMessage(Messages.MaximumLength(200));
    }
}

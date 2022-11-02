using FluentValidation;

namespace Application.MessageQuery.Commands.CreateRequest;

public class CreateRequestCommandValidator : AbstractValidator<CreateRequestCommand>
{
    public CreateRequestCommandValidator()
    {
        RuleFor(x => x.Json).MaximumLength(500).WithMessage(Messages.MaximumLength(500));
    }
}

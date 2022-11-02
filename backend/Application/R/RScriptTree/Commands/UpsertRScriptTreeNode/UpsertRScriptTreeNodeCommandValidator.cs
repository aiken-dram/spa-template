using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.R.RScriptTree.Commands.UpsertRScriptTreeNode;

public class UpsertRScriptTreeNodeCommandValidator : AbstractValidator<UpsertRScriptTreeNodeCommand>
{
    private readonly ISPADbContext _context;

    public UpsertRScriptTreeNodeCommandValidator(ISPADbContext context)
    {
        _context = context;

        RuleFor(x => x.idParent).Must((request, value) => ParentExists(request, value)).WithMessage(Messages.MissingParentRScriptTreeNode);
        RuleFor(x => x.name).NotEmpty().MinimumLength(1).WithMessage(Messages.MustNotBeEmpty(Messages.Name));
        RuleFor(x => x.name).MaximumLength(120).WithMessage(Messages.MaximumLength(120));
        RuleFor(x => x.modules).MaximumLength(255).WithMessage(Messages.MaximumLength(255));
        RuleFor(x => x.icon).MaximumLength(50).WithMessage(Messages.MaximumLength(50));
        RuleFor(x => x.color).MaximumLength(50).WithMessage(Messages.MaximumLength(50));
        RuleFor(x => x.description).MaximumLength(255).WithMessage(Messages.MaximumLength(255));
    }

    /// <summary>
    /// Checks that parent element exists in database
    /// </summary>
    /// <param name="request"></param>
    /// <param name="value"></param>
    /// <returns>True if parent exists or not provided, </returns>
    public bool ParentExists(UpsertRScriptTreeNodeCommand request, long? value)
    {
        //if parent not provided dont check
        if (!request.idParent.HasValue)
            return true;

        return _context.RScriptTree.Any(p => p.Id == request.idParent.Value);
    }
}
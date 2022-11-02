using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Account.User.Commands.UpsertUser;

public class UpsertUserCommandValidator : AbstractValidator<UpsertUserCommand>
{
    private readonly ISPADbContext _context;

    public UpsertUserCommandValidator(ISPADbContext context)
    {
        _context = context;

        RuleFor(x => x.Login).MinimumLength(1).NotEmpty().WithMessage(Messages.MustNotBeEmpty(Messages.Login));
        RuleFor(x => x.Login).Must((request, value) => UniqueLogin(request, value)).WithMessage(Messages.UserWithProvidedLoginAlreadyExists);
        RuleFor(x => x.Password).MinimumLength(1).NotEmpty().WithMessage(Messages.MustNotBeEmpty(Messages.Password)).When(p => !p.IdUser.HasValue);
        RuleFor(x => x.Name).MinimumLength(1).NotEmpty().WithMessage(Messages.MustNotBeEmpty(Messages.UserName));
        RuleFor(x => x.Login).MaximumLength(20).WithMessage(Messages.MaximumLength(20));
        RuleFor(x => x.Name).MaximumLength(120).WithMessage(Messages.MaximumLength(120));
        RuleFor(x => x.Description).MaximumLength(255).WithMessage(Messages.MaximumLength(255));
        RuleFor(x => x.Groups).Must((request, value) => GroupsExist(request, value)).WithMessage(Messages.UserGroupNotInDictionary);
        RuleFor(x => x.Roles).Must((request, value) => RolesExist(request, value)).WithMessage(Messages.UserRoleNotInDictionary);
        RuleFor(x => x.Districts).Must((request, value) => DistrictsExist(request, value)).WithMessage(Messages.UserDistrictNotInDictionary);
    }

    /// <summary>
    /// Check if login is unique
    /// </summary>
    public bool UniqueLogin(UpsertUserCommand request, string value)
    {
        var loginDuplicates = _context.Users
            .Count(p => p.Login == request.Login && p.IdUser != request.IdUser);
        return loginDuplicates == 0;
    }

    /// <summary>
    /// Check if provided groups exist in dictionary
    /// </summary>
    public bool GroupsExist(UpsertUserCommand request, long[]? value)
    {
        if (request.Groups != null && request.Groups.Length > 0)
            foreach (var i in request.Groups)
                if (!(_context.Groups.Any(p => p.IdGroup == i)))
                    return false;
        return true;
    }

    /// <summary>
    /// Check if provided roles exist in dictionary
    /// </summary>
    public bool RolesExist(UpsertUserCommand request, long[]? value)
    {
        if (request.Roles != null && request.Roles.Length > 0)
            foreach (var i in request.Roles)
                if (!(_context.Roles.Any(p => p.IdRole == i)))
                    return false;
        return true;
    }

    /// <summary>
    /// Check if provided districts exist in dictionary
    /// </summary>
    public bool DistrictsExist(UpsertUserCommand request, long[]? value)
    {
        if (request.Districts != null && request.Districts.Length > 0)
            foreach (var i in request.Districts)
                if (!(_context.Districts.Any(p => p.IdDistrict == (int)i)))
                    return false;
        return true;
    }
}
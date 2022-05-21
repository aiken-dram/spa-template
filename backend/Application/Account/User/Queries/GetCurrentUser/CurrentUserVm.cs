namespace Application.Account.User.Queries.GetCurrentUser;

/// <summary>
/// View model for current user information
/// </summary>
public class CurrentUserVm : IMapFrom<Domain.Entities.User>
{
    /// <summary>
    /// Id of user in database
    /// </summary>
    /// <example>1</example>
    public long IdUser { get; set; }

    /// <summary>
    /// Login
    /// </summary>
    /// <example>test17</example>
    public string Login { get; set; } = null!;

    /// <summary>
    /// User name
    /// </summary>
    /// <example>Test user 17</example>
    public string Name { get; set; } = null!;

    /// <summary>
    /// User description
    /// </summary>
    /// <example>Test user #17</example>
    public string? Description { get; set; }
}

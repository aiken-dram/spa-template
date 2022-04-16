namespace Application.Common.Interfaces;

public interface ICurrentUserService
{
    /// <summary>
    /// Id of currently authorized user in database
    /// </summary>
    string? UserId { get; }
}

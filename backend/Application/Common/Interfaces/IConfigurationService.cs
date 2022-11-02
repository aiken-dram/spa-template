namespace Application.Common.Interfaces;

public interface IConfigurationService
{
    /// <summary>
    /// Maximum allowed requests waiting in queue for user
    /// </summary>
    int MessageQueryUserLimit { get; }
}

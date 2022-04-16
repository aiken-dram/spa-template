namespace Application.Common.Models;

public class CurrentUser
{
    /// <summary>
    /// Id of current user in database
    /// </summary>
    public long IdUser { get; set; }

    /// <summary>
    /// List of available module names for current user
    /// </summary>
    public string[] Modules { get; set; }
}

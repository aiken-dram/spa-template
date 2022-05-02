using Domain.Enums;
using Shared.Domain.Attributes;
using Shared.Domain.Models;

namespace Domain.Entities;

/// <summary>
/// Application user
/// </summary>
public partial class User : AuditableEntity, IHasDomainEvent
{
    #region ENTITY
    /// <summary>
    /// Default constructor
    /// </summary>
    public User()
    {
        UserGroups = new HashSet<UserGroup>();
        UserRoles = new HashSet<UserRole>();
        UserDistricts = new HashSet<UserDistrict>();
        Requests = new HashSet<Request>();
        UserEvents = new HashSet<UserEvent>();
    }

    /// <summary>
    /// Id of user in database
    /// </summary>
    public long IdUser { get; set; }

    /// <summary>
    /// User login
    /// </summary>
    [Audit]
    public string Login { get; set; } = null!;

    /// <summary>
    /// Hash of user password
    /// </summary>
    public string Pass { get; set; } = null!;

    /// <summary>
    /// Is user active:
    /// T - true
    /// F - false
    /// </summary>
    [Audit(isCharBoolean: true)]
    public string IsActive { get; set; } = null!;

    /// <summary>
    /// Expire date of the password
    /// </summary>
    [Audit]
    public DateTime? PassDate { get; set; }

    /// <summary>
    /// User name
    /// </summary>
    [Audit]
    public string Name { get; set; } = null!;

    /// <summary>
    /// User description
    /// </summary>
    [Audit]
    public string? Description { get; set; }

    /// <summary>
    /// Collection of user groups
    /// </summary>
    public virtual ICollection<UserGroup> UserGroups { get; set; }

    /// <summary>
    /// Collection of user roles
    /// </summary>
    public virtual ICollection<UserRole> UserRoles { get; set; }

    /// <summary>
    /// Collection of user district
    /// </summary>
    public virtual ICollection<UserDistrict> UserDistricts { get; set; }

    /// <summary>
    /// Collection of requests from user
    /// </summary>
    public virtual ICollection<Request> Requests { get; set; }

    /// <summary>
    /// Collection of audit events for general targets
    /// </summary>
    public virtual ICollection<UserEvent> UserEvents { get; set; }
    #endregion

    #region DOMAIN EVENTS
    /// <summary>
    /// Domain events
    /// </summary>
    public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();
    #endregion

    #region AUDIT
    public override int AuditIdTarget => (int)eEventTarget.AccountUser;

    public override long? AuditTargetId => this.IdUser;

    public override string AuditTargetName => this.Login;
    #endregion
}

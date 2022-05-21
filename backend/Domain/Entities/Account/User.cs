namespace Domain.Entities;

/// <summary>
/// Application user
/// </summary>
public partial class User : AuditableEntity
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
        UserAudits = new HashSet<UserAudit>();
        SampleAudits = new HashSet<SampleAudit>();
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
    /// Collection of audit for general targets
    /// </summary>
    public virtual ICollection<UserAudit> UserAudits { get; set; }


    /// <summary>
    /// Collection of audit for sample
    /// </summary>
#warning This is example, remove in actual application
    public virtual ICollection<SampleAudit> SampleAudits { get; set; }
    #endregion

    #region AUDIT
    public override int AuditIdTarget => (int)eAuditTarget.AccountUser;

    public override long? AuditTargetId => this.IdUser;

    public override string AuditTargetName => this.Login;
    #endregion

    #region DOMAIN LOGIC
    /// <summary>
    /// Update password from file
    /// </summary>
    /// <param name="hash">MD5 hash of password to store in Pass field</param>
    /// <param name="passDate">Increment of password expiration date (default 90)</param>
    public void UpdatePassword(string? hash, int passDate = 90)
    {
        //well, it wasnt worth moving this here, just as example i guess?
        this.Pass = hash ?? String.Empty;
        this.IsActive = CharBoolean.True;
        this.PassDate = DateTime.Now.AddDays(passDate);
    }
    #endregion
}

using System;
using System.Collections.Generic;

namespace Scaffold.Context
{
    public partial class User
    {
        public User()
        {
            Requests = new HashSet<Request>();
            SampleAudits = new HashSet<SampleAudit>();
            UserAudits = new HashSet<UserAudit>();
            UserDistricts = new HashSet<UserDistrict>();
            UserGroups = new HashSet<UserGroup>();
            UserRoles = new HashSet<UserRole>();
        }

        public long IdUser { get; set; }
        public string Login { get; set; } = null!;
        public string Pass { get; set; } = null!;
        public string IsActive { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime? PassDate { get; set; }

        public virtual ICollection<Request> Requests { get; set; }
        public virtual ICollection<SampleAudit> SampleAudits { get; set; }
        public virtual ICollection<UserAudit> UserAudits { get; set; }
        public virtual ICollection<UserDistrict> UserDistricts { get; set; }
        public virtual ICollection<UserGroup> UserGroups { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}

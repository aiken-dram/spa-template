using System;
using System.Collections.Generic;

namespace Scaffold.Context
{
    public partial class Group
    {
        public Group()
        {
            GroupRoles = new HashSet<GroupRole>();
            UserGroups = new HashSet<UserGroup>();
        }

        public string? Desc { get; set; }
        public long IdGroup { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<GroupRole> GroupRoles { get; set; }
        public virtual ICollection<UserGroup> UserGroups { get; set; }
    }
}

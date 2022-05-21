using Domain.Entities;
using IBM.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration;

public class GroupRoleConfiguration : IEntityTypeConfiguration<GroupRole>
{
    public void Configure(EntityTypeBuilder<GroupRole> entity)
    {
        entity.HasKey(e => e.Id)
            .HasName("GROUP_ROLES_PK")
            .ForDb2IsClustered(false);

        entity.ToTable("GROUP_ROLES", "ACCOUNT");

        entity.Property(e => e.Id)
            .HasColumnType("bigint(8)")
            .HasColumnName("ID");

        entity.Property(e => e.IdGroup)
            .HasColumnType("bigint(8)")
            .HasColumnName("ID_GROUP");

        entity.Property(e => e.IdRole)
            .HasColumnType("bigint(8)")
            .HasColumnName("ID_ROLE");

        entity.HasOne(d => d.IdGroupNavigation)
            .WithMany(p => p.GroupRoles)
            .HasForeignKey(d => d.IdGroup)
            .HasConstraintName("GROUP_ROLES_GROUPS_FK");

        entity.HasOne(d => d.IdRoleNavigation)
            .WithMany(p => p.GroupRoles)
            .HasForeignKey(d => d.IdRole)
            .HasConstraintName("GROUP_ROLES_ROLES_FK");
    }
}

public class GroupConfiguration : IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> entity)
    {
        entity.HasKey(e => e.IdGroup)
            .HasName("GROUPS_PK")
            .ForDb2IsClustered(false);

        entity.ToTable("GROUPS", "ACCOUNT");

        entity.Property(e => e.IdGroup)
            .HasColumnType("bigint(8)")
            .HasColumnName("ID_GROUP");

        entity.Property(e => e.Description)
            .HasMaxLength(255)
            .HasPrecision(255)
            .IsUnicode(false)
            .HasColumnName("DESC");

        entity.Property(e => e.Name)
            .HasMaxLength(120)
            .HasPrecision(120)
            .IsUnicode(false)
            .HasColumnName("NAME");
    }
}

public class ModuleConfiguration : IEntityTypeConfiguration<Module>
{
    public void Configure(EntityTypeBuilder<Module> entity)
    {
        entity.HasKey(e => e.IdModule)
            .HasName("MODULES_PK")
            .ForDb2IsClustered(false);

        entity.ToTable("MODULES", "ACCOUNT");

        entity.Property(e => e.IdModule)
            .HasColumnType("bigint(8)")
            .HasColumnName("ID_MODULE");

        entity.Property(e => e.Description)
            .HasMaxLength(255)
            .HasPrecision(255)
            .IsUnicode(false)
            .HasColumnName("DESC");

        entity.Property(e => e.Name)
            .HasMaxLength(120)
            .HasPrecision(120)
            .IsUnicode(false)
            .HasColumnName("NAME");
    }
}

public class RoleModuleConfiguration : IEntityTypeConfiguration<RoleModule>
{
    public void Configure(EntityTypeBuilder<RoleModule> entity)
    {
        entity.HasKey(e => e.Id)
            .HasName("ROLE_MODULES_PK")
            .ForDb2IsClustered(false);

        entity.ToTable("ROLE_MODULES", "ACCOUNT");

        entity.Property(e => e.Id)
            .HasColumnType("bigint(8)")
            .HasColumnName("ID");

        entity.Property(e => e.IdModule)
            .HasColumnType("bigint(8)")
            .HasColumnName("ID_MODULE");

        entity.Property(e => e.IdRole)
            .HasColumnType("bigint(8)")
            .HasColumnName("ID_ROLE");

        entity.HasOne(d => d.IdModuleNavigation)
            .WithMany(p => p.RoleModules)
            .HasForeignKey(d => d.IdModule)
            .HasConstraintName("ROLE_MODULES_MODULES_FK");

        entity.HasOne(d => d.IdRoleNavigation)
            .WithMany(p => p.RoleModules)
            .HasForeignKey(d => d.IdRole)
            .HasConstraintName("ROLE_MODULES_ROLES_FK");
    }
}

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> entity)
    {
        entity.HasKey(e => e.IdRole)
            .HasName("ROLES_PK")
            .ForDb2IsClustered(false);

        entity.ToTable("ROLES", "ACCOUNT");

        entity.Property(e => e.IdRole)
            .HasColumnType("bigint(8)")
            .HasColumnName("ID_ROLE");

        entity.Property(e => e.Description)
            .HasMaxLength(255)
            .HasPrecision(255)
            .IsUnicode(false)
            .HasColumnName("DESC");

        entity.Property(e => e.Name)
            .HasMaxLength(120)
            .HasPrecision(120)
            .IsUnicode(false)
            .HasColumnName("NAME");
    }
}

public class UserGroupConfiguration : IEntityTypeConfiguration<UserGroup>
{
    public void Configure(EntityTypeBuilder<UserGroup> entity)
    {
        entity.HasKey(e => e.Id)
            .HasName("USER_GROUPS_PK")
            .ForDb2IsClustered(false);

        entity.ToTable("USER_GROUPS", "ACCOUNT");

        entity.Property(e => e.Id)
            .HasColumnType("bigint(8)")
            .HasColumnName("ID");

        entity.Property(e => e.IdGroup)
            .HasColumnType("bigint(8)")
            .HasColumnName("ID_GROUP");

        entity.Property(e => e.IdUser)
            .HasColumnType("bigint(8)")
            .HasColumnName("ID_USER");

        entity.HasOne(d => d.IdGroupNavigation)
            .WithMany(p => p.UserGroups)
            .HasForeignKey(d => d.IdGroup)
            .HasConstraintName("USER_GROUPS_GROUPS_FK");

        entity.HasOne(d => d.IdUserNavigation)
            .WithMany(p => p.UserGroups)
            .HasForeignKey(d => d.IdUser)
            .HasConstraintName("USER_GROUPS_USERS_FK");
    }
}

public class UserDistrictConfiguration : IEntityTypeConfiguration<UserDistrict>
{
    public void Configure(EntityTypeBuilder<UserDistrict> entity)
    {
        entity.HasKey(e => new { e.IdUser, e.IdDistrict, e.Id })
            .HasName("USER_DISTRICTS_PK")
            .ForDb2IsClustered(false);

        entity.ToTable("USER_DISTRICTS", "ACCOUNT");

        entity.Property(e => e.IdUser)
            .HasColumnType("bigint(8)")
            .HasColumnName("ID_USER");

        entity.Property(e => e.IdDistrict)
            .HasColumnType("integer(4)")
            .HasColumnName("ID_DISTRICT");

        entity.Property(e => e.Id)
            .HasColumnType("bigint(8)")
            .ValueGeneratedOnAdd()
            .HasColumnName("ID");

        entity.HasOne(d => d.IdDistrictNavigation)
            .WithMany(p => p.UserDistricts)
            .HasForeignKey(d => d.IdDistrict)
            .HasConstraintName("USER_DISTRICTS_DISTRICTS_FK");

        entity.HasOne(d => d.IdUserNavigation)
            .WithMany(p => p.UserDistricts)
            .HasForeignKey(d => d.IdUser)
            .HasConstraintName("USER_DISTRICTS_USERS_FK");
    }
}

public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> entity)
    {
        entity.HasKey(e => e.Id)
            .HasName("USER_ROLES_PK")
            .ForDb2IsClustered(false);

        entity.ToTable("USER_ROLES", "ACCOUNT");

        entity.Property(e => e.Id)
            .HasColumnType("bigint(8)")
            .HasColumnName("ID");

        entity.Property(e => e.IdRole)
            .HasColumnType("bigint(8)")
            .HasColumnName("ID_ROLE");

        entity.Property(e => e.IdUser)
            .HasColumnType("bigint(8)")
            .HasColumnName("ID_USER");

        entity.HasOne(d => d.IdRoleNavigation)
            .WithMany(p => p.UserRoles)
            .HasForeignKey(d => d.IdRole)
            .HasConstraintName("USER_ROLES_ROLES_FK");

        entity.HasOne(d => d.IdUserNavigation)
            .WithMany(p => p.UserRoles)
            .HasForeignKey(d => d.IdUser)
            .HasConstraintName("USER_ROLES_USERS_FK");
    }
}

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> entity)
    {
        entity.HasKey(e => e.IdUser)
            .HasName("USERS_PK")
            .ForDb2IsClustered(false);

        entity.ToTable("USERS", "ACCOUNT");

        entity.Property(e => e.IdUser)
            .HasColumnType("bigint(8)")
            .HasColumnName("ID_USER");

        entity.Property(e => e.Description)
            .HasMaxLength(255)
            .HasPrecision(255)
            .IsUnicode(false)
            .HasColumnName("DESC");

        entity.Property(e => e.IsActive)
            .HasColumnType("character(1)")
            .HasColumnName("IS_ACTIVE");

        entity.Property(e => e.Login)
            .HasMaxLength(20)
            .HasPrecision(20)
            .IsUnicode(false)
            .HasColumnName("LOGIN");

        entity.Property(e => e.Name)
            .HasMaxLength(120)
            .HasPrecision(120)
            .IsUnicode(false)
            .HasColumnName("NAME");

        entity.Property(e => e.Pass)
            .HasMaxLength(32)
            .HasPrecision(32)
            .IsUnicode(false)
            .HasColumnName("PASS");

        entity.Property(e => e.PassDate)
            .HasColumnType("date(4)")
            .HasColumnName("PASS_DATE");

        entity.Ignore(e => e.Audits);
    }
}

public class UserAuditConfiguration : IEntityTypeConfiguration<UserAudit>
{
    public void Configure(EntityTypeBuilder<UserAudit> entity)
    {
        entity.HasKey(e => e.IdAudit)
            .HasName("USER_AUDIT_PK")
            .ForDb2IsClustered(false);

        entity.ToTable("USER_AUDIT", "ACCOUNT");

        entity.Property(e => e.IdAudit)
            .HasColumnType("bigint(8)")
            .HasColumnName("ID_AUDIT");

        entity.Property(e => e.IdAction)
            .HasColumnType("integer(4)")
            .HasColumnName("ID_ACTION");

        entity.Property(e => e.IdTarget)
            .HasColumnType("integer(4)")
            .HasColumnName("ID_TARGET");

        entity.Property(e => e.IdUser)
            .HasColumnType("bigint(8)")
            .HasColumnName("ID_USER");

        entity.Property(e => e.Message)
            .HasMaxLength(255)
            .HasPrecision(255)
            .IsUnicode(false)
            .HasColumnName("MESSAGE");

        entity.Property(e => e.Stamp)
            .HasMaxLength(10)
            .HasPrecision(10)
            .HasColumnName("STAMP");

        entity.Property(e => e.TargetId)
            .HasColumnType("bigint(8)")
            .HasColumnName("TARGET_ID");

        entity.Property(e => e.TargetName)
            .HasMaxLength(255)
            .HasPrecision(255)
            .IsUnicode(false)
            .HasColumnName("TARGET_NAME");

        entity.HasOne(d => d.IdActionNavigation)
            .WithMany(p => p.UserAudits)
            .HasForeignKey(d => d.IdAction)
            .HasConstraintName("USER_AUDIT_AUDIT_ACTIONS_FK");

        entity.HasOne(d => d.IdTargetNavigation)
            .WithMany(p => p.UserAudits)
            .HasForeignKey(d => d.IdTarget)
            .HasConstraintName("USER_AUDIT_AUDIT_TARGETS_FK");

        entity.HasOne(d => d.IdUserNavigation)
            .WithMany(p => p.UserAudits)
            .HasForeignKey(d => d.IdUser)
            .HasConstraintName("USER_AUDIT_USERS_FK");

        entity.Ignore(e => e.AuditData);
    }
}

public class UserAuditDataConfiguration : IEntityTypeConfiguration<UserAuditData>
{
    public void Configure(EntityTypeBuilder<UserAuditData> entity)
    {
        entity.HasKey(e => e.Id)
            .HasName("USER_AUDIT_DATA_PK")
            .ForDb2IsClustered(false);

        entity.ToTable("USER_AUDIT_DATA", "ACCOUNT");

        entity.Property(e => e.Id)
            .HasColumnType("bigint(8)")
            .HasColumnName("ID");

        entity.Property(e => e.IdAudit)
            .HasColumnType("bigint(8)")
            .HasColumnName("ID_AUDIT");

        entity.Property(e => e.IdType)
            .HasColumnType("integer(4)")
            .HasColumnName("ID_AUDIT");

        entity.Property(e => e.Json)
            .HasMaxLength(255)
            .HasPrecision(255)
            .IsUnicode(false)
            .HasColumnName("JSON");

        entity.HasOne(d => d.IdAuditNavigation)
            .WithMany(p => p.UserAuditData)
            .HasForeignKey(d => d.IdAudit)
            .HasConstraintName("USER_AUDIT_DATA_USER_AUDIT_FK");

        entity.HasOne(d => d.IdTypeNavigation)
            .WithMany(p => p.UserAuditData)
            .HasForeignKey(d => d.IdType)
            .HasConstraintName("USER_AUDIT_DATA_AUDIT_DATA_TYPES_FK");
    }
}

public class VAuditConfiguration : IEntityTypeConfiguration<VAudit>
{
    public void Configure(EntityTypeBuilder<VAudit> entity)
    {
        entity.HasKey(p => new { p.Source, p.IdAudit });

        entity.ToView("V_AUDIT", "ACCOUNT");

        entity.Property(e => e.Source)
            .HasColumnType("character(6)")
            .HasColumnName("SOURCE");

        entity.Property(e => e.IdAudit)
            .HasColumnType("bigint(8)")
            .HasColumnName("ID_AUDIT");

        entity.Property(e => e.IdAction)
            .HasColumnType("integer(4)")
            .HasColumnName("ID_ACTION");

        entity.Property(e => e.IdTarget)
            .HasColumnType("integer(4)")
            .HasColumnName("ID_TARGET");

        entity.Property(e => e.IdUser)
            .HasColumnType("bigint(8)")
            .HasColumnName("ID_USER");

        entity.Property(e => e.Message)
            .HasMaxLength(255)
            .HasPrecision(255)
            .IsUnicode(false)
            .HasColumnName("MESSAGE");

        entity.Property(e => e.Stamp)
            .HasMaxLength(10)
            .HasPrecision(10)
            .HasColumnName("STAMP");

        entity.Property(e => e.TargetId)
            .HasColumnType("bigint(8)")
            .HasColumnName("TARGET_ID");

        entity.Property(e => e.TargetName)
            .HasMaxLength(255)
            .HasPrecision(255)
            .IsUnicode(false)
            .HasColumnName("TARGET_NAME");

        entity.ToView("V_AUDITS", "ACCOUNT");
    }
}

public class VAuditDataConfiguration : IEntityTypeConfiguration<VAuditData>
{
    public void Configure(EntityTypeBuilder<VAuditData> entity)
    {
        entity.HasKey(p => new { p.Source, p.IdAudit });

        entity.ToView("V_AUDIT_DATA", "ACCOUNT");

        entity.Property(e => e.Source)
            .HasColumnType("character(6)")
            .HasColumnName("SOURCE");

        entity.Property(e => e.Id)
            .HasColumnType("bigint(8)")
            .HasColumnName("ID");

        entity.Property(e => e.IdAudit)
            .HasColumnType("bigint(8)")
            .HasColumnName("ID_AUDIT");

        entity.Property(e => e.IdType)
            .HasColumnType("integer(4)")
            .HasColumnName("ID_TYPE");

        entity.Property(e => e.Json)
            .HasMaxLength(255)
            .HasPrecision(255)
            .IsUnicode(false)
            .HasColumnName("JSON");
    }
}
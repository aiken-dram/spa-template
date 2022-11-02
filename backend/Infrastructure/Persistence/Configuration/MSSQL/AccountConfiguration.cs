using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration;

public class GroupRoleConfiguration : IEntityTypeConfiguration<GroupRole>
{
    public void Configure(EntityTypeBuilder<GroupRole> entity)
    {
        entity.ToTable("GROUP_ROLES", "ACCOUNT");

        entity.HasOne(d => d.IdGroupNavigation)
            .WithMany(p => p.GroupRoles)
            .HasForeignKey(d => d.IdGroup)
            .HasConstraintName("FK_ACCOUNT_GROUP_ROLES_GROUPS");

        entity.HasOne(d => d.IdRoleNavigation)
            .WithMany(p => p.GroupRoles)
            .HasForeignKey(d => d.IdRole)
            .HasConstraintName("FK_ACCOUNT_GROUP_ROLES_ROLES");
    }
}

public class GroupConfiguration : IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> entity)
    {
        entity.HasKey(e => e.IdGroup)
            .HasName("PK_ACCOUNT_GROUPS");

        entity.ToTable("GROUPS", "ACCOUNT");

        entity.Property(e => e.Description)
            .HasMaxLength(255)
            .IsUnicode(false);

        entity.Property(e => e.Name)
            .HasMaxLength(120)
            .IsUnicode(false);
    }
}

public class ModuleConfiguration : IEntityTypeConfiguration<Module>
{
    public void Configure(EntityTypeBuilder<Module> entity)
    {
        entity.HasKey(e => e.IdModule)
            .HasName("PK_ACCOUNT_MODULES");

        entity.ToTable("MODULES", "ACCOUNT");

        entity.Property(e => e.Description)
            .HasMaxLength(255)
            .IsUnicode(false);

        entity.Property(e => e.Name)
            .HasMaxLength(120)
            .IsUnicode(false);
    }
}

public class RoleModuleConfiguration : IEntityTypeConfiguration<RoleModule>
{
    public void Configure(EntityTypeBuilder<RoleModule> entity)
    {
        entity.ToTable("ROLE_MODULES", "ACCOUNT");

        entity.HasOne(d => d.IdModuleNavigation)
            .WithMany(p => p.RoleModules)
            .HasForeignKey(d => d.IdModule)
            .HasConstraintName("FK_ACCOUNT_ROLE_MODULES_MODULES");

        entity.HasOne(d => d.IdRoleNavigation)
            .WithMany(p => p.RoleModules)
            .HasForeignKey(d => d.IdRole)
            .HasConstraintName("FK_ACCOUNT_ROLE_MODULES_ROLES");
    }
}

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> entity)
    {
        entity.HasKey(e => e.IdRole)
            .HasName("PK_ACCOUNT_ROLES");

        entity.ToTable("ROLES", "ACCOUNT");

        entity.Property(e => e.Description)
            .HasMaxLength(255)
            .IsUnicode(false);

        entity.Property(e => e.Name)
            .HasMaxLength(120)
            .IsUnicode(false);
    }
}

public class UserGroupConfiguration : IEntityTypeConfiguration<UserGroup>
{
    public void Configure(EntityTypeBuilder<UserGroup> entity)
    {
        entity.ToTable("USER_GROUPS", "ACCOUNT");

        entity.HasOne(d => d.IdGroupNavigation)
            .WithMany(p => p.UserGroups)
            .HasForeignKey(d => d.IdGroup)
            .HasConstraintName("FK_ACCOUNT_USER_GROUPS_GROUPS");

        entity.HasOne(d => d.IdUserNavigation)
            .WithMany(p => p.UserGroups)
            .HasForeignKey(d => d.IdUser)
            .HasConstraintName("FK_ACCOUNT_USER_GROUPS_USERS");
    }
}

public class UserDistrictConfiguration : IEntityTypeConfiguration<UserDistrict>
{
    public void Configure(EntityTypeBuilder<UserDistrict> entity)
    {
        entity.ToTable("USER_DISTRICTS", "ACCOUNT");

        entity.HasOne(d => d.IdDistrictNavigation)
            .WithMany(p => p.UserDistricts)
            .HasForeignKey(d => d.IdDistrict)
            .HasConstraintName("FK_ACCOUNT_USER_DISTRICTS_DISTRICTS");

        entity.HasOne(d => d.IdUserNavigation)
            .WithMany(p => p.UserDistricts)
            .HasForeignKey(d => d.IdUser)
            .HasConstraintName("FK_ACCOUNT_USER_DISTRICTS_USERS");
    }
}

public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> entity)
    {
        entity.ToTable("USER_ROLES", "ACCOUNT");

        entity.HasOne(d => d.IdRoleNavigation)
            .WithMany(p => p.UserRoles)
            .HasForeignKey(d => d.IdRole)
            .HasConstraintName("FK_ACCOUNT_USER_ROLES_ROLES");

        entity.HasOne(d => d.IdUserNavigation)
            .WithMany(p => p.UserRoles)
            .HasForeignKey(d => d.IdUser)
            .HasConstraintName("FK_ACCOUNT_USER_ROLES_USERS");
    }
}

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> entity)
    {
        entity.HasKey(e => e.IdUser)
            .HasName("PK_ACCOUNT_USERS");

        entity.ToTable("USERS", "ACCOUNT");

        entity.Property(e => e.Description)
            .HasMaxLength(255)
            .IsUnicode(false);

        entity.Property(e => e.IsActive)
            .HasMaxLength(1)
            .IsUnicode(false)
            .IsFixedLength();

        entity.Property(e => e.Login)
            .HasMaxLength(20)
            .IsUnicode(false);

        entity.Property(e => e.Name)
            .HasMaxLength(120)
            .IsUnicode(false);

        entity.Property(e => e.Pass)
            .HasMaxLength(32)
            .IsUnicode(false);

        entity.Property(e => e.PassDate).HasColumnType("date");

        entity.Ignore(e => e.Audits);
    }
}

public class UserAuditConfiguration : IEntityTypeConfiguration<UserAudit>
{
    public void Configure(EntityTypeBuilder<UserAudit> entity)
    {
        entity.HasKey(e => e.IdAudit)
            .HasName("PK_ACCOUNT_USER_AUDIT");

        entity.ToTable("USER_AUDIT", "ACCOUNT");

        entity.Property(e => e.Message)
            .HasMaxLength(255)
            .IsUnicode(false);

        entity.Property(e => e.Stamp).HasColumnType("datetime");

        entity.Property(e => e.TargetName)
            .HasMaxLength(255)
            .IsUnicode(false);

        entity.HasOne(d => d.IdActionNavigation)
            .WithMany(p => p.UserAudits)
            .HasForeignKey(d => d.IdAction)
            .HasConstraintName("FK_ACCOUNT_USER_AUDIT_AUDIT_ACTIONS");

        entity.HasOne(d => d.IdTargetNavigation)
            .WithMany(p => p.UserAudits)
            .HasForeignKey(d => d.IdTarget)
            .HasConstraintName("FK_ACCOUNT_USER_AUDIT_AUDIT_TARGETS");

        entity.HasOne(d => d.IdUserNavigation)
            .WithMany(p => p.UserAudits)
            .HasForeignKey(d => d.IdUser)
            .HasConstraintName("FK_ACCOUNT_USER_AUDIT_USERS");

        entity.Ignore(e => e.AuditData);
    }
}

public class UserAuditDataConfiguration : IEntityTypeConfiguration<UserAuditData>
{
    public void Configure(EntityTypeBuilder<UserAuditData> entity)
    {
        entity.ToTable("USER_AUDIT_DATA", "ACCOUNT");

        entity.Property(e => e.Json)
            .HasMaxLength(255)
            .IsUnicode(false)
            .HasColumnName("JSON");

        entity.HasOne(d => d.IdAuditNavigation)
            .WithMany(p => p.UserAuditData)
            .HasForeignKey(d => d.IdAudit)
            .HasConstraintName("FK_ACCOUNT_USER_AUDIT_DATA_USER_AUDIT");

        entity.HasOne(d => d.IdTypeNavigation)
            .WithMany(p => p.UserAuditData)
            .HasForeignKey(d => d.IdType)
            .HasConstraintName("FK_ACCOUNT_USER_AUDIT_DATA_AUDIT_DATA_TYPES");
    }
}

public class VAuditConfiguration : IEntityTypeConfiguration<VAudit>
{
    public void Configure(EntityTypeBuilder<VAudit> entity)
    {
        entity.HasKey(p => new { p.Source, p.IdAudit });

        entity.ToView("V_AUDIT", "ACCOUNT");

        entity.Property(e => e.Action)
            .HasMaxLength(255)
            .IsUnicode(false);

        entity.Property(e => e.Login)
            .HasMaxLength(20)
            .IsUnicode(false);

        entity.Property(e => e.Message)
            .HasMaxLength(255)
            .IsUnicode(false);

        entity.Property(e => e.Source)
            .HasMaxLength(6)
            .IsUnicode(false);

        entity.Property(e => e.Stamp).HasColumnType("datetime");

        entity.Property(e => e.Target)
            .HasMaxLength(120)
            .IsUnicode(false);

        entity.Property(e => e.TargetDesc)
            .HasMaxLength(255)
            .IsUnicode(false);

        entity.Property(e => e.TargetName)
            .HasMaxLength(255)
            .IsUnicode(false);
    }
}

public class VAuditDataConfiguration : IEntityTypeConfiguration<VAuditData>
{
    public void Configure(EntityTypeBuilder<VAuditData> entity)
    {
        entity.HasKey(p => new { p.Source, p.IdAudit });

        entity.ToView("V_AUDIT_DATA", "ACCOUNT");

        entity.Property(e => e.Json)
            .HasMaxLength(255)
            .IsUnicode(false)
            .HasColumnName("JSON");

        entity.Property(e => e.Source)
            .HasMaxLength(6)
            .IsUnicode(false);

        entity.Property(e => e.Type)
            .HasMaxLength(120)
            .IsUnicode(false);
    }
}
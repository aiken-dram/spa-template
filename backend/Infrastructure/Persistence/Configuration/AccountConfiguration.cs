using System;
using System.Collections.Generic;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration;

public class GroupRolesConfiguration : IEntityTypeConfiguration<GroupRole>
{
    public void Configure(EntityTypeBuilder<GroupRole> entity)
    {
        entity.ToTable("GROUP_ROLES", "ACCOUNT");

        entity.Property(e => e.Id).HasColumnName("ID");

        entity.Property(e => e.IdGroup).HasColumnName("ID_GROUP");

        entity.Property(e => e.IdRole).HasColumnName("ID_ROLE");

        entity.HasOne(d => d.IdGroupNavigation)
            .WithMany(p => p.GroupRoles)
            .HasForeignKey(d => d.IdGroup)
            .HasConstraintName("FK_GROUP_ROLES_GROUP");

        entity.HasOne(d => d.IdRoleNavigation)
            .WithMany(p => p.GroupRoles)
            .HasForeignKey(d => d.IdRole)
            .HasConstraintName("FK_GROUP_ROLES_ROLE");
    }
}

public class GroupsConfiguration : IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> entity)
    {
        entity.HasKey(e => e.IdGroup);

        entity.ToTable("GROUPS", "ACCOUNT");

        entity.Property(e => e.IdGroup).HasColumnName("ID_GROUP");

        entity.Property(e => e.Description)
            .HasMaxLength(255)
            .HasColumnName("DESC");

        entity.Property(e => e.Name)
            .HasMaxLength(120)
            .HasColumnName("NAME");
    }
}

public class ModulesConfiguration : IEntityTypeConfiguration<Module>
{
    public void Configure(EntityTypeBuilder<Module> entity)
    {
        entity.HasKey(e => e.IdModule);

        entity.ToTable("MODULES", "ACCOUNT");

        entity.Property(e => e.IdModule).HasColumnName("ID_MODULE");

        entity.Property(e => e.Description)
            .HasMaxLength(255)
            .HasColumnName("DESC");

        entity.Property(e => e.Name)
            .HasMaxLength(120)
            .HasColumnName("NAME");
    }
}

public class RoleModulesConfiguration : IEntityTypeConfiguration<RoleModule>
{
    public void Configure(EntityTypeBuilder<RoleModule> entity)
    {
        entity.ToTable("ROLE_MODULES", "ACCOUNT");

        entity.Property(e => e.Id).HasColumnName("ID");

        entity.Property(e => e.IdModule).HasColumnName("ID_MODULE");

        entity.Property(e => e.IdRole).HasColumnName("ID_ROLE");

        entity.HasOne(d => d.IdModuleNavigation)
            .WithMany(p => p.RoleModules)
            .HasForeignKey(d => d.IdModule)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_ROLE_MODULES_MODULE");

        entity.HasOne(d => d.IdRoleNavigation)
            .WithMany(p => p.RoleModules)
            .HasForeignKey(d => d.IdRole)
            .HasConstraintName("FK_ROLE_MODULES_ROLE");
    }
}

public class RolesConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> entity)
    {
        entity.HasKey(e => e.IdRole);

        entity.ToTable("ROLES", "ACCOUNT");

        entity.Property(e => e.IdRole).HasColumnName("ID_ROLE");

        entity.Property(e => e.Description)
            .HasMaxLength(255)
            .HasColumnName("DESC");

        entity.Property(e => e.Name)
            .HasMaxLength(120)
            .HasColumnName("NAME");
    }
}

public class UserGroupsConfiguration : IEntityTypeConfiguration<UserGroup>
{
    public void Configure(EntityTypeBuilder<UserGroup> entity)
    {
        entity.ToTable("USER_GROUPS", "ACCOUNT");

        entity.Property(e => e.Id).HasColumnName("ID");

        entity.Property(e => e.IdGroup).HasColumnName("ID_GROUP");

        entity.Property(e => e.IdUser).HasColumnName("ID_USER");

        entity.HasOne(d => d.IdGroupNavigation)
            .WithMany(p => p.UserGroups)
            .HasForeignKey(d => d.IdGroup)
            .HasConstraintName("FK_USER_GROUPS_GROUP");

        entity.HasOne(d => d.IdUserNavigation)
            .WithMany(p => p.UserGroups)
            .HasForeignKey(d => d.IdUser)
            .HasConstraintName("FK_USER_GROUPS_USER");
    }
}

public class UserRolesConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> entity)
    {
        entity.ToTable("USER_ROLES", "ACCOUNT");

        entity.Property(e => e.Id).HasColumnName("ID");

        entity.Property(e => e.IdRole).HasColumnName("ID_ROLE");

        entity.Property(e => e.IdUser).HasColumnName("ID_USER");

        entity.HasOne(d => d.IdRoleNavigation)
            .WithMany(p => p.UserRoles)
            .HasForeignKey(d => d.IdRole)
            .HasConstraintName("FK_USER_ROLES_ROLE");

        entity.HasOne(d => d.IdUserNavigation)
            .WithMany(p => p.UserRoles)
            .HasForeignKey(d => d.IdUser)
            .HasConstraintName("FK_USER_ROLES_USER");
    }
}

public class UsersConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> entity)
    {
        entity.HasKey(e => e.IdUser);

        entity.ToTable("USERS", "ACCOUNT");

        entity.Property(e => e.IdUser).HasColumnName("ID_USER");

        entity.Property(e => e.Description)
            .HasMaxLength(255)
            .HasColumnName("DESC");

        entity.Property(e => e.IsActive)
            .HasColumnType("char")
            .HasColumnName("IS_ACTIVE");

        entity.Property(e => e.Login)
            .HasMaxLength(20)
            .HasColumnName("LOGIN");

        entity.Property(e => e.Name)
            .HasMaxLength(120)
            .HasColumnName("NAME");

        entity.Property(e => e.Pass)
            .HasMaxLength(32)
            .HasColumnName("PASS");

        entity.Property(e => e.PassDate).HasColumnName("PASS_DATE");
    }
}
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration;

public class RScriptConfiguration : IEntityTypeConfiguration<Domain.Entities.RScript>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.RScript> entity)
    {
        entity.HasKey(e => e.IdRScript)
            .HasName("PK_R_RSCRIPTS");

        entity.ToTable("RSCRIPTS", "R");

        entity.Property(e => e.IdRScript).HasColumnName("IdRScript");

        entity.Property(e => e.ContentType)
            .HasMaxLength(50)
            .IsUnicode(false);

        entity.Property(e => e.Description)
            .HasMaxLength(255)
            .IsUnicode(false);

        entity.Property(e => e.Name)
            .HasMaxLength(120)
            .IsUnicode(false);

        entity.Property(e => e.ResultFile)
            .HasMaxLength(200)
            .IsUnicode(false);

        entity.Property(e => e.ScriptFile)
            .HasMaxLength(255)
            .IsUnicode(false);

        entity.Ignore(e => e.Audits);
    }
}

public class RScriptParamConfiguration : IEntityTypeConfiguration<RScriptParam>
{
    public void Configure(EntityTypeBuilder<RScriptParam> entity)
    {
        entity.ToTable("RSCRIPT_PARAMS", "R");

        entity.Property(e => e.Description)
            .HasMaxLength(255)
            .IsUnicode(false);

        entity.Property(e => e.Hint)
            .HasMaxLength(255)
            .IsUnicode(false);

        entity.Property(e => e.IdRScript).HasColumnName("IdRScript");

        entity.Property(e => e.Name)
            .HasMaxLength(120)
            .IsUnicode(false);

        entity.HasOne(d => d.IdRScriptNavigation)
            .WithMany(p => p.RScriptParams)
            .HasForeignKey(d => d.IdRScript)
            .HasConstraintName("FK_R_RSCRIPT_PARAMS_RSCRIPTS");

        entity.HasOne(d => d.IdTypeNavigation)
            .WithMany(p => p.RScriptParams)
            .HasForeignKey(d => d.IdType)
            .HasConstraintName("FK_R_RSCRIPT_PARAMS_RSCRIPT_PARAM_TYPES");
    }
}

public class RScriptTreeConfiguration : IEntityTypeConfiguration<RScriptTreeNode>
{
    public void Configure(EntityTypeBuilder<RScriptTreeNode> entity)
    {
        entity.ToTable("RSCRIPT_TREE", "R");

        entity.Property(e => e.Color)
            .HasMaxLength(50)
            .IsUnicode(false);

        entity.Property(e => e.Description)
            .HasMaxLength(255)
            .IsUnicode(false);

        entity.Property(e => e.Icon)
            .HasMaxLength(50)
            .IsUnicode(false);

        entity.Property(e => e.IdRScript).HasColumnName("IdRScript");

        entity.Property(e => e.Modules)
            .HasMaxLength(255)
            .IsUnicode(false);

        entity.Property(e => e.Name)
            .HasMaxLength(120)
            .IsUnicode(false);

        entity.HasOne(d => d.IdRScriptNavigation)
            .WithMany(p => p.RScriptTree)
            .HasForeignKey(d => d.IdRScript)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_R_RSCRIPT_TREE_RSCRIPTS");

        entity.Ignore(e => e.Audits);
    }
}
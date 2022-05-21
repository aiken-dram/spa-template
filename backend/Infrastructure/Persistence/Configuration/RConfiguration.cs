using Domain.Entities;
using IBM.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration;

public class RScriptConfiguration : IEntityTypeConfiguration<Domain.Entities.RScript>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.RScript> entity)
    {
        entity.HasKey(e => e.IdRScript)
            .HasName("RSCRIPTS_PK")
            .ForDb2IsClustered(false);

        entity.ToTable("RSCRIPTS", "R");

        entity.Property(e => e.IdRScript)
            .HasColumnType("bigint(8)")
            .HasColumnName("ID_RSCRIPT");

        entity.Property(e => e.ScriptFile)
            .HasMaxLength(255)
            .HasPrecision(255)
            .IsUnicode(false)
            .HasColumnName("SCRIPT_FILE");

        entity.Property(e => e.Name)
            .HasMaxLength(120)
            .HasPrecision(120)
            .IsUnicode(false)
            .HasColumnName("NAME");

        entity.Property(e => e.ContentType)
            .HasMaxLength(50)
            .HasPrecision(50)
            .IsUnicode(false)
            .HasColumnName("CONTENT_TYPE");

        entity.Property(e => e.ResultFile)
            .HasMaxLength(200)
            .HasPrecision(200)
            .IsUnicode(false)
            .HasColumnName("RESULT_FILE");

        entity.Property(e => e.Description)
            .HasMaxLength(255)
            .HasPrecision(255)
            .IsUnicode(false)
            .HasColumnName("DESC");

        entity.Ignore(e => e.Audits);
    }
}

public class RScriptParamConfiguration : IEntityTypeConfiguration<RScriptParam>
{
    public void Configure(EntityTypeBuilder<RScriptParam> entity)
    {
        entity.HasKey(e => e.Id)
            .HasName("RSCRIPT_PARAMS_PK")
            .ForDb2IsClustered(false);

        entity.ToTable("RSCRIPT_PARAMS", "R");

        entity.Property(e => e.Id)
            .HasColumnType("bigint(8)")
            .HasColumnName("ID");

        entity.Property(e => e.IdRScript)
            .HasColumnType("bigint(8)")
            .HasColumnName("ID_RSCRIPT");

        entity.Property(e => e.IdType)
            .HasColumnType("integer(4)")
            .HasColumnName("ID_TYPE");

        entity.Property(e => e.Name)
            .HasMaxLength(120)
            .HasPrecision(120)
            .IsUnicode(false)
            .HasColumnName("NAME");

        entity.Property(e => e.Hint)
            .HasMaxLength(255)
            .HasPrecision(255)
            .IsUnicode(false)
            .HasColumnName("HINT");

        entity.Property(e => e.Description)
            .HasMaxLength(255)
            .HasPrecision(255)
            .IsUnicode(false)
            .HasColumnName("DESC");

        entity.HasOne(d => d.IdTypeNavigation)
            .WithMany(p => p.RScriptParams)
            .HasForeignKey(d => d.IdType)
            .HasConstraintName("RSCRIPT_PARAM_RSCRIPT_PARAM_TYPES_FK");

        entity.HasOne(d => d.IdRScriptNavigation)
            .WithMany(p => p.RScriptParams)
            .HasForeignKey(d => d.IdRScript)
            .HasConstraintName("RSCRIPT_PARAM_RSCRIPTS_FK");
    }
}

public class RScriptTreeConfiguration : IEntityTypeConfiguration<RScriptTreeNode>
{
    public void Configure(EntityTypeBuilder<RScriptTreeNode> entity)
    {
        entity.HasKey(e => e.Id)
            .HasName("RSCRIPT_TREE_PK")
            .ForDb2IsClustered(false);

        entity.ToTable("RSCRIPT", "R");

        entity.Property(e => e.Id)
            .HasColumnType("bigint(8)")
            .HasColumnName("ID");

        entity.Property(e => e.IdParent)
            .HasColumnType("bigint(8)")
            .HasColumnName("ID_PARENT");

        entity.Property(e => e.IdRScript)
            .HasColumnType("bigint(8)")
            .HasColumnName("ID_RSCRIPT");

        entity.Property(e => e.Name)
            .HasMaxLength(120)
            .HasPrecision(120)
            .IsUnicode(false)
            .HasColumnName("NAME");

        entity.Property(e => e.Modules)
            .HasMaxLength(255)
            .HasPrecision(255)
            .IsUnicode(false)
            .HasColumnName("MODULES");

        entity.Property(e => e.Icon)
            .HasMaxLength(50)
            .HasPrecision(50)
            .IsUnicode(false)
            .HasColumnName("ICON");

        entity.Property(e => e.Color)
            .HasMaxLength(50)
            .HasPrecision(50)
            .IsUnicode(false)
            .HasColumnName("COLOR");

        entity.Property(e => e.Description)
            .HasMaxLength(255)
            .HasPrecision(255)
            .IsUnicode(false)
            .HasColumnName("DESC");

        entity.HasOne(d => d.IdRScriptNavigation)
            .WithMany(p => p.RScriptTree)
            .HasForeignKey(d => d.IdRScript)
            .HasConstraintName("RSCRIPT_TREE_RSCRIPTS_FK");

        entity.Ignore(e => e.Audits);
    }
}

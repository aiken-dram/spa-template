using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration;

#warning This is example, remove entire file in actual application
public class SampleConfiguration : IEntityTypeConfiguration<Sample>
{
    public void Configure(EntityTypeBuilder<Sample> entity)
    {
        entity.HasKey(e => e.IdSample)
            .HasName("PK_SAMPLE_SAMPLE");

        entity.ToTable("SAMPLE", "SAMPLE");

        entity.Property(e => e.Date).HasColumnType("date");

        entity.Property(e => e.Sum).HasColumnType("decimal(10, 2)");

        entity.Property(e => e.Text)
            .HasMaxLength(120)
            .IsUnicode(false);

        entity.Property(e => e.Timestamp).HasColumnType("datetime");

        entity.HasOne(d => d.IdDictNavigation)
            .WithMany(p => p.Samples)
            .HasForeignKey(d => d.IdDict)
            .HasConstraintName("FK_SAMPLE_SAMPLE_SAMPLE_DICTS");

        entity.HasOne(d => d.IdDistrictNavigation)
            .WithMany(p => p.Samples)
            .HasForeignKey(d => d.IdDistrict)
            .HasConstraintName("FK_SAMPLE_SAMPLE_DISTRICTS");

        entity.HasOne(d => d.IdTypeNavigation)
            .WithMany(p => p.Samples)
            .HasForeignKey(d => d.IdType)
            .HasConstraintName("FK_SAMPLE_SAMPLE_SAMPLE_TYPES");

        entity.Ignore(e => e.Audits);
        entity.Ignore(e => e.DomainEvents);
    }
}

public class SampleChildConfiguration : IEntityTypeConfiguration<SampleChild>
{
    public void Configure(EntityTypeBuilder<SampleChild> entity)
    {
        entity.HasKey(e => e.IdChild)
            .HasName("PK_SAMPLE_SAMPLE_CHILD");

        entity.ToTable("SAMPLE_CHILD", "SAMPLE");

        entity.Property(e => e.Text)
            .HasMaxLength(120)
            .IsUnicode(false);

        entity.HasOne(d => d.IdSampleNavigation)
            .WithMany(p => p.SampleChildren)
            .HasForeignKey(d => d.IdSample)
            .HasConstraintName("FK_SAMPLE_SAMPLE_CHILD_SAMPLE");
    }
}

public class SampleAuditConfiguration : IEntityTypeConfiguration<SampleAudit>
{
    public void Configure(EntityTypeBuilder<SampleAudit> entity)
    {
        entity.HasKey(e => e.IdAudit)
            .HasName("PK_SAMPLE_SAMPLE_AUDIT");

        entity.ToTable("SAMPLE_AUDIT", "SAMPLE");

        entity.Property(e => e.Message)
            .HasMaxLength(255)
            .IsUnicode(false);

        entity.Property(e => e.Stamp).HasColumnType("datetime");

        entity.Property(e => e.TargetName)
            .HasMaxLength(255)
            .IsUnicode(false);

        entity.HasOne(d => d.IdActionNavigation)
            .WithMany(p => p.SampleAudits)
            .HasForeignKey(d => d.IdAction)
            .HasConstraintName("FK_SAMPLE_SAMPLE_AUDIT_AUDIT_ACTIONS");

        entity.HasOne(d => d.IdTargetNavigation)
            .WithMany(p => p.SampleAudits)
            .HasForeignKey(d => d.IdTarget)
            .HasConstraintName("FK_SAMPLE_SAMPLE_AUDIT_AUDIT_TARGETS");

        entity.HasOne(d => d.IdUserNavigation)
            .WithMany(p => p.SampleAudits)
            .HasForeignKey(d => d.IdUser)
            .HasConstraintName("FK_SAMPLE_SAMPLE_AUDIT_USERS");

        entity.HasOne(d => d.TargetIdNavigation)
            .WithMany(p => p.SampleAudits)
            .HasForeignKey(d => d.TargetId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_SAMPLE_SAMPLE_AUDIT_SAMPLE");

        entity.Ignore(e => e.AuditData);
    }
}

public class SampleAuditDataConfiguration : IEntityTypeConfiguration<SampleAuditData>
{
    public void Configure(EntityTypeBuilder<SampleAuditData> entity)
    {
        entity.ToTable("SAMPLE_AUDIT_DATA", "SAMPLE");

        entity.Property(e => e.Json)
            .HasMaxLength(255)
            .IsUnicode(false)
            .HasColumnName("JSON");

        entity.HasOne(d => d.IdAuditNavigation)
            .WithMany(p => p.SampleAuditData)
            .HasForeignKey(d => d.IdAudit)
            .HasConstraintName("FK_SAMPLE_SAMPLE_AUDIT_DATA_SAMPLE");

        entity.HasOne(d => d.IdTypeNavigation)
            .WithMany(p => p.SampleAuditData)
            .HasForeignKey(d => d.IdType)
            .HasConstraintName("FK_SAMPLE_SAMPLE_AUDIT_DATA_AUDIT_DATA_TYPES");
    }
}
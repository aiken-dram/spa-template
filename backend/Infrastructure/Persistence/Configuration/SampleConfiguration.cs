using Domain.Entities;
using IBM.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration;

#warning This is example, remove entire file in actual application
public class SampleConfiguration : IEntityTypeConfiguration<Sample>
{
    public void Configure(EntityTypeBuilder<Sample> entity)
    {
        entity.HasKey(e => e.IdSample)
                    .HasName("SAMPLE_PK")
                    .ForDb2IsClustered(false);

        entity.ToTable("SAMPLE", "SAMPLE");

        entity.Property(e => e.IdSample)
            .HasColumnType("bigint(8)")
            .HasColumnName("ID_SAMPLE");

        entity.Property(e => e.Date)
            .HasColumnType("date(4)")
            .HasColumnName("DATE");

        entity.Property(e => e.IdDict)
            .HasColumnType("bigint(8)")
            .HasColumnName("ID_DICT");

        entity.Property(e => e.IdType)
            .HasColumnType("integer(4)")
            .HasColumnName("ID_TYPE");

        entity.Property(e => e.Number)
            .HasColumnType("bigint(8)")
            .HasColumnName("NUMBER");

        entity.Property(e => e.Sum)
            .HasColumnType("decimal(10, 2)")
            .HasColumnName("SUM");

        entity.Property(e => e.Text)
            .HasMaxLength(120)
            .HasPrecision(120)
            .IsUnicode(false)
            .HasColumnName("TEXT");

        entity.Property(e => e.TimeStamp)
            .HasMaxLength(10)
            .HasPrecision(10)
            .HasColumnName("TIMESTAMP");

        entity.HasOne(d => d.IdDictNavigation)
            .WithMany(p => p.Samples)
            .HasForeignKey(d => d.IdDict)
            .HasConstraintName("SAMPLE_SAMPLE_DICTS_FK");

        entity.HasOne(d => d.IdTypeNavigation)
            .WithMany(p => p.Samples)
            .HasForeignKey(d => d.IdType)
            .HasConstraintName("SAMPLE_SAMPLE_TYPES_FK");

        entity.Ignore(e => e.Audits);
        entity.Ignore(e => e.DomainEvents);
    }
}

public class SampleChildConfiguration : IEntityTypeConfiguration<SampleChild>
{
    public void Configure(EntityTypeBuilder<SampleChild> entity)
    {
        entity.HasKey(e => e.IdChild)
            .HasName("SAMPLE_CHILD_PK")
            .ForDb2IsClustered(false);

        entity.ToTable("SAMPLE_CHILD", "SAMPLE");

        entity.Property(e => e.IdChild)
            .HasColumnType("bigint(8)")
            .HasColumnName("ID_CHILD");

        entity.Property(e => e.IdSample)
            .HasColumnType("bigint(8)")
            .HasColumnName("ID_SAMPLE");

        entity.Property(e => e.Text)
            .HasMaxLength(120)
            .HasPrecision(120)
            .IsUnicode(false)
            .HasColumnName("TEXT");

        entity.HasOne(d => d.IdSampleNavigation)
            .WithMany(p => p.SampleChildren)
            .HasForeignKey(d => d.IdSample)
            .HasConstraintName("SAMPLE_CHILD_SAMPLE_FK");
    }
}

public class SampleAuditConfiguration : IEntityTypeConfiguration<SampleAudit>
{
    public void Configure(EntityTypeBuilder<SampleAudit> entity)
    {
        entity.HasKey(e => e.IdAudit)
                    .HasName("SAMPLE_AUDIT_PK")
                    .ForDb2IsClustered(false);

        entity.ToTable("SAMPLE_AUDIT", "SAMPLE");

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
            .WithMany(p => p.SampleAudits)
            .HasForeignKey(d => d.IdAction)
            .HasConstraintName("SAMPLE_AUDIT_AUDIT_ACTIONS_FK");

        entity.HasOne(d => d.IdTargetNavigation)
            .WithMany(p => p.SampleAudits)
            .HasForeignKey(d => d.IdTarget)
            .HasConstraintName("SAMPLE_AUDIT_AUDIT_TARGETS_FK");

        entity.HasOne(d => d.IdUserNavigation)
            .WithMany(p => p.SampleAudits)
            .HasForeignKey(d => d.IdUser)
            .HasConstraintName("SAMPLE_AUDIT_USERS_FK");

        entity.HasOne(d => d.Target)
            .WithMany(p => p.SampleAudits)
            .HasForeignKey(d => d.TargetId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("SAMPLE_AUDIT_SAMPLE_FK");

        entity.Ignore(e => e.AuditData);
    }
}

public class SampleAuditDataConfiguration : IEntityTypeConfiguration<SampleAuditData>
{
    public void Configure(EntityTypeBuilder<SampleAuditData> entity)
    {
        entity.HasKey(e => e.Id)
            .HasName("SAMPLE_AUDIT_DATA_PK")
            .ForDb2IsClustered(false);

        entity.ToTable("SAMPLE_AUDIT_DATA", "SAMPLE");

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

        entity.HasOne(d => d.IdAuditNavigation)
            .WithMany(p => p.SampleAuditData)
            .HasForeignKey(d => d.IdAudit)
            .HasConstraintName("SAMPLE_AUDIT_DATA_SAMPLE_AUDIT_FK");

        entity.HasOne(d => d.IdTypeNavigation)
            .WithMany(p => p.SampleAuditData)
            .HasForeignKey(d => d.IdType)
            .HasConstraintName("SAMPLE_AUDIT_DATA_AUDIT_DATA_TYPES_FK");
    }
}
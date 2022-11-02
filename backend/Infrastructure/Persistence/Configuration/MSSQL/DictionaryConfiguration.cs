using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Infrastructure.Persistence.Configuration;

public class RequestTypeConfiguration : IEntityTypeConfiguration<RequestType>
{
    public void Configure(EntityTypeBuilder<RequestType> entity)
    {
        entity.HasKey(e => e.IdType)
            .HasName("PK_DICT_REQUEST_TYPES");

        entity.ToTable("REQUEST_TYPES", "DICT");

        entity.Property(e => e.IdType).ValueGeneratedNever();

        entity.Property(e => e.Description)
            .HasMaxLength(255)
            .IsUnicode(false);

        entity.Property(e => e.Type)
            .HasMaxLength(120)
            .IsUnicode(false);
    }
}

public class RequestStateConfiguration : IEntityTypeConfiguration<RequestState>
{
    public void Configure(EntityTypeBuilder<RequestState> entity)
    {
        entity.HasKey(e => e.IdState)
            .HasName("PK_DICT_REQUEST_STATES");

        entity.ToTable("REQUEST_STATES", "DICT");

        entity.Property(e => e.IdState).ValueGeneratedNever();

        entity.Property(e => e.Description)
            .HasMaxLength(255)
            .IsUnicode(false);

        entity.Property(e => e.State)
            .HasMaxLength(120)
            .IsUnicode(false);
    }
}

public class AuditDataTypeConfiguration : IEntityTypeConfiguration<AuditDataType>
{
    public void Configure(EntityTypeBuilder<AuditDataType> entity)
    {
        entity.HasKey(e => e.IdType)
            .HasName("PK_DICT_AUDIT_DATA_TYPES");

        entity.ToTable("AUDIT_DATA_TYPES", "DICT");

        entity.Property(e => e.IdType).ValueGeneratedNever();

        entity.Property(e => e.Description)
            .HasMaxLength(255)
            .IsUnicode(false);

        entity.Property(e => e.Type)
            .HasMaxLength(120)
            .IsUnicode(false);
    }
}

public class AuditActionConfiguration : IEntityTypeConfiguration<AuditAction>
{
    public void Configure(EntityTypeBuilder<AuditAction> entity)
    {
        entity.HasKey(e => e.IdAction)
            .HasName("PK_DICT_AUDIT_ACTIONS");

        entity.ToTable("AUDIT_ACTIONS", "DICT");

        entity.Property(e => e.IdAction).ValueGeneratedNever();

        entity.Property(e => e.Action)
            .HasMaxLength(120)
            .IsUnicode(false);

        entity.Property(e => e.Description)
            .HasMaxLength(255)
            .IsUnicode(false);
    }
}

public class AuditTargetConfiguration : IEntityTypeConfiguration<AuditTarget>
{
    public void Configure(EntityTypeBuilder<AuditTarget> entity)
    {
        entity.HasKey(e => e.IdTarget)
            .HasName("PK_DICT_AUDIT_TARGETS");

        entity.ToTable("AUDIT_TARGETS", "DICT");

        entity.Property(e => e.IdTarget).ValueGeneratedNever();

        entity.Property(e => e.Description)
            .HasMaxLength(255)
            .IsUnicode(false);

        entity.Property(e => e.Target)
            .HasMaxLength(120)
            .IsUnicode(false);
    }
}

public class DistrictConfiguration : IEntityTypeConfiguration<District>
{
    public void Configure(EntityTypeBuilder<District> entity)
    {
        entity.HasKey(e => e.IdDistrict)
            .HasName("PK_DICT_DISTRICTS");

        entity.ToTable("DISTRICTS", "DICT");

        entity.Property(e => e.IdDistrict).ValueGeneratedNever();

        entity.Property(e => e.Name)
            .HasMaxLength(200)
            .IsUnicode(false);

        entity.Ignore(e => e.Audits);
    }
}

public class RScriptParamTypeConfiguration : IEntityTypeConfiguration<RScriptParamType>
{
    public void Configure(EntityTypeBuilder<RScriptParamType> entity)
    {
        entity.HasKey(e => e.IdType)
            .HasName("PK_DICT_RSCRIPT_PARAM_TYPES");

        entity.ToTable("RSCRIPT_PARAM_TYPES", "DICT");

        entity.Property(e => e.IdType).ValueGeneratedNever();

        entity.Property(e => e.Description)
            .HasMaxLength(255)
            .IsUnicode(false);

        entity.Property(e => e.Type)
            .HasMaxLength(120)
            .IsUnicode(false);
    }
}

#warning This is example, remove in actual application
public class SampleTypeConfiguration : IEntityTypeConfiguration<SampleType>
{
    public void Configure(EntityTypeBuilder<SampleType> entity)
    {
        entity.HasKey(e => e.IdType)
            .HasName("PK_DICT_SAMPLE_TYPES");

        entity.ToTable("SAMPLE_TYPES", "DICT");

        entity.Property(e => e.IdType).ValueGeneratedNever();

        entity.Property(e => e.Description)
            .HasMaxLength(255)
            .IsUnicode(false);

        entity.Property(e => e.Type)
            .HasMaxLength(120)
            .IsUnicode(false);
    }
}

#warning This is example, remove in actual application
public class SampleDictConfiguration : IEntityTypeConfiguration<SampleDict>
{
    public void Configure(EntityTypeBuilder<SampleDict> entity)
    {
        entity.HasKey(e => e.IdDict)
            .HasName("PK_DICT_SAMPLE_DICTS");

        entity.ToTable("SAMPLE_DICTS", "DICT");

        entity.Property(e => e.Description)
            .HasMaxLength(255)
            .IsUnicode(false);

        entity.Property(e => e.Dict)
            .HasMaxLength(120)
            .IsUnicode(false);

        entity.Ignore(e => e.Audits);
    }
}
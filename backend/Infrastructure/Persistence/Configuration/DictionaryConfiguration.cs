using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;
using IBM.EntityFrameworkCore;

namespace Infrastructure.Persistence.Configuration;

public class RequestTypeConfiguration : IEntityTypeConfiguration<RequestType>
{
    public void Configure(EntityTypeBuilder<RequestType> entity)
    {
        entity.HasKey(e => e.IdType)
            .HasName("REQUEST_TYPES_PK")
            .ForDb2IsClustered(false);

        entity.ToTable("REQUEST_TYPES", "DICT");

        entity.Property(e => e.IdType)
            .HasColumnType("integer(4)")
            .ValueGeneratedNever()
            .HasColumnName("ID_TYPE");

        entity.Property(e => e.Description)
            .HasMaxLength(255)
            .HasPrecision(255)
            .IsUnicode(false)
            .HasColumnName("DESC");

        entity.Property(e => e.Type)
            .HasMaxLength(100)
            .HasPrecision(100)
            .IsUnicode(false)
            .HasColumnName("TYPE");
    }
}

public class RequestStateConfiguration : IEntityTypeConfiguration<RequestState>
{
    public void Configure(EntityTypeBuilder<RequestState> entity)
    {
        entity.HasKey(e => e.IdState)
            .HasName("REQUEST_STATES_PK")
            .ForDb2IsClustered(false);

        entity.ToTable("REQUEST_STATES", "DICT");

        entity.Property(e => e.IdState)
            .HasColumnType("integer(4)")
            .ValueGeneratedNever()
            .HasColumnName("ID_STATE");

        entity.Property(e => e.Description)
            .HasMaxLength(255)
            .HasPrecision(255)
            .IsUnicode(false)
            .HasColumnName("DESC");

        entity.Property(e => e.State)
            .HasMaxLength(100)
            .HasPrecision(100)
            .IsUnicode(false)
            .HasColumnName("STATE");
    }
}

public class AuditDataTypeConfiguration : IEntityTypeConfiguration<AuditDataType>
{
    public void Configure(EntityTypeBuilder<AuditDataType> entity)
    {
        entity.HasKey(e => e.IdType)
            .HasName("AUDIT_DATA_TYPES_PK")
            .ForDb2IsClustered(false);

        entity.ToTable("AUDIT_DATA_TYPES", "DICT");

        entity.Property(e => e.IdType)
            .HasColumnType("integer(4)")
            .ValueGeneratedNever()
            .HasColumnName("ID_TYPE");

        entity.Property(e => e.Description)
            .HasMaxLength(255)
            .HasPrecision(255)
            .IsUnicode(false)
            .HasColumnName("DESC");

        entity.Property(e => e.Type)
            .HasMaxLength(120)
            .HasPrecision(120)
            .IsUnicode(false)
            .HasColumnName("TYPE");
    }
}

public class AuditActionConfiguration : IEntityTypeConfiguration<AuditAction>
{
    public void Configure(EntityTypeBuilder<AuditAction> entity)
    {
        entity.HasKey(e => e.IdAction)
            .HasName("AUDIT_ACTIONS_PK")
            .ForDb2IsClustered(false);

        entity.ToTable("AUDIT_ACTIONS", "DICT");

        entity.Property(e => e.IdAction)
            .HasColumnType("integer(4)")
            .ValueGeneratedNever()
            .HasColumnName("ID_ACTION");

        entity.Property(e => e.Action)
            .HasMaxLength(120)
            .HasPrecision(120)
            .IsUnicode(false)
            .HasColumnName("ACTION");

        entity.Property(e => e.Description)
            .HasMaxLength(255)
            .HasPrecision(255)
            .IsUnicode(false)
            .HasColumnName("DESC");
    }
}

public class AuditTargetConfiguration : IEntityTypeConfiguration<AuditTarget>
{
    public void Configure(EntityTypeBuilder<AuditTarget> entity)
    {
        entity.HasKey(e => e.IdTarget)
            .HasName("AUDIT_TARGETS_PK")
            .ForDb2IsClustered(false);

        entity.ToTable("AUDIT_TARGETS", "DICT");

        entity.Property(e => e.IdTarget)
            .HasColumnType("integer(4)")
            .ValueGeneratedNever()
            .HasColumnName("ID_TARGET");

        entity.Property(e => e.Description)
            .HasMaxLength(255)
            .HasPrecision(255)
            .IsUnicode(false)
            .HasColumnName("DESC");

        entity.Property(e => e.Target)
            .HasMaxLength(120)
            .HasPrecision(120)
            .IsUnicode(false)
            .HasColumnName("TARGET");
    }
}

public class DistrictConfiguration : IEntityTypeConfiguration<District>
{
    public void Configure(EntityTypeBuilder<District> entity)
    {
        entity.HasKey(e => e.IdDistrict)
            .HasName("DISTRICTS_PK")
            .ForDb2IsClustered(false);

        entity.ToTable("DISTRICTS", "DICT");

        entity.Property(e => e.IdDistrict)
            .HasColumnType("integer(4)")
            .ValueGeneratedNever()
            .HasColumnName("ID_DISTRICT");

        entity.Property(e => e.Name)
            .HasMaxLength(200)
            .HasPrecision(200)
            .IsUnicode(false)
            .HasColumnName("NAME");

        entity.Ignore(e => e.Audits);
    }
}

public class RScriptParamTypeConfiguration : IEntityTypeConfiguration<RScriptParamType>
{
    public void Configure(EntityTypeBuilder<RScriptParamType> entity)
    {
        entity.HasKey(e => e.IdType)
            .HasName("RSCRIPT_PARAM_TYPES_PK")
            .ForDb2IsClustered(false);

        entity.ToTable("RSCRIPT_PARAM_TYPES", "DICT");

        entity.Property(e => e.IdType)
            .HasColumnType("integer(4)")
            .ValueGeneratedNever()
            .HasColumnName("ID_TYPE");

        entity.Property(e => e.Description)
            .HasMaxLength(255)
            .HasPrecision(255)
            .IsUnicode(false)
            .HasColumnName("DESC");

        entity.Property(e => e.Type)
            .HasMaxLength(120)
            .HasPrecision(120)
            .IsUnicode(false)
            .HasColumnName("TYPE");
    }
}

#warning This is example, remove in actual application
public class SampleTypeConfiguration : IEntityTypeConfiguration<SampleType>
{
    public void Configure(EntityTypeBuilder<SampleType> entity)
    {
        entity.HasKey(e => e.IdType)
            .HasName("SAMPLE_TYPES_PK")
            .ForDb2IsClustered(false);

        entity.ToTable("SAMPLE_TYPES", "DICT");

        entity.Property(e => e.IdType)
            .HasColumnType("integer(4)")
            .ValueGeneratedNever()
            .HasColumnName("ID_TYPE");

        entity.Property(e => e.Description)
            .HasMaxLength(255)
            .HasPrecision(255)
            .IsUnicode(false)
            .HasColumnName("DESC");

        entity.Property(e => e.Type)
            .HasMaxLength(120)
            .HasPrecision(120)
            .IsUnicode(false)
            .HasColumnName("TYPE");
    }
}

#warning This is example, remove in actual application
public class SampleDictConfiguration : IEntityTypeConfiguration<SampleDict>
{
    public void Configure(EntityTypeBuilder<SampleDict> entity)
    {
        entity.HasKey(e => e.IdDict)
            .HasName("SAMPLE_DICTS_PK")
            .ForDb2IsClustered(false);

        entity.ToTable("SAMPLE_DICTS", "DICT");

        entity.Property(e => e.IdDict)
            .HasColumnType("bigint(8)")
            .HasColumnName("ID_DICT");

        entity.Property(e => e.Dict)
            .HasMaxLength(120)
            .HasPrecision(120)
            .IsUnicode(false)
            .HasColumnName("DICT");

        entity.Property(e => e.Description)
            .HasMaxLength(255)
            .HasPrecision(255)
            .IsUnicode(false)
            .HasColumnName("DESC");

        entity.Ignore(e => e.Audits);
    }
}
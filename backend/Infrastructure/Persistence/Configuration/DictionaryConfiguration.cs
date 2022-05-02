using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Infrastructure.Persistence.Configuration;

public class RequestTypeConfiguration : IEntityTypeConfiguration<RequestType>
{
    public void Configure(EntityTypeBuilder<RequestType> entity)
    {
        entity.HasKey(e => e.IdType);

        entity.ToTable("REQUEST_TYPES", "DICT");

        entity.Property(e => e.IdType).HasColumnName("ID_TYPE");

        entity.Property(e => e.Description)
            .HasMaxLength(255)
            .HasColumnName("DESC");

        entity.Property(e => e.Type)
            .HasMaxLength(120)
            .HasColumnName("TYPE");
    }
}

public class RequestStateConfiguration : IEntityTypeConfiguration<RequestState>
{
    public void Configure(EntityTypeBuilder<RequestState> entity)
    {
        entity.HasKey(e => e.IdState);

        entity.ToTable("REQUEST_STATES", "DICT");

        entity.Property(e => e.IdState).HasColumnName("ID_STATE");

        entity.Property(e => e.Description)
            .HasMaxLength(255)
            .HasColumnName("DESC");

        entity.Property(e => e.State)
            .HasMaxLength(120)
            .HasColumnName("STATE");
    }
}

public class EventTargetConfiguration : IEntityTypeConfiguration<EventTarget>
{
    public void Configure(EntityTypeBuilder<EventTarget> entity)
    {
        entity.HasKey(e => e.IdTarget);

        entity.ToTable("EVENT_TARGETS", "DICT");

        entity.Property(e => e.IdTarget)
            .ValueGeneratedNever()
            .HasColumnName("ID_TARGET");

        entity.Property(e => e.Description)
            .HasMaxLength(255)
            .HasColumnName("DESC");

        entity.Property(e => e.Target)
            .HasMaxLength(120)
            .HasColumnName("TARGET");
    }
}

public class EventActionConfiguration : IEntityTypeConfiguration<EventAction>
{
    public void Configure(EntityTypeBuilder<EventAction> entity)
    {
        entity.HasKey(e => e.IdAction);

        entity.ToTable("EVENT_ACTIONS", "DICT");

        entity.Property(e => e.IdAction)
            .ValueGeneratedNever()
            .HasColumnName("ID_ACTION");

        entity.Property(e => e.Action)
            .HasMaxLength(120)
            .HasColumnName("ACTION");

        entity.Property(e => e.Description)
            .HasMaxLength(255)
            .HasColumnName("DESC");
    }
}

public class EventDataTypeConfiguration : IEntityTypeConfiguration<EventDataType>
{
    public void Configure(EntityTypeBuilder<EventDataType> entity)
    {
        entity.HasKey(e => e.IdType);

        entity.ToTable("EVENT_DATA_TYPES", "DICT");

        entity.Property(e => e.IdType)
            .ValueGeneratedNever()
            .HasColumnName("ID_TYPE");

        entity.Property(e => e.Description)
            .HasMaxLength(255)
            .HasColumnName("DESC");

        entity.Property(e => e.Type)
            .HasMaxLength(120)
            .HasColumnName("TYPE");
    }
}

public class DistrictConfiguration : IEntityTypeConfiguration<District>
{
    public void Configure(EntityTypeBuilder<District> entity)
    {
        entity.HasKey(e => e.IdDistrict);

        entity.ToTable("DISTRICTS", "DICT");

        entity.Property(e => e.IdDistrict)
            .ValueGeneratedNever()
            .HasColumnName("ID_DISTRICT");

        entity.Property(e => e.Name)
            .HasMaxLength(200)
            .HasColumnName("NAME");
    }
}

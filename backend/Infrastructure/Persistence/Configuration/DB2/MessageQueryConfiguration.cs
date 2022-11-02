/*using IBM.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration;

public class RequestConfiguration : IEntityTypeConfiguration<Domain.Entities.Request>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Request> entity)
    {
        entity.HasKey(e => e.IdRequest)
            .HasName("REQUESTS_PK")
            .ForDb2IsClustered(false);

        entity.ToTable("REQUESTS", "MQ");

        entity.Property(e => e.IdRequest)
            .HasColumnType("bigint(8)")
            .HasColumnName("ID_REQUEST");

        entity.Property(e => e.Created)
            .HasMaxLength(10)
            .HasPrecision(10)
            .HasColumnName("CREATED");

        entity.Property(e => e.Delivered)
            .HasMaxLength(10)
            .HasPrecision(10)
            .HasColumnName("DELIVERED");

        entity.Property(e => e.Guid)
            .HasMaxLength(100)
            .HasPrecision(100)
            .IsUnicode(false)
            .HasColumnName("GUID");

        entity.Property(e => e.IdState)
            .HasColumnType("integer(4)")
            .HasColumnName("ID_STATE");

        entity.Property(e => e.IdType)
            .HasColumnType("integer(4)")
            .HasColumnName("ID_TYPE");

        entity.Property(e => e.IdUser)
            .HasColumnType("bigint(8)")
            .HasColumnName("ID_USER");

        entity.Property(e => e.Message)
            .HasMaxLength(500)
            .HasPrecision(500)
            .IsUnicode(false)
            .HasColumnName("MESSAGE");

        entity.Property(e => e.Json)
            .HasMaxLength(500)
            .HasPrecision(500)
            .IsUnicode(false)
            .HasColumnName("JSON");

        entity.Property(e => e.Processed)
            .HasMaxLength(10)
            .HasPrecision(10)
            .HasColumnName("PROCESSED");

        entity.HasOne(d => d.IdStateNavigation)
            .WithMany(p => p.Requests)
            .HasForeignKey(d => d.IdState)
            .HasConstraintName("REQUESTS_REQUEST_STATES_FK");

        entity.HasOne(d => d.IdTypeNavigation)
            .WithMany(p => p.Requests)
            .HasForeignKey(d => d.IdType)
            .HasConstraintName("REQUESTS_REQUEST_TYPES_FK");

        entity.HasOne(d => d.IdUserNavigation)
            .WithMany(p => p.Requests)
            .HasForeignKey(d => d.IdUser)
            .HasConstraintName("REQUESTS_USERS_FK");

        entity.Ignore(e => e.DomainEvents);
        entity.Ignore(e => e.Audits);
    }
}
*/
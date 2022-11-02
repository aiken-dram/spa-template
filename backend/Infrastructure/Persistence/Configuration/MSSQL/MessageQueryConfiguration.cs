using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration;

public class RequestConfiguration : IEntityTypeConfiguration<Domain.Entities.Request>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Request> entity)
    {
        entity.HasKey(e => e.IdRequest)
            .HasName("PK_MQ_REQUESTS");

        entity.ToTable("REQUESTS", "MQ");

        entity.Property(e => e.Created).HasColumnType("date");

        entity.Property(e => e.Delivered).HasColumnType("date");

        entity.Property(e => e.Guid)
            .HasMaxLength(100)
            .IsUnicode(false)
            .HasColumnName("GUID");

        entity.Property(e => e.Json)
            .HasMaxLength(500)
            .IsUnicode(false)
            .HasColumnName("JSON");

        entity.Property(e => e.Message)
            .HasMaxLength(500)
            .IsUnicode(false);

        entity.Property(e => e.Processed).HasColumnType("date");

        entity.HasOne(d => d.IdStateNavigation)
            .WithMany(p => p.Requests)
            .HasForeignKey(d => d.IdState)
            .HasConstraintName("FK_MQ_REQUESTS_REQUEST_STATES");

        entity.HasOne(d => d.IdTypeNavigation)
            .WithMany(p => p.Requests)
            .HasForeignKey(d => d.IdType)
            .HasConstraintName("FK_MQ_REQUESTS_REQUEST_TYPES");

        entity.HasOne(d => d.IdUserNavigation)
            .WithMany(p => p.Requests)
            .HasForeignKey(d => d.IdUser)
            .HasConstraintName("FK_MQ_REQUESTS_USERS");

        entity.Ignore(e => e.Audits);
    }
}
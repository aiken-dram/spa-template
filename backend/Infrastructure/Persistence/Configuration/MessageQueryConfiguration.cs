using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public class DocumentConfiguration : IEntityTypeConfiguration<Domain.Entities.Request>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Request> entity)
        {
            entity.HasKey(e => e.IdRequest);

            entity.ToTable("REQUESTS", "MQ");

            entity.Property(e => e.IdRequest).HasColumnName("ID_REQUEST");

            entity.Property(e => e.Created)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("CREATED");

            entity.Property(e => e.Delivered)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("DELIVERED");

            entity.Property(e => e.Guid)
                .HasMaxLength(100)
                .HasColumnName("GUID");

            entity.Property(e => e.IdState).HasColumnName("ID_STATE");

            entity.Property(e => e.IdType).HasColumnName("ID_TYPE");

            entity.Property(e => e.IdUser).HasColumnName("ID_USER");

            entity.Property(e => e.Json)
                .HasMaxLength(500)
                .HasColumnName("JSON");

            entity.Property(e => e.Message)
                .HasMaxLength(500)
                .HasColumnName("MESSAGE");

            entity.Property(e => e.Processed)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("PROCESSED");

            entity.HasOne(d => d.IdStateNavigation)
                .WithMany(p => p.Requests)
                .HasForeignKey(d => d.IdState)
                .HasConstraintName("FK_REQUESTS_REQUEST_STATE");

            entity.HasOne(d => d.IdTypeNavigation)
                .WithMany(p => p.Requests)
                .HasForeignKey(d => d.IdType)
                .HasConstraintName("FK_REQUESTS_REQUEST_TYPE");

            entity.HasOne(d => d.IdUserNavigation)
                .WithMany(p => p.Requests)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("FK_REQUESTS_USER");
        }
    }
}
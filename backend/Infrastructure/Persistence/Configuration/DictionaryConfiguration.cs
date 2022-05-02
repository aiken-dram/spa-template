using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Infrastructure.Persistence.Configuration
{
    public class RequestTypesConfiguration : IEntityTypeConfiguration<RequestType>
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

    public class RequestStatesConfiguration : IEntityTypeConfiguration<RequestState>
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
}
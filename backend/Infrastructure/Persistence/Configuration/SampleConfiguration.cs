using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration;

#warning This is example, remove entire file in actual application
public class SampleConfiguration : IEntityTypeConfiguration<Sample>
{
    public void Configure(EntityTypeBuilder<Sample> entity)
    {

    }
}

public class SampleChildConfiguration : IEntityTypeConfiguration<SampleChild>
{
    public void Configure(EntityTypeBuilder<SampleChild> entity)
    {

    }
}

public class SampleAuditConfiguration : IEntityTypeConfiguration<SampleAudit>
{
    public void Configure(EntityTypeBuilder<SampleAudit> entity)
    {

    }
}

public class SampleAuditDataConfiguration : IEntityTypeConfiguration<SampleAuditData>
{
    public void Configure(EntityTypeBuilder<SampleAuditData> entity)
    {

    }
}
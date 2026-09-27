using Jobtri.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jobtri.Infrastructure.Persistence.Configurations;

public sealed class JobTargetConfiguration : IEntityTypeConfiguration<JobTarget>
{
    public void Configure(EntityTypeBuilder<JobTarget> builder)
    {
        builder.ToTable("job_targets");

        builder.HasKey(target => target.Id);

        builder.Property(target => target.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(target => target.Role)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(target => target.Country)
            .HasMaxLength(100);

        builder.Property(target => target.Domain)
            .HasMaxLength(100);

        builder.Property(target => target.CreatedAt)
            .IsRequired();

        builder.Property(target => target.IsEnabled)
            .IsRequired();
    }
}
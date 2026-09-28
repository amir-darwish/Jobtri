using Jobtri.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jobtri.Infrastructure.Persistence.Configurations;

public sealed class SavedJobConfiguration : IEntityTypeConfiguration<SavedJob>
{
    public void Configure(EntityTypeBuilder<SavedJob> builder)
    {
        builder.ToTable("saved_jobs");

        builder.HasKey(savedJob => savedJob.Id);

        builder.Property(savedJob => savedJob.SavedAt)
            .IsRequired();

        builder.Property(savedJob => savedJob.Notes)
            .HasMaxLength(1000);

        builder.HasOne<Job>()
            .WithMany()
            .HasForeignKey(savedJob => savedJob.JobId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
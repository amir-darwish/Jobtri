using Jobtri.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jobtri.Infrastructure.Persistence.Configurations;

public sealed class JobConfiguration : IEntityTypeConfiguration<Job>
{
    public void Configure(EntityTypeBuilder<Job> builder)
    {
        builder.ToTable("jobs");

        builder.HasKey(job => job.Id);

        builder.Property(job => job.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(job => job.CompanySourceId)
            .IsRequired();

        builder.Property(job => job.SourceJobId)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(job => job.JobUrl)
            .HasConversion<string>()
            .HasMaxLength(2048)
            .IsRequired();

        builder.Property(job => job.Location)
            .HasMaxLength(200);

        builder.Property(job => job.DatePosted);

        builder.Property(job => job.FirstSeenAt)
            .IsRequired();

        builder.HasIndex(job => new { job.CompanySourceId, job.SourceJobId })
            .IsUnique();

        builder.HasOne<CompanySource>()
            .WithMany()
            .HasForeignKey(job => job.CompanySourceId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
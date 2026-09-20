using Jobtri.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jobtri.Infrastructure.Persistence.Configurations
{
    public sealed class CompanySourceConfiguration : IEntityTypeConfiguration<CompanySource>
    {
        public void Configure(EntityTypeBuilder<CompanySource> builder)
        {
            builder.ToTable("company_sources");

            builder.HasKey(companySource => companySource.Id);

            builder.Property(companySource => companySource.CompanyId)
                .IsRequired();

            builder.Property(companySource => companySource.CareersUrl)
                .HasConversion<string>()
                .HasMaxLength(2048)
                .IsRequired();

            builder.Property(companySource => companySource.Ats)
                .IsRequired();

            builder.Property(companySource => companySource.AtsIdentifier)
                .HasMaxLength(200);

            builder.Property(companySource => companySource.IsEnabled)
                .IsRequired();

            builder.HasOne<Company>()
                .WithMany()
                .HasForeignKey(companySource => companySource.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(companySource => new { companySource.CompanyId, companySource.CareersUrl })
                .IsUnique();
        }
    }
}
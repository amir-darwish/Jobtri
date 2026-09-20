using Jobtri.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jobtri.Infrastructure.Persistence.Configurations
{
    public sealed class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("companies");

            builder.HasKey(company => company.Id);

            builder.Property(company => company.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(company => company.Website)
                .HasConversion<string>()
                .HasMaxLength(2048);

            builder.Property(company => company.IsEnabled)
                .IsRequired();
        }
    }
}
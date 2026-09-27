using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jobtri.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixJobLastSeenAtDefault : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                UPDATE jobs
                SET "LastSeenAt" = "FirstSeenAt"
                WHERE "LastSeenAt" = '-infinity'::timestamptz;
                """);

            migrationBuilder.Sql("""
                ALTER TABLE jobs
                ALTER COLUMN "LastSeenAt" DROP DEFAULT;
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                ALTER TABLE jobs
                ALTER COLUMN "LastSeenAt"
                SET DEFAULT TIMESTAMPTZ '-infinity';
                """);
        }
    }
}
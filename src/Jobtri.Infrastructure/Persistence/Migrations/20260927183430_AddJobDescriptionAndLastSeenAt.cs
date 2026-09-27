using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jobtri.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddJobDescriptionAndLastSeenAt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "jobs",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LastSeenAt",
                table: "jobs",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "jobs");

            migrationBuilder.DropColumn(
                name: "LastSeenAt",
                table: "jobs");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Migrations
{
    public partial class changedmovieschema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsActive = table.Column<bool>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    LastModifiedOn = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 200, nullable: true),
                    LastModifiedBy = table.Column<string>(maxLength: 200, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 250, nullable: true),
                    ReleaseDate = table.Column<DateTime>(nullable: false),
                    IMDBRating = table.Column<double>(nullable: false),
                    Budget = table.Column<string>(maxLength: 50, nullable: true),
                    BoxOfficeAmount = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "BoxOfficeAmount", "Budget", "CreatedBy", "CreatedOn", "IMDBRating", "IsActive", "IsDeleted", "LastModifiedBy", "LastModifiedOn", "Name", "ReleaseDate" },
                values: new object[] { 1, "2B", "400M", null, new DateTime(2018, 12, 30, 8, 39, 34, 123, DateTimeKind.Utc).AddTicks(5947), 8.5, true, false, null, null, "Avengers: Infinity War", new DateTime(2018, 4, 30, 8, 39, 34, 123, DateTimeKind.Utc).AddTicks(5933) });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "BoxOfficeAmount", "Budget", "CreatedBy", "CreatedOn", "IMDBRating", "IsActive", "IsDeleted", "LastModifiedBy", "LastModifiedOn", "Name", "ReleaseDate" },
                values: new object[] { 2, "389M", "50M", null, new DateTime(2018, 12, 30, 8, 39, 34, 123, DateTimeKind.Utc).AddTicks(9152), 8.1999999999999993, true, false, null, null, "The Wolf of Wall Street", new DateTime(2013, 12, 30, 8, 39, 34, 123, DateTimeKind.Utc).AddTicks(9138) });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "BoxOfficeAmount", "Budget", "CreatedBy", "CreatedOn", "IMDBRating", "IsActive", "IsDeleted", "LastModifiedBy", "LastModifiedOn", "Name", "ReleaseDate" },
                values: new object[] { 3, "90M", "20M", null, new DateTime(2018, 12, 30, 8, 39, 34, 123, DateTimeKind.Utc).AddTicks(9184), 7.2999999999999998, true, false, null, null, "Identity", new DateTime(2003, 12, 30, 8, 39, 34, 123, DateTimeKind.Utc).AddTicks(9184) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movies");
        }
    }
}

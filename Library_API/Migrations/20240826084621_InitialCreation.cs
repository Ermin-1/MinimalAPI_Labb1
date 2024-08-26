using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Library_API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "book",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Genre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsAvalible = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_book", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "book",
                columns: new[] { "Id", "Author", "Description", "Genre", "IsAvalible", "ReleaseDate", "Title" },
                values: new object[,]
                {
                    { 101, "Alice Johansson", "A journey through a mysterious world hidden beneath the earth's surface, filled with ancient magic and forgotten creatures.", "Fantasy", true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Phantom Abyss" },
                    { 102, "Liam Eriksson", "A small town is haunted by strange voices that echo through the night, leading its residents to unravel a chilling secret.", "Horror", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Whispers in the Dark" },
                    { 103, "Sofia Lundgren", "In a distant future, humanity must explore uncharted galaxies to find new habitable planets, but they are not alone.", "Science Fiction", true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Galactic Frontiers" },
                    { 104, "Oskar Nilsson", "Set during World War II, a young woman uncovers a long-lost family secret that changes her life forever.", "Historical Fiction", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Echoes of the Past" },
                    { 105, "Nina Bergström", "A brilliant scientist's groundbreaking discovery threatens to disrupt the fabric of reality, and now everyone is after it.", "Thriller", true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Quantum Enigma" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "book");
        }
    }
}

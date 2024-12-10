using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Tascii.Server.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Boards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OwnerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BoardId = table.Column<int>(type: "int", nullable: false),
                    content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    xCoord = table.Column<int>(type: "int", nullable: true),
                    yCoord = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Boards",
                columns: new[] { "Id", "Name", "OwnerId" },
                values: new object[,]
                {
                    { 1, "Board1", 1 },
                    { 2, "Board2", 1 },
                    { 3, "Board1", 2 },
                    { 4, "Board1", 2 }
                });

            migrationBuilder.InsertData(
                table: "Notes",
                columns: new[] { "Id", "BoardId", "content", "xCoord", "yCoord" },
                values: new object[,]
                {
                    { 1, 1, "Note1 for User1 Board1", 10, 10 },
                    { 2, 1, "Note2 for User1 Board1", -10, -10 },
                    { 3, 2, "Note1 for User1 Board2", -10, -10 },
                    { 4, 2, "Note2 for User1 Board2", 10, 10 },
                    { 5, 3, "Note2 for User2 Board1", -10, -10 },
                    { 6, 3, "Note1 for User2 Board1", 10, 10 },
                    { 7, 4, "Note2 for User2 Board2", -10, -10 },
                    { 8, 4, "Note1 for User2 Board2", 10, 10 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Name", "Password" },
                values: new object[,]
                {
                    { 1, "user1", "password1" },
                    { 2, "user2", "password2" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Boards");

            migrationBuilder.DropTable(
                name: "Notes");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}

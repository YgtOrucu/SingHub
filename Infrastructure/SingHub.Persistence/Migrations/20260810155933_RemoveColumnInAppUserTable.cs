using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SingHub.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemoveColumnInAppUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvatarUrl",
                table: "AspNetUsers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AvatarUrl",
                table: "AspNetUsers",
                type: "varchar(250)",
                nullable: true);
        }
    }
}

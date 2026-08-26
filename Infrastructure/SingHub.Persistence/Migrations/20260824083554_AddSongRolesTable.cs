using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SingHub.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddSongRolesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Songs_AspNetRoles_RoleId",
                table: "Songs");

            migrationBuilder.DropIndex(
                name: "IX_Songs_RoleId",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Songs");

            migrationBuilder.CreateTable(
                name: "SongAppRoles",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SongId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SongAppRoles", x => new { x.RoleId, x.SongId });
                    table.ForeignKey(
                        name: "FK_SongAppRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SongAppRoles_Songs_SongId",
                        column: x => x.SongId,
                        principalTable: "Songs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SongAppRoles_SongId",
                table: "SongAppRoles",
                column: "SongId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SongAppRoles");

            migrationBuilder.AddColumn<Guid>(
                name: "RoleId",
                table: "Songs",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Songs_RoleId",
                table: "Songs",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Songs_AspNetRoles_RoleId",
                table: "Songs",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

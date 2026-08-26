using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SingHub.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddAppRoleSongRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}

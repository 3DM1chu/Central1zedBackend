using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Central1zedCSharp.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreation3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClientEndpointConfirmations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Guid = table.Column<Guid>(type: "TEXT", nullable: false),
                    IsConfirmed = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientEndpointConfirmations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClientTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Guid = table.Column<Guid>(type: "TEXT", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    Role = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientTokens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClientEndpoints",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    TokenId = table.Column<int>(type: "INTEGER", nullable: false),
                    ConfirmationId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientEndpoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientEndpoints_ClientEndpointConfirmations_ConfirmationId",
                        column: x => x.ConfirmationId,
                        principalTable: "ClientEndpointConfirmations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientEndpoints_ClientTokens_TokenId",
                        column: x => x.TokenId,
                        principalTable: "ClientTokens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientEndpointLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Message = table.Column<string>(type: "TEXT", nullable: false),
                    Time = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndpointClientId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientEndpointLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientEndpointLogs_ClientEndpoints_EndpointClientId",
                        column: x => x.EndpointClientId,
                        principalTable: "ClientEndpoints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientEndpointLogs_EndpointClientId",
                table: "ClientEndpointLogs",
                column: "EndpointClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientEndpoints_ConfirmationId",
                table: "ClientEndpoints",
                column: "ConfirmationId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientEndpoints_TokenId",
                table: "ClientEndpoints",
                column: "TokenId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientEndpointLogs");

            migrationBuilder.DropTable(
                name: "ClientEndpoints");

            migrationBuilder.DropTable(
                name: "ClientEndpointConfirmations");

            migrationBuilder.DropTable(
                name: "ClientTokens");
        }
    }
}

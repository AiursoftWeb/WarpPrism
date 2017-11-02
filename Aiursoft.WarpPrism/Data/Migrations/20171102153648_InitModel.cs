using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Aiursoft.WarpPrism.Data.Migrations
{
    public partial class InitModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UserNameIndex",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserRoles_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles");

            migrationBuilder.CreateTable(
                name: "Databases",
                columns: table => new
                {
                    DataBaseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataBaseName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Databases", x => x.DataBaseId);
                });

            migrationBuilder.CreateTable(
                name: "Tables",
                columns: table => new
                {
                    TableId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DatabaseId = table.Column<int>(type: "int", nullable: false),
                    TableName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tables", x => x.TableId);
                    table.ForeignKey(
                        name: "FK_Tables_Databases_DatabaseId",
                        column: x => x.DatabaseId,
                        principalTable: "Databases",
                        principalColumn: "DataBaseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    ItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TableId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.ItemId);
                    table.ForeignKey(
                        name: "FK_Items_Tables_TableId",
                        column: x => x.TableId,
                        principalTable: "Tables",
                        principalColumn: "TableId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Propertys",
                columns: table => new
                {
                    PropertyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PropertyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TableId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Propertys", x => x.PropertyId);
                    table.ForeignKey(
                        name: "FK_Propertys_Tables_TableId",
                        column: x => x.TableId,
                        principalTable: "Tables",
                        principalColumn: "TableId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Values",
                columns: table => new
                {
                    ValueId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    PropertyId = table.Column<int>(type: "int", nullable: false),
                    RealValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Values", x => x.ValueId);
                    table.ForeignKey(
                        name: "FK_Values_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Values_Propertys_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Propertys",
                        principalColumn: "PropertyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Items_TableId",
                table: "Items",
                column: "TableId");

            migrationBuilder.CreateIndex(
                name: "IX_Propertys_TableId",
                table: "Propertys",
                column: "TableId");

            migrationBuilder.CreateIndex(
                name: "IX_Tables_DatabaseId",
                table: "Tables",
                column: "DatabaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Values_ItemId",
                table: "Values",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Values_PropertyId",
                table: "Values",
                column: "PropertyId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Values");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Propertys");

            migrationBuilder.DropTable(
                name: "Tables");

            migrationBuilder.DropTable(
                name: "Databases");

            migrationBuilder.DropIndex(
                name: "UserNameIndex",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_UserId",
                table: "AspNetUserRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName");
        }
    }
}

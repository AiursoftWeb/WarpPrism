using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Aiursoft.WarpPrism.Data.Migrations
{
    public partial class renameProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Propertys_Tables_TableId",
                table: "Propertys");

            migrationBuilder.DropForeignKey(
                name: "FK_Values_Propertys_PropertyId",
                table: "Values");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Propertys",
                table: "Propertys");

            migrationBuilder.RenameTable(
                name: "Propertys",
                newName: "Properties");

            migrationBuilder.RenameIndex(
                name: "IX_Propertys_TableId",
                table: "Properties",
                newName: "IX_Properties_TableId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Properties",
                table: "Properties",
                column: "PropertyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Tables_TableId",
                table: "Properties",
                column: "TableId",
                principalTable: "Tables",
                principalColumn: "TableId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Values_Properties_PropertyId",
                table: "Values",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "PropertyId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Tables_TableId",
                table: "Properties");

            migrationBuilder.DropForeignKey(
                name: "FK_Values_Properties_PropertyId",
                table: "Values");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Properties",
                table: "Properties");

            migrationBuilder.RenameTable(
                name: "Properties",
                newName: "Propertys");

            migrationBuilder.RenameIndex(
                name: "IX_Properties_TableId",
                table: "Propertys",
                newName: "IX_Propertys_TableId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Propertys",
                table: "Propertys",
                column: "PropertyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Propertys_Tables_TableId",
                table: "Propertys",
                column: "TableId",
                principalTable: "Tables",
                principalColumn: "TableId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Values_Propertys_PropertyId",
                table: "Values",
                column: "PropertyId",
                principalTable: "Propertys",
                principalColumn: "PropertyId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

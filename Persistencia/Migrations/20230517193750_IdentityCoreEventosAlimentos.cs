using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistencia.Migrations
{
    public partial class IdentityCoreEventosAlimentos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoriaProducto_Producto_productoId",
                table: "CategoriaProducto");

            migrationBuilder.DropIndex(
                name: "IX_CategoriaProducto_productoId",
                table: "CategoriaProducto");

            migrationBuilder.DropColumn(
                name: "productoId",
                table: "CategoriaProducto");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "productoId",
                table: "CategoriaProducto",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CategoriaProducto_productoId",
                table: "CategoriaProducto",
                column: "productoId");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoriaProducto_Producto_productoId",
                table: "CategoriaProducto",
                column: "productoId",
                principalTable: "Producto",
                principalColumn: "productoId");
        }
    }
}

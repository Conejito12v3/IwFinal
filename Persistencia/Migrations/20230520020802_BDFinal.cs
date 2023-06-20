using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistencia.Migrations
{
    public partial class BDFinal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productor_RedesSociales_redesSocialesId",
                table: "Productor");

            migrationBuilder.DropForeignKey(
                name: "FK_RedesSociales_Producto_productoId",
                table: "RedesSociales");

            migrationBuilder.DropTable(
                name: "ProductoProductor");

            migrationBuilder.DropIndex(
                name: "IX_RedesSociales_productoId",
                table: "RedesSociales");

            migrationBuilder.DropIndex(
                name: "IX_Productor_redesSocialesId",
                table: "Productor");

            migrationBuilder.DropColumn(
                name: "redesSocialesId",
                table: "Productor");

            migrationBuilder.RenameColumn(
                name: "productoId",
                table: "RedesSociales",
                newName: "productorId");

            migrationBuilder.CreateIndex(
                name: "IX_RedesSociales_productorId",
                table: "RedesSociales",
                column: "productorId",
                unique: true,
                filter: "[productorId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Producto_categoriaId",
                table: "Producto",
                column: "categoriaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Producto_CategoriaProducto_categoriaId",
                table: "Producto",
                column: "categoriaId",
                principalTable: "CategoriaProducto",
                principalColumn: "categoriaProductoId");

            migrationBuilder.AddForeignKey(
                name: "FK_RedesSociales_Productor_productorId",
                table: "RedesSociales",
                column: "productorId",
                principalTable: "Productor",
                principalColumn: "productorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Producto_CategoriaProducto_categoriaId",
                table: "Producto");

            migrationBuilder.DropForeignKey(
                name: "FK_RedesSociales_Productor_productorId",
                table: "RedesSociales");

            migrationBuilder.DropIndex(
                name: "IX_RedesSociales_productorId",
                table: "RedesSociales");

            migrationBuilder.DropIndex(
                name: "IX_Producto_categoriaId",
                table: "Producto");

            migrationBuilder.RenameColumn(
                name: "productorId",
                table: "RedesSociales",
                newName: "productoId");

            migrationBuilder.AddColumn<Guid>(
                name: "redesSocialesId",
                table: "Productor",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProductoProductor",
                columns: table => new
                {
                    productoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    productorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductoProductor", x => new { x.productoId, x.productorId });
                    table.ForeignKey(
                        name: "FK_ProductoProductor_Producto_productoId",
                        column: x => x.productoId,
                        principalTable: "Producto",
                        principalColumn: "productoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductoProductor_Productor_productorId",
                        column: x => x.productorId,
                        principalTable: "Productor",
                        principalColumn: "productorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RedesSociales_productoId",
                table: "RedesSociales",
                column: "productoId");

            migrationBuilder.CreateIndex(
                name: "IX_Productor_redesSocialesId",
                table: "Productor",
                column: "redesSocialesId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductoProductor_productorId",
                table: "ProductoProductor",
                column: "productorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Productor_RedesSociales_redesSocialesId",
                table: "Productor",
                column: "redesSocialesId",
                principalTable: "RedesSociales",
                principalColumn: "redesSocialesId");

            migrationBuilder.AddForeignKey(
                name: "FK_RedesSociales_Producto_productoId",
                table: "RedesSociales",
                column: "productoId",
                principalTable: "Producto",
                principalColumn: "productoId");
        }
    }
}

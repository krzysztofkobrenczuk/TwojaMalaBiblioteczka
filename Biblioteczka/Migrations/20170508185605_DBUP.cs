using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Biblioteczka.Migrations
{
    public partial class DBUP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BookshelveId",
                table: "Books",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Bookshalves",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookshalves", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_BookshelveId",
                table: "Books",
                column: "BookshelveId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Bookshalves_BookshelveId",
                table: "Books",
                column: "BookshelveId",
                principalTable: "Bookshalves",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Bookshalves_BookshelveId",
                table: "Books");

            migrationBuilder.DropTable(
                name: "Bookshalves");

            migrationBuilder.DropIndex(
                name: "IX_Books_BookshelveId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "BookshelveId",
                table: "Books");
        }
    }
}

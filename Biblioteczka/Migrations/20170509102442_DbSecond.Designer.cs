using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Biblioteczka.Models;

namespace Biblioteczka.Migrations
{
    [DbContext(typeof(LibraryContext))]
    [Migration("20170509102442_DbSecond")]
    partial class DbSecond
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Biblioteczka.Models.Book", b =>
                {
                    b.Property<int>("BookId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Author");

                    b.Property<int?>("BookshelveId");

                    b.Property<DateTime>("DateStarted");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<int>("Pages");

                    b.HasKey("BookId");

                    b.HasIndex("BookshelveId");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("Biblioteczka.Models.Bookshelve", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("Name");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("Bookshalves");
                });

            modelBuilder.Entity("Biblioteczka.Models.Book", b =>
                {
                    b.HasOne("Biblioteczka.Models.Bookshelve")
                        .WithMany("Books")
                        .HasForeignKey("BookshelveId");
                });
        }
    }
}

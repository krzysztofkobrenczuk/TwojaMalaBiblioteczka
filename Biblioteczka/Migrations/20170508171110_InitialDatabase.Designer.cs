using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Biblioteczka.Models;

namespace Biblioteczka.Migrations
{
    [DbContext(typeof(LibraryContext))]
    [Migration("20170508171110_InitialDatabase")]
    partial class InitialDatabase
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

                    b.Property<DateTime>("DateStarted");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<int>("Pages");

                    b.HasKey("BookId");

                    b.ToTable("Books");
                });
        }
    }
}

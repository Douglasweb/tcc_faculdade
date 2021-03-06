// <auto-generated />
using System;
using ApiCQRS.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ApiCQRS.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ApiCQRS.Domain.Entities.User", b =>
                {
                    b.Property<Guid?>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("CanBeUpdated")
                        .HasColumnType("bit");

                    b.Property<DateTime>("UserCreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserEmail")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("UserName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("UserPassword")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("UserStatus")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("UserUpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("UserId");

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            UserId = new Guid("310d95d4-9f90-4db7-ac52-fa85a1d08622"),
                            CanBeUpdated = true,
                            UserCreatedAt = new DateTime(2021, 10, 3, 18, 58, 54, 734, DateTimeKind.Local).AddTicks(1398),
                            UserEmail = "webdouglasti@gmail.com",
                            UserName = "Douglas Eduardo",
                            UserPassword = "0xB123E9E19D217169B981A61188920F9D28638709A5132201684D792B9264271B7F09157ED4321B1C097F7A4ABECFC0977D40A7EE599C845883BD1074CA23C4AF",
                            UserStatus = true
                        });
                });
#pragma warning restore 612, 618
        }
    }
}

﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Catalog.Api.Infrastructure;

#nullable disable

namespace Catalog.Api.Migrations
{
    [DbContext(typeof(CatalogDbContext))]
    partial class CatalogDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("catalog")
                .HasAnnotation("ProductVersion", "9.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Catalog.Api.Models.CatalogBrand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("CatalogBrands", "catalog");
                });

            modelBuilder.Entity("Catalog.Api.Models.CatalogCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("ParentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("CatalogCategories", "catalog");
                });

            modelBuilder.Entity("Catalog.Api.Models.CatalogItem", b =>
                {
                    b.Property<string>("Slug")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<int>("AvailableStock")
                        .HasColumnType("int");

                    b.Property<int>("CatalogBrandId")
                        .HasColumnType("int");

                    b.Property<int>("CatalogCategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(5000)
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaxStockThreshold")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(15,2)");

                    b.HasKey("Slug");

                    b.HasIndex("CatalogBrandId");

                    b.HasIndex("CatalogCategoryId");

                    b.ToTable("CatalogItems", "catalog");
                });

            modelBuilder.Entity("Catalog.Api.Models.CatalogCategory", b =>
                {
                    b.HasOne("Catalog.Api.Models.CatalogCategory", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId");

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("Catalog.Api.Models.CatalogItem", b =>
                {
                    b.HasOne("Catalog.Api.Models.CatalogBrand", "CatalogBrand")
                        .WithMany()
                        .HasForeignKey("CatalogBrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Catalog.Api.Models.CatalogCategory", "CatalogCategory")
                        .WithMany()
                        .HasForeignKey("CatalogCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsMany("Catalog.Api.Models.CatalogMedia", "Medias", b1 =>
                        {
                            b1.Property<string>("CatalogItemSlug")
                                .HasColumnType("nvarchar(150)");

                            b1.Property<int>("__synthesizedOrdinal")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)");

                            b1.Property<string>("Url")
                                .IsRequired()
                                .HasMaxLength(1098)
                                .HasColumnType("nvarchar(1098)");

                            b1.HasKey("CatalogItemSlug", "__synthesizedOrdinal");

                            b1.ToTable("CatalogItems", "catalog");

                            b1.ToJson("Medias");

                            b1.WithOwner()
                                .HasForeignKey("CatalogItemSlug");
                        });

                    b.Navigation("CatalogBrand");

                    b.Navigation("CatalogCategory");

                    b.Navigation("Medias");
                });

            modelBuilder.Entity("Catalog.Api.Models.CatalogCategory", b =>
                {
                    b.Navigation("Children");
                });
#pragma warning restore 612, 618
        }
    }
}

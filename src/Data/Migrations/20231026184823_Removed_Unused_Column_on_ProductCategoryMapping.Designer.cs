﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Scroll.Data;

#nullable disable

namespace Scroll.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20231026184823_Removed_Unused_Column_on_ProductCategoryMapping")]
    partial class Removed_Unused_Column_on_ProductCategoryMapping
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0-rc.2.23480.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("ClaimValue")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("ClaimValue")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId")
                        .HasDatabaseName("IX_UserLogins_User");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("Value")
                        .HasMaxLength(1024)
                        .HasColumnType("character varying(1024)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Scroll.Domain.Entities.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasDatabaseName("IX_Category_Name");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("Scroll.Domain.Entities.Favorite", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.HasKey("UserId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("Favorite");
                });

            modelBuilder.Entity("Scroll.Domain.Entities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("AddedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("ClickCount")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(5000)
                        .HasColumnType("character varying(5000)");

                    b.Property<int>("FavoriteCount")
                        .HasColumnType("integer");

                    b.Property<string>("ImageName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Link")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<decimal>("Price")
                        .HasPrecision(18, 6)
                        .HasColumnType("decimal(18, 6)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.HasIndex("Price")
                        .HasDatabaseName("IX_Product_Price");

                    b.HasIndex("Title")
                        .IsUnique()
                        .HasDatabaseName("IX_Product_Title");

                    b.HasIndex("FavoriteCount", "ClickCount")
                        .HasDatabaseName("IX_Product_Engagement");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("Scroll.Domain.Entities.ProductCategoryMapping", b =>
                {
                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uuid");

                    b.HasKey("ProductId", "CategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("ProductCategoryMapping");
                });

            modelBuilder.Entity("Scroll.Domain.Entities.ScrollFileInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("AddedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ContentType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(21)
                        .HasColumnType("character varying(21)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<long>("Size")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasDatabaseName("UIX_ScrollFile_Name");

                    b.ToTable("ScrollFiles", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("ScrollFileInfo");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Scroll.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("FullName")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<DateTimeOffset>("JoinedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasDatabaseName("IX_Users_Email");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.HasIndex("PhoneNumber")
                        .HasDatabaseName("IX_Users_Phone");

                    b.HasIndex("UserName")
                        .IsUnique()
                        .HasDatabaseName("IX_Users_UserName");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Scroll.Domain.Entities.ScrollFile", b =>
                {
                    b.HasBaseType("Scroll.Domain.Entities.ScrollFileInfo");

                    b.Property<byte[]>("Content")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.HasDiscriminator().HasValue("ScrollFile");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("Scroll.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("Scroll.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Scroll.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("Scroll.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Scroll.Domain.Entities.Favorite", b =>
                {
                    b.HasOne("Scroll.Domain.Entities.Product", "Product")
                        .WithMany("Favorites")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Scroll.Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Scroll.Domain.Entities.ProductCategoryMapping", b =>
                {
                    b.HasOne("Scroll.Domain.Entities.Category", "Category")
                        .WithMany("ProductCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Scroll.Domain.Entities.Product", "Product")
                        .WithMany("ProductCategories")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Scroll.Domain.Entities.Category", b =>
                {
                    b.Navigation("ProductCategories");
                });

            modelBuilder.Entity("Scroll.Domain.Entities.Product", b =>
                {
                    b.Navigation("Favorites");

                    b.Navigation("ProductCategories");
                });
#pragma warning restore 612, 618
        }
    }
}

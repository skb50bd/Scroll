﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Scroll.Data;

#nullable disable

namespace Scroll.Data.Migrations
{
    [DbContext(typeof(ScrollDbContext))]
    [Migration("20220312063825_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ConcurrencyStamp")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ClaimValue")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("RoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ClaimValue")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("UserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ProviderKey", "LoginProvider");

                    b.HasIndex("UserId")
                        .HasDatabaseName("IX_UserLogins_User");

                    b.ToTable("UserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("RoleId", "UserId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasMaxLength(1024)
                        .HasColumnType("nvarchar(1024)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserTokens");
                });

            modelBuilder.Entity("Scroll.Library.Models.Entities.AppUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("NormalizedUserName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .HasDatabaseName("IX_Users_Email");

                    b.HasIndex("PhoneNumber")
                        .HasDatabaseName("IX_Users_Phone");

                    b.HasIndex("UserName")
                        .HasDatabaseName("IX_Users_UserName");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Scroll.Library.Models.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasDatabaseName("IX_Category_Name");

                    b.ToTable("Category");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "This is a toy category. Toys are nice, aren't they?",
                            Name = "Toy"
                        },
                        new
                        {
                            Id = 2,
                            Description = "This is a food category. Food is tasty.",
                            Name = "Food"
                        },
                        new
                        {
                            Id = 3,
                            Description = "These are products for geeks. If you are a geek, you're gonna love these.",
                            Name = "Geek"
                        },
                        new
                        {
                            Id = 4,
                            Description = "These products will improve your lifestyle. And will help you enjoy a better life.",
                            Name = "Lifestyle"
                        },
                        new
                        {
                            Id = 5,
                            Description = "You’re a funny guy, but if you really want to ramp up the LOLs at your next party, you need one of these hilarious and downright evil prank gifts.",
                            Name = "Pranks"
                        },
                        new
                        {
                            Id = 6,
                            Description = "Level up your gift giving powers and find the perfect gift for gamers in this hand curated list. Whether you're looking for something for console gamer, the PC gamer, the old school gaming type who loves everything in 8-bit pixelation or the youngster who loves virtual reality and microtransactions, this collection of gifts features all of the best sellers from this year.",
                            Name = "Gamer"
                        },
                        new
                        {
                            Id = 7,
                            Description = "Capture the heart of your photographer friend with one of these delightful gifts for photographers. Professionals and shutterbugs who enjoy it as a hobby alike will light up brighter than a flash when they unwrap something fun or functional from this list of great gift ideas.",
                            Name = "Photography"
                        });
                });

            modelBuilder.Entity("Scroll.Library.Models.Entities.Favorite", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("Favorite");
                });

            modelBuilder.Entity("Scroll.Library.Models.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTimeOffset>("AddedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<int>("ClickCount")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(5000)
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FavoriteCount")
                        .HasColumnType("int");

                    b.Property<string>("ImageName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Link")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<decimal>("Price")
                        .HasPrecision(18, 6)
                        .HasColumnType("decimal(18,6)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Price")
                        .HasDatabaseName("IX_Product_Price");

                    b.HasIndex("Title")
                        .IsUnique()
                        .HasDatabaseName("IX_Product_Title");

                    b.HasIndex("FavoriteCount", "ClickCount")
                        .HasDatabaseName("IX_Product_Engagement");

                    b.ToTable("Product");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AddedOn = new DateTimeOffset(new DateTime(2022, 3, 12, 12, 38, 24, 480, DateTimeKind.Unspecified).AddTicks(2652), new TimeSpan(0, 6, 0, 0, 0)),
                            ClickCount = 10,
                            Description = "If you need to get even with someone you love, the impossible to open frustration box is just what you need.",
                            FavoriteCount = 0,
                            ImageName = "abc-xyz.webp",
                            Link = "https://mynextdig.com/1",
                            Price = 17.99m,
                            Title = "The Impossible To Open Frustration Box"
                        },
                        new
                        {
                            Id = 2,
                            AddedOn = new DateTimeOffset(new DateTime(2022, 3, 12, 12, 38, 24, 481, DateTimeKind.Unspecified).AddTicks(1891), new TimeSpan(0, 6, 0, 0, 0)),
                            ClickCount = 3,
                            Description = "Nothing will embarrass your poor unsuspecting victim quite like receiving one of these prank mail packages at their place or work.",
                            FavoriteCount = 0,
                            ImageName = "abc-xyz.webp",
                            Link = "https://mynextdig.com/2",
                            Price = 11.86m,
                            Title = "Prank Mail Packages"
                        },
                        new
                        {
                            Id = 3,
                            AddedOn = new DateTimeOffset(new DateTime(2022, 3, 12, 12, 38, 24, 481, DateTimeKind.Unspecified).AddTicks(2036), new TimeSpan(0, 6, 0, 0, 0)),
                            ClickCount = 69,
                            Description = "Ensure your buddy has nightmares for years to come by sending him 1500 live ladybugs.",
                            FavoriteCount = 0,
                            ImageName = "abc-xyz.webp",
                            Link = "https://mynextdig.com/3",
                            Price = 30.96m,
                            Title = "1500 Live Ladybugs"
                        },
                        new
                        {
                            Id = 4,
                            AddedOn = new DateTimeOffset(new DateTime(2022, 3, 12, 12, 38, 24, 481, DateTimeKind.Unspecified).AddTicks(2046), new TimeSpan(0, 6, 0, 0, 0)),
                            ClickCount = 3,
                            Description = "Gamers rejoice! Ensure no good game slips under your nose by playing everything on the 100 Video Games Bucket List poster.",
                            FavoriteCount = 0,
                            ImageName = "abc-xyz.webp",
                            Link = "https://mynextdig.com/4",
                            Price = 17.47m,
                            Title = "100 Must Play Video Games Scratch Off Poster"
                        },
                        new
                        {
                            Id = 5,
                            AddedOn = new DateTimeOffset(new DateTime(2022, 3, 12, 12, 38, 24, 481, DateTimeKind.Unspecified).AddTicks(2054), new TimeSpan(0, 6, 0, 0, 0)),
                            ClickCount = 1,
                            Description = "For both streamers and gamers, this backlit LED is the perfect way to give a customized gift to any gamer.",
                            FavoriteCount = 0,
                            ImageName = "abc-xyz.webp",
                            Link = "https://mynextdig.com/5",
                            Price = 58.49m,
                            Title = "Personalized Gamertag Backlit LED"
                        },
                        new
                        {
                            Id = 6,
                            AddedOn = new DateTimeOffset(new DateTime(2022, 3, 12, 12, 38, 24, 481, DateTimeKind.Unspecified).AddTicks(2061), new TimeSpan(0, 6, 0, 0, 0)),
                            ClickCount = 4,
                            Description = "After an intense gaming session getting pwned by kids half your age, you need a way to relax and calm your nerves.",
                            FavoriteCount = 0,
                            ImageName = "abc-xyz.webp",
                            Link = "https://mynextdig.com/6",
                            Price = 7.96m,
                            Title = "Video Game Rage Candle"
                        },
                        new
                        {
                            Id = 7,
                            AddedOn = new DateTimeOffset(new DateTime(2022, 3, 12, 12, 38, 24, 481, DateTimeKind.Unspecified).AddTicks(2067), new TimeSpan(0, 6, 0, 0, 0)),
                            ClickCount = 420,
                            Description = "Take the rainbow with a photography prism.",
                            FavoriteCount = 0,
                            ImageName = "abc-xyz.webp",
                            Link = "https://mynextdig.com/7",
                            Price = 16.49m,
                            Title = "Photography Prism"
                        });
                });

            modelBuilder.Entity("Scroll.Library.Models.Entities.ProductCategoryMapping", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.HasKey("ProductId", "CategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("ProductCategoryMapping");

                    b.HasData(
                        new
                        {
                            ProductId = 1,
                            CategoryId = 1
                        },
                        new
                        {
                            ProductId = 1,
                            CategoryId = 2
                        },
                        new
                        {
                            ProductId = 1,
                            CategoryId = 3
                        },
                        new
                        {
                            ProductId = 2,
                            CategoryId = 2
                        },
                        new
                        {
                            ProductId = 2,
                            CategoryId = 3
                        },
                        new
                        {
                            ProductId = 3,
                            CategoryId = 3
                        },
                        new
                        {
                            ProductId = 4,
                            CategoryId = 4
                        },
                        new
                        {
                            ProductId = 5,
                            CategoryId = 1
                        },
                        new
                        {
                            ProductId = 5,
                            CategoryId = 2
                        },
                        new
                        {
                            ProductId = 6,
                            CategoryId = 7
                        },
                        new
                        {
                            ProductId = 7,
                            CategoryId = 2
                        },
                        new
                        {
                            ProductId = 7,
                            CategoryId = 7
                        });
                });

            modelBuilder.Entity("Scroll.Library.Models.Entities.Favorite", b =>
                {
                    b.HasOne("Scroll.Library.Models.Entities.Product", "Product")
                        .WithMany("Favorites")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Scroll.Library.Models.Entities.AppUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Scroll.Library.Models.Entities.ProductCategoryMapping", b =>
                {
                    b.HasOne("Scroll.Library.Models.Entities.Category", "Category")
                        .WithMany("ProductCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Scroll.Library.Models.Entities.Product", "Product")
                        .WithMany("ProductCategories")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Scroll.Library.Models.Entities.Category", b =>
                {
                    b.Navigation("ProductCategories");
                });

            modelBuilder.Entity("Scroll.Library.Models.Entities.Product", b =>
                {
                    b.Navigation("Favorites");

                    b.Navigation("ProductCategories");
                });
#pragma warning restore 612, 618
        }
    }
}
﻿// <auto-generated />
using System;
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataLayer.Migrations
{
    [DbContext(typeof(StackInternshipDbContext))]
    [Migration("20211230234142_AddedUserViewVote")]
    partial class AddedUserViewVote
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DataLayer.Entities.Models.Entry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AuthorId")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateOfPublishing")
                        .HasColumnType("datetime2");

                    b.Property<int>("Department")
                        .HasColumnType("int");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DownvoteCount")
                        .HasColumnType("int");

                    b.Property<int>("UpvoteCount")
                        .HasColumnType("int");

                    b.Property<int>("ViewCount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Entries");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Entry");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AuthorId = 1,
                            Content = "Prva obavijest",
                            DateOfPublishing = new DateTime(2021, 12, 1, 8, 0, 0, 0, DateTimeKind.Unspecified),
                            Department = 53,
                            DownvoteCount = 0,
                            UpvoteCount = 0,
                            ViewCount = 0
                        },
                        new
                        {
                            Id = 2,
                            AuthorId = 3,
                            Content = "Kad je iduće predavanje?",
                            DateOfPublishing = new DateTime(2021, 12, 1, 12, 12, 12, 0, DateTimeKind.Unspecified),
                            Department = 49,
                            DownvoteCount = 0,
                            UpvoteCount = 0,
                            ViewCount = 0
                        },
                        new
                        {
                            Id = 3,
                            AuthorId = 2,
                            Content = "Zašto mi se ne ispiše ništa kad stavim where?",
                            DateOfPublishing = new DateTime(2021, 12, 25, 14, 30, 0, 0, DateTimeKind.Unspecified),
                            Department = 50,
                            DownvoteCount = 0,
                            UpvoteCount = 0,
                            ViewCount = 0
                        },
                        new
                        {
                            Id = 4,
                            AuthorId = 3,
                            Content = "Upute za LINQ!",
                            DateOfPublishing = new DateTime(2021, 12, 1, 0, 30, 0, 0, DateTimeKind.Unspecified),
                            Department = 49,
                            DownvoteCount = 0,
                            UpvoteCount = 0,
                            ViewCount = 0
                        },
                        new
                        {
                            Id = 5,
                            AuthorId = 3,
                            Content = "Uskršnji party je u učionici na Veliki petak u 19h, nemojte kasnit!",
                            DateOfPublishing = new DateTime(2021, 12, 1, 23, 59, 59, 0, DateTimeKind.Unspecified),
                            Department = 53,
                            DownvoteCount = 0,
                            UpvoteCount = 0,
                            ViewCount = 0
                        });
                });

            modelBuilder.Entity("DataLayer.Entities.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsTrustedUser")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ReputationPoints")
                        .HasColumnType("int");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IsTrustedUser = false,
                            Password = "12345",
                            ReputationPoints = 10,
                            Role = 0,
                            UserName = "MateaB"
                        },
                        new
                        {
                            Id = 2,
                            IsTrustedUser = false,
                            Password = "54321",
                            ReputationPoints = 44444,
                            Role = 1,
                            UserName = "lovre"
                        },
                        new
                        {
                            Id = 3,
                            IsTrustedUser = false,
                            Password = "qweqw",
                            ReputationPoints = 1,
                            Role = 0,
                            UserName = "bartol_deak"
                        },
                        new
                        {
                            Id = 4,
                            IsTrustedUser = false,
                            Password = "qwqwq",
                            ReputationPoints = 1000,
                            Role = 0,
                            UserName = "mmaretic"
                        },
                        new
                        {
                            Id = 5,
                            IsTrustedUser = false,
                            Password = "password",
                            ReputationPoints = 1,
                            Role = 0,
                            UserName = "anamarija"
                        },
                        new
                        {
                            Id = 6,
                            IsTrustedUser = false,
                            Password = "sifra",
                            ReputationPoints = 1,
                            Role = 0,
                            UserName = "boze topic"
                        },
                        new
                        {
                            Id = 7,
                            IsTrustedUser = false,
                            Password = "asdas",
                            ReputationPoints = 10000,
                            Role = 1,
                            UserName = "petra123"
                        });
                });

            modelBuilder.Entity("DataLayer.Entities.Models.Comment", b =>
                {
                    b.HasBaseType("DataLayer.Entities.Models.Entry");

                    b.Property<int>("ParentId")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("Comment");

                    b.HasData(
                        new
                        {
                            Id = 100,
                            AuthorId = 2,
                            Content = "ok",
                            DateOfPublishing = new DateTime(2021, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Department = 0,
                            DownvoteCount = 0,
                            UpvoteCount = 0,
                            ViewCount = 0,
                            ParentId = 1
                        });
                });
#pragma warning restore 612, 618
        }
    }
}

﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SignalR.Persistence.DbContexts;

#nullable disable

namespace SignalR.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250103195812_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SignalR.Domain.Entities.ApplicationUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ApplicationUsers", "Auth");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedDate = new DateTime(2024, 12, 23, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FullName = "Hazem Dawood",
                            ImageUrl = "https://bootdey.com/img/Content/avatar/avatar2.png",
                            IsDeleted = false,
                            Password = "123456",
                            UserName = "hazem"
                        },
                        new
                        {
                            Id = 2,
                            CreatedDate = new DateTime(2024, 12, 23, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FullName = "Ahmed Emad",
                            ImageUrl = "https://bootdey.com/img/Content/avatar/avatar1.png",
                            IsDeleted = false,
                            Password = "123456",
                            UserName = "ahmed"
                        },
                        new
                        {
                            Id = 3,
                            CreatedDate = new DateTime(2024, 12, 23, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FullName = "Kareem Belal",
                            ImageUrl = "https://bootdey.com/img/Content/avatar/avatar4.png",
                            IsDeleted = false,
                            Password = "123456",
                            UserName = "kareem"
                        },
                        new
                        {
                            Id = 4,
                            CreatedDate = new DateTime(2024, 12, 23, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FullName = "Mahmoud Bahrawy",
                            ImageUrl = "https://bootdey.com/img/Content/avatar/avatar7.png",
                            IsDeleted = false,
                            Password = "123456",
                            UserName = "bahrawy"
                        });
                });

            modelBuilder.Entity("SignalR.Domain.Entities.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("CreatedUserId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CreatedUserId");

                    b.ToTable("Groups", "Chats");
                });

            modelBuilder.Entity("SignalR.Domain.Entities.GroupMessage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("UserId");

                    b.ToTable("GroupMessages", "Chats");
                });

            modelBuilder.Entity("SignalR.Domain.Entities.GroupUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("UserId");

                    b.ToTable("GroupUsers", "Chats");
                });

            modelBuilder.Entity("SignalR.Domain.Entities.OnlineUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("OnlineUsers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedDate = new DateTime(2024, 12, 23, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            LastModified = new DateTime(2024, 12, 23, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            CreatedDate = new DateTime(2024, 12, 23, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            LastModified = new DateTime(2024, 12, 23, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserId = 2
                        },
                        new
                        {
                            Id = 3,
                            CreatedDate = new DateTime(2024, 12, 23, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            LastModified = new DateTime(2024, 12, 23, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserId = 3
                        },
                        new
                        {
                            Id = 4,
                            CreatedDate = new DateTime(2024, 12, 23, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            LastModified = new DateTime(2024, 12, 23, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserId = 4
                        });
                });

            modelBuilder.Entity("SignalR.Domain.Entities.UserChat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("ToUserId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ToUserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserChats", "Chats");
                });

            modelBuilder.Entity("SignalR.Domain.Entities.UserChatMessage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("CreatedUserId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsSeen")
                        .HasColumnType("bit");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserChatId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreatedUserId");

                    b.HasIndex("UserChatId");

                    b.ToTable("UserChatMessages", "Chats");
                });

            modelBuilder.Entity("SignalR.Domain.Entities.Group", b =>
                {
                    b.HasOne("SignalR.Domain.Entities.ApplicationUser", "CreatedUser")
                        .WithMany("Groups")
                        .HasForeignKey("CreatedUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatedUser");
                });

            modelBuilder.Entity("SignalR.Domain.Entities.GroupMessage", b =>
                {
                    b.HasOne("SignalR.Domain.Entities.Group", "Group")
                        .WithMany("Messages")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SignalR.Domain.Entities.ApplicationUser", "User")
                        .WithMany("GroupMessages")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SignalR.Domain.Entities.GroupUser", b =>
                {
                    b.HasOne("SignalR.Domain.Entities.Group", "Group")
                        .WithMany("GroupUsers")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SignalR.Domain.Entities.ApplicationUser", "User")
                        .WithMany("GroupUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SignalR.Domain.Entities.OnlineUser", b =>
                {
                    b.HasOne("SignalR.Domain.Entities.ApplicationUser", "User")
                        .WithOne("OnlineUser")
                        .HasForeignKey("SignalR.Domain.Entities.OnlineUser", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("SignalR.Domain.Entities.UserChat", b =>
                {
                    b.HasOne("SignalR.Domain.Entities.ApplicationUser", "ToUser")
                        .WithMany("ToMessages")
                        .HasForeignKey("ToUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SignalR.Domain.Entities.ApplicationUser", "FromUser")
                        .WithMany("FromMessages")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FromUser");

                    b.Navigation("ToUser");
                });

            modelBuilder.Entity("SignalR.Domain.Entities.UserChatMessage", b =>
                {
                    b.HasOne("SignalR.Domain.Entities.ApplicationUser", "CreatedUser")
                        .WithMany("UserChatMessages")
                        .HasForeignKey("CreatedUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SignalR.Domain.Entities.UserChat", "UserChat")
                        .WithMany("UserChatMessages")
                        .HasForeignKey("UserChatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatedUser");

                    b.Navigation("UserChat");
                });

            modelBuilder.Entity("SignalR.Domain.Entities.ApplicationUser", b =>
                {
                    b.Navigation("FromMessages");

                    b.Navigation("GroupMessages");

                    b.Navigation("GroupUsers");

                    b.Navigation("Groups");

                    b.Navigation("OnlineUser");

                    b.Navigation("ToMessages");

                    b.Navigation("UserChatMessages");
                });

            modelBuilder.Entity("SignalR.Domain.Entities.Group", b =>
                {
                    b.Navigation("GroupUsers");

                    b.Navigation("Messages");
                });

            modelBuilder.Entity("SignalR.Domain.Entities.UserChat", b =>
                {
                    b.Navigation("UserChatMessages");
                });
#pragma warning restore 612, 618
        }
    }
}

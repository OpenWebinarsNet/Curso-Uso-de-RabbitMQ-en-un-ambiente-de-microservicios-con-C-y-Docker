﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NotificationsManagement.Infrastructure.Context;

namespace NotificationsManagement.Infrastructure.Migrations
{
    [DbContext(typeof(NotificationsContext))]
    partial class NotificationsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("NotificationsManagement.Domain.Entities.NotificationSent", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("NotificationDatetime")
                        .HasColumnType("datetime2");

                    b.Property<string>("NotificationMessage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("NotificationTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id")
                        .HasName("PK_{nameof(NotificationSent)}");

                    b.HasIndex("NotificationTypeId");

                    b.HasIndex("UserId");

                    b.ToTable("NotificationsSent");

                    b.HasData(
                        new
                        {
                            Id = new Guid("1c0e12d7-0b39-464f-9c00-a4a29b7948ff"),
                            NotificationDatetime = new DateTime(2023, 2, 4, 15, 5, 6, 222, DateTimeKind.Local).AddTicks(6457),
                            NotificationMessage = "Congrats on this achievment! Keep up the good work!",
                            NotificationTypeId = new Guid("2e09803f-2d04-4a1b-8ce2-527958b17e59"),
                            UserId = new Guid("5997c8dc-b95e-45dc-a7b6-622f5d087da7")
                        },
                        new
                        {
                            Id = new Guid("56db6512-3c96-4072-b84c-7735477b1aa7"),
                            NotificationDatetime = new DateTime(2023, 2, 4, 15, 5, 6, 222, DateTimeKind.Local).AddTicks(6707),
                            NotificationMessage = "Congrats on this achievment! Keep up the good work!",
                            NotificationTypeId = new Guid("2e09803f-2d04-4a1b-8ce2-527958b17e59"),
                            UserId = new Guid("42030bd2-0ec4-445a-84fd-71bee25e3405")
                        },
                        new
                        {
                            Id = new Guid("04683347-9718-4f82-af5e-8355edc0a2a8"),
                            NotificationDatetime = new DateTime(2023, 2, 4, 15, 5, 6, 222, DateTimeKind.Local).AddTicks(6723),
                            NotificationMessage = "Congrats on this achievment! Keep up the good work!",
                            NotificationTypeId = new Guid("2e09803f-2d04-4a1b-8ce2-527958b17e59"),
                            UserId = new Guid("37b89795-4297-4678-8193-c98470a721c2")
                        },
                        new
                        {
                            Id = new Guid("f668b4ce-597c-44d3-8383-fdb60eaf06f3"),
                            NotificationDatetime = new DateTime(2023, 2, 4, 15, 5, 6, 222, DateTimeKind.Local).AddTicks(6729),
                            NotificationMessage = "Congrats on this achievment! Keep up the good work!",
                            NotificationTypeId = new Guid("2e09803f-2d04-4a1b-8ce2-527958b17e59"),
                            UserId = new Guid("bf935030-aa9b-463f-8637-357ac08e8d1c")
                        });
                });

            modelBuilder.Entity("NotificationsManagement.Domain.Entities.NotificationType", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("NotificationTypeName")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("Id")
                        .HasName("PK_NotificationType");

                    b.ToTable("NotificationTypes");

                    b.HasData(
                        new
                        {
                            Id = new Guid("2e09803f-2d04-4a1b-8ce2-527958b17e59"),
                            NotificationTypeName = "Congrats"
                        },
                        new
                        {
                            Id = new Guid("dce80c72-6f19-49d2-8034-2501dfa08cc7"),
                            NotificationTypeName = "Warning"
                        });
                });

            modelBuilder.Entity("NotificationsManagement.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreationDatetime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.HasKey("Id")
                        .HasName("PK_User");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("5997c8dc-b95e-45dc-a7b6-622f5d087da7"),
                            CreationDatetime = new DateTime(2023, 2, 4, 15, 5, 6, 218, DateTimeKind.Local).AddTicks(7396),
                            IsActive = true
                        },
                        new
                        {
                            Id = new Guid("42030bd2-0ec4-445a-84fd-71bee25e3405"),
                            CreationDatetime = new DateTime(2023, 2, 4, 15, 5, 6, 221, DateTimeKind.Local).AddTicks(6240),
                            IsActive = true
                        },
                        new
                        {
                            Id = new Guid("37b89795-4297-4678-8193-c98470a721c2"),
                            CreationDatetime = new DateTime(2023, 2, 4, 15, 5, 6, 221, DateTimeKind.Local).AddTicks(6442),
                            IsActive = true
                        },
                        new
                        {
                            Id = new Guid("bf935030-aa9b-463f-8637-357ac08e8d1c"),
                            CreationDatetime = new DateTime(2023, 2, 4, 15, 5, 6, 221, DateTimeKind.Local).AddTicks(6448),
                            IsActive = true
                        });
                });

            modelBuilder.Entity("NotificationsManagement.Domain.Entities.NotificationSent", b =>
                {
                    b.HasOne("NotificationsManagement.Domain.Entities.NotificationType", "NotificationType")
                        .WithMany("NotificationsSent")
                        .HasForeignKey("NotificationTypeId")
                        .HasConstraintName("FK_NotificationSent_NotificationType")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NotificationsManagement.Domain.Entities.User", "User")
                        .WithMany("NotificationsSent")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_NotificationSent_User")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("NotificationType");

                    b.Navigation("User");
                });

            modelBuilder.Entity("NotificationsManagement.Domain.Entities.NotificationType", b =>
                {
                    b.Navigation("NotificationsSent");
                });

            modelBuilder.Entity("NotificationsManagement.Domain.Entities.User", b =>
                {
                    b.Navigation("NotificationsSent");
                });
#pragma warning restore 612, 618
        }
    }
}

using Flunt.Notifications;
using Microsoft.EntityFrameworkCore;
using NotificationsManagement.Domain.Entities;
using System;

namespace NotificationsManagement.Infrastructure.Context
{
    public class NotificationsContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<NotificationType> NotificationTypes { get; set; }
        public DbSet<NotificationSent> NotificationsSent { get; set; }

        public NotificationsContext(DbContextOptions<NotificationsContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<Notification>();

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable(nameof(Users)).HasKey(p => p.Id).HasName($"PK_{nameof(User)}");                
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<NotificationType>(entity =>
            {
                entity.ToTable(nameof(NotificationTypes)).HasKey(p => p.Id).HasName($"PK_{nameof(NotificationType)}");                
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.NotificationTypeName).IsRequired().HasMaxLength(250); ;
            });

            modelBuilder.Entity<NotificationSent>(entity =>
            {
                entity.ToTable(nameof(NotificationsSent));
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.HasKey(e => e.Id).HasName("PK_{nameof(NotificationSent)}");
                entity.Property(e => e.UserId).IsRequired();
                entity.Property(e => e.NotificationTypeId).IsRequired();
                entity.Property(e => e.NotificationMessage).IsRequired();
                entity.Property(e => e.NotificationDatetime).IsRequired();
                entity.HasOne(d => d.User)
                    .WithMany(p => p.NotificationsSent)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName($"FK_{nameof(NotificationSent)}_{nameof(User)}");
                entity.HasOne(d => d.NotificationType)
                    .WithMany(p => p.NotificationsSent)
                    .HasForeignKey(d => d.NotificationTypeId)
                    .HasConstraintName($"FK_{nameof(NotificationSent)}_{nameof(NotificationType)}");
            });

            modelBuilder.Entity<User>().HasData
            (
                new User { Id = Guid.Parse("5997C8DC-B95E-45DC-A7B6-622F5D087DA7"), CreationDatetime = DateTime.Now, IsActive = true },
                new User { Id = Guid.Parse("42030BD2-0EC4-445A-84FD-71BEE25E3405"), CreationDatetime = DateTime.Now, IsActive = true },
                new User { Id = Guid.Parse("37B89795-4297-4678-8193-C98470A721C2"), CreationDatetime = DateTime.Now, IsActive = true },
                new User { Id = Guid.Parse("BF935030-AA9B-463F-8637-357AC08E8D1C"), CreationDatetime = DateTime.Now, IsActive = true }
            );

            modelBuilder.Entity<NotificationType>().HasData
            (
                new NotificationType { Id = Guid.Parse("2E09803F-2D04-4A1B-8CE2-527958B17E59"), NotificationTypeName = "Congrats" },
                new NotificationType { Id = Guid.Parse("DCE80C72-6F19-49D2-8034-2501DFA08CC7"), NotificationTypeName = "Warning" }                
            );

            modelBuilder.Entity<NotificationSent>().HasData
            (
                new NotificationSent { Id = Guid.NewGuid(), UserId = Guid.Parse("5997C8DC-B95E-45DC-A7B6-622F5D087DA7"), NotificationTypeId = Guid.Parse("2E09803F-2D04-4A1B-8CE2-527958B17E59"), NotificationMessage = "Congrats on this achievment! Keep up the good work!", NotificationDatetime = DateTime.Now },
                new NotificationSent { Id = Guid.NewGuid(), UserId = Guid.Parse("42030BD2-0EC4-445A-84FD-71BEE25E3405"), NotificationTypeId = Guid.Parse("2E09803F-2D04-4A1B-8CE2-527958B17E59"), NotificationMessage = "Congrats on this achievment! Keep up the good work!", NotificationDatetime = DateTime.Now },
                new NotificationSent { Id = Guid.NewGuid(), UserId = Guid.Parse("37B89795-4297-4678-8193-C98470A721C2"), NotificationTypeId = Guid.Parse("2E09803F-2D04-4A1B-8CE2-527958B17E59"), NotificationMessage = "Congrats on this achievment! Keep up the good work!", NotificationDatetime = DateTime.Now },
                new NotificationSent { Id = Guid.NewGuid(), UserId = Guid.Parse("BF935030-AA9B-463F-8637-357AC08E8D1C"), NotificationTypeId = Guid.Parse("2E09803F-2D04-4A1B-8CE2-527958B17E59"), NotificationMessage = "Congrats on this achievment! Keep up the good work!", NotificationDatetime = DateTime.Now }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}

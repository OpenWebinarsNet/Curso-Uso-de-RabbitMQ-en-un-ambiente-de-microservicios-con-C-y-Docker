using Flunt.Notifications;
using Microsoft.EntityFrameworkCore;
using System;
using WorkinghoursManagement.Domain.Entities;

namespace WorkinghoursManagement.Infrastructure.Context
{
    public class WorkinghoursContext : DbContext
    {
        public DbSet<WorkingHoursByUser> WorkingHoursByUsers { get; set; }
        public DbSet<User> Users { get; set; }

        public WorkinghoursContext(DbContextOptions<WorkinghoursContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<Notification>();

            modelBuilder.Entity<WorkingHoursByUser>(entity =>
            {
                entity.ToTable(nameof(WorkingHoursByUsers));
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.HasKey(e => e.Id).HasName("PK_{nameof(WorkingHoursByUser)}");
                entity.Property(e => e.UserId).IsRequired();
                entity.Property(e => e.RegisteredDateTime).IsRequired();
                entity.HasOne(d => d.User)
                    .WithMany(p => p.WorkingHoursByUser)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName($"FK_{nameof(WorkingHoursByUser)}_{nameof(User)}");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable(nameof(Users)).HasKey(p => p.Id).HasName($"PK_{nameof(User)}");
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<User>().HasData
            (
                new User { Id = Guid.Parse("5997C8DC-B95E-45DC-A7B6-622F5D087DA7"), CreationDatetime = DateTime.Now, IsActive = true },
                new User { Id = Guid.Parse("42030BD2-0EC4-445A-84FD-71BEE25E3405"), CreationDatetime = DateTime.Now, IsActive = true },
                new User { Id = Guid.Parse("37B89795-4297-4678-8193-C98470A721C2"), CreationDatetime = DateTime.Now, IsActive = true },
                new User { Id = Guid.Parse("BF935030-AA9B-463F-8637-357AC08E8D1C"), CreationDatetime = DateTime.Now, IsActive = true }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
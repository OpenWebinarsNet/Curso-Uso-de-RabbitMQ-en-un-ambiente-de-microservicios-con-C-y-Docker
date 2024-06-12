using Flunt.Notifications;
using Microsoft.EntityFrameworkCore;
using System;
using UsersManagement.Domain.Entities;

namespace UsersManagement.Infrastructure.Context
{
    public class UsersContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public UsersContext(DbContextOptions<UsersContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<Notification>();

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable(nameof(Users)).HasKey(p => p.Id).HasName($"PK_{nameof(Users)}");
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(p => p.Name).IsRequired().HasMaxLength(250);
                entity.Property(p => p.Email).IsRequired().HasMaxLength(50);
                entity.Property(p => p.Password).IsRequired().HasMaxLength(25);
            });

            modelBuilder.Entity<User>().HasData
            (
                new User { Id = Guid.Parse("5997C8DC-B95E-45DC-A7B6-622F5D087DA7"), Name = "Rony Cuzco", Email = "rony@mail.com", Password = "Password12023", IsActive = true },
                new User { Id = Guid.Parse("42030BD2-0EC4-445A-84FD-71BEE25E3405"), Name = "Julie Zarco", Email = "julie@mail.com", Password = "Password12023", IsActive = true },
                new User { Id = Guid.Parse("37B89795-4297-4678-8193-C98470A721C2"), Name = "Ariadne Cuzco Zarco", Email = "ariadne@mail.com", Password = "Password12023", IsActive = true },
                new User { Id = Guid.Parse("BF935030-AA9B-463F-8637-357AC08E8D1C"), Name = "Truferia Zarco", Email = "trufa@mail.com", Password = "Password12023", IsActive = true }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
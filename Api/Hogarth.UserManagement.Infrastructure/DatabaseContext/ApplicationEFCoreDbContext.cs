using Hogarth.UserManagement.Domain.Entities;
using Hogarth.UserManagement.Domain.Seeding;
using Microsoft.EntityFrameworkCore;

namespace Hogarth.UserManagement.Infrastructure.DatabaseContext
{
    public class ApplicationEFCoreDbContext : DbContext
    {
        public ApplicationEFCoreDbContext (DbContextOptions<ApplicationEFCoreDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Role> Roles { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed Role Data
            modelBuilder.Entity<Role>().HasData(SeedData.GetRoles());

            // Seed Contact Data
            modelBuilder.Entity<Contact>().HasData(SeedData.GetContacts());

            // Seed User Data
            modelBuilder.Entity<User>().HasData(SeedData.GetUsers());

            modelBuilder.Entity<User>()
                .HasOne(u => u.Contact)
                .WithOne(c => c.User)
                .HasForeignKey<User>(u => u.ContactId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}

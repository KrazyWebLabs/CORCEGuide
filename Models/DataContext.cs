using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORCEGuideApp.Models
{
    public partial class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> dbContextOptions) : base(dbContextOptions) { } // Hace referencia al constructor base de DbContext

        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Favorite model
            modelBuilder.Entity<Favorite>()
                .HasKey(f => f.FavoriteId);

            modelBuilder.Entity<Favorite>()
                .HasOne(f => f.User)
                .WithMany(u => u.Favorites)
                .HasForeignKey("UserId");

            modelBuilder.Entity<Favorite>()
                .HasOne(f => f.Announcement)
                .WithMany(a => a.Favorites)
                .HasForeignKey("AnnouncementId");

            // User model
            modelBuilder.Entity<User>()
                .HasKey(u => u.UserId);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey("RoleId");

            // Role model
            modelBuilder.Entity<Role>()
                .HasKey(r => r.RoleId);

            // Category model
            modelBuilder.Entity<Category>()
                .HasKey(c => c.CategoryId);

            // Announcement model
            modelBuilder.Entity<Announcement>()
                .HasKey(a => a.AnnouncementId);

            modelBuilder.Entity<Announcement>()
                .HasOne(a => a.Category)
                .WithMany(c => c.Announcements)
                .HasForeignKey("CategoryId");

        }
    }
}

using Microsoft.EntityFrameworkCore;
using Szoftverfejlesztés_dotnet_hw.DAL.Entities;

namespace Szoftverfejlesztés_dotnet_hw.DAL
{
    public class AppDbContext : DbContext
    {

        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Groups)
                .WithMany(g => g.Users);

            modelBuilder.Entity<Event>()
                .HasOne(e => e.Group)
                .WithMany(g => g.Events)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Event>()
                .HasOne(e => e.Creator)
                .WithMany(u => u.CreatedEvents)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Creator)
                .WithMany(u => u.Comments)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Event)
                .WithMany(e => e.Comments)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)  
                .IsUnique();

            modelBuilder.Entity<Group>()
                .HasIndex(g => g.Groupname)
                .IsUnique();

            var admin = new User { Id = 1, Username = "admin", Password = "admin" };
            var user1 = new User { Id = 2, Username = "user1", Password = "user1" };
            var adminGroup = new Group { Id = 1, Groupname = "admin", Creatorname = "admin" };
            var group1 = new Group { Id = 2, Groupname = "group1", Creatorname = "admin" };
            modelBuilder.Entity<User>().HasData(
                               admin,user1);

            modelBuilder.Entity<Group>().HasData(
                    adminGroup,group1);

            

        }
    }
}

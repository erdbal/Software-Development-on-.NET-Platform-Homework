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

            
            //alternate key
            modelBuilder.Entity<User>()
                .HasAlternateKey(u => u.Username);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Groups)
                .WithMany(g => g.Users)
                .UsingEntity(j => j.HasData(
                    new { UsersId = 1, GroupsId = 1 },
                    new { UsersId = 1, GroupsId = 2 },
                    new { UsersId = 2, GroupsId = 2 }
                ));

            modelBuilder.Entity<Event>()
                .HasOne(e => e.Group)
                .WithMany(g => g.Events)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Event>()
                .HasOne(e => e.Creator)
                .WithMany(u => u.CreatedEvents)
                .OnDelete(DeleteBehavior.Restrict);

            //relationship using alternate key
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Creator)
                .WithMany(u => u.Comments)
                .HasPrincipalKey(u => u.Username)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Event)
                .WithMany(e => e.Comments)
                .OnDelete(DeleteBehavior.Restrict);

            //index for unique groupname
            modelBuilder.Entity<Group>()
                .HasIndex(g => g.Groupname)
                .IsUnique();

            //if (!StaticSettings.IsTesting)
            //{
            //    modelBuilder.Entity<User>()
            //        .Property(u => u.Id)
            //        .UseHiLo();

            //    modelBuilder.Entity<Group>()
            //        .Property(g => g.Id)
            //        .UseHiLo();

            //    modelBuilder.Entity<Event>()
            //        .Property(e => e.Id)
            //        .UseHiLo();

            //    modelBuilder.Entity<Comment>()
            //        .Property(c => c.Id)
            //        .UseHiLo();
            //}

            var admin = new User { Id = 1, Username = "admin", Password = "admin" };
            var user1 = new User { Id = 2, Username = "asd", Password = "asd" };
            var adminGroup = new Group { Id = 1, Groupname = "admin", Creatorname = "admin" };
            var group1 = new Group { Id = 2, Groupname = "group1", Creatorname = "admin" };
            var admingroupevent = new Event { Id = 1, Eventname = "event1", Description = "desc1",Location="Budapest", Date = new System.DateTime(2022, 1, 1), GroupId = 1, CreatorId = 1 };
            var group1event = new Event { Id = 2, Eventname = "event1", Description = "desc1",Location="Budapest", Date = new System.DateTime(2022, 1, 1), GroupId = 2, CreatorId = 2 };
            modelBuilder.Entity<User>().HasData(
                               admin,user1);

            modelBuilder.Entity<Group>().HasData(
                    adminGroup,group1);

            modelBuilder.Entity<Event>().HasData(admingroupevent,group1event);

            

        }
    }
}

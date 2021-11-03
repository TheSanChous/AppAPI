using Microsoft.EntityFrameworkCore;
using Models.Auth;
using Models.Species;
using Group = Data.Models.Species.Group;
using GroupMemberType = Data.Models.Species.GroupMemberType;
using Homework = Data.Models.Species.Homework;
using Subject = Data.Models.Species.Subject;
using UserGroup = Data.Models.Species.UserGroup;

namespace Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<UserGroup> UsersGroups { get; set; }

        public DbSet<Group> Groups { get; set; }

        public DbSet<Subject> Subjects { get; set; }

        public DbSet<Homework> Homeworks { get; set; }

        public DbSet<GroupMemberType> GroupMemberTypes { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) 
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}

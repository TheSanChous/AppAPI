using AppAPI.Models;
using Data.Models.Auth;
using Data.Models.Species;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public sealed class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<UserGroup> UsersGroups { get; set; }

        public DbSet<Group> Groups { get; set; }

        public DbSet<Subject> Subjects { get; set; }

        public DbSet<Homework> Homework { get; set; }

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

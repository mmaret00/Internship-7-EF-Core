using DataLayer.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Linq;

namespace DataLayer.Entities
{
    public class StackInternshipDbContext : DbContext
    {
        public StackInternshipDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Entry> Entries { get; set; }
        public DbSet<UserEntry> UserEntries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<UserEntry>()
                .HasOne(ue => ue.User)
                .WithMany(u => u.UserEntries)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder
                .Entity<UserEntry>()
                .HasOne(ue => ue.Entry)
                .WithMany(e => e.UserEntries)
                .OnDelete(DeleteBehavior.NoAction);

            DbSeeder.Execute(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }
    }

    public class StackInternshipContextFactory : IDesignTimeDbContextFactory<StackInternshipDbContext>
    {
        public StackInternshipDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddXmlFile("App.config")
                .Build();

            configuration
                .Providers
                .First()
                .TryGet("connectionStrings:add:StackInternship:connectionString", out var connectionString);

            var options = new DbContextOptionsBuilder<StackInternshipDbContext>()
                .UseSqlServer(connectionString)
                .Options;

            return new StackInternshipDbContext(options);
        }
    }
}

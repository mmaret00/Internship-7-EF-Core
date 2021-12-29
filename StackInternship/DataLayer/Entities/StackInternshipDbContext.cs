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
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //ovdi se grade n-m veze izmedu tablica (vidit u duje)



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

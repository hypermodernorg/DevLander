using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using BL;

namespace DAL
{
    public class PuzzleContext : DbContext
    {
        public PuzzleContext(DbContextOptions<PuzzleContext> options) : base(options) { }

        public virtual DbSet<Puzzle> Puzzles { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
    }

    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<PuzzleContext>
    {
        public PuzzleContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(@Directory.GetCurrentDirectory() + "/../WDP/appsettings.json").Build();
            var builder = new DbContextOptionsBuilder<PuzzleContext>();
            var connectionString = configuration.GetConnectionString("PuzzleDbContextConnection");
            builder.UseSqlite(connectionString);
            return new PuzzleContext(builder.Options);
        }
    }
}

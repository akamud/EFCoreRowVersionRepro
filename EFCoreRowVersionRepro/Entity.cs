using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreRowVersionRepro
{
    public class Entity
    {
        public int Id { get; set; }
        public ulong RowVersion { get; set; }
    }
    public class BloggingContext : DbContext
    {
        public DbSet<Entity> Entities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer("Data Source=(LocalDb)\\MSSQLLocalDB;Integrated Security=true;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Entity>()
                .Property(x => x.RowVersion)
                .IsRowVersion()
                .HasConversion<byte[]>();
        }
    }
}

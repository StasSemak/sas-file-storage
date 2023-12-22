using DataLayer.Generators;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {

        }
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Storage>()
                .Property(x => x.Id)
                .HasValueGenerator<GuidGenerator>();
            builder.Entity<Storage>()
                .Property(x => x.CreatedAt)
                .HasValueGenerator<DateGenerator>();
        }

        public DbSet<Storage> Storages { get; set; }
    }
}

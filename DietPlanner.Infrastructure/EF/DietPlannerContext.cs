using DietPlanner.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DietPlanner.Infrastructure.EF
{
    public class DietPlannerContext: DbContext
    {
        private readonly SqlSettings _sqlSettings;

        public DbSet<User> Users { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<WeightPoint> WeightPoints { get; set; }
        public DbSet<CustomDay> CustomDays { get; set; }

        public DietPlannerContext(DbContextOptions<DietPlannerContext> options, SqlSettings sqlSettings) : base(options)
        {
            _sqlSettings = sqlSettings;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(_sqlSettings.InMemory)
            {
                optionsBuilder.UseInMemoryDatabase();
                return;
            }
            optionsBuilder.UseSqlServer(_sqlSettings.ConnectionString, b => b.MigrationsAssembly("DietPlanner.Api"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(u => u.Plan)
                .WithOne(p => p.User)
                .HasForeignKey<Plan>(e => e.UserId);
          //      .HasKey(u => u.UserId);
        }
    }
}

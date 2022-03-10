using Actions.Core.Domain.Risks.Entities;
using Actions.Core.Domain.Users.Entities;
using Actions.Infrasctructure.Data.Configurations;
using Actions.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actions.Infrastructure.Data
{
    public class ActionsContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Risk> Risks { get; set; }

        public ActionsContext(DbContextOptions<ActionsContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RiskConfiguration());

            modelBuilder.ApplyConfiguration(new UserConfiguration());

            modelBuilder.Entity<FeatureProfile>().ToView("FeatureProfile").HasKey(x => x.Id);
        }
    }
}

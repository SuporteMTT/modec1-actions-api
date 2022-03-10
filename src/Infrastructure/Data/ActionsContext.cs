﻿using Actions.Core.Domain.Deviations.Entities;
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
        public DbSet<Core.Domain.Actions.Entities.Action> Actions { get; set; }
        public DbSet<Deviation> Deviations { get; set; }
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
            modelBuilder.ApplyConfiguration(new ActionConfiguration());
            modelBuilder.ApplyConfiguration(new DeviationConfiguration());
            modelBuilder.ApplyConfiguration(new RiskConfiguration());

            modelBuilder.ApplyConfiguration(new UserConfiguration());

            modelBuilder.Entity<FeatureProfile>().ToView("FeatureProfile").HasKey(x => x.Id);
        }
    }
}

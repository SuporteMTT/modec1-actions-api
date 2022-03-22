using Actions.Core.Domain.Departments.Entities;
using Actions.Core.Domain.Deviations.Entities;
using Actions.Core.Domain.ResponsePlans.Entities;
using Actions.Core.Domain.Risks.Entities;
using Actions.Core.Domain.StatusHistories.Entities;
using Actions.Core.Domain.Tasks.Entities;
using Actions.Core.Domain.Users.Entities;
using Actions.Infrasctructure.Data.Configurations;
using Actions.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Actions.Infrastructure.Data
{
    public class ActionsContext : DbContext
    {
        public DbSet<Core.Domain.Actions.Entities.Action> Actions { get; set; }
        public DbSet<Deviation> Deviations { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Risk> Risks { get; set; }
        public DbSet<ResponsePlan> ResponsePlans { get; set; }
        public DbSet<StatusHistory> StatusHistories { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<ProjectTask> ProjectTasks { get; set; }
        public DbSet<RiskTask> RiskTasks { get; set; }

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
            modelBuilder.ApplyConfiguration(new ResponsePlanConfiguration());
            modelBuilder.ApplyConfiguration(new RiskConfiguration());
            modelBuilder.ApplyConfiguration(new RiskTaskConfiguration());
            modelBuilder.ApplyConfiguration(new StatusHistoryConfiguration());

            modelBuilder.ApplyConfiguration(new DepartmentConfiguration());
            modelBuilder.ApplyConfiguration(new ProjectTaskConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());

            modelBuilder.Entity<FeatureProfile>().ToView("FeatureProfile").HasKey(x => x.Id);
        }
    }
}

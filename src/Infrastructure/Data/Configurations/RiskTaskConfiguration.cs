using Actions.Core.Domain.Risks.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;

namespace Actions.Infrasctructure.Data.Configurations
{
    [ExcludeFromCodeCoverage]
    public class RiskTaskConfiguration : IEntityTypeConfiguration<RiskTask>
    {
        void IEntityTypeConfiguration<RiskTask>.Configure(EntityTypeBuilder<RiskTask> builder)
        {
            builder.ToTable("RiskTask");

            builder.HasKey(x => new { x.RiskId, x.ProjectTaskId });

            builder.Property(x => x.ProjectTaskId).HasColumnName("TaskId");

            builder.HasOne(x => x.Risk).WithMany(x => x.RiskTask).HasForeignKey(x => x.RiskId).IsRequired();

            builder.HasOne(x => x.ProjectTask).WithMany(x => x.RiskTask).HasForeignKey(x => x.ProjectTaskId).IsRequired();
        }
    }
}

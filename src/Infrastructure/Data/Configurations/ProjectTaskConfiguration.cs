using Actions.Core.Domain.Tasks.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;

namespace Actions.Infrasctructure.Data.Configurations
{
    public class ProjectTaskConfiguration : IEntityTypeConfiguration<ProjectTask>
    {
        [ExcludeFromCodeCoverage]
        void IEntityTypeConfiguration<ProjectTask>.Configure(EntityTypeBuilder<ProjectTask> builder)
        {
            builder.ToTable("ProjectTask");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasMaxLength(36);

            builder.Property(x => x.ProjectId);

            builder.Property(x => x.ShowInGantt);

            builder.Property(x => x.Title);
        }
    }
}

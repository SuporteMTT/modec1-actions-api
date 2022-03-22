using Actions.Core.Domain.Tasks.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;

namespace Actions.Infrasctructure.Data.Configurations
{
    [ExcludeFromCodeCoverage]
    public class TaskConfiguration : IEntityTypeConfiguration<Task>
    {
        void IEntityTypeConfiguration<Task>.Configure(EntityTypeBuilder<Task> builder)
        {
            builder.ToTable("Task");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.ProjectId);

            builder.Property(x => x.ShowInGantt);

            builder.Property(x => x.Title);
        }
    }
}

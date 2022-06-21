using Actions.Core.Domain.Actions.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Actions.Infrastructure.Data.Configurations
{
    public class ActionConfiguration : IEntityTypeConfiguration<Action>
    {
        void IEntityTypeConfiguration<Action>.Configure(EntityTypeBuilder<Action> builder)
        {
            builder.ToTable("Action");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasMaxLength(36);

            builder.Property(x => x.Status).IsRequired();

            builder.Property(x => x.Description).HasMaxLength(100).IsRequired();
            builder.Property(x => x.DueDate).IsRequired();
            builder.Property(x => x.OriginalDueDate);

            builder.HasOne(x => x.Responsible).WithMany().HasForeignKey(x => x.ResponsibleId);

            builder.Property(x => x.ActualStartDate);
            builder.Property(x => x.ActualEndDate);

            builder.Property(x => x.Cost);
            builder.Property(x => x.Comments);

            builder.HasOne(x => x.ClosedBy).WithMany().HasForeignKey(x => x.ClosedById);

            builder.Property(x => x.ClosedDate);
            builder.Property(x => x.CreatedDate);

        }
    }
}

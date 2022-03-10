using Actions.Core.Domain.Risks.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actions.Infrastructure.Data.Configurations
{
    public class RiskConfiguration : IEntityTypeConfiguration<Risk>
    {
        void IEntityTypeConfiguration<Risk>.Configure(EntityTypeBuilder<Risk> builder)
        {
            builder.ToTable("Risk");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasMaxLength(36);

            builder.Property(x => x.Code).HasMaxLength(20).IsRequired();
            builder.HasIndex(x => x.Code).IsUnique();

            builder.Property(x => x.Status).IsRequired();

            builder.HasOne(x => x.Owner).WithMany().HasForeignKey(x => x.OwnerId);

            builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Description).IsRequired();
            builder.Property(x => x.Cause);
            builder.Property(x => x.Impact).IsRequired();
            builder.Property(x => x.Category).IsRequired();
            builder.Property(x => x.Level).IsRequired();
            builder.Property(x => x.DimensionDescription);
            builder.Property(x => x.ProjectStep).IsRequired();

            builder.Property(x => x.CreateDate).IsRequired();
            builder.Property(x => x.CreateById).IsRequired();
            builder.Property(x => x.ClosedCancelledDate);
            builder.Property(x => x.ClosedCancelledById);
            builder.Property(x => x.Justification);
            builder.Property(x => x.RealImpact);
        }
    }
}

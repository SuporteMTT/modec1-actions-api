using Actions.Core.Domain.Deviations.Entities;
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
    public class DeviationConfiguration : IEntityTypeConfiguration<Deviation>
    {
        void IEntityTypeConfiguration<Deviation>.Configure(EntityTypeBuilder<Deviation> builder)
        {
            builder.ToTable("Deviation");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasMaxLength(36);

            builder.Property(x => x.Code).HasMaxLength(20).IsRequired();
            builder.HasIndex(x => x.Code).IsUnique();

            builder.Property(x => x.Status).IsRequired();

            builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Description).IsRequired();
            builder.Property(x => x.Cause);
            builder.Property(x => x.Category).IsRequired();

            builder.HasOne(x => x.AssociatedRisk).WithMany().HasForeignKey(x => x.AssociatedRiskId);

            builder.Property(x => x.Priority);
            builder.Property(x => x.CreatedDate).IsRequired();
            builder.Property(x => x.CreatedById).IsRequired();

            builder.HasOne(x => x.CreatedBy).WithMany().HasForeignKey(x => x.CreatedById);

            builder.Property(x => x.ClosedCancelledDate);

            builder.HasOne(x => x.ClosedCancelledBy).WithMany().HasForeignKey(x => x.ClosedCancelledById);
            
        }
    }
}

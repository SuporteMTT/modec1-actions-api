using Actions.Core.Domain.ResponsePlans.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actions.Infrastructure.Data.Configurations
{
    public class ResponsePlanConfiguration : IEntityTypeConfiguration<ResponsePlan>
    {
        void IEntityTypeConfiguration<ResponsePlan>.Configure(EntityTypeBuilder<ResponsePlan> builder)
        {
            builder.ToTable("ResponsePlan");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasMaxLength(36);

            builder.Property(x => x.Status).IsRequired();

            builder.Property(x => x.ActionDescription).HasMaxLength(200).IsRequired();
            builder.Property(x => x.DueDate).IsRequired();
            builder.Property(x => x.OriginalDueDate);

            builder.Property(x => x.ResponsibleId).IsRequired();
            builder.HasOne(x => x.Responsible).WithMany().HasForeignKey(x => x.ResponsibleId);

            builder.Property(x => x.ActualStartDate);
            builder.Property(x => x.ActualEndDate);

            builder.Property(x => x.Cost);
            builder.Property(x => x.Comments);

            builder.Property(x => x.ClosedById);
            builder.HasOne(x => x.ClosedBy).WithMany().HasForeignKey(x => x.ClosedById);

            builder.Property(x => x.ClosedDate);
            builder.Property(x => x.CreatedDate);

        }
    }
}

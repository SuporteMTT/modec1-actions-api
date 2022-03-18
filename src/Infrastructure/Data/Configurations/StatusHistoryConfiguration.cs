using Actions.Core.Domain.StatusHistories.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Actions.Infrastructure.Data.Configurations
{
    public class StatusHistoryConfiguration : IEntityTypeConfiguration<StatusHistory>
    {
        void IEntityTypeConfiguration<StatusHistory>.Configure(EntityTypeBuilder<StatusHistory> builder)
        {
            builder.ToTable("StatusHistory");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasMaxLength(36);

            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.Date).IsRequired();
            builder.Property(x => x.UserId).IsRequired().HasMaxLength(36);
            builder.Property(x => x.MetadataId).IsRequired().HasMaxLength(36);

            builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId);
        }
    }
}

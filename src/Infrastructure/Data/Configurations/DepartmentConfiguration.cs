using System.Diagnostics.CodeAnalysis;
using Actions.Core.Domain.Departments.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Actions.Infrastructure.Data.Configurations
{
    [ExcludeFromCodeCoverage]
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        void IEntityTypeConfiguration<Department>.Configure(EntityTypeBuilder<Department> builder)
        {
            builder.ToView("Department");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasMaxLength(36);

            builder.Property(x => x.RegionId).HasColumnName("StanaRegion");

            builder.Property(x => x.ParentId).HasColumnName("SuperiorDepartmentId");

            builder.Property(x => x.Code);

            builder.Property(x => x.Name);

            builder.Property(x => x.Order);
        }
    }
}
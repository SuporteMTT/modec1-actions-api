using Actions.Core.Domain.Actions.Enums;
using Shared.Core.Domain.Impl.Entity;
using System;

namespace Actions.Core.Domain.Actions.Entities
{
    public class Risk : Entity<Risk, string>
    {
        public string Code { get; set; }
        public StatusEnum Status { get; set; }
        public string OwnerId { get; set; }
        public string AssociatedTaskId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Cause { get; set; }
        public string Impact { get; set; }
        public RiskCategoryEnum Category { get; set; }
        public RiskLevelEnum Level { get; set; }
        public DimensionEnum Dimension { get; set; }
        public string DimensionDescription { get; set; }
        public ProjectStepEnum ProjectStep { get; set; }

        public DateTime CreateDate { get; set; }
        public string CreateById { get; set; }
        public DateTime? ClosedCancelledDate { get; set; }
        public string ClosedCancelledById { get; set; }
        public RiskJustificationEnum Justification { get; set; }
        public string RealImpact { get; set; }
        public OriginTypeEnum OriginType { get; set; }
        public string OriginId { get; set; }
    }
}
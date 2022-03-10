using Actions.Core.Domain.Deviations.Enums;
using Actions.Core.Domain.Risks.Entities;
using Actions.Core.Domain.Shared.Enums;
using Actions.Core.Domain.Users.Entities;
using Shared.Core.Domain.Impl.Entity;
using System;

namespace Actions.Core.Domain.Deviations.Entities
{
    public class Deviation : Entity<Deviation, string>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public StatusEnum Status { get; set; }
        public RiskCategoryEnum Category { get; set; }
        public Risk AssociatedRisk { get; set; }
        public string AssociatedRiskId { get; set; }
        public string Cause { get; set; }
        public PriorityEnum Priority { get; set; }
        public DateTime CreatedDate { get; set; }
        public User CreatedBy { get; set; }
        public string CreatedById { get; set; }
        public DateTime? ClosedCancelledDate { get; set; }
        public User ClosedCancelledBy { get; set; }
        public string ClosedCancelledById { get; set; }
        public MetadataTypeEnum MetadataType { get; set; }
        public string MetadataId { get; set; }
    }
}

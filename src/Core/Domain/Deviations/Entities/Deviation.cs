using Actions.Core.Domain.Deviations.Enums;
using Actions.Core.Domain.Risks.Entities;
using Actions.Core.Domain.Shared.Enums;
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
        public string CreatedById { get; set; }
        public DateTime? ClosedDate { get; set; }
        public string ClosedById { get; set; }
        public MetadataTypeEnum MetadataType { get; set; }
        public string MetadataId { get; set; }
    }
}

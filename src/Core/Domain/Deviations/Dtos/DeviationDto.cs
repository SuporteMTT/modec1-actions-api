using System;
using Actions.Core.Domain.Deviations.Enums;
using Actions.Core.Domain.Risks.Dtos;
using Actions.Core.Domain.Shared.Enums;

namespace Actions.Core.Domain.Deviations.Dtos
{
    public class DeviationDto
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public StatusEnum Status { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Cause { get; set; }
        public RiskCategoryEnum Category { get; set; }
        public RiskDto AssociatedRisk { get; set; }
        public PriorityEnum Priority { get; set; }      
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ClosedCancelledDate { get; set; }
        public string ClosedCancelledBy { get; set; }
    }
}

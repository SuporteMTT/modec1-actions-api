using Actions.Core.Domain.ResponsePlans.Dtos;
using Actions.Core.Domain.Risks.Enums;
using Actions.Core.Domain.Shared.Enums;
using Actions.Core.Domain.Users.Dtos;
using System;
using System.Collections.Generic;

namespace Actions.Core.Domain.Risks.Dtos
{
    public class RiskDto
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public StatusEnum Status { get; set; }
        public UserDto Owner { get; set; }
        public ICollection<string> AssociatedTaskIds { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Cause { get; set; }
        public string Impact { get; set; }
        public RiskCategoryEnum Category { get; set; }
        public RiskLevelEnum Level { get; set; }
        public DimensionEnum Dimension { get; set; }
        public string DimensionDescription { get; set; }
        public ProjectStepEnum ProjectStep { get; set; }        
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ClosedCancelledDate { get; set; }
        public string ClosedCancelledBy { get; set; }
        public RiskJustificationEnum Justification { get; set; }
        public string RealImpact { get; set; }
        public ICollection<ResponsePlanDto> ResponsePlans { get; set; }
        
    }
}

using Actions.Core.Domain.Shared.Dtos;
using Actions.Core.Domain.Shared.Enums;
using System;

namespace Actions.Core.Domain.Risks.Dtos
{
    public class RiskDto
    {
        public string Id { get; set; }
        public DateTime CreateDate { get; set; }
        public RiskLevelEnum Level { get; set; }
        public string Name { get; set; }
        public string Cause { get; set; }
        public string Owner { get; set; }
        public int? NotInitated { get; set; }
        public int? OnGoing { get; set; }
        public int? Concluded { get; set; }
        public int? Delayed { get; set; }
        public StatusDTO Status { get; set; }
        public DateTime? ClosedCancelledDate { get; set; }
    }
}

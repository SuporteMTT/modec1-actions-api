using Actions.Core.Domain.ResponsePlans.Entities;
using Actions.Core.Domain.Shared.Dtos;
using System;
using System.Collections.Generic;

namespace Actions.Core.Domain.Risks.Dtos
{
    public class RiskListDto
    {
        public string Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public StatusDTO Level { get; set; }
        public string Name { get; set; }
        public string Cause { get; set; }
        public string Owner { get; set; }
        public int? NotInitiated { get; set; }
        public int? OnGoing { get; set; }
        public int? Concluded { get; set; }
        public int? Delayed { get; set; }
        public StatusDTO Status { get; set; }
        public DateTime? ClosedCancelledDate { get; set; }
        public List<Actions.Entities.Action> ResponsePlans { get; set; }
        public string Code { get; set; }
    }
}

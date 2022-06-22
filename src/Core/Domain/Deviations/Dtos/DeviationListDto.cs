using Actions.Core.Domain.Shared.Dtos;
using Actions.Core.Domain.Users.Entities;
using System;

namespace Actions.Core.Domain.Deviations.Dtos
{
    public class DeviationListDto
    {
        public string Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public PriorityDTO Priority { get; set; }
        public string Name { get; set; }
        public string Cause { get; set; }
        public int? NotInitiated { get; set; }
        public int? OnGoing { get; set; }
        public int? Concluded { get; set; }
        public int? Delayed { get; set; }
        public StatusDTO Status { get; set; }
        public DateTime? ClosedCancelledDate { get; set; }
        public User Responsible { get; set; }
    }
}

using Actions.Core.Domain.Deviations.Enums;
using Actions.Core.Domain.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actions.Core.Domain.Deviations.Dtos
{
    public class DeviationListDto
    {
        public string Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public PriorityEnum Priority { get; set; }
        public string Name { get; set; }
        public string Cause { get; set; }
        public ActionsStatusCountDto Actions { get; set; }
        public StatusDTO Status { get; set; }
        public DateTime? ClosedCancelledDate { get; set; }
    }
}

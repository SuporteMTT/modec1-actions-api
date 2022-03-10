using Actions.Core.Domain.Shared.Dtos;
using Actions.Core.Domain.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actions.Core.Domain.Risks.Dtos
{
    public class RiskListDto
    {
        public string Id { get; set; }
        public DateTime CreateDate { get; set; }
        public RiskLevelEnum Level { get; set; }
        public string Name { get; set; }
        public string Cause { get; set; }
        public string Owner { get; set; }
        public ActionsStatusCountDto Actions { get; set; }
        public StatusDTO Status { get; set; }
        public DateTime? ClosedCancelledDate { get; set; }
    }
}

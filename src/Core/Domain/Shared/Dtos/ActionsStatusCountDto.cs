using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actions.Core.Domain.Shared.Dtos
{
    public class ActionsStatusCountDto
    {
        public StatusCountDto NotInitiated { get; set; }
        public StatusCountDto OnGoing { get; set; }
        public StatusCountDto ConcludedOrDelayed { get; set; }
    }
}

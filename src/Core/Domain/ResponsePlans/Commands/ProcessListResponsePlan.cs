using System.Collections.Generic;
using Actions.Core.Domain.ResponsePlans.Dtos;

namespace Actions.Core.Domain.ResponsePlans.Commands
{
    public class ProcessListResponsePlan
    {
        public List<ResponsePlanDto> ResponsePlanList { get; set; }
        public string MetadataId { get; set; }
    }
}
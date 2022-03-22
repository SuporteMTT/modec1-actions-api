using System.Collections.Generic;
using Actions.Core.Domain.ResponsePlans.Dtos;

namespace Actions.Core.Domain.ResponsePlans.Commands
{
    public class ProcessListResponsePlan
    {
        public ProcessListResponsePlan(string metadataId, ICollection<ResponsePlanDto> responsePlanList)
        {
            MetadataId = metadataId;
            ResponsePlanList = responsePlanList;
        }
        public ICollection<ResponsePlanDto> ResponsePlanList { get; set; }
        public string MetadataId { get; set; }
    }
}
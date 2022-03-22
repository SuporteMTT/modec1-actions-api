using Actions.Core.Domain.Tasks.Entities;

namespace Actions.Core.Domain.Risks.Entities
{
    public class RiskTask
    {
        public Risk Risk { get; set; }
        public string RiskId { get; set; }
        public ProjectTask ProjectTask { get; set; }
        public string ProjectTaskId { get; set; }
    }
}
using Actions.Core.Domain.Tasks.Entities;

namespace Actions.Core.Domain.Risks.Entities
{
    public class RiskTask
    {
        public Risk Risk { get; set; }
        public string RiskId { get; set; }
        public Task Task { get; set; }
        public string TaskId { get; set; }
    }
}
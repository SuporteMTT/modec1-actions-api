using System.Collections.Generic;
using Actions.Core.Domain.Risks.Entities;
using Shared.Core.Domain.Impl.Entity;

namespace Actions.Core.Domain.Tasks.Entities
{
    public class Task : Entity<Task, string>
    {
        public string ProjectId { get; set; }
        public bool ShowInGantt { get; set; }
        public string Title { get; set; }

        public ICollection<RiskTask> RiskTask { get; set; }
    }
}
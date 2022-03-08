using Actions.Core.Domain.Actions.Enums;
using Shared.Core.Domain.Impl.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actions.Core.Domain.Actions.Entities
{
    public class Deviation : Entity<Deviation, string>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public StatusEnum Status { get; set; }
        public RiskCategoryEnum Category { get; set; }
        public string AssociatedRiskId { get; set; }
        public string Cause { get; set; }
        public PriorityEnum Priority { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedById { get; set; }
        public DateTime? ClosedDate { get; set; }
        public string ClosedById { get; set; }
        public OriginTypeEnum OriginType { get; set; }
        public string OriginId { get; set; }
    }
}

using Actions.Core.Domain.Actions.Enums;
using Actions.Core.Domain.Users.Entities;
using Shared.Core.Domain.Impl.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actions.Core.Domain.ResponsePlans.Entities
{
    public class ResponsePlan : Entity<Action, string>
    {
        public string ActionDescription { get; set; }
        public User Responsible { get; set; }
        public string ResponsibleId { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime OriginalDueDate { get; set; }
        public ActionStatusEnum Status { get; set; }
        public DateTime ActualStartDate { get; set; }
        public DateTime ActualEndDate { get; set; }
        public decimal? Cost { get; set; }
        public string Comments { get; set; }
        public DateTime? ClosedDate { get; set; }
        public User ClosedBy { get; set; }
        public string ClosedById { get; set; }
        public string MetadataId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}

using Actions.Core.Domain.Actions.Enums;
using Actions.Core.Domain.Shared.Enums;
using Shared.Core.Domain.Impl.Entity;
using System;

namespace Actions.Core.Domain.Actions.Entities
{
    public class Action : Entity<Action, string>
    {
        public string RelatedId { get; set; }
        public string Description { get; set; }
        public string ResponsibleId { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime OriginalDueDate { get; set; }
        public ActionStatusEnum Status { get; set; }
        public DateTime ActualStartDate { get; set; }
        public DateTime ActualEndDate { get; set; }
        public decimal? Cost { get; set; }
        public string Comments { get; set; }
        public DateTime? ClosedDate { get; set; }
        public string ClosedById { get; set; }
        public MetadataTypeEnum MetadataType { get; set; }
        public string MetadataId { get; set; }
    }
}
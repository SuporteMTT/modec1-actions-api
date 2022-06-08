using Actions.Core.Domain.Actions.Enums;
using Actions.Core.Domain.Users.Entities;
using Shared.Core.Domain.Impl.Entity;
using Shared.CrossCutting.Tools;
using System;

namespace Actions.Core.Domain.ResponsePlans.Entities
{
    public class ResponsePlan : Entity<Action, string>
    {
        public ResponsePlan(
            string id,
            string actionDescription,
            string responsibleId,
            DateTime? dueDate,
            DateTime? originalDueDate,
            ActionStatusEnum status,
            DateTime? actualStartDate,
            DateTime? actualEndDate,
            decimal? cost,
            string comments,
            string metadataId,
            DateTime createdDate
        )
        {
            Id = id;
            ActionDescription = actionDescription;
            ResponsibleId = responsibleId;
            Status = status;
            DueDate = dueDate;
            OriginalDueDate = originalDueDate;
            ActualStartDate = actualStartDate;
            ActualEndDate = actualEndDate;
            Cost = cost;
            Comments = comments;
            MetadataId = metadataId;
            CreatedDate = createdDate;
        }

        public ResponsePlan(
            string actionDescription,
            string responsibleId,
            DateTime? dueDate,
            DateTime? originalDueStart,
            ActionStatusEnum status,
            DateTime? actualStartDate,
            DateTime? actualEndDate,
            decimal? cost,
            string comments,
            string metadataId
        ) : this (
            GuidExtensions.GenerateGuid(),
            actionDescription,
            responsibleId,
            dueDate,
            originalDueStart,
            status,
            actualStartDate,
            actualEndDate,
            cost,
            comments,
            metadataId,
            DateTime.Now
        ) {}

        public string ActionDescription { get; set; }
        public User Responsible { get; set; }
        public string ResponsibleId { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? OriginalDueDate { get; set; }
        public ActionStatusEnum Status { get; set; }
        public DateTime? ActualStartDate { get; set; }
        public DateTime? ActualEndDate { get; set; }
        public decimal? Cost { get; set; }
        public string Comments { get; set; }
        public DateTime? ClosedDate { get; set; }
        public User ClosedBy { get; set; }
        public string ClosedById { get; set; }
        public string MetadataId { get; set; }
        public DateTime CreatedDate { get; set; }

        internal void UpdateData(
            string actionDescription,
            string responsibleId,
            DateTime? dueDate,
            DateTime? originalDueDate,
            ActionStatusEnum status,
            DateTime? actualStartDate,
            DateTime? actualEndDate,
            decimal? cost,
            string comments,
            string closedById
        )
        {
            if (!OriginalDueDate.HasValue && DueDate != dueDate)
                OriginalDueDate = DueDate;

            ActionDescription = actionDescription;
            ResponsibleId = responsibleId;
            Status = status;
            DueDate = dueDate;
            OriginalDueDate = originalDueDate;
            ActualStartDate = actualStartDate;
            ActualEndDate = actualEndDate;
            Cost = cost;
            Comments = comments;

            if (status == ActionStatusEnum.Concluded && Status != ActionStatusEnum.Concluded)
            {
                ClosedDate = DateTime.Now;
                ClosedById = closedById;
            }
        }

        internal bool HasModified (
            string actionDescription,
            string responsibleId,
            DateTime? dueDate,
            DateTime? originalDueDate,
            ActionStatusEnum status,
            DateTime? actualStartDate,
            DateTime? actualEndDate,
            decimal? cost,
            string comments
        )
        {
            var hasModified = false;

            if (ActionDescription != actionDescription)
                hasModified = true;

            if (ResponsibleId != responsibleId)
                hasModified = true;

            if (DueDate != dueDate)
                hasModified = true;

            if (OriginalDueDate != originalDueDate)
                hasModified = true;

            if (Status != status)
                hasModified = true;

            if (ActualStartDate != actualStartDate)
                hasModified = true;

            if (ActualEndDate != actualEndDate)
                hasModified = true;

            if (Cost != cost)
                hasModified = true;

            if (Comments != comments)
                hasModified = true;

            return hasModified;
        }
    }
}

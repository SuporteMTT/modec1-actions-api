using Actions.Core.Domain.Actions.Enums;
using Actions.Core.Domain.Shared.Enums;
using Actions.Core.Domain.Users.Entities;
using FluentValidation;
using Shared.Core.Domain.Impl.Entity;
using Shared.CrossCutting.Tools;
using System;

namespace Actions.Core.Domain.Actions.Entities
{
    public class Action : Entity<Action, string>
    {
        protected Action () {}

        protected Action (
            string id,
            string description,
            string responsibleId,
            DateTime dueDate,
            ActionStatusEnum status,
            DateTime? actualStartDate,
            DateTime? actualEndDate,
            string comments,
            MetadataTypeEnum metadataType,
            string metadataId,
            DateTime createdDate,
            string relatedId,
            decimal? cost
        )
        {
            Id = id;
            RelatedId = relatedId;
            Description = description;
            ResponsibleId = responsibleId;
            Status = status;
            DueDate = dueDate;
            ActualStartDate = actualStartDate;
            ActualEndDate = actualEndDate;
            Cost = cost;
            Comments = comments;
            MetadataType = metadataType;
            MetadataId = metadataId;
            CreatedDate = createdDate;
        }

        public Action (
            string description,
            string responsibleId,
            DateTime dueDate,
            ActionStatusEnum status,
            DateTime? actualStartDate,
            DateTime? actualEndDate,
            string comments,
            MetadataTypeEnum metadataType,
            string metadataId,
            string relatedId,
            decimal? cost
        ) : this(
            GuidExtensions.GenerateGuid(),
            description,
            responsibleId,
            dueDate,
            status,
            actualStartDate,
            actualEndDate,
            comments,
            metadataType,
            metadataId,
            DateTime.Now,
            relatedId,
            cost
        ) {}
        public string RelatedId { get; set; }
        public string Description { get; set; }
        public User Responsible { get; set; }
        public string ResponsibleId { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? OriginalDueDate { get; set; }
        public ActionStatusEnum Status { get; set; }
        public DateTime? ActualStartDate { get; set; }
        public DateTime? ActualEndDate { get; set; }
        public decimal? Cost { get; set; }
        public string Comments { get; set; }
        public DateTime? ClosedDate { get; set; }
        public User ClosedBy { get; set; }
        public string ClosedById { get; set; }
        public MetadataTypeEnum MetadataType { get; set; }
        public string MetadataId { get; set; }
        public DateTime CreatedDate { get; set; }

        internal void UpdateData(
            string description,
            string responsibleId,
            DateTime dueDate,
            ActionStatusEnum status,
            DateTime actualStartDate,
            DateTime actualEndDate,
            string comments,
            string closedById,
            string relatedId,
            decimal? cost,
            string originalDueDate
        )
        {
            Description = description;
            ResponsibleId = responsibleId;
            DueDate = dueDate;
            Status = status;
            ActualStartDate = actualStartDate;
            ActualEndDate = actualEndDate;
            Comments = comments;
            Cost = cost;
            RelatedId = relatedId;

            if (status == ActionStatusEnum.Concluded && Status != ActionStatusEnum.Concluded)
            {
                ClosedDate = DateTime.Now;
                ClosedById = closedById;
            }
        }
    }

    public class ActionValidator : AbstractValidator<Action>
    {
        public ActionValidator()
        {
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
            RuleFor(x => x.ResponsibleId).NotEmpty().WithMessage("RespnsibleId is required");
            RuleFor(x => x.DueDate).NotNull().WithMessage("Due Date is required");
            RuleFor(x => x.Status).IsInEnum().WithMessage("Status is required");
            RuleFor(x => x.MetadataId).NotEmpty();
            RuleFor(x => x.MetadataType).IsInEnum();
        }
    }
}
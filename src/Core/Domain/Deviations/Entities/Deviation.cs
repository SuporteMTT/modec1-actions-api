using Actions.Core.Domain.Deviations.Enums;
using Actions.Core.Domain.Risks.Entities;
using Actions.Core.Domain.Shared.Enums;
using Actions.Core.Domain.Users.Entities;
using FluentValidation;
using Shared.Core.Domain.Impl.Entity;
using Shared.CrossCutting.Tools;
using System;

namespace Actions.Core.Domain.Deviations.Entities
{
    public class Deviation : Entity<Deviation, string>
    {
        protected Deviation () {}

        protected Deviation (
            string id,
            string code,
            StatusEnum status,
            string name,
            string description,
            string cause,
            RiskCategoryEnum category,
            PriorityEnum priority,
            string associatedRiskId,
            string createById,
            MetadataTypeEnum metadataType,
            string metadataId,
            DateTime createdDate
        )
        {
            Id = id;
            Code = code;
            Status = status;
            Name = name;
            Description = description;
            Cause = cause;
            Category = category;
            CreatedById = createById;
            MetadataType = metadataType;
            Priority = priority;
            MetadataId = metadataId;
            CreatedDate = createdDate;
            AssociatedRiskId = associatedRiskId;
        }

        public Deviation (
            string code,
            string name,
            string description,
            string cause,
            RiskCategoryEnum category,
            PriorityEnum priority,
            string associatedRiskId,
            string createById,
            MetadataTypeEnum metadataType,
            string metadataId
        ) : this(
            GuidExtensions.GenerateGuid(), 
            code,
            StatusEnum.Active,
            name,
            description,
            cause,
            category,
            priority,
            associatedRiskId,
            createById,
            metadataType,
            metadataId,
            DateTime.Now
        ) {}

        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public StatusEnum Status { get; set; }
        public RiskCategoryEnum Category { get; set; }
        public Risk AssociatedRisk { get; set; }
        public string AssociatedRiskId { get; set; }
        public string Cause { get; set; }
        public PriorityEnum Priority { get; set; }
        public DateTime CreatedDate { get; set; }
        public User CreatedBy { get; set; }
        public string CreatedById { get; set; }
        public DateTime? ClosedCancelledDate { get; set; }
        public User ClosedCancelledBy { get; set; }
        public string ClosedCancelledById { get; set; }
        public MetadataTypeEnum MetadataType { get; set; }
        public string MetadataId { get; set; }
        public string CancelledJustification { get; set; }

        internal void UpdateData(
            StatusEnum status,
            string name,
            string description,
            string cause,
            RiskCategoryEnum category,
            PriorityEnum priority,
            string associatedRiskId,
            string closedCancelledById,
            string cancelledJustification = null
        )
        {
            Status = status;
            Name = name;
            Description = description;
            Cause = cause;
            Category = category;
            Priority = priority;
            AssociatedRiskId = associatedRiskId;
            ClosedCancelledById = closedCancelledById;
            CancelledJustification = cancelledJustification;

            if (status == StatusEnum.Concluded || status == StatusEnum.Cancelled) 
                ClosedCancelledDate = DateTime.Now;
        }

        internal bool HasModified(
            string name,
            string description,
            StatusEnum status,
            RiskCategoryEnum category,            
            string associatedRiskId,
            string cause,
            PriorityEnum priority
        )
        {
            var hasModified = false;

            if (Status != status)
                hasModified = true;

            if (Name != name)
                hasModified = true;

            if (Description != description)
                hasModified = true;

            if (Cause != cause)
                hasModified = true;

            if (Category != category)
                hasModified = true;

            if (Priority != priority)
                hasModified = true;

            if (AssociatedRiskId != associatedRiskId)
                hasModified = true;

            return hasModified;
        }
    }

    public class DeviationValidator : AbstractValidator<Deviation>
    {
        public DeviationValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
            RuleFor(x => x.Category).IsInEnum().WithMessage("Category is required");
            RuleFor(x => x.MetadataId).NotEmpty();
            RuleFor(x => x.MetadataType).IsInEnum();
        }
    }
}

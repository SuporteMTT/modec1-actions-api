using Actions.Core.Domain.Risks.Enums;
using Actions.Core.Domain.Shared.Enums;
using Actions.Core.Domain.Users.Entities;
using FluentValidation;
using Shared.Core.Domain.Impl.Entity;
using Shared.CrossCutting.Tools;
using System;
using System.Collections.Generic;

namespace Actions.Core.Domain.Risks.Entities
{
    public class Risk : Entity<Risk, string>
    {
        protected Risk () {}

        protected Risk (
            string id,
            string code,
            StatusEnum status,
            string ownerId,
            string name,
            string description,
            string cause,
            string impact,
            RiskCategoryEnum category,
            RiskLevelEnum level,
            DimensionEnum dimension,
            string dimensionDescription,
            ProjectStepEnum projectStep,
            string createById,
            RiskJustificationEnum justification,
            string realImpact,
            MetadataTypeEnum metadataType,
            string metadataId,
            DateTime createdDate
        )
        {
            Id = id;
            Code = code;
            Status = status;
            OwnerId = ownerId;
            Name = name;
            Description = description;
            Cause = cause;
            Impact = impact;
            Category = category;
            Level = level;
            Dimension = dimension;
            DimensionDescription = dimensionDescription;
            ProjectStep = projectStep;
            CreatedById = createById;
            Justification = justification;
            RealImpact = realImpact;
            MetadataType = metadataType;
            MetadataId = metadataId;
            CreatedDate = createdDate;          
        }

        public Risk (
            string code,
            string ownerId,
            string name,
            string description,
            string cause,
            string impact,
            RiskCategoryEnum category,
            RiskLevelEnum level,
            DimensionEnum dimension,
            string dimensionDescription,
            ProjectStepEnum projectStep,
            string createById,
            RiskJustificationEnum justification,
            string realImpact,
            MetadataTypeEnum metadataType,
            string metadataId
        ) : this(
            GuidExtensions.GenerateGuid(), 
            code,
            StatusEnum.Active,
            ownerId,
            name,
            description,
            cause,
            impact,
            category,
            level,
            dimension,
            dimensionDescription,
            projectStep,
            createById,
            justification,
            realImpact,
            metadataType,
            metadataId,
            DateTime.Now
        ) {}

        public string Code { get; set; }
        public StatusEnum Status { get; set; }
        public User Owner { get; set; }
        public string OwnerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Cause { get; set; }
        public string Impact { get; set; }
        public RiskCategoryEnum Category { get; set; }
        public RiskLevelEnum Level { get; set; }
        public DimensionEnum Dimension { get; set; }
        public string DimensionDescription { get; set; }
        public ProjectStepEnum ProjectStep { get; set; }

        public DateTime CreatedDate { get; set; }
        public User CreatedBy { get; set; }
        public string CreatedById { get; set; }
        public DateTime? ClosedCancelledDate { get; set; }
        public User ClosedCancelledBy { get; set; }
        public string ClosedCancelledById { get; set; }
        public RiskJustificationEnum Justification { get; set; }
        public string RealImpact { get; set; }
        public MetadataTypeEnum MetadataType { get; set; }
        public string MetadataId { get; set; }
        public string CancelledJustification { get; set; }

        public ICollection<RiskTask> RiskTask { get; set; }

        internal void UpdateData(
            StatusEnum status,
            string ownerId,
            string name,
            string description,
            string cause,
            string impact,
            RiskCategoryEnum category,
            RiskLevelEnum level,
            DimensionEnum dimension,
            string dimensionDescription,
            ProjectStepEnum projectStep,
            RiskJustificationEnum justification,
            string realImpact,
            string closedCancelledById,
            string cancelledJustification = null
        )
        {
            Status = status;
            OwnerId = ownerId;
            Name = name;
            Description = description;
            Cause = cause;
            Impact = impact;
            Category = category;
            Level = level;
            Dimension = dimension;
            DimensionDescription = dimensionDescription;
            ProjectStep = projectStep;
            ClosedCancelledById = closedCancelledById;
            Justification = justification;
            RealImpact = realImpact;
            CancelledJustification = cancelledJustification;

            if (status == StatusEnum.Concluded || status == StatusEnum.Cancelled) 
                ClosedCancelledDate = DateTime.Now;
        }

        internal bool HasModified(
            StatusEnum status,
            string ownerId,
            string name,
            string description,
            string cause,
            string impact,
            RiskCategoryEnum category,
            RiskLevelEnum level,
            DimensionEnum dimension,
            string dimensionDescription,
            ProjectStepEnum projectStep,
            RiskJustificationEnum justification,
            string realImpact
        )
        {
            var hasModified = false;

            if (Status != status)
                hasModified = true;

            if (OwnerId != ownerId)
                hasModified = true;

            if (Name != name)
                hasModified = true;

            if (Description != description)
                hasModified = true;

            if (Cause != cause)
                hasModified = true;

            if (Impact != impact)
                hasModified = true;

            if (Category != category)
                hasModified = true;

            if (Level != level)
                hasModified = true;

            if (Dimension != dimension)
                hasModified = true;

            if (DimensionDescription != dimensionDescription)
                hasModified = true;

            if (ProjectStep != projectStep)
                hasModified = true;

            if (Justification != justification)
                hasModified = true;

            if (RealImpact != realImpact)
                hasModified = true;
            
            return hasModified;
        }
    }

    public class RiskValidator : AbstractValidator<Risk>
    {
        public RiskValidator()
        {
            RuleFor(x => x.OwnerId).NotEmpty().WithMessage("OwnerId is required");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Risk Name is required");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Risk Description is required");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Impact is required");
            RuleFor(x => x.Category).IsInEnum().WithMessage("Category is required");
            RuleFor(x => x.Level).IsInEnum().WithMessage("Risk Matrix is required");
            RuleFor(x => x.Dimension).IsInEnum().WithMessage("Dimension is required");
            RuleFor(x => x.ProjectStep).IsInEnum().WithMessage("Project Step is required");
            RuleFor(x => x.Justification).IsInEnum().WithMessage("Justification is required");
            RuleFor(x => x.MetadataId).NotEmpty();
            RuleFor(x => x.MetadataType).IsInEnum();
            When(x => x.Dimension == DimensionEnum.Other, () =>
            {
                RuleFor(x => x.DimensionDescription).NotEmpty();
            });
        }
    }
    
}
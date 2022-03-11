using Actions.Core.Domain.Risks.Enums;
using Actions.Core.Domain.Shared.Enums;
using Actions.Core.Domain.Users.Entities;
using FluentValidation;
using Shared.Core.Domain.Impl.Entity;
using Shared.CrossCutting.Tools;
using System;

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
            CreateById = createById;
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
        public string AssociatedTaskId { get; set; }
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
        public string CreateById { get; set; }
        public DateTime? ClosedCancelledDate { get; set; }
        public string ClosedCancelledById { get; set; }
        public RiskJustificationEnum Justification { get; set; }
        public string RealImpact { get; set; }
        public MetadataTypeEnum MetadataType { get; set; }
        public string MetadataId { get; set; }

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
            string closedCancelledById
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

            if ((status == StatusEnum.Concluded || status == StatusEnum.Cancelled) && Status == StatusEnum.Active) 
                ClosedCancelledDate = DateTime.Now;
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
            RuleFor(x => x.RealImpact).NotEmpty().WithMessage("Real Impact is required");
            RuleFor(x => x.MetadataId).NotEmpty();
            RuleFor(x => x.MetadataType).IsInEnum();
            When(x => x.Dimension == DimensionEnum.Other, () =>
            {
                RuleFor(x => x.DimensionDescription).NotEmpty();
            });
        }
    }
    
}
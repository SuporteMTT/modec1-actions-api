using System.Collections.Generic;
using Actions.Core.Domain.ResponsePlans.Dtos;
using Actions.Core.Domain.Risks.Enums;
using Actions.Core.Domain.Shared.Enums;
using FluentValidation;

namespace Actions.Core.Domain.Risks.Commands
{
    public class UpdateRiskCommand
    {
        public string Id { get; set; }
        public StatusEnum Status { get; set; }
        public string OwnerId { get; set; }
        public string[] AssociatedTaskIds { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Cause { get; set; }
        public string Impact { get; set; }
        public RiskCategoryEnum Category { get; set; }
        public RiskLevelEnum Level { get; set; }
        public DimensionEnum Dimension { get; set; }
        public string DimensionDescription { get; set; }
        public ProjectStepEnum ProjectStep { get; set; }
        public RiskJustificationEnum Justification { get; set; }
        public string RealImpact { get; set; }
        public MetadataTypeEnum MetadataType { get; set; }
        public string MetadataId { get; set; }
        public ICollection<ResponsePlanDto> ResponsePlans { get; set; }
    }

    public class UpdateRiskCommandValidator : AbstractValidator<UpdateRiskCommand>
    {
        public UpdateRiskCommandValidator()
        {
            RuleFor(x => x.OwnerId).NotEmpty().WithMessage("OwnerId is required");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
            RuleFor(x => x.Impact).NotEmpty().WithMessage("Impact is required");
            RuleFor(x => x.Category).IsInEnum().WithMessage("Category is required");
            RuleFor(x => x.Level).IsInEnum().WithMessage("Risk Matrix is required");
            RuleFor(x => x.Dimension).IsInEnum().WithMessage("Dimension is required");
            RuleFor(x => x.ProjectStep).IsInEnum().WithMessage("Project Step is required");
            RuleFor(x => x.Justification).IsInEnum().WithMessage("Justification is required");
            RuleFor(x => x.MetadataId).NotEmpty();
            RuleFor(x => x.MetadataType).NotNull();
        }
    }
}
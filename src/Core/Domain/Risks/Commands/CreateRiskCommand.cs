using Actions.Core.Domain.Risks.Enums;
using Actions.Core.Domain.Shared.Enums;
using FluentValidation;

namespace Actions.Core.Domain.Risks.Commands
{
    public class CreateRiskCommand
    {
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
    }

    public class CreateRiskCommandValidator : AbstractValidator<CreateRiskCommand>
    {
        public CreateRiskCommandValidator()
        {
            RuleFor(x => x.OwnerId).NotEmpty().NotNull().WithMessage("OwnerId is required");
            RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("Risk Name is required");
            RuleFor(x => x.Description).NotEmpty().NotNull().WithMessage("Risk Description is required");
            RuleFor(x => x.Description).NotEmpty().NotNull().WithMessage("Impact is required");
            RuleFor(x => x.Category).NotNull().WithMessage("Category is required");
            RuleFor(x => x.Level).NotNull().WithMessage("Risk Matrix is required");
            RuleFor(x => x.Dimension).NotNull().WithMessage("Dimension is required");
            RuleFor(x => x.ProjectStep).NotNull().WithMessage("Project Step is required");
            RuleFor(x => x.Justification).NotNull().WithMessage("Justification is required");
            RuleFor(x => x.RealImpact).NotEmpty().NotNull().WithMessage("Real Impact is required");
            RuleFor(x => x.MetadataId).NotEmpty();
            RuleFor(x => x.MetadataType).NotNull();
        }
    }
}
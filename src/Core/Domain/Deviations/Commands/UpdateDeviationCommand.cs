using System.Collections.Generic;
using Actions.Core.Domain.Deviations.Enums;
using Actions.Core.Domain.ResponsePlans.Dtos;
using Actions.Core.Domain.Shared.Enums;
using FluentValidation;

namespace Actions.Core.Domain.Deviations.Commands
{
    public class UpdateDeviationCommand
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public StatusEnum Status { get; set; }
        public RiskCategoryEnum Category { get; set; }
        public string AssociatedRiskId { get; set; }
        public string Cause { get; set; }
        public PriorityEnum Priority { get; set; }
        public MetadataTypeEnum MetadataType { get; set; }
        public string MetadataId { get; set; }
        public ICollection<ResponsePlanDto> ResponsePlans { get; set; }
        public string CancelledJustification { get; set; }
    }

    public class UpdateDeviationCommandValidator : AbstractValidator<UpdateDeviationCommand>
    {
        public UpdateDeviationCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
            RuleFor(x => x.Category).IsInEnum().WithMessage("Category is required");
            RuleFor(x => x.MetadataId).NotEmpty();
            RuleFor(x => x.MetadataType).IsInEnum();
        }
    }
}
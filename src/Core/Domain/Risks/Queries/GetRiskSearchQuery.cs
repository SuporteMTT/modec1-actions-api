using FluentValidation;

namespace Actions.Core.Domain.Risks.Queries
{
    public class GetRiskSearchQuery
    {
        public string Search { get; private set; }
        public string MetadataId { get; private set; }

        public GetRiskSearchQuery(string id, string metadataId)
        {
            Search = id;
            MetadataId = metadataId;
        }
    }

    public class GetRiskSearchQueryValidator : AbstractValidator<GetRiskSearchQuery>
    {
        public GetRiskSearchQueryValidator()
        {
            RuleFor(x => x.Search).NotEmpty().WithMessage("Id is required");
            RuleFor(x => x.MetadataId).NotEmpty().WithMessage("MetadataId is required");
        }
    }    
}
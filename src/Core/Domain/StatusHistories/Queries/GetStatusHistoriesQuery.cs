using FluentValidation;

namespace Actions.Core.Domain.StatusHistories.Queries
{
    public class GetStatusHistoriesQuery
    {
        public int Page { get; set; }
        public int Count { get; set; }
        public string MetadataId { get; set; }

        public GetStatusHistoriesQuery(string metadataId, int? page, int? count)
        {
            MetadataId = metadataId;
            Page = page ?? 1;
            Count = count ?? 20;
        }
    }

    public class GetStatusHistoriesQueryValidator : AbstractValidator<GetStatusHistoriesQuery>
    {
        public GetStatusHistoriesQueryValidator()
        {
            RuleFor(x => x.MetadataId).NotEmpty();
        }
    }
}
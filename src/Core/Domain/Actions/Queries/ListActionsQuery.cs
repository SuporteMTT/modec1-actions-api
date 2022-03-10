using Actions.Core.Domain.Shared.Enums;
using FluentValidation;

namespace Actions.Core.Domain.Actions.Queries
{
    public class ListActionsQuery
    {
        public int Page { get; set; }
        public int Count { get; set; }
        public string MetadataId { get; set; }
        public MetadataTypeEnum MetadataType { get; set; }

        public ListActionsQuery(
            string metadataId,
            MetadataTypeEnum metadataType,
            int? page,
            int? count
        )
        {
            Page = page ?? 1;
            Count = count ?? 20;
            MetadataId = metadataId;
            MetadataType = metadataType;
        }

        public class ListActionsQueryValidator : AbstractValidator<ListActionsQuery>
        {
            public ListActionsQueryValidator()
            {
                RuleFor(x => x.MetadataId).NotEmpty();
                RuleFor(x => x.MetadataType).NotNull();
            }
        }
    }
}

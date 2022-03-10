using Actions.Core.Domain.Shared.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actions.Core.Domain.Deviations.Queries
{
    public class ListDeviationsQuery
    {
        public int Page { get; set; }
        public int Count { get; set; }
        public string MetadataId { get; set; }
        public MetadataTypeEnum MetadataType { get; set; }

        public ListDeviationsQuery(
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
    }

    public class ListDeviationsQueryValidator : AbstractValidator<ListDeviationsQuery>
    {
        public ListDeviationsQueryValidator()
        {
            RuleFor(x => x.MetadataId).NotEmpty();
            RuleFor(x => x.MetadataType).NotNull();
        }
    }
}

using Actions.Core.Domain.Shared.Enums;
using FluentValidation;
using Shared.Core.Domain.Impl.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actions.Core.Domain.Risks.Queries
{
    public class ListRisksQuery
    {
        public int Page { get; set; }
        public int Count { get; set; }
        public string MetadataId { get; set; }
        public MetadataTypeEnum MetadataType { get; set; }

        public ListRisksQuery(
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

    public class GetRisksQueryValidator : AbstractValidator<ListRisksQuery>
    {
        public GetRisksQueryValidator()
        {
            RuleFor(x => x.MetadataId).NotEmpty();
            RuleFor(x => x.MetadataType).NotNull();
        }
    }
}

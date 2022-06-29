using Actions.Core.Domain.Actions.Dtos;
using Actions.Core.Domain.Actions.Interfaces;
using Actions.Core.Domain.Actions.Queries;
using Actions.Core.Domain.Deviations.Handlers;
using Actions.Core.Domain.Risks.Handlers;
using Actions.Core.Domain.Shared.Dtos;
using Actions.Core.Domain.Shared.Interfaces.Entities;
using Shared.Core.Domain.Impl.Validator;
using Shared.Core.Domain.Interface.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Actions.Core.Domain.Actions.Handlers
{
    public class ActionsQueryHandler
    {
        private readonly ITokenUtil _tokenUtil;
        private readonly IActionRepository _repository;
        private readonly DeviationsQueryHandler _deviationsQueryHandler;
        private readonly RisksQueryHandler _risksQueryHandler;

        public ActionsQueryHandler(ITokenUtil tokenUtil,
            IActionRepository repository,
            RisksQueryHandler risksQueryHandler,
            DeviationsQueryHandler deviationsQueryHandler
        )
        {
            _tokenUtil = tokenUtil;
            _repository = repository;
            _risksQueryHandler = risksQueryHandler;
            _deviationsQueryHandler = deviationsQueryHandler;
        }

        public async Task<IPagination<ActionListDto>> Handle(ListActionsQuery query)
        {
            query.ValidateAndThrow();

            return await _repository.GetAsync(query.MetadataId, query.MetadataType, query.Page, query.Count);
        }

        public async Task<ActionDto> Handle(GetActionByIdQuery query)
        {
            query.ValidateAndThrow();

            var action = await _repository.GetAsync(query.Id);
            if (!string.IsNullOrWhiteSpace(action.RelatedId))
            {
                var riskRelated = await _risksQueryHandler.Handle(new Risks.Queries.GetShortRiskByIdQuery(action.RelatedId));
                if (riskRelated != null &&  !string.IsNullOrWhiteSpace(riskRelated.Id))
                    action.Related = riskRelated;
                else
                {
                    var deviationRelated = await _deviationsQueryHandler.Handle(new Deviations.Queries.GetShortDeviationByIdQuery(action.RelatedId));
                    action.Related = deviationRelated;
                }
            }

            return action;
        }

        public async Task<ICollection<ShortObjectDto>> Handle(GetDeviationAndRiskSearchQuery query)
        {
            query.ValidateAndThrow();

            return await _repository.GetAsync(query.Search, query.MetadataId);
        }

        public async Task<ICollection<ActionDto>> Handle(GetActionByMetadataIdQuery query)
        {
            query.ValidateAndThrow();

            var action = await _repository.GetByMetadataId(query.MetadataId);

            return action;
        }
    }
}

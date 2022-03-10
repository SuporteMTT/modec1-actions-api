using Actions.Core.Domain.Actions.Dtos;
using Actions.Core.Domain.Actions.Interfaces;
using Actions.Core.Domain.Actions.Queries;
using Actions.Core.Domain.Shared.Interfaces.Entities;
using Shared.Core.Domain.Impl.Validator;
using Shared.Core.Domain.Interface.Entity;
using System.Threading.Tasks;

namespace Actions.Core.Domain.Actions.Handlers
{
    public class ActionsQueryHandler
    {
        private readonly ITokenUtil _tokenUtil;
        private readonly IActionRepository _repository;

        public ActionsQueryHandler(ITokenUtil tokenUtil, IActionRepository repository)
        {
            _tokenUtil = tokenUtil;
            _repository = repository;
        }

        public async Task<IPagination<ActionListDto>> Handle(ListActionsQuery query)
        {
            query.ValidateAndThrow();

            return await _repository.GetAsync(query.MetadataId, query.MetadataType, query.Page, query.Count);
        }
    }
}

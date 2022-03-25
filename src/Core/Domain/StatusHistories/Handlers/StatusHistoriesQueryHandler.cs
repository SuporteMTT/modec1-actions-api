using System.Collections.Generic;
using System.Threading.Tasks;
using Actions.Core.Domain.Shared.Interfaces.Entities;
using Actions.Core.Domain.StatusHistories.Dtos;
using Actions.Core.Domain.StatusHistories.Interfaces;
using Actions.Core.Domain.StatusHistories.Queries;
using Shared.Core.Domain.Impl.Validator;
using Shared.Core.Domain.Interface.Entity;

namespace Actions.Core.Domain.StatusHistories.Handlers
{
    public class StatusHistoriesQueryHandler
    {
        private readonly ITokenUtil _tokenUtil;
        private readonly IStatusHistoryRepository _repository;

        public StatusHistoriesQueryHandler(IStatusHistoryRepository repository, ITokenUtil tokenUtil)
        {
            _tokenUtil = tokenUtil;
            _repository = repository;
        }

        public async Task<ICollection<StatusHistoryListDto>> Handle(GetStatusHistoriesQuery query)
        {
            query.ValidateAndThrow();

            return await _repository.GetAsync(query.MetadataId);        
        }
    }
}
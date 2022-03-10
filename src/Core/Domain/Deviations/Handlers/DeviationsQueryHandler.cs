using Actions.Core.Domain.Deviations.Dtos;
using Actions.Core.Domain.Deviations.Interfaces;
using Actions.Core.Domain.Queries;
using Actions.Core.Domain.Shared.Interfaces.Entities;
using Shared.Core.Domain.Impl.Validator;
using Shared.Core.Domain.Interface.Entity;
using System.Threading.Tasks;

namespace Actions.Core.Domain.Deviations.Handlers
{
    public class DeviationsQueryHandler
    {
        private readonly ITokenUtil _tokenUtil;
        private readonly IDeviationRepository _repository;

        public DeviationsQueryHandler(ITokenUtil tokenUtil, IDeviationRepository repository)
        {
            _tokenUtil = tokenUtil;
            _repository = repository;
        }

        public async Task<IPagination<DeviationListDto>> Handle(ListDeviationsQuery query)
        {
            query.ValidateAndThrow();

            return await _repository.GetAsync(query.MetadataId, query.MetadataType, query.Page, query.Count);
        }
    }
}

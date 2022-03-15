
using System.Collections.Generic;
using Actions.Core.Domain.ResponsePlans.Dtos;
using Actions.Core.Domain.ResponsePlans.Interfaces;

namespace Actions.Infrastructure.Data.Repositories
{
    public class ResponsePlanRepository : UnitOfWork, IResponsePlanRepository
    {
        public ResponsePlanRepository(ActionsContext context) : base(context) { }

        public IEnumerable<ResponsePlanDto> GetByMetadataId(string metadataId)
        {
            return null;
        }
    }
}

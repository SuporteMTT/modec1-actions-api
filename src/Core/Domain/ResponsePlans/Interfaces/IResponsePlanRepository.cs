using System.Collections.Generic;
using Actions.Core.Domain.ResponsePlans.Dtos;
using Actions.Core.Domain.Shared.Interfaces;

namespace Actions.Core.Domain.ResponsePlans.Interfaces
{
    public interface IResponsePlanRepository : IUnitOfWork
    {
        IEnumerable<ResponsePlanDto> GetByMetadataId(string metadataId);
    }
}

using Actions.Core.Domain.Deviations.Dtos;
using Actions.Core.Domain.Shared.Enums;
using Actions.Core.Domain.Shared.Interfaces;
using Shared.Core.Domain.Interface.Entity;
using System.Threading.Tasks;

namespace Actions.Core.Domain.Deviations.Interfaces
{
    public interface IDeviationRepository : IUnitOfWork
    {
        Task<IPagination<DeviationListDto>> GetAsync(
            string metadataId,
            MetadataTypeEnum metadataType,
            int? page = null,
            int? count = null
        );
    }
}

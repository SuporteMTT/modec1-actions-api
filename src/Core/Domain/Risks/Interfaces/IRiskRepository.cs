
using Actions.Core.Domain.Risks.Dtos;
using Actions.Core.Domain.Shared.Enums;
using Actions.Core.Domain.Shared.Interfaces;
using Shared.Core.Domain.Interface.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Actions.Core.Domain.Risks.Interfaces
{
    public interface IRiskRepository : IUnitOfWork
    {
        Task<IPagination<RiskListDto>> GetAsync(
            string metadataId,
            MetadataTypeEnum metadataType,
            int? page = null,
            int? count = null
        );

        Task<string> GetLastCode(string departmentCode);
        Task<RiskDto> GetAsync(string id);
        Task<ICollection<RiskAutocompleteDto>> GetAsync(string search, string metadataId);
        Task DeleteById(string id);
    }
}

using Actions.Core.Domain.Actions.Dtos;
using Actions.Core.Domain.Shared.Enums;
using Actions.Core.Domain.Shared.Interfaces;
using Shared.Core.Domain.Interface.Entity;
using System.Threading.Tasks;

namespace Actions.Core.Domain.Actions.Interfaces
{
    public interface IActionRepository : IUnitOfWork
    {
        Task<IPagination<ActionListDto>> GetAsync(
            string metadataId,
            MetadataTypeEnum metadataType,
            int? page = null,
            int? count = null
        );
        Task<ActionDto> GetAsync(string id);
    }
}

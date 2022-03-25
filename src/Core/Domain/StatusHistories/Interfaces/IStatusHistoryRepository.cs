using System.Collections.Generic;
using System.Threading.Tasks;
using Actions.Core.Domain.Shared.Interfaces;
using Actions.Core.Domain.StatusHistories.Dtos;
using Shared.Core.Domain.Interface.Entity;

namespace Actions.Core.Domain.StatusHistories.Interfaces
{
    public interface IStatusHistoryRepository  : IUnitOfWork 
    {
        Task<IPagination<StatusHistoryListDto>> GetAsync(string metadataId,int? page = null, int? count = null);
        Task<ICollection<StatusHistoryListDto>> GetAsync(string metadataId);
    }
}
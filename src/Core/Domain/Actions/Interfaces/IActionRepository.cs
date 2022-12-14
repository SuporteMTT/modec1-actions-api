using Actions.Core.Domain.Actions.Dtos;
using Actions.Core.Domain.Actions.Entities;
using Actions.Core.Domain.Shared.Dtos;
using Actions.Core.Domain.Shared.Enums;
using Actions.Core.Domain.Shared.Interfaces;
using Shared.Core.Domain.Interface.Entity;
using System.Collections.Generic;
using System.Linq.Expressions;
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
        Task<ICollection<ShortObjectDto>> GetAsync(string search, string metadataId);
        Task<Action> GetAsNoTrackingAsync(Expression<System.Func<Action, bool>> expression);
        Task DeleteById(string id);
        Task<ICollection<ActionDto>> GetByMetadataId(string metadataId);
    }
}

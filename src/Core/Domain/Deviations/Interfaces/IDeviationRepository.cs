using Actions.Core.Domain.Deviations.Dtos;
using Actions.Core.Domain.Deviations.Entities;
using Actions.Core.Domain.Shared.Enums;
using Actions.Core.Domain.Shared.Interfaces;
using Shared.Core.Domain.Interface.Entity;
using System.Linq.Expressions;
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
        Task<DeviationDto> GetAsync(string id);
        Task<string> GetLastCode(string departmentId);
        Task<Deviation> GetAsNoTrackingAsync(Expression<System.Func<Deviation, bool>> expression);
        Task DeleteById(string id);
    }
}

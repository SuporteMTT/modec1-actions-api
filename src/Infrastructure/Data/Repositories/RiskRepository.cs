using Actions.Core.Domain.Risks.Dtos;
using Actions.Core.Domain.Risks.Entities;
using Actions.Core.Domain.Risks.Interfaces;
using Actions.Core.Domain.Shared;
using Actions.Core.Domain.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Domain.Impl.Entity;
using Shared.Core.Domain.Interface.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Actions.Infrastructure.Data.Repositories
{
    public class RiskRepository : UnitOfWork, IRiskRepository
    {
        public RiskRepository(ActionsContext context) : base(context) { }

        public async Task<IPagination<RiskListDto>> GetAsync(
            string metadataId,
            MetadataTypeEnum metadataType,
            int? page,
            int? count)
        {
            var condition = context.Set<Risk>()
                .Where(x => x.MetadataId == metadataId && x.MetadataType == metadataType);

            var results = await condition
                .OrderBy(r => r.CreateDate)
                .Select(o => new
                {
                    Data = new RiskListDto()
                    {
                        Id = o.Id,
                        CreateDate = o.CreateDate,
                        Cause = o.Cause,
                        ClosedCancelledDate = o.ClosedCancelledDate,
                        Name = o.Name,
                        Owner = o.Owner.Name,
                        Level = o.Level,
                        Status = o.Status.Status(),
                        Actions = new Core.Domain.Shared.Dtos.ActionsStatusCountDto
                        {
                            ConcludedOrDelayed = null,
                            NotInitiated = null,
                            OnGoing = null
                        }
                    },
                    Total = condition.Count()
                })
                .Skip((page.Value - 1) * count.Value).Take(count.Value)
                .ToArrayAsync();

            var risks = results.Select(r => r.Data).ToList();
            var total = results.FirstOrDefault()?.Total ?? risks.Count;

            return new Pagination<RiskListDto>()
            {
                Data = risks,
                Total = total
            };
        }
    }
}

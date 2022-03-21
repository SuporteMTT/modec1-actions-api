using System.Linq;
using System.Threading.Tasks;
using Actions.Core.Domain.Shared;
using Actions.Core.Domain.StatusHistories.Dtos;
using Actions.Core.Domain.StatusHistories.Entities;
using Actions.Core.Domain.StatusHistories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Domain.Impl.Entity;
using Shared.Core.Domain.Interface.Entity;

namespace Actions.Infrastructure.Data.Repositories
{
    public class StatusHistoryRepository : UnitOfWork, IStatusHistoryRepository
    {
        public StatusHistoryRepository(ActionsContext context) : base(context) { }

        async Task<IPagination<StatusHistoryListDto>> IStatusHistoryRepository.GetAsync(
            string metadataId,
            int? page,
            int? count)
        {
            var condition = context.Set<StatusHistory>()
                .Where(x => x.MetadataId == metadataId);

            var results = await condition
                .OrderBy(r => r.Date)
                .Select(o => new
                {
                    Data = new StatusHistoryListDto()
                    {
                        Id = o.Id,
                        Date = o.Date,
                        UserName = o.User.Name,
                        Status = o.Status.Status()
                    },
                    Total = condition.Count()
                })
                .Skip((page.Value - 1) * count.Value).Take(count.Value)
                .ToArrayAsync();

            var actions = results.Select(r => r.Data).ToList();
            var total = results.FirstOrDefault()?.Total ?? actions.Count;

            return new Pagination<StatusHistoryListDto>()
            {
                Data = actions,
                Total = total
            };
        }
    }
}

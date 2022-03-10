using Actions.Core.Domain.Actions.Dtos;
using Actions.Core.Domain.Actions.Interfaces;
using Actions.Core.Domain.Shared;
using Actions.Core.Domain.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Domain.Impl.Entity;
using Shared.Core.Domain.Interface.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Actions.Infrastructure.Data.Repositories
{
    public class ActionRepository : UnitOfWork, IActionRepository
    {
        public ActionRepository(ActionsContext context) : base(context) { }

        public async Task<IPagination<ActionListDto>> GetAsync(
            string metadataId,
            MetadataTypeEnum metadataType,
            int? page,
            int? count)
        {
            var condition = context.Set<Core.Domain.Actions.Entities.Action>()
                .Where(x => x.MetadataId == metadataId && x.MetadataType == metadataType);

            var results = await condition
                .OrderBy(r => r.DueDate)
                .Select(o => new
                {
                    Data = new ActionListDto()
                    {
                        Id = o.Id,
                        CreatedDate = o.CreatedDate,
                        Description = o.Description,
                        Responsible = o.Responsible.Name,
                        DueDate = o.DueDate,
                        Status = o.Status.Status(),
                        ActualEndDate = o.ActualEndDate,
                    },
                    Total = condition.Count()
                })
                .Skip((page.Value - 1) * count.Value).Take(count.Value)
                .ToArrayAsync();

            var actions = results.Select(r => r.Data).ToList();
            var total = results.FirstOrDefault()?.Total ?? actions.Count;

            return new Pagination<ActionListDto>()
            {
                Data = actions,
                Total = total
            };
        }
    }
}

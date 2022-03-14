using Actions.Core.Domain.Actions.Dtos;
using Actions.Core.Domain.Actions.Entities;
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

        public async Task<ActionDto> GetAsync(string id)
        {
            return await context.Set<Action>().Where(action => action.Id == id).Select(action => new ActionDto
            {
                Id = action.Id,
                Description = action.Description,
                Responsible = action.Responsible != null ? new Core.Domain.Users.Dtos.UserDto 
                { 
                    Id = action.Responsible.Id, 
                    Name = action.Responsible.Name
                } : null,
                DueDate = action.DueDate,
                OriginalDueDate = action.OriginalDueDate,
                Status = action.Status,
                ActualStartDate = action.ActualStartDate,
                ActualEndDate = action.ActualEndDate,
                Cost = action.Cost,               
                Comments = action.Comments,
                ClosedDate = action.ClosedDate,
                ClosedBy = action.ClosedBy != null ? action.ClosedBy.Name : null,                              
            }) .FirstOrDefaultAsync();
        }
    }
}

using Actions.Core.Domain.Actions.Dtos;
using Actions.Core.Domain.Actions.Entities;
using Actions.Core.Domain.Actions.Interfaces;
using Actions.Core.Domain.Deviations.Entities;
using Actions.Core.Domain.Risks.Entities;
using Actions.Core.Domain.Shared;
using Actions.Core.Domain.Shared.Dtos;
using Actions.Core.Domain.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Domain.Impl.Entity;
using Shared.Core.Domain.Interface.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
            var deviationsIds = await (from deviation in context.Set<Deviation>().AsNoTracking().Where(r => r.MetadataId == metadataId)
                                       select deviation.Id).ToArrayAsync();

            var risksIds = await (from risk in context.Set<Risk>().AsNoTracking().Where(r => r.MetadataId == metadataId)
                                  select risk.Id).ToArrayAsync();

            var condition = context.Set<Core.Domain.Actions.Entities.Action>()
                .Where(x => (x.MetadataId == metadataId ||
                            deviationsIds.Contains(x.MetadataId) ||
                            risksIds.Contains(x.MetadataId)) &&
                            x.MetadataType == metadataType);

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
                RelatedId = action.RelatedId
            }) .FirstOrDefaultAsync();
        }

        public async Task<ICollection<ShortObjectDto>> GetAsync(string search, string metadataId)
        {
            var queryRisk = (from risk in context.Set<Risk>()
                         where (risk.MetadataId == metadataId &&
                         (string.IsNullOrWhiteSpace(search) || (risk.Code + risk.Name).Contains(search)))
                         orderby (risk.Code + risk.Name)
                         select new ShortObjectDto
                         {
                             Id = risk.Id,
                             Name = $"{risk.Code} {risk.Name}",
                         }).ToList();

            var queryDeviation = (from deviation in context.Set<Deviation>()
                         where (deviation.MetadataId == metadataId &&
                         (string.IsNullOrWhiteSpace(search) || (deviation.Code + deviation.Name).Contains(search)))
                         orderby (deviation.Code + deviation.Name)               
                         select new ShortObjectDto
                         {
                             Id = deviation.Id,
                             Name = $"{deviation.Code} {deviation.Name}",
                         }).ToList();

            var query = queryRisk.Concat(queryDeviation).AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
                query = query.Take(10);

            return await Task.Run(() => query.ToList());
        }

        async Task<Action> IActionRepository.GetAsNoTrackingAsync(Expression<System.Func<Action, bool>> expression)
        {
            return await context.Set<Action>()
                .AsNoTracking()
                .Where(expression)
                .SingleOrDefaultAsync();
        }

        async Task IActionRepository.DeleteById(string id)
        {
            var actionToDelete = await (from action in context.Set<Action>()
                            where action.Id == id select action).SingleOrDefaultAsync();

            if (actionToDelete != null && !string.IsNullOrWhiteSpace(actionToDelete.Id))
                context.Remove(actionToDelete);
        }

        async Task<ICollection<ActionDto>> IActionRepository.GetByMetadataId(string metadataId)
        {
            return await context.Set<Action>()
                .AsNoTracking()
                .Where(x => x.MetadataId == metadataId)
                .Select(action => new ActionDto
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
                    RelatedId = action.RelatedId
                }).ToListAsync();
        }
    }
}

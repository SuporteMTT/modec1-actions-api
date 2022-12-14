using Actions.Core.Domain.Actions.Entities;
using Actions.Core.Domain.Departments.Entities;
using Actions.Core.Domain.ResponsePlans.Dtos;
using Actions.Core.Domain.ResponsePlans.Entities;
using Actions.Core.Domain.Risks.Dtos;
using Actions.Core.Domain.Risks.Entities;
using Actions.Core.Domain.Risks.Interfaces;
using Actions.Core.Domain.Shared;
using Actions.Core.Domain.Shared.Dtos;
using Actions.Core.Domain.Shared.Enums;
using Actions.Core.Domain.Users.Entities;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Domain.Impl.Entity;
using Shared.Core.Domain.Interface.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
                .OrderBy(r => r.CreatedDate)
                .Select(o => new
                {
                    Data = new RiskListDto()
                    {
                        Id = o.Id,
                        CreatedDate = o.CreatedDate,
                        Cause = o.Cause,
                        ClosedCancelledDate = o.ClosedCancelledDate,
                        Name = o.Name,
                        Owner = o.Owner.Name,
                        Level = o.Level.Status(),
                        Status = o.Status.Status(),
                        NotInitiated = context.Set<Action>().Where(x => x.RelatedId == o.Id && x.Status == Core.Domain.Actions.Enums.ActionStatusEnum.NotInitiated).Count(),
                        OnGoing = context.Set<Action>().Where(x => x.RelatedId == o.Id && x.Status == Core.Domain.Actions.Enums.ActionStatusEnum.OnGoing).Count(),
                        Concluded = context.Set<Action>().Where(x => x.RelatedId == o.Id && x.Status == Core.Domain.Actions.Enums.ActionStatusEnum.Concluded).Count(),
                        Delayed = context.Set<Action>().Where(x => x.RelatedId == o.Id && x.Status == Core.Domain.Actions.Enums.ActionStatusEnum.Delayed).Count(),
                        ResponsePlans = context.Set<Action>().Include(x => x.Responsible).AsNoTracking().Where(x => x.MetadataId == o.Id).ToList(),
                        Code = o.Code,
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

        public async Task<RiskDto> GetAsync(string id)
        {
            return await context.Set<Risk>().Where(risk => risk.Id == id).Select(risk => new RiskDto
            {
                Id = risk.Id,
                Code = risk.Code,
                Status = risk.Status,
                Owner = risk.Owner != null ? new Core.Domain.Users.Dtos.UserDto{ Id = risk.Owner.Id, Name = risk.Owner.Name } : null,
                Name = risk.Name,
                Description = risk.Description,
                Cause = risk.Cause,
                Impact = risk.Impact,
                Category = risk.Category,
                Level = risk.Level,
                Dimension = risk.Dimension,
                DimensionDescription = risk.DimensionDescription,
                ProjectStep = risk.ProjectStep,
                CreatedDate = risk.CreatedDate,
                CreatedBy = risk.CreatedBy.Name,
                ClosedCancelledDate = risk.ClosedCancelledDate,
                ClosedCancelledBy = risk.ClosedCancelledBy != null ? risk.ClosedCancelledBy.Name : null,
                Justification = risk.Justification,
                RealImpact = risk.RealImpact,
                ResponsePlans = (from responsePlan in context.Set<Action>()
                where responsePlan.MetadataId == risk.Id
                select new ResponsePlanDto
                {
                    Id = responsePlan.Id,
                    ActualEndDate = responsePlan.ActualEndDate,
                    ActualStartDate = responsePlan.ActualStartDate,
                    ClosedBy = responsePlan.ClosedBy != null ? responsePlan.ClosedBy.Name : null,
                    ClosedDate = responsePlan.ClosedDate,
                    Comments = responsePlan.Comments,
                    Cost = responsePlan.Cost,
                    CreatedDate = responsePlan.CreatedDate,
                    Description = responsePlan.Description,
                    DueDate = responsePlan.DueDate,
                    MetadataId = responsePlan.MetadataId,
                    OriginalDueDate = responsePlan.OriginalDueDate,
                    Responsible = responsePlan.Responsible != null ? new Core.Domain.Users.Dtos.UserDto
                    {
                        Id = responsePlan.Responsible.Id,
                        Name = responsePlan.Responsible.Name
                    } : null,
                    Status = responsePlan.Status.Status(),
                    Sync = true
                }).ToList(),
                CancelledJustification = risk.CancelledJustification,
                AssociatedTaskIds = (from riskTask in context.Set<RiskTask>() 
                                    where riskTask.RiskId == risk.Id
                                    select riskTask.ProjectTaskId).ToList()
            }) .FirstOrDefaultAsync();
        }

        public async Task<ICollection<RiskAutocompleteDto>> GetAsync(string search, string metadataId)
        {
            var query = (from risk in context.Set<Risk>()
                         where (risk.MetadataId == metadataId &&
                         (string.IsNullOrWhiteSpace(search) || (risk.Code + risk.Name).Contains(search)))
                         orderby (risk.Code + risk.Name)
                         select new RiskAutocompleteDto
                         {
                             Id = risk.Id,
                             Name = $"{risk.Code} {risk.Name}",
                         });

            if (!string.IsNullOrWhiteSpace(search))
                query = query.Take(10);

            return await query.ToListAsync();
        }
        
        public async Task<string> GetLastCode(string departmentId)
        {
            var departmentCode = (from department in context.Set<Department>()
                                  where department.Id == departmentId
                                  select department.Code).SingleOrDefault();

            var codeStart = $"R-{departmentCode}";
            var startCodeLength = codeStart.Length;

            var lastCode = await (from risk in context.Set<Risk>()
                                  where risk.Code.Substring(0, startCodeLength) == codeStart
                                  orderby risk.Code descending
                                  select risk.Code.Substring(startCodeLength + 1, 4)
                                 ).FirstOrDefaultAsync();

            if (string.IsNullOrWhiteSpace(lastCode))
                return $"{codeStart}-0001";

            return $"{codeStart}-{int.Parse(lastCode) + 1:0000}";
        }

        async Task<Risk> IRiskRepository.GetAsNoTrackingAsync(Expression<System.Func<Risk, bool>> expression)
        {
            return await context.Set<Risk>()
                .AsNoTracking()
                .Where(expression)
                .SingleOrDefaultAsync();
        }

        async Task IRiskRepository.DeleteById(string id)
        {
            var riskToDelete = await (from risk in context.Set<Risk>()
                            where risk.Id == id select risk).SingleOrDefaultAsync();

            if (riskToDelete != null && !string.IsNullOrWhiteSpace(riskToDelete.Id))
                context.Remove(riskToDelete);
        }

        async Task<ShortObjectDto> IRiskRepository.GetByIdAsync(string id)
        {
            return await (from risk in context.Set<Risk>() 
                            where risk.Id == id
                            select new ShortObjectDto
                            {
                                Id = risk.Id,
                                Name = $"{risk.Code} {risk.Name}",
                            }).FirstOrDefaultAsync();
        }
    }
}

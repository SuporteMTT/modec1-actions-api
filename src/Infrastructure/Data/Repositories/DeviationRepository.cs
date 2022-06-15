using Actions.Core.Domain.Actions.Entities;
using Actions.Core.Domain.Departments.Entities;
using Actions.Core.Domain.Deviations.Dtos;
using Actions.Core.Domain.Deviations.Entities;
using Actions.Core.Domain.Deviations.Interfaces;
using Actions.Core.Domain.ResponsePlans.Dtos;
using Actions.Core.Domain.ResponsePlans.Entities;
using Actions.Core.Domain.Shared;
using Actions.Core.Domain.Shared.Dtos;
using Actions.Core.Domain.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Domain.Impl.Entity;
using Shared.Core.Domain.Interface.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Actions.Infrastructure.Data.Repositories
{
    public class DeviationRepository : UnitOfWork, IDeviationRepository
    {
        public DeviationRepository(ActionsContext context) : base(context) { }

        public async Task<IPagination<DeviationListDto>> GetAsync(
            string metadataId,
            MetadataTypeEnum metadataType,
            int? page,
            int? count)
        {
            var condition = context.Set<Deviation>()
                .Where(x => x.MetadataId == metadataId && x.MetadataType == metadataType);

            var results = await condition
                .OrderBy(r => r.CreatedDate)
                .Select(o => new
                {
                    Data = new DeviationListDto()
                    {
                        Id = o.Id,
                        CreatedDate = o.CreatedDate,
                        Cause = o.Cause,
                        ClosedCancelledDate = o.ClosedCancelledDate,
                        Name = o.Name,
                        Priority = o.Priority.Priority(),
                        Status = o.Status.Status(),
                        NotInitiated = context.Set<ResponsePlan>().Where(x => x.MetadataId == o.Id && x.Status == Core.Domain.Actions.Enums.ActionStatusEnum.NotInitiated).Count(),
                        OnGoing = context.Set<ResponsePlan>().Where(x => x.MetadataId == o.Id && x.Status == Core.Domain.Actions.Enums.ActionStatusEnum.OnGoing).Count(),
                        Concluded = context.Set<ResponsePlan>().Where(x => x.MetadataId == o.Id && x.Status == Core.Domain.Actions.Enums.ActionStatusEnum.Concluded).Count(),
                        Delayed = context.Set<ResponsePlan>().Where(x => x.MetadataId == o.Id && x.Status == Core.Domain.Actions.Enums.ActionStatusEnum.Delayed).Count()
                    },
                    Total = condition.Count()
                })
                .Skip((page.Value - 1) * count.Value).Take(count.Value)
                .ToArrayAsync();

            var risks = results.Select(r => r.Data).ToList();
            var total = results.FirstOrDefault()?.Total ?? risks.Count;

            return new Pagination<DeviationListDto>()
            {
                Data = risks,
                Total = total
            };
        }

        public async Task<DeviationDto> GetAsync(string id)
        {
            return await context.Set<Deviation>().Where(deviation => deviation.Id == id).Select(deviation => new DeviationDto
            {
                Id = deviation.Id,
                Code = deviation.Code,
                Status = deviation.Status,
                Name = deviation.Name,
                Description = deviation.Description,
                Cause = deviation.Cause,
                Category = deviation.Category,
                AssociatedRisk = deviation.AssociatedRisk != null ? new Core.Domain.Risks.Dtos.RiskDto 
                { 
                    Id = deviation.AssociatedRisk.Id, 
                    Name = $"{deviation.AssociatedRisk.Code} {deviation.AssociatedRisk.Name}"
                } : null,                
                Priority = deviation.Priority,
                CreatedDate = deviation.CreatedDate,
                CreatedBy = deviation.CreatedBy.Name,
                ClosedCancelledDate = deviation.ClosedCancelledDate,
                ClosedCancelledBy = deviation.ClosedCancelledBy != null ? deviation.ClosedCancelledBy.Name : null,
                ResponsePlans = (from responsePlan in context.Set<ResponsePlan>()
                where responsePlan.MetadataId == deviation.Id
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
                    Description = responsePlan.ActionDescription,
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
                CancelledJustification = deviation.CancelledJustification
            }) .FirstOrDefaultAsync();
        }

        public async Task<string> GetLastCode(string departmentId)
        {
            var departmentCode = (from department in context.Set<Department>()
                                  where department.Id == departmentId
                                  select department.Code).SingleOrDefault();
                                  
            var codeStart = $"D-{departmentCode}";
            var startCodeLength = codeStart.Length;

            var lastCode = await (from deviation in context.Set<Deviation>()
                                  where deviation.Code.Substring(0, startCodeLength) == codeStart
                                  orderby deviation.Code descending
                                  select deviation.Code.Substring(startCodeLength + 1, 4)
                                 ).FirstOrDefaultAsync();

            if (string.IsNullOrWhiteSpace(lastCode))
                return $"{codeStart}-0001";

            return $"{codeStart}-{int.Parse(lastCode) + 1:0000}";
        }

        async Task<Deviation> IDeviationRepository.GetAsNoTrackingAsync(Expression<System.Func<Deviation, bool>> expression)
        {
            return await context.Set<Deviation>()
                .AsNoTracking()
                .Where(expression)
                .SingleOrDefaultAsync();
        }

        async Task IDeviationRepository.DeleteById(string id)
        {
            var deviationToDelete = await (from deviation in context.Set<Deviation>()
                            where deviation.Id == id select deviation).SingleOrDefaultAsync();

            if (deviationToDelete != null && !string.IsNullOrWhiteSpace(deviationToDelete.Id))
                context.Remove(deviationToDelete);
        }

        async Task<ShortObjectDto> IDeviationRepository.GetByIdAsync(string id)
        {
            return await (from deviation in context.Set<Deviation>() 
                            where deviation.Id == id
                            select new ShortObjectDto
                            {
                                Id = deviation.Id,
                                Name = $"{deviation.Code} {deviation.Name}",
                            }).FirstOrDefaultAsync();
        }
    }
}

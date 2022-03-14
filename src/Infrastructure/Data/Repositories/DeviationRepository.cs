using Actions.Core.Domain.Deviations.Dtos;
using Actions.Core.Domain.Deviations.Entities;
using Actions.Core.Domain.Deviations.Interfaces;
using Actions.Core.Domain.Shared;
using Actions.Core.Domain.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Domain.Impl.Entity;
using Shared.Core.Domain.Interface.Entity;
using System.Linq;
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
                        Priority = o.Priority,
                        Status = o.Status.Status(),
                        NotInitated = null,
                        OnGoing = null,
                        Concluded = null,
                        Delayed = null
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
            }) .FirstOrDefaultAsync();
        }

        public async Task<string> GetLastCode(string departmentCode)
        {

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
    }
}

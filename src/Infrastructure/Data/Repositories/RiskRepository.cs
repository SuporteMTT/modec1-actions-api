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
                .OrderBy(r => r.CreatedDate)
                .Select(o => new
                {
                    Data = new RiskListDto()
                    {
                        Id = o.Id,
                        CreateDate = o.CreatedDate,
                        Cause = o.Cause,
                        ClosedCancelledDate = o.ClosedCancelledDate,
                        Name = o.Name,
                        Owner = o.Owner.Name,
                        Level = o.Level,
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
                CreateDate = risk.CreatedDate,
                Cause = risk.Cause,
                ClosedCancelledDate = risk.ClosedCancelledDate,
                Name = risk.Name,
                Owner = risk.Owner.Name,
                Level = risk.Level,
                Status = risk.Status.Status(),
                NotInitated = null,
                OnGoing = null,
                Concluded = null,
                Delayed = null
            }) .FirstOrDefaultAsync();
        }

        public async Task<string> GetLastCode(string departmentCode)
        {

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


    }
}

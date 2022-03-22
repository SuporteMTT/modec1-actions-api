
using Actions.Core.Domain.Risks.Entities;
using Actions.Core.Domain.Risks.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Actions.Infrastructure.Data.Repositories
{
    public class RiskTaskRepository : UnitOfWork, IRiskTaskRepository
    {
        private readonly ActionsContext _context;
        
        public RiskTaskRepository(ActionsContext context) : base(context) 
        {
            _context = context;
        }

        async Task<ICollection<RiskTask>> IRiskTaskRepository.GetAsNoTrackingByProjectId(string riskId)
        {
            return await (from riskTask in _context.Set<RiskTask>().AsNoTracking()
                    where riskTask.RiskId == riskId
                    select riskTask).ToListAsync();
        }
    }
}

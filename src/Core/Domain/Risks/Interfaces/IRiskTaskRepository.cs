using Actions.Core.Domain.Risks.Entities;
using Actions.Core.Domain.Shared.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Actions.Core.Domain.Risks.Interfaces
{
    public interface IRiskTaskRepository : IUnitOfWork
    {
        Task<ICollection<RiskTask>> GetAsNoTrackingByProjectId(string riskId);
    }
}

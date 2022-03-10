using Actions.Core.Domain.Risks.Interfaces;

namespace Actions.Infrastructure.Data.Repositories
{
    public class RiskRepository : UnitOfWork, IRiskRepository
    {
        public RiskRepository(ActionsContext context) : base(context) { }
    }
}

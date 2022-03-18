using Actions.Core.Domain.StatusHistories.Interfaces;

namespace Actions.Infrastructure.Data.Repositories
{
    public class StatusHistoryRepository : UnitOfWork, IStatusHistoryRepository
    {
        public StatusHistoryRepository(ActionsContext context) : base(context) { }

    }
}

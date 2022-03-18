using System.Threading.Tasks;
using Actions.Core.Domain.Actions.Interfaces;
using Actions.Core.Domain.Shared.Interfaces.Entities;
using Actions.Core.Domain.StatusHistories.Commands;
using Actions.Core.Domain.StatusHistories.Entities;
using Shared.Core.Domain.Impl.Validator;

namespace Actions.Core.Domain.StatusHistories.Handlers
{
    public class StatusHistoriesCommandHandler
    {
        private readonly ITokenUtil _tokenUtil;
        private readonly IActionRepository _repository;

        public StatusHistoriesCommandHandler(IActionRepository repository, ITokenUtil tokenUtil)
        {
            _tokenUtil = tokenUtil;
            _repository = repository;
        }

        public async Task Handle(CreateStatusHistoryCommand request)
        {
            if (request is null) throw new System.ArgumentNullException(nameof(request), "The object received is not valid");

            request.ValidateAndThrow();

            var statusHistory = new StatusHistory(request.Date, _tokenUtil.Id, request.Status, request.MetadataId);

            statusHistory.ValidateAndThrow();

            _repository.Insert(statusHistory);
            
            await _repository.SaveChangesAsync();
        }
    }
}
using System.Threading.Tasks;
using Actions.Core.Domain.Actions.Commands;
using Actions.Core.Domain.Actions.Dtos;
using Actions.Core.Domain.Actions.Interfaces;
using Actions.Core.Domain.Shared.Interfaces.Entities;
using Shared.Core.Domain.Impl.Validator;

namespace Actions.Core.Domain.Actions.Handlers
{
    public class ActionsCommandHandler
    {
        private readonly ITokenUtil _tokenUtil;
        private readonly IActionRepository _repository;

        public ActionsCommandHandler(IActionRepository repository, ITokenUtil tokenUtil)
        {
            _tokenUtil = tokenUtil;
            _repository = repository;
        }

        public async Task<ActionDto> Handle(CreateActionCommand request)
        {
            if (request is null) throw new System.ArgumentNullException(nameof(request), "The object received is not valid");

            request.ValidateAndThrow();

            var action = new Core.Domain.Actions.Entities.Action(
                request.Description,
                request.ResponsibleId,
                request.DueDate,
                request.Status,
                request.ActualStartDate,
                request.ActualEndDate,
                request.Coments,
                request.MetadataType,
                request.MetadataId,
                request.RelatedId,
                request.Cost
            );

            action.ValidateAndThrow();

            _repository.Insert(action);
            
            await _repository.SaveChangesAsync();

            return await _repository.GetAsync(action.Id);
        }

        public async Task Handle(DeleteActionCommand request)
        {
            request.ValidateAndThrow();

            await _repository.DeleteById(request.Id);

            await _repository.SaveChangesAsync();
        }
    }
}
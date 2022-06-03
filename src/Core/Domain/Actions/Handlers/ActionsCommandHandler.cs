using System.Threading.Tasks;
using Actions.Core.Domain.Actions.Commands;
using Actions.Core.Domain.Actions.Dtos;
using Actions.Core.Domain.Actions.Interfaces;
using Actions.Core.Domain.Shared.Interfaces.Entities;
using Actions.Core.Domain.StatusHistories.Handlers;
using Actions.Core.Domain.StatusHistories.Helpers;
using Shared.Core.Domain.Impl.Validator;

namespace Actions.Core.Domain.Actions.Handlers
{
    public class ActionsCommandHandler
    {
        private readonly ITokenUtil _tokenUtil;
        private readonly IActionRepository _repository;
        private readonly StatusHistoriesCommandHandler _statusHistoryCommandHandler;

        public ActionsCommandHandler
        (
            IActionRepository repository,
            ITokenUtil tokenUtil,
            StatusHistoriesCommandHandler statusHistoryCommandHandler
        )
        {
            _tokenUtil = tokenUtil;
            _repository = repository;
            _statusHistoryCommandHandler = statusHistoryCommandHandler;
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

            await _statusHistoryCommandHandler.Handle(
                new StatusHistories.Commands.CreateStatusHistoryCommand(
                    System.DateTime.Now,
                    _tokenUtil.Id,
                    StatusHistories.Enums.StatusHistoryEnum.Active,
                    action.Id)
            );

            return await _repository.GetAsync(action.Id);
        }

        public async Task Handle(UpdateActionCommand request)
        {
            if (request is null) throw new System.ArgumentNullException(nameof(request), "The object received is not valid");

            request.ValidateAndThrow();

            var action = await _repository.GetAsNoTrackingAsync(x => x.Id == request.Id);

            if (action.HasModified(request.Description, request.ResponsibleId, request.DueDate, request.Status, request.ActualStartDate,
                                request.ActualEndDate,request.Coments, request.RelatedId, request.Cost))
            {
                if (action.Status != request.Status)
                {
                    if (request.Status == Enums.ActionStatusEnum.Concluded)
                    {
                        action.ClosedDate = System.DateTime.Now;
                        action.ClosedById = request.ResponsibleId;

                    }
                    else
                    {
                        action.ClosedDate = null;
                        action.ClosedById = null;
                    }
                    await _statusHistoryCommandHandler.Handle(
                        new StatusHistories.Commands.CreateStatusHistoryCommand(
                            System.DateTime.Now, 
                            _tokenUtil.Id, 
                            StatusHistoryEnumHelper.ToStatusHistoryEnum(request.Status),
                            action.Id)
                    );
                }

                action.UpdateData(request.Description, request.ResponsibleId, request.DueDate, request.Status, request.ActualStartDate,
                                request.ActualEndDate,request.Coments, _tokenUtil.Id, request.RelatedId, request.Cost);
                
                 _repository.Update(action);

                await _repository.SaveChangesAsync();
            }
        }

        public async Task Handle(DeleteActionCommand request)
        {
            request.ValidateAndThrow();

            await _repository.DeleteById(request.Id);

            await _repository.SaveChangesAsync();
        }
    }
}
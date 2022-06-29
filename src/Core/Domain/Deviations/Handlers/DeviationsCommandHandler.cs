using System;
using System.Linq;
using System.Threading.Tasks;
using Actions.Core.Domain.Actions.Commands;
using Actions.Core.Domain.Actions.Enums;
using Actions.Core.Domain.Actions.Handlers;
using Actions.Core.Domain.Actions.Queries;
using Actions.Core.Domain.Deviations.Commands;
using Actions.Core.Domain.Deviations.Dtos;
using Actions.Core.Domain.Deviations.Entities;
using Actions.Core.Domain.Deviations.Interfaces;
using Actions.Core.Domain.ResponsePlans.Handlers;
using Actions.Core.Domain.Shared.Interfaces.Entities;
using Actions.Core.Domain.StatusHistories.Handlers;
using Actions.Core.Domain.StatusHistories.Helpers;
using Shared.Core.Domain.Impl.Validator;

namespace Actions.Core.Domain.Deviations.Handlers
{
    public class DeviationsCommandHandler
    {
        private readonly ITokenUtil _tokenUtil;
        private readonly IDeviationRepository _repository;
        private readonly StatusHistoriesCommandHandler _statusHistoryCommandHandler;
        private readonly ResponsePlansCommandHandler _responsePlansCommandHandler;
        private readonly ActionsCommandHandler _actionsCommandHandler;
        private readonly ActionsQueryHandler _actionsQueryHandler;

        public DeviationsCommandHandler
        (
            IDeviationRepository repository,
            ITokenUtil tokenUtil,
            StatusHistoriesCommandHandler statusHistoryCommandHandler,
            ResponsePlansCommandHandler responsePlansCommandHandler,
            ActionsCommandHandler actionsCommandHandler,
            ActionsQueryHandler actionsQueryHandler
        )
        {
            _tokenUtil = tokenUtil;
            _repository = repository;
            _statusHistoryCommandHandler = statusHistoryCommandHandler;
            _responsePlansCommandHandler = responsePlansCommandHandler;
            _actionsCommandHandler = actionsCommandHandler;
            _actionsQueryHandler = actionsQueryHandler;
        }

        public async Task<DeviationDto> Handle(CreateDeviationCommand request)
        {
            if (request is null) throw new ArgumentNullException(nameof(request), "The object received is not valid");

            request.ValidateAndThrow();

            var code = await _repository.GetLastCode(request.DepartmentId);

            var deviation = new Deviation(
                code,
                request.Name,
                request.Description,
                request.Cause,
                request.Category,
                request.Priority,
                request.AssociatedRiskId,
                _tokenUtil.Id,
                request.MetadataType,
                request.MetadataId
            );

            deviation.ValidateAndThrow();

            _repository.Insert(deviation);

            foreach(var responsePlan in request.ResponsePlans){
                await _actionsCommandHandler.Handle(
                                            new CreateActionCommand(deviation.Id, responsePlan.Description, responsePlan.Responsible?.Id,
                                                responsePlan.DueDate, (ActionStatusEnum)responsePlan.Status, responsePlan.ActualStartDate, responsePlan.ActualEndDate,
                                                responsePlan.Cost, responsePlan.Comments, request.MetadataType, deviation.Id)
                                            );
            }
            
            await _repository.SaveChangesAsync();

            await _statusHistoryCommandHandler.Handle(
                new StatusHistories.Commands.CreateStatusHistoryCommand(
                    System.DateTime.Now, 
                    _tokenUtil.Id, 
                    StatusHistories.Enums.StatusHistoryEnum.Active,
                    deviation.Id)
            );

            return await _repository.GetAsync(deviation.Id);
        }

        public async Task Handle(UpdateDeviationCommand request)
        {
            if (request is null) throw new System.ArgumentNullException(nameof(request), "The object received is not valid");

            request.ValidateAndThrow();

            var deviation = await _repository.GetAsNoTrackingAsync(x => x.Id == request.Id);

            if (deviation.HasModified(request.Name, request.Description, request.Status, request.Category,
                                request.AssociatedRiskId,request.Cause, request.Priority))
            {
                var previousStatus = deviation.Status;

                deviation.UpdateData(request.Status, request.Name, request.Description, request.Cause, request.Category,
                                    request.Priority, request.AssociatedRiskId, _tokenUtil.Id, request.CancelledJustification);
                
                 _repository.Update(deviation);

                await _repository.SaveChangesAsync();

                if (previousStatus != request.Status)
                {
                    await _statusHistoryCommandHandler.Handle(
                        new StatusHistories.Commands.CreateStatusHistoryCommand(
                            System.DateTime.Now, 
                            _tokenUtil.Id, 
                            StatusHistoryEnumHelper.ToStatusHistoryEnum(request.Status),
                            deviation.Id)
                    );
                }
            }

            var existingActions = await _actionsQueryHandler.Handle(new GetActionByMetadataIdQuery(deviation.Id));
            var toAdd = request.ResponsePlans.Where(x => !(existingActions.Where(y => y.Id == x.Id).Count() > 0));
            var toUpdate = request.ResponsePlans.Where(x => existingActions.Where(y => y.Id == x.Id).Count() > 0);

            foreach (var responsePlan in toUpdate)
            {
                await _actionsCommandHandler.Handle(new UpdateActionCommand(responsePlan.Id, deviation.Id, responsePlan.Description, responsePlan.Responsible?.Id,
                                                responsePlan.DueDate.Value, (ActionStatusEnum)responsePlan.Status.Id, responsePlan.ActualStartDate, responsePlan.ActualEndDate,
                                                responsePlan.Cost, responsePlan.Comments));
            }

            foreach (var responsePlan in toAdd)
            {
                await _actionsCommandHandler.Handle(new CreateActionCommand(deviation.Id, responsePlan.Description, responsePlan.Responsible?.Id,
                                               responsePlan.DueDate, (ActionStatusEnum)responsePlan.Status.Id, responsePlan.ActualStartDate, responsePlan.ActualEndDate,
                                               responsePlan.Cost, responsePlan.Comments, request.MetadataType, deviation.Id));
            }

            await _repository.SaveChangesAsync();
        }

        public async Task Handle(DeleteDeviationCommand request)
        {
            request.ValidateAndThrow();

            await _repository.DeleteById(request.Id);

            await _repository.SaveChangesAsync();
        }
    }
}
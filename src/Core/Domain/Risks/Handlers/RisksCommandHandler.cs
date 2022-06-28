using System;
using System.Linq;
using System.Threading.Tasks;
using Actions.Core.Domain.Actions.Commands;
using Actions.Core.Domain.Actions.Enums;
using Actions.Core.Domain.Actions.Handlers;
using Actions.Core.Domain.ResponsePlans.Handlers;
using Actions.Core.Domain.Risks.Commands;
using Actions.Core.Domain.Risks.Dtos;
using Actions.Core.Domain.Risks.Entities;
using Actions.Core.Domain.Risks.Interfaces;
using Actions.Core.Domain.Shared.Interfaces.Entities;
using Actions.Core.Domain.StatusHistories.Handlers;
using Actions.Core.Domain.StatusHistories.Helpers;
using Shared.Core.Domain.Impl.Validator;

namespace Actions.Core.Domain.Risks.Handlers
{
    public class RisksCommandHandler
    {
        private readonly ITokenUtil _tokenUtil;
        private readonly IRiskRepository _repository;
        private readonly IRiskTaskRepository _riskTaskRepository;
        private readonly StatusHistoriesCommandHandler _statusHistoryCommandHandler;
        private readonly ResponsePlansCommandHandler _responsePlansCommandHandler;
        private readonly ActionsCommandHandler _actionsCommandHandler;
        public RisksCommandHandler(
            IRiskRepository repository,
            IRiskTaskRepository riskTaskRepository,
            ITokenUtil tokenUtil,
            StatusHistoriesCommandHandler statusHistoryCommandHandler,
            ResponsePlansCommandHandler responsePlansCommandHandler,
            ActionsCommandHandler actionsCommandHandler
        )
        {
            _tokenUtil = tokenUtil;
            _repository = repository;
            _riskTaskRepository = riskTaskRepository;
            _statusHistoryCommandHandler = statusHistoryCommandHandler;
            _responsePlansCommandHandler = responsePlansCommandHandler;
            _actionsCommandHandler = actionsCommandHandler;
        }

        public async Task<RiskDto> Handle(CreateRiskCommand request)
        {
            if (request is null) throw new ArgumentNullException(nameof(request), "The object received is not valid");

            request.ValidateAndThrow();

            var code = await _repository.GetLastCode(request.DepartmentId);

            var risk = new Risk(
                code,
                request.OwnerId,
                request.Name,
                request.Description,
                request.Cause,
                request.Impact,
                request.Category,
                request.Level,
                request.Dimension,
                request.DimensionDescription,
                request.ProjectStep,
                _tokenUtil.Id,
                request.Justification,
                request.RealImpact,
                request.MetadataType,
                request.MetadataId
            );

            risk.ValidateAndThrow();

            _repository.Insert(risk);
            
            if (request.MetadataType == Shared.Enums.MetadataTypeEnum.Project)
                await ManagerTasks(risk.Id, request.AssociatedTaskIds);

            foreach (var responsePlan in request.ResponsePlans)
            {
                await _actionsCommandHandler.Handle(
                                            new CreateActionCommand(risk.Id, responsePlan.Description, responsePlan.Responsible?.Id,
                                                responsePlan.DueDate, (ActionStatusEnum)responsePlan.Status.Id, responsePlan.ActualStartDate, responsePlan.ActualEndDate,
                                                responsePlan.Cost, responsePlan.Comments, request.MetadataType, risk.Id)
                                            );
            }

            //_responsePlansCommandHandler.Handle(new ResponsePlans.Commands.ProcessListResponsePlan(risk.Id, request.ResponsePlans));
            
            await _repository.SaveChangesAsync();

            await _statusHistoryCommandHandler.Handle(
                new StatusHistories.Commands.CreateStatusHistoryCommand(
                    System.DateTime.Now, 
                    _tokenUtil.Id, 
                    StatusHistories.Enums.StatusHistoryEnum.Active,
                    risk.Id)
            );

            return await _repository.GetAsync(risk.Id);
        }

        public async Task Handle(UpdateRiskCommand request)
        {
            if (request is null) throw new System.ArgumentNullException(nameof(request), "The object received is not valid");

            request.ValidateAndThrow();

            var risk = await _repository.GetAsNoTrackingAsync(x => x.Id == request.Id);

            if (request.MetadataType == Shared.Enums.MetadataTypeEnum.Project)
                await ManagerTasks(risk.Id, request.AssociatedTaskIds);

            if (risk.HasModified(request.Status, request.OwnerId, request.Name, request.Description, request.Cause, request.Impact, request.Category,
                                request.Level, request.Dimension, request.DimensionDescription, request.ProjectStep, request.Justification, request.RealImpact))
            {
                var previousStatus = risk.Status;
                risk.UpdateData(request.Status, request.OwnerId, request.Name, request.Description, request.Cause, request.Impact, request.Category,
                                request.Level, request.Dimension, request.DimensionDescription, request.ProjectStep, request.Justification, request.RealImpact, _tokenUtil.Id, request.CancelledJustification);
                
                 _repository.Update(risk);

                await _repository.SaveChangesAsync();

                if (previousStatus != request.Status)
                {
                    await _statusHistoryCommandHandler.Handle(
                        new StatusHistories.Commands.CreateStatusHistoryCommand(
                            System.DateTime.Now, 
                            _tokenUtil.Id, 
                            StatusHistoryEnumHelper.ToStatusHistoryEnum(request.Status),
                            risk.Id)
                    );
                }
            }

            foreach (var responsePlan in request.ResponsePlans)
            {
                await _actionsCommandHandler.Handle(
                                            new UpdateActionCommand(responsePlan.Id, risk.Id, responsePlan.Description, responsePlan.Responsible?.Id,
                                                responsePlan.DueDate.Value, (ActionStatusEnum)responsePlan.Status.Id, responsePlan.ActualStartDate, responsePlan.ActualEndDate,
                                                responsePlan.Cost, responsePlan.Comments)
                                            );
            }

            //_responsePlansCommandHandler.Handle(new ResponsePlans.Commands.ProcessListResponsePlan(risk.Id, request.ResponsePlans));

            await _repository.SaveChangesAsync();
        }

        public async Task Handle(DeleteRiskCommand request)
        {
            request.ValidateAndThrow();

            await _repository.DeleteById(request.Id);

            await _repository.SaveChangesAsync();
        }

        private async Task ManagerTasks(string riskId, string[] tasksIds)
        {
            var riskTasks = await _riskTaskRepository.GetAsNoTrackingByProjectId(riskId);
            var tasksIdsDb = riskTasks.Select(riskTask => riskTask.ProjectTaskId);
            var listToRemove = riskTasks.Where(riskTask => !tasksIds.Contains(riskTask.ProjectTaskId));
            foreach (var item in listToRemove)
            {
                var riskTask = new RiskTask
                {
                    RiskId = riskId,
                    ProjectTaskId = item.ProjectTaskId
                };
                _riskTaskRepository.Delete(riskTask);
            }
            var listToAdd = tasksIdsDb?.Count() == 0 ? tasksIds : tasksIds.Where(id => !tasksIdsDb.Contains(id));
            if (listToAdd != null)
            {
                foreach (var taskId in listToAdd)
                {
                    var projectClient = new RiskTask
                    {
                        RiskId = riskId,
                        ProjectTaskId = taskId
                    };
                    _riskTaskRepository.Insert(projectClient);
                }
            }
        }
    }
}
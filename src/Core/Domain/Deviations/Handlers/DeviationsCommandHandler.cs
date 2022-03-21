using System;
using System.Threading.Tasks;
using Actions.Core.Domain.Deviations.Commands;
using Actions.Core.Domain.Deviations.Dtos;
using Actions.Core.Domain.Deviations.Entities;
using Actions.Core.Domain.Deviations.Interfaces;
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

        public DeviationsCommandHandler
        (
            IDeviationRepository repository,
            ITokenUtil tokenUtil,
            StatusHistoriesCommandHandler statusHistoryCommandHandler
        )
        {
            _tokenUtil = tokenUtil;
            _repository = repository;
            _statusHistoryCommandHandler = statusHistoryCommandHandler;
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
                deviation.UpdateData(request.Status, request.Name, request.Description, request.Cause, request.Category,
                                    request.Priority, request.AssociatedRiskId, _tokenUtil.Id);
                
                 _repository.Update(deviation);

                await _repository.SaveChangesAsync();

                if (deviation.Status != request.Status)
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
        }

        public async Task Handle(DeleteDeviationCommand request)
        {
            request.ValidateAndThrow();

            await _repository.DeleteById(request.Id);

            await _repository.SaveChangesAsync();
        }
    }
}
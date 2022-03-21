using System;
using System.Threading.Tasks;
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
        private readonly StatusHistoriesCommandHandler _statusHistoryCommandHandler;

        public RisksCommandHandler(
            IRiskRepository repository, 
            ITokenUtil tokenUtil,
            StatusHistoriesCommandHandler statusHistoryCommandHandler
        )
        {
            _tokenUtil = tokenUtil;
            _repository = repository;
            _statusHistoryCommandHandler = statusHistoryCommandHandler;
        }

        public async Task<RiskDto> Handle(CreateRiskCommand request)
        {
            if (request is null) throw new ArgumentNullException(nameof(request), "The object received is not valid");

            request.ValidateAndThrow();

            var code = await _repository.GetLastCode("BPM");

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

            if (risk.HasModified(request.Status, request.OwnerId, request.Name, request.Description, request.Cause, request.Impact, request.Category,
                                request.Level, request.Dimension, request.DimensionDescription, request.ProjectStep, request.Justification, request.RealImpact))
            {
                risk.UpdateData(request.Status, request.OwnerId, request.Name, request.Description, request.Cause, request.Impact, request.Category,
                                request.Level, request.Dimension, request.DimensionDescription, request.ProjectStep, request.Justification, request.RealImpact, _tokenUtil.Id);
                
                 _repository.Update(risk);

                await _repository.SaveChangesAsync();

                if (risk.Status != request.Status)
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
        }

        public async Task Handle(DeleteRiskCommand request)
        {
            request.ValidateAndThrow();

            await _repository.DeleteById(request.Id);

            await _repository.SaveChangesAsync();
        }
    }
}
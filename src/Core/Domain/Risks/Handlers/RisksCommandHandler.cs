using System;
using System.Threading.Tasks;
using Actions.Core.Domain.Risks.Commands;
using Actions.Core.Domain.Risks.Dtos;
using Actions.Core.Domain.Risks.Entities;
using Actions.Core.Domain.Risks.Interfaces;
using Actions.Core.Domain.Shared.Interfaces.Entities;
using Shared.Core.Domain.Impl.Validator;

namespace Actions.Core.Domain.Risks.Handlers
{
    public class RisksCommandHandler
    {
        private readonly ITokenUtil _tokenUtil;
        private readonly IRiskRepository _repository;

        public RisksCommandHandler(IRiskRepository repository, ITokenUtil tokenUtil)
        {
            _tokenUtil = tokenUtil;
            _repository = repository;
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

            return await _repository.GetAsync(risk.Id);
        }

        public async Task Handle(DeleteRiskCommand request)
        {
            request.ValidateAndThrow();

            await _repository.DeleteById(request.Id);

            await _repository.SaveChangesAsync();

        }
    }
}
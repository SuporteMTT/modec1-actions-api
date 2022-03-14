using System;
using System.Threading.Tasks;
using Actions.Core.Domain.Deviations.Commands;
using Actions.Core.Domain.Deviations.Dtos;
using Actions.Core.Domain.Deviations.Entities;
using Actions.Core.Domain.Deviations.Interfaces;
using Actions.Core.Domain.Shared.Interfaces.Entities;
using Shared.Core.Domain.Impl.Validator;

namespace Actions.Core.Domain.Deviations.Handlers
{
    public class DeviationsCommandHandler
    {
        private readonly ITokenUtil _tokenUtil;
        private readonly IDeviationRepository _repository;

        public DeviationsCommandHandler(IDeviationRepository repository, ITokenUtil tokenUtil)
        {
            _tokenUtil = tokenUtil;
            _repository = repository;
        }

        public async Task<DeviationDto> Handle(CreateDeviationCommand request)
        {
            if (request is null) throw new ArgumentNullException(nameof(request), "The object received is not valid");

            request.ValidateAndThrow();

            var code = await _repository.GetLastCode("BPM");

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

            return await _repository.GetAsync(deviation.Id);
        }
    }
}
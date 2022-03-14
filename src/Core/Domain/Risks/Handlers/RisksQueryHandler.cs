using Actions.Core.Domain.Risks.Dtos;
using Actions.Core.Domain.Risks.Interfaces;
using Actions.Core.Domain.Risks.Queries;
using Actions.Core.Domain.Shared.Interfaces.Entities;
using Shared.Core.Domain.Impl.Validator;
using Shared.Core.Domain.Interface.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actions.Core.Domain.Risks.Handlers
{
    public class RisksQueryHandler
    {
        private readonly ITokenUtil _tokenUtil;
        private readonly IRiskRepository _repository;

        public RisksQueryHandler(ITokenUtil tokenUtil, IRiskRepository repository)
        {
            _tokenUtil = tokenUtil;
            _repository = repository;
        }

        public async Task<IPagination<RiskListDto>> Handle(ListRisksQuery query)
        {
            query.ValidateAndThrow();

            return await _repository.GetAsync(query.MetadataId, query.MetadataType, query.Page, query.Count);
        }

        public async Task<RiskDto> Handle(GetRiskByIdQuery query)
        {
            query.ValidateAndThrow();

            return await _repository.GetAsync(query.Id);
        }

    }
}

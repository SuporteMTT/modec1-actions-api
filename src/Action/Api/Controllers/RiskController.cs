using Actions.Core.Domain.Actions.Enums;
using Actions.Core.Domain.Risks.Commands;
using Actions.Core.Domain.Risks.Dtos;
using Actions.Core.Domain.Risks.Handlers;
using Actions.Core.Domain.Risks.Queries;
using Actions.Core.Domain.Shared;
using Actions.Core.Domain.Shared.Dtos;
using Actions.Core.Domain.Shared.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Actions.Api.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "Risk")]
    [Route("api/v{version:apiVersion}/risk")]    
    public class RiskController : ControllerBase
    {
        /// <summary>
        /// Retrieve the list risks 
        /// </summary>
        /// <response code="200">If it is successful</response>
        /// <response code="401">If has no access</response>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/v1/risk?originId=&amp;originType==&amp;
        ///
        /// </remarks>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ICollection<RiskListDto>> GetAsync(
            [FromServices] RisksQueryHandler handler,
            [FromQuery] string metadataId,
            [FromQuery] MetadataTypeEnum metadataType,
            [FromQuery] int page = 1,
            [FromQuery] int count = 10
        )
        {
            var risks = await handler.Handle(new ListRisksQuery(metadataId, metadataType, page, count));

            this.Response.Headers.Add("X-Total-Count", risks.Total.ToString());

            return risks.Data;            
        }

        /// <summary>
        /// Retrieve a risk by id 
        /// </summary>
        /// <response code="200">If it is successful</response>
        /// <response code="401">If has no access</response>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/v1/risk/{id}
        ///
        /// </remarks>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<RiskDto> GetByIdAsync(
            [FromServices] RisksQueryHandler handler,
            [FromRoute] string id)
        {
            return await handler.Handle(new GetRiskByIdQuery(id));
        }

        /// <summary>
        /// Search risk(s)
        /// </summary>
        /// <response code="200">If it is successful</response>
        /// <response code="401">If has no access</response>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/v1/risk/search/{search}/metadataId{metadataId}
        ///
        /// </remarks>
        [HttpGet("search/{search}/metadataId/{metadataId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ICollection<RiskAutocompleteDto>> GetByIdAsync(
            [FromServices] RisksQueryHandler handler,
            [FromRoute] string search,
            [FromRoute] string metadataId)
        {
            return await handler.Handle(new GetRiskSearchQuery(search, metadataId));
        }

        /// <summary>
        /// Insert a risk
        /// </summary>
        /// <response code="200">If it is successful</response>
        /// <response code="400">If invalid data is sent</response>
        /// <response code="401">If has no access</response>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/v1/risk
        ///      
        /// </remarks>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<RiskDto> PostAsync(
            [FromServices] RisksCommandHandler handler,
            [FromBody] CreateRiskCommand request
        )
        {
            return await handler.Handle(request);
        }

        /// <summary>
        /// Change a risk
        /// </summary>
        /// <response code="200">If it is successful</response>
        /// <response code="400">If invalid data is sent</response>
        /// <response code="401">If has no access</response>
        /// <response code="404">If no data is found</response>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/v1/risk
        ///
        /// </remarks>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        async public Task PutAsync(
            [FromServices] RisksCommandHandler handler,
            [FromBody] UpdateRiskCommand request
        )
        {
            await handler.Handle(request);
        }

        /// <summary>
        /// Delete risk by id
        /// </summary>
        /// <response code="200">If it is successful</response>
        /// <response code="400">If invalid data is sent</response>
        /// <response code="401">If has no access</response>
        /// <remarks>
        /// Sample request:
        /// 
        ///     DELETE /api/v1/risk/{id}
        ///      
        /// </remarks>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("{id}")]
        async public Task Delete(
            [FromServices] RisksCommandHandler handler,
            [FromRoute] string id)
        {
            await handler.Handle(new DeleteRiskCommand(id));
        }
    }
}

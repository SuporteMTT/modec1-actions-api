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
        public dynamic GetAsync(
            [FromQuery] string metadataId = null,
            [FromQuery] MetadataTypeEnum? metadataType = null
        )
        {
            return new[]
            {
                new {
                    id = "896765e8-4428-444f-aa5f-93024fd8c373",
                    createdDate = DateTime.Now,
                    name = "Risk 1",
                    riskLevel = RiskLevelEnum.High_Risk_High_Impact_X_High_Probability,
                    cause = "cause sample text",
                    owner = "Owner Name",
                    notInitiated = 5,
                    onGoing = 2,
                    concluded = 3,
                    delayed = 4,
                    status = StatusEnum.Active.Status(),
                    closedCancelledDate = DateTime.Now,
                },
                new {
                    id = "1f184b26-34b5-4b6b-8dba-b0ae9116f751",
                    createdDate = DateTime.Now,
                    name = "Risk 2",
                    riskLevel = RiskLevelEnum.High_Risk_High_Impact_X_High_Probability,
                    cause = "cause sample text",
                    owner = "Owner Name",
                    notInitiated = 2,
                    onGoing = 1,
                    concluded = 6,
                    delayed = 4,
                    status = StatusEnum.Cancelled.Status(),
                    closedCancelledDate = DateTime.Now,
                },
                new {
                    id = "b3ba6c94-235c-4500-9868-84f8c3c62539",
                    createdDate = DateTime.Now,
                    name = "Risk 3",
                    riskLevel = RiskLevelEnum.Low_Risk_Medium_Impact_X_Very_Low_Probability,
                    cause = "cause sample text",
                    owner = "Owner Name",
                    notInitiated = 5,
                    onGoing = 2,
                    concluded = 3,
                    delayed = 4,
                    status = StatusEnum.Active.Status(),
                    closedCancelledDate = DateTime.Now,
                },
                new {
                    id = "c5bc25d8-b24b-494c-a20b-92037e3e7271",
                    createdDate = DateTime.Now,
                    name = "Risk 4",
                    riskLevel = RiskLevelEnum.Low_Risk_Medium_Impact_X_Very_Low_Probability,
                    cause = "cause sample text",
                    owner = "Owner Name",
                    notInitiated = 5,
                    onGoing = 2,
                    concluded = 3,
                    delayed = 4,
                    status = StatusEnum.Active.Status(),
                    closedCancelledDate = DateTime.Now,
                },
                new {
                    id = "efe697e1-2ce3-4270-9f06-646c9f0469ec",
                    createdDate = DateTime.Now,
                    name = "Risk 5",
                    riskLevel = RiskLevelEnum.Low_Risk_Medium_Impact_X_Very_Low_Probability,
                    cause = "cause sample text",
                    owner = "Owner Name",
                    notInitiated = 5,
                    onGoing = 2,
                    concluded = 3,
                    delayed = 4,
                    status = StatusEnum.Concluded.Status(),
                    closedCancelledDate = DateTime.Now,
                }
            };
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
    }
}

using Actions.Core.Domain.Actions.Commands;
using Actions.Core.Domain.Actions.Dtos;
using Actions.Core.Domain.Actions.Enums;
using Actions.Core.Domain.Actions.Handlers;
using Actions.Core.Domain.Actions.Queries;
using Actions.Core.Domain.Deviations.Enums;
using Actions.Core.Domain.Risks.Enums;
using Actions.Core.Domain.Shared.Dtos;
using Actions.Core.Domain.Shared.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.CrossCutting.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Actions.Api.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "Action")]
    [Route("api/v{version:apiVersion}/action")]    
    public class ActionController : ControllerBase
    {
        /// <summary>
        /// Retrieve the list of actions
        /// </summary>
        /// <response code="200">If it is successful</response>
        /// <response code="401">If has no access</response>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/v1/action
        ///
        /// </remarks>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ICollection<ActionListDto>> GetActionsAsync(            
            [FromServices] ActionsQueryHandler handler,
            [FromQuery] string metadataId,
            [FromQuery] MetadataTypeEnum metadataType,
            [FromQuery] int page = 1,
            [FromQuery] int count = 10
        )
        {
            var actions = await handler.Handle(new ListActionsQuery(metadataId, metadataType, page, count));

            this.Response.Headers.Add("X-Total-Count", actions.Total.ToString());

            return actions.Data;
        }

        #region lists of selects in form
        /// <summary>
        /// Retrieves the list of action status
        /// </summary>
        /// <response code="200">If it is successful</response>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/v1/action/status
        ///
        /// </remarks>
        [HttpGet]
        [Route("status")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public dynamic GetActionStatus()
        {
            return Enum.GetValues(typeof(ActionStatusEnum))
                .Cast<Enum>()
                .Select(item => EnumExtensions.GetAttibutesDetails(item))
                .OrderBy(x => x.order)
                .ToList()
                ;
        }

        /// <summary>
        /// Retrieves the list of shared status
        /// </summary>
        /// <response code="200">If it is successful</response>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/v1/action/status
        ///
        /// </remarks>
        [HttpGet]
        [Route("shared-status")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public dynamic GetStatus()
        {
            return Enum.GetValues(typeof(StatusEnum))
                .Cast<Enum>()
                .Select(item => EnumExtensions.GetAttibutesDetails(item))
                .OrderBy(x => x.order)
                .ToList()
                ;
        }

        /// <summary>
        /// Retrieves the list of risk category
        /// </summary>
        /// <response code="200">If it is successful</response>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/v1/action/risk-category
        ///
        /// </remarks>
        [HttpGet]
        [Route("risk-category")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public dynamic GetRiskCategory()
        {
            return EnumExtensions.ToJson<RiskCategoryEnum>();
        }

        /// <summary>
        /// Retrieves the list of priorities
        /// </summary>
        /// <response code="200">If it is successful</response>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/v1/action/priority
        ///
        /// </remarks>
        [HttpGet]
        [Route("priority")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public dynamic GetPriority()
        {
            return EnumExtensions.ToJson<PriorityEnum>();
        }

        /// <summary>
        /// Retrieves the list of project step
        /// </summary>
        /// <response code="200">If it is successful</response>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/v1/action/project-step
        ///
        /// </remarks>
        [HttpGet]
        [Route("project-step")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public dynamic GetProjectStep()
        {
            return Enum.GetValues(typeof(ProjectStepEnum))
                .Cast<Enum>()
                .Select(item => EnumExtensions.GetAttibutesDetails(item))
                .OrderBy(x => x.order)
                .ToList()
                ;
        }

        /// <summary>
        /// Retrieves the list of dimension
        /// </summary>
        /// <response code="200">If it is successful</response>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/v1/action/dimension
        ///
        /// </remarks>
        [HttpGet]
        [Route("dimension")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public dynamic GetDimension()
        {
            return Enum.GetValues(typeof(DimensionEnum))
                .Cast<Enum>()
                .Select(item => EnumExtensions.GetAttibutesDetails(item))
                .OrderBy(x => x.order)
                .ToList()
                ;
        }
        #endregion

        /// <summary>
        /// Retrieve a action by id 
        /// </summary>
        /// <response code="200">If it is successful</response>
        /// <response code="401">If has no access</response>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/v1/action/{id}
        ///
        /// </remarks>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionDto> GetByIdAsync(
            [FromServices] ActionsQueryHandler handler,
            [FromRoute] string id)
        {
            return await handler.Handle(new GetActionByIdQuery(id));
        }

        /// <summary>
        /// Search risk(s) and deviation(s)
        /// </summary>
        /// <response code="200">If it is successful</response>
        /// <response code="401">If has no access</response>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/v1/action/riskdeviation/search/{search}/metadataId{metadataId}
        ///
        /// </remarks>
        [HttpGet("riskdeviation/search/{search}/metadataId/{metadataId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ICollection<ShortObjectDto>> GetByIdAsync(
            [FromServices] ActionsQueryHandler handler,
            [FromRoute] string search,
            [FromRoute] string metadataId)
        {
            return await handler.Handle(new GetDeviationAndRiskSearchQuery(search, metadataId));
        }

        /// <summary>
        /// Insert a action
        /// </summary>
        /// <response code="200">If it is successful</response>
        /// <response code="400">If invalid data is sent</response>
        /// <response code="401">If has no access</response>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/v1/action
        ///      
        /// </remarks>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionDto> PostAsync(
            [FromServices] ActionsCommandHandler handler,
            [FromBody] CreateActionCommand request
        )
        {
            return await handler.Handle(request);
        }

        /// <summary>
        /// Change a action
        /// </summary>
        /// <response code="200">If it is successful</response>
        /// <response code="400">If invalid data is sent</response>
        /// <response code="401">If has no access</response>
        /// <response code="404">If no data is found</response>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/v1/action
        ///
        /// </remarks>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        async public Task PutAsync(
            [FromServices] ActionsCommandHandler handler,
            [FromBody] UpdateActionCommand request
        )
        {
            await handler.Handle(request);
        }

        /// <summary>
        /// Delete action by id
        /// </summary>
        /// <response code="200">If it is successful</response>
        /// <response code="400">If invalid data is sent</response>
        /// <response code="401">If has no access</response>
        /// <remarks>
        /// Sample request:
        /// 
        ///     DELETE /api/v1/action/{id}
        ///      
        /// </remarks>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("{id}")]
        async public Task Delete(
            [FromServices] ActionsCommandHandler handler,
            [FromRoute] string id)
        {
            await handler.Handle(new DeleteActionCommand(id));
        }
    }
}

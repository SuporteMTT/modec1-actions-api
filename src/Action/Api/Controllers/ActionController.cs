using Actions.Core.Domain.Actions.Enums;
using Actions.Core.Domain.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.CrossCutting.Tools;
using System;

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
        public dynamic GetActionsAsync(
            [FromQuery] string originId = null,
            [FromQuery] OriginTypeEnum? originType = null
        )
        {
            return new[]
            {
                new {
                    id = GuidExtensions.GenerateGuid(),
                    createdDate = DateTime.Now,
                    description = "Action 2 Vestibulum varius volutpat nulla eu mollis.",
                    responsible = "Responsible Name",
                    dueDate = DateTime.Now,
                    status = StatusEnum.Active.Status(),
                    endDate = new DateTime?(new DateTime(2021, 6, 20))
                },
                new {
                    id = GuidExtensions.GenerateGuid(),
                    createdDate = DateTime.Now,
                    description = "Action 3 Vestibulum varius volutpat nulla eu mollis.​",
                    responsible = "Responsible Name",
                    dueDate = DateTime.Now,
                    status = StatusEnum.Cancelled.Status(),
                    endDate = new DateTime?(new DateTime(2021, 7, 17))
                },
                new {
                    id = GuidExtensions.GenerateGuid(),
                    createdDate = DateTime.Now,
                    description = "Action 4 Vestibulum varius volutpat nulla eu mollis.​",
                    responsible = "Responsible Name",
                    dueDate = DateTime.Now,
                    status = StatusEnum.Concluded.Status(),
                    endDate = new DateTime?()
                },
            };
        }

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
            return EnumExtensions.ToJson<ActionStatusEnum>();
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
            return EnumExtensions.ToJson<StatusEnum>();
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
            return EnumExtensions.ToJson<ProjectStepEnum>();
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
            return EnumExtensions.ToJson<DimensionEnum>();
        }
    }
}

using Actions.Core.Domain.Actions.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.CrossCutting.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Actions.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "Deviation")]
    [Route("api/v{version:apiVersion}/deviation")]    
    public class DeviationController : ControllerBase
    {
        /// <summary>
        /// Retrieve the list of deviation
        /// </summary>
        /// <response code="200">If it is successful</response>
        /// <response code="401">If has no access</response>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/v1/deviation
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
                    priority = "priority",
                    name = "Deviation 2 Vestibulum varius volutpat nulla eu mollis.",
                    cause = "cause text sample",
                    actions = new {
                        notInitiated =  5,
                        onGoing = 2,
                        concludedOrDelayed = 3
                    },
                    status = StatusEnum.Active,
                    closedCancelledDate = DateTime.Now,
                },
                new {
                    id = GuidExtensions.GenerateGuid(),
                    createdDate = DateTime.Now,
                    priority = "priority",
                    name = "Deviation 3 Vestibulum varius volutpat nulla eu mollis.​",
                    cause = "cause text sample",
                    actions = new {
                        notInitiated =  4,
                        onGoing = 1,
                        concludedOrDelayed = 5
                    },
                    status = StatusEnum.Concluded,
                    closedCancelledDate = DateTime.Now,
                },
                new {
                    id = GuidExtensions.GenerateGuid(),
                    createdDate = DateTime.Now,
                    priority = "priority",
                    name = "Deviation 4 Vestibulum varius volutpat nulla eu mollis.​",
                    cause = "cause text sample",
                    actions = new {
                        notInitiated =  5,
                        onGoing = 5,
                        concludedOrDelayed = 1
                    },
                    status = StatusEnum.Cancelled,
                    closedCancelledDate = DateTime.Now,
                },
            };
        }
    }
}

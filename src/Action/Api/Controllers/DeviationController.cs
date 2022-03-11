using Actions.Core.Domain.Actions.Enums;
using Actions.Core.Domain.Deviations.Enums;
using Actions.Core.Domain.Shared;
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
            [FromQuery] string metadataId = null,
            [FromQuery] MetadataTypeEnum? metadataType = null
        )
        {
            return new[]
            {
                new {
                    id = GuidExtensions.GenerateGuid(),
                    createdDate = DateTime.Now,
                    priority = PriorityEnum.High.Status(),
                    name = "Deviation 2 Vestibulum varius volutpat nulla eu mollis.",
                    cause = "cause text sample",
                    notInitiated = 5,
                    onGoing = 2,
                    concluded = 3,
                    delayed = 4,
                    status = StatusEnum.Active.Status(),
                    closedCancelledDate = DateTime.Now,
                },
                new {
                    id = GuidExtensions.GenerateGuid(),
                    createdDate = DateTime.Now,
                    priority = PriorityEnum.Medium.Status(),
                    name = "Deviation 3 Vestibulum varius volutpat nulla eu mollis.​",
                    cause = "cause text sample",
                    notInitiated = 4,
                    onGoing = 1,
                    concluded = 4,
                    delayed = 3,
                    status = StatusEnum.Concluded.Status(),
                    closedCancelledDate = DateTime.Now,
                },
                new {
                    id = GuidExtensions.GenerateGuid(),
                    createdDate = DateTime.Now,
                    priority = PriorityEnum.Low.Status(),
                    name = "Deviation 4 Vestibulum varius volutpat nulla eu mollis.​",
                    cause = "cause text sample",
                    notInitiated = 5,
                    onGoing = 5,
                    concluded = 1,
                    delayed = 3,
                    status = StatusEnum.Cancelled.Status(),
                    closedCancelledDate = DateTime.Now,
                },
            };
        }
    }
}

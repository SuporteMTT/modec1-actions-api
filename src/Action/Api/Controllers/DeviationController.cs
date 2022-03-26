using Actions.Core.Domain.Actions.Enums;
using Actions.Core.Domain.Deviations.Commands;
using Actions.Core.Domain.Deviations.Dtos;
using Actions.Core.Domain.Deviations.Enums;
using Actions.Core.Domain.Deviations.Handlers;
using Actions.Core.Domain.Deviations.Queries;
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
        public async Task<ICollection<DeviationListDto>> GetActionsAsync(
            [FromServices] DeviationsQueryHandler handler,
            [FromQuery] string metadataId,
            [FromQuery] MetadataTypeEnum metadataType,
            [FromQuery] int page = 1,
            [FromQuery] int count = 10
        )
        {

            var deviations = await handler.Handle(new ListDeviationsQuery(metadataId, metadataType, page, count));

            this.Response.Headers.Add("X-Total-Count", deviations.Total.ToString());

            return deviations.Data;
            // return new[]
            // {
            //     new {
            //         id = GuidExtensions.GenerateGuid(),
            //         createdDate = DateTime.Now,
            //         priority = PriorityEnum.High.Status(),
            //         name = "Deviation 2 Vestibulum varius volutpat nulla eu mollis.",
            //         cause = "cause text sample",
            //         notInitiated = 5,
            //         onGoing = 2,
            //         concluded = 3,
            //         delayed = 4,
            //         status = StatusEnum.Active.Status(),
            //         closedCancelledDate = DateTime.Now,
            //     },
            //     new {
            //         id = GuidExtensions.GenerateGuid(),
            //         createdDate = DateTime.Now,
            //         priority = PriorityEnum.Medium.Status(),
            //         name = "Deviation 3 Vestibulum varius volutpat nulla eu mollis.​",
            //         cause = "cause text sample",
            //         notInitiated = 4,
            //         onGoing = 1,
            //         concluded = 4,
            //         delayed = 3,
            //         status = StatusEnum.Concluded.Status(),
            //         closedCancelledDate = DateTime.Now,
            //     },
            //     new {
            //         id = GuidExtensions.GenerateGuid(),
            //         createdDate = DateTime.Now,
            //         priority = PriorityEnum.Low.Status(),
            //         name = "Deviation 4 Vestibulum varius volutpat nulla eu mollis.​",
            //         cause = "cause text sample",
            //         notInitiated = 5,
            //         onGoing = 5,
            //         concluded = 1,
            //         delayed = 3,
            //         status = StatusEnum.Cancelled.Status(),
            //         closedCancelledDate = DateTime.Now,
            //     },
            // };
        }

        /// <summary>
        /// Retrieve a deviation by id 
        /// </summary>
        /// <response code="200">If it is successful</response>
        /// <response code="401">If has no access</response>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/v1/deviation/{id}
        ///
        /// </remarks>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<DeviationDto> GetByIdAsync(
            [FromServices] DeviationsQueryHandler handler,
            [FromRoute] string id)
        {
            return await handler.Handle(new GetDeviationByIdQuery(id));
        }

        /// <summary>
        /// Insert a deviation
        /// </summary>
        /// <response code="200">If it is successful</response>
        /// <response code="400">If invalid data is sent</response>
        /// <response code="401">If has no access</response>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/v1/deviation
        ///      
        /// </remarks>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<DeviationDto> PostAsync(
            [FromServices] DeviationsCommandHandler handler,
            [FromBody] CreateDeviationCommand request
        )
        {
            return await handler.Handle(request);
        }

        /// <summary>
        /// Change a deviation
        /// </summary>
        /// <response code="200">If it is successful</response>
        /// <response code="400">If invalid data is sent</response>
        /// <response code="401">If has no access</response>
        /// <response code="404">If no data is found</response>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/v1/deviation
        ///
        /// </remarks>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        async public Task PutAsync(
            [FromServices] DeviationsCommandHandler handler,
            [FromBody] UpdateDeviationCommand request
        )
        {
            await handler.Handle(request);
        }

        /// <summary>
        /// Delete deviation by id
        /// </summary>
        /// <response code="200">If it is successful</response>
        /// <response code="400">If invalid data is sent</response>
        /// <response code="401">If has no access</response>
        /// <remarks>
        /// Sample request:
        /// 
        ///     DELETE /api/v1/deviation/{id}
        ///      
        /// </remarks>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("{id}")]
        async public Task Delete(
            [FromServices] DeviationsCommandHandler handler,
            [FromRoute] string id)
        {
            await handler.Handle(new DeleteDeviationCommand(id));
        }
    }
}

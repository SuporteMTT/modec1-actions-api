using Actions.Core.Domain.StatusHistories.Handlers;
using Actions.Core.Domain.StatusHistories.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Actions.Api.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "StatusHistory")]
    [Route("api/v{version:apiVersion}/statushistory")]    
    public class StatusHistoryController : ControllerBase
    {
        /// <summary>
        /// Retrieve the list of status history
        /// </summary>
        /// <response code="200">If it is successful</response>
        /// <response code="401">If has no access</response>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/v1/statushistory?page=[1]&amp;count=[20]
        ///
        /// </remarks>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public dynamic GetAsync(
            [FromServices] StatusHistoriesQueryHandler handler,
            int page = 1,
            int count = 10,
            string metadataId = null
        )
        {
            return handler.Handle(new GetStatusHistoriesQuery(metadataId, page, count));
        }
    }
}

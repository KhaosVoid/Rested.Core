using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Rested.Core.Data;
using System.Net.Mime;

namespace Rested.Core.Server.Mvc
{
    [ApiController]
    [RestedApiControllerRoute]
    public abstract class ProjectionController<TData, TProjection> : ControllerBase
        where TData : IData
        where TProjection : Projection
    {
        #region Members

        protected readonly IMediator _mediator;
        protected readonly IHttpContextAccessor _httpContext;
        protected readonly ILogger _logger;

        #endregion Members

        #region Ctor

        public ProjectionController(
            IMediator mediator,
            IHttpContextAccessor httpContext,
            ILoggerFactory loggerFactory)
        {
            _mediator = mediator;
            _httpContext = httpContext;
            _logger = loggerFactory?.CreateLogger(GetType());
        }

        #endregion Ctor

        #region Methods

        /// <summary>
        /// Gets a projection by Id.
        /// </summary>
        /// <param name="id">The Id of the projection to retrieve.</param>
        /// <returns></returns>
        [HttpGet, RestedGetProjectionRoute]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public abstract Task<ActionResult<TProjection>> GetProjection([FromRoute] Guid id);

        /// <summary>
        /// Gets all projections.
        /// </summary>
        /// <returns></returns>
        [HttpGet, RestedGetProjectionsRoute]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public abstract Task<ActionResult<List<TProjection>>> GetProjections();

        /// <summary>
        /// Performs a search using the information in the <see cref="SearchRequest"/> and returns the results.
        /// </summary>
        /// <param name="searchRequest">The <see cref="SearchRequest"/>.</param>
        [HttpPost, RestedSearchProjectionsRoute]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public abstract Task<ActionResult<SearchProjectionsResults<TProjection>>> SearchProjections([FromBody] SearchRequest searchRequest);

        #endregion Methods
    }
}

using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Rested.Core.Data;
using System.Net.Mime;

namespace Rested.Core.Controllers
{
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
        [HttpGet("[controller]/{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status401Unauthorized)]
        //[ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status403Forbidden)]
        //[ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        public abstract Task<ActionResult<TProjection>> GetProjection([FromRoute] Guid id);

        /// <summary>
        /// Gets all projections.
        /// </summary>
        /// <returns></returns>
        [HttpGet("[controller]s")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status401Unauthorized)]
        //[ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status403Forbidden)]
        //[ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        public abstract Task<ActionResult<List<TProjection>>> GetProjections();

        /// <summary>
        /// Performs a search using the information in the <see cref="SearchRequest"/> and returns the results.
        /// </summary>
        /// <param name="searchRequest">The <see cref="SearchRequest"/>.</param>
        [HttpPost("[controller]s/search")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status401Unauthorized)]
        //[ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status403Forbidden)]
        //[ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        public abstract Task<ActionResult<SearchProjectionsResults<TData, TProjection>>> SearchProjections([FromBody] SearchRequest searchRequest);

        #endregion Methods
    }
}

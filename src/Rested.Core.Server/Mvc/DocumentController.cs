using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Rested.Core.Data;
using Rested.Core.Server.Http;
using System.Net.Mime;
using Rested.Core.Data.Document;
using Rested.Core.Data.Dto;
using Rested.Core.Data.Search;

namespace Rested.Core.Server.Mvc;

[ApiController]
[RestedApiControllerRoute]
public abstract class DocumentController<TData> : ControllerBase where TData : IData
{
    #region Members

    protected readonly IMediator _mediator;
    protected readonly IHttpContextAccessor _httpContext;
    protected readonly ILogger _logger;

    #endregion Members

    #region Ctor

    public DocumentController(
        IMediator mediator,
        IHttpContextAccessor httpContext,
        ILoggerFactory loggerFactory)
    {
        _mediator = mediator;
        _httpContext = httpContext;
        _logger = loggerFactory.CreateLogger(GetType());
    }

    #endregion Ctor

    #region Methods

    /// <summary>
    /// Gets a Document by Id.
    /// </summary>
    /// <param name="id">The Id of the Document to retrieve.</param>
    [HttpGet, RestedGetDocumentRoute]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public abstract Task<ActionResult<IDocument<TData>>> GetDocument([FromRoute] Guid id);

    /// <summary>
    /// Gets all Documents.
    /// </summary>
    [HttpGet, RestedGetDocumentsRoute]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public abstract Task<ActionResult<List<IDocument<TData>>>> GetDocuments();

    /// <summary>
    /// Performs a search using the information in the <see cref="SearchRequest"/> and returns the results.
    /// </summary>
    /// <param name="searchRequest">The <see cref="SearchRequest"/>.</param>
    [HttpPost, RestedSearchDocumentsRoute]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public abstract Task<ActionResult<SearchDocumentsResults<TData, IDocument<TData>>>> SearchDocuments([FromBody] SearchRequest searchRequest);

    /// <summary>
    /// Inserts a Document.
    /// </summary>
    /// <param name="data">The data to create and insert a Document with.</param>
    /// <returns></returns>
    [HttpPost, RestedInsertDocumentRoute]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public abstract Task<ActionResult<IDocument<TData>>> InsertDocument([FromBody] TData data);

    /// <summary>
    /// Inserts multiple Documents.
    /// </summary>
    /// <param name="datas">The list of data to create and insert multiple Documents with.</param>
    [HttpPost, RestedInsertMultipleDocumentsRoute]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public abstract Task<ActionResult<List<IDocument<TData>>>> InsertMultipleDocuments([FromBody] List<TData> datas);

    /// <summary>
    /// Updates a Document.
    /// </summary>
    /// <param name="id">The Id of the Document.</param>
    /// <param name="etag">The ETag of the Document.</param>
    /// <param name="data">The data to update the Document with.</param>
    [HttpPut, RestedUpdateDocumentRoute]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public abstract Task<ActionResult<IDocument<TData>>> UpdateDocument(
        [FromRoute] Guid id,
        [FromHeader] IfMatchByteArray etag,
        [FromBody] TData data);

    /// <summary>
    /// Updates multiple Documents.
    /// </summary>
    /// <param name="dtos">The list of dtos to update multiple Documents with.</param>
    [HttpPut, RestedUpdateMultipleDocumentsRoute]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public abstract Task<ActionResult<List<IDocument<TData>>>> UpdateMultipleDocuments([FromBody] List<Dto<TData>> dtos);

    /// <summary>
    /// Patches a Document.
    /// </summary>
    /// <param name="id">The Id of the Document.</param>
    /// <param name="etag">The ETag of the Document.</param>
    /// <param name="data">The data to patch the Document with.</param>
    [HttpPatch, RestedPatchDocumentRoute]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public abstract Task<ActionResult<IDocument<TData>>> PatchDocument(
        [FromRoute] Guid id,
        [FromHeader] IfMatchByteArray etag,
        [FromBody] TData data);

    /// <summary>
    /// Patches multiple Documents.
    /// </summary>
    /// <param name="dtos">The list of dtos to patch multiple Documents with.</param>
    [HttpPatch, RestedPatchMultipleDocumentsRoute]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public abstract Task<ActionResult<List<IDocument<TData>>>> PatchMultipleDocuments([FromBody] List<Dto<TData>> dtos);

    /// <summary>
    /// Prunes a Document.
    /// </summary>
    /// <param name="id">The Id of the Document.</param>
    /// <param name="etag">The ETag of the Document.</param>
    /// <param name="data">The data to prune from the Document.</param>
    [HttpPost, RestedPruneDocumentRoute]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public abstract Task<ActionResult<IDocument<TData>>> PruneDocument(
        [FromRoute] Guid id,
        [FromHeader] IfMatchByteArray etag,
        [FromBody] TData data);

    /// <summary>
    /// Prunes multiple Documents.
    /// </summary>
    /// <param name="dtos">The list of dtos to prune multiple Documents with.</param>
    [HttpPost, RestedPruneMultipleDocumentsRoute]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public abstract Task<ActionResult<List<IDocument<TData>>>> PruneMultipleDocuments([FromBody] List<Dto<TData>> dtos);

    /// <summary>
    /// Deletes a Document.
    /// </summary>
    /// <param name="id">The Id of the Document.</param>
    /// <param name="etag">The ETag of the Document.</param>
    [HttpDelete, RestedDeleteDocumentRoute]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public abstract Task<NoContentResult> DeleteDocument(
        [FromRoute] Guid id,
        [FromHeader] IfMatchByteArray etag);

    /// <summary>
    /// Deletes multiple Documents.
    /// </summary>
    /// <param name="baseDtos">The list of base dtos to delete multiple Documents with.</param>
    [HttpPost, RestedDeleteMultipleDocumentsRoute]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public abstract Task<NoContentResult> DeleteMultipleDocuments([FromBody] List<BaseDto> baseDtos);

    #endregion Methods
}
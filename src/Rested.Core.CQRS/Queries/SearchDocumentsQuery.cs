using Microsoft.Extensions.Logging;
using Rested.Core.CQRS.Data;

namespace Rested.Core.CQRS.Queries
{
    public abstract class SearchDocumentsQuery<TData, TDocument> :
        SearchQuery<TDocument, SearchDocumentsResults<TData, TDocument>>
        where TData : IData
        where TDocument : IDocument<TData>
    {
        #region Ctor

        public SearchDocumentsQuery(SearchRequest searchRequest, int minPageSize = 1, int maxPageSize = 100, int defaultPageSize = 25) :
            base(searchRequest, minPageSize, maxPageSize, defaultPageSize)
        {

        }

        #endregion Ctor
    }

    public abstract class SearchDocumentsQueryValidator<TData, TDocument, TSearchDocumentsQuery> :
        SearchQueryValidator<TDocument, SearchDocumentsResults<TData, TDocument>, TSearchDocumentsQuery>
        where TData : IData
        where TDocument : IDocument<TData>
        where TSearchDocumentsQuery : SearchDocumentsQuery<TData, TDocument>
    {
        #region Ctor

        public SearchDocumentsQueryValidator() : base()
        {

        }

        #endregion Ctor
    }

    public abstract class SearchDocumentsQueryHandler<TData, TDocument, TSearchDocumentsQuery> :
        SearchQueryHandler<TDocument, SearchDocumentsResults<TData, TDocument>, TSearchDocumentsQuery>
        where TData : IData
        where TDocument : IDocument<TData>
        where TSearchDocumentsQuery : SearchDocumentsQuery<TData, TDocument>
    {
        #region Ctor

        public SearchDocumentsQueryHandler(ILoggerFactory loggerFactory) : base(loggerFactory)
        {

        }

        #endregion Ctor
    }
}

using Microsoft.Extensions.Logging;
using Rested.Core.CQRS.Data;

namespace Rested.Core.CQRS.Queries
{
    public abstract class SearchProjectionsQuery<TData, TProjection> :
        SearchQuery<TProjection, SearchProjectionsResults<TData, TProjection>>
        where TData : IData
        where TProjection : Projection
    {
        #region Ctor

        public SearchProjectionsQuery(SearchRequest searchRequest, int minPageSize = 1, int maxPageSize = 100, int defaultPageSize = 25) :
            base(searchRequest, minPageSize, maxPageSize, defaultPageSize)
        {

        }

        #endregion Ctor
    }

    public abstract class SearchProjectionsQueryValidator<TData, TProjection, TSearchProjectionsQuery> :
        SearchQueryValidator<TProjection, SearchProjectionsResults<TData, TProjection>, TSearchProjectionsQuery>
        where TData : IData
        where TProjection : Projection
        where TSearchProjectionsQuery : SearchProjectionsQuery<TData, TProjection>
    {
        #region Ctor

        public SearchProjectionsQueryValidator() : base()
        {

        }

        #endregion Ctor
    }

    public abstract class SearchProjectionsQueryHandler<TData, TProjection, TSearchProjectionsQuery> :
        SearchQueryHandler<TProjection, SearchProjectionsResults<TData, TProjection>, TSearchProjectionsQuery>
        where TData : IData
        where TProjection : Projection
        where TSearchProjectionsQuery : SearchProjectionsQuery<TData, TProjection>
    {
        #region Ctor

        public SearchProjectionsQueryHandler(ILoggerFactory loggerFactory) : base(loggerFactory)
        {

        }

        #endregion Ctor
    }
}

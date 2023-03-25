namespace Rested.Core.CQRS.Data
{
    public class SearchProjectionsResults<TData, TProjection> : SearchResults<TProjection>
        where TData : IData
        where TProjection : Projection
    {
        #region Ctor

        public SearchProjectionsResults(SearchRequest searchRequest) : base(searchRequest)
        {

        }

        #endregion Ctor
    }
}

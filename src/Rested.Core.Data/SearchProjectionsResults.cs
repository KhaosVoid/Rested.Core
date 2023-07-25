namespace Rested.Core.Data
{
    public class SearchProjectionsResults<TProjection> :
        SearchResults<TProjection>
        where TProjection : Projection
    {
        #region Ctor

        public SearchProjectionsResults(SearchRequest searchRequest) : base(searchRequest)
        {

        }

        #endregion Ctor
    }
}

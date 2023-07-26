namespace Rested.Core.Data
{
    public record SearchProjectionsResults<TProjection> :
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

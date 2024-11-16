namespace Rested.Core.Data.Search;

public record SearchProjectionsResults<TProjection> :
    SearchResults<TProjection>
    where TProjection : Projection.Projection
{
    #region Ctor

    public SearchProjectionsResults(SearchRequest searchRequest) : base(searchRequest)
    {

    }

    #endregion Ctor
}
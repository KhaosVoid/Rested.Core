﻿namespace Rested.Core.Data
{
    public record SearchDocumentsResults<TData, TDocument> : SearchResults<TDocument>
        where TData : IData
        where TDocument : IDocument<TData>
    {
        #region Ctor

        public SearchDocumentsResults(SearchRequest searchRequest) : base(searchRequest)
        {

        }

        #endregion Ctor
    }
}

using Rested.Core.CQRS.Data;
using System.Net.Http.Json;

namespace Rested.Core.CQRS.Services
{
    public abstract class DocumentService<TData, TDocument> : IDocumentService<TData, TDocument>
        where TData : IData
        where TDocument : IDocument<TData>
    {
        #region Members

        protected readonly HttpClient _httpClient;

        #endregion Members

        #region Ctor

        public DocumentService(HttpClient httpClient)
        {
            _httpClient = httpClient;

            _httpClient.BaseAddress = new Uri(GetBaseApiRoute());
        }

        #endregion Ctor

        #region Methods

        protected abstract string GetBaseApiRoute();

        public virtual async Task<TDocument> GetDocument(Guid id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<TDocument>($"{typeof(TData).Name}/{id}");
            }
            catch { throw; }
        }

        public virtual async Task<List<TDocument>> GetDocuments()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<TDocument>>($"{typeof(TData).Name}s");
            }
            catch { throw; }
        }

        public virtual async Task<SearchDocumentsResults<TData, TDocument>> SearchDocuments(SearchRequest searchRequest)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(
                    requestUri: $"{typeof(TData).Name}s/search",
                    value: searchRequest);

                return await response.Content.ReadFromJsonAsync<SearchDocumentsResults<TData, TDocument>>();
            }
            catch { throw; }
        }

        public virtual async Task<TDocument> InsertDocument(TData data)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(
                    requestUri: $"{typeof(TData).Name}",
                    value: data);

                return await response.Content.ReadFromJsonAsync<TDocument>();
            }
            catch { throw; }
        }

        public virtual async Task<List<TDocument>> InsertMultipleDocuments(List<TData> datas)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(
                    requestUri: $"{typeof(TData).Name}s",
                    value: datas);

                return await response.Content.ReadFromJsonAsync<List<TDocument>>();
            }
            catch { throw; }
        }

        public virtual async Task<TDocument> UpdateDocument(Guid id, byte[] etag, TData data)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Add(
                    name: "If-Match",
                    value: Convert.ToBase64String(etag));

                var response = await _httpClient.PutAsJsonAsync(
                    requestUri: $"{typeof(TData).Name}/{id}",
                    value: data);

                return await response.Content.ReadFromJsonAsync<TDocument>();
            }
            catch { throw; }
        }

        public virtual async Task<List<TDocument>> UpdateMultipleDocuments(List<Dto<TData>> dtos)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync(
                    requestUri: $"{typeof(TData).Name}s",
                    value: dtos);

                return await response.Content.ReadFromJsonAsync<List<TDocument>>();
            }
            catch { throw; }
        }

        public virtual async Task<TDocument> PatchDocument(Guid id, byte[] etag, TData data)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Add(
                    name: "If-Match",
                    value: Convert.ToBase64String(etag));

                var response = await _httpClient.PatchAsJsonAsync(
                    requestUri: $"{typeof(TData).Name}/{id}",
                    value: data);

                return await response.Content.ReadFromJsonAsync<TDocument>();
            }
            catch { throw; }
        }

        public virtual async Task<List<TDocument>> PatchMultipleDocuments(List<Dto<TData>> dtos)
        {
            try
            {
                var response = await _httpClient.PatchAsJsonAsync(
                    requestUri: $"{typeof(TData).Name}s",
                    value: dtos);

                return await response.Content.ReadFromJsonAsync<List<TDocument>>();
            }
            catch { throw; }
        }

        public virtual async Task DeleteDocument(Guid id, byte[] etag)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Add(
                    name: "If-Match",
                    value: Convert.ToBase64String(etag));

                await _httpClient.DeleteAsync($"{typeof(TData).Name}/{id}");
            }
            catch { throw; }
        }

        public virtual async Task DeleteMultipleDocuments(List<BaseDto> baseDtos)
        {
            try
            {
                await _httpClient.PostAsJsonAsync(
                    requestUri: $"{typeof(TData).Name}s/delete",
                    value: baseDtos);
            }
            catch { throw; }
        }

        #endregion Methods
    }
}

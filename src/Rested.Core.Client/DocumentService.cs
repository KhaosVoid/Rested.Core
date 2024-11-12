using Rested.Core.Data;
using System.Net.Http.Json;
using Rested.Core.Data.Document;
using Rested.Core.Data.Dto;
using Rested.Core.Data.Search;

namespace Rested.Core.Client
{
    public abstract class DocumentService<TData> : IDocumentService<TData> where TData : IData
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

        public virtual async Task<IDocument<TData>> GetDocument(Guid id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<IDocument<TData>>($"{typeof(TData).Name}/{id}");
            }
            catch { throw; }
        }

        public virtual async Task<List<IDocument<TData>>> GetDocuments()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<IDocument<TData>>>($"{typeof(TData).Name}s");
            }
            catch { throw; }
        }

        public virtual async Task<SearchDocumentsResults<TData, IDocument<TData>>> SearchDocuments(SearchRequest searchRequest)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(
                    requestUri: $"{typeof(TData).Name}s/search",
                    value: searchRequest);

                return await response.Content.ReadFromJsonAsync<SearchDocumentsResults<TData, IDocument<TData>>>();
            }
            catch { throw; }
        }

        public virtual async Task<IDocument<TData>> InsertDocument(TData data)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(
                    requestUri: $"{typeof(TData).Name}",
                    value: data);

                return await response.Content.ReadFromJsonAsync<IDocument<TData>>();
            }
            catch { throw; }
        }

        public virtual async Task<List<IDocument<TData>>> InsertMultipleDocuments(List<TData> datas)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(
                    requestUri: $"{typeof(TData).Name}s",
                    value: datas);

                return await response.Content.ReadFromJsonAsync<List<IDocument<TData>>>();
            }
            catch { throw; }
        }

        public virtual async Task<IDocument<TData>> UpdateDocument(Guid id, byte[] etag, TData data)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Add(
                    name: "If-Match",
                    value: Convert.ToBase64String(etag));

                var response = await _httpClient.PutAsJsonAsync(
                    requestUri: $"{typeof(TData).Name}/{id}",
                    value: data);

                return await response.Content.ReadFromJsonAsync<IDocument<TData>>();
            }
            catch { throw; }
        }

        public virtual async Task<List<IDocument<TData>>> UpdateMultipleDocuments(List<Dto<TData>> dtos)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync(
                    requestUri: $"{typeof(TData).Name}s",
                    value: dtos);

                return await response.Content.ReadFromJsonAsync<List<IDocument<TData>>>();
            }
            catch { throw; }
        }

        public virtual async Task<IDocument<TData>> PatchDocument(Guid id, byte[] etag, TData data)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Add(
                    name: "If-Match",
                    value: Convert.ToBase64String(etag));

                var response = await _httpClient.PatchAsJsonAsync(
                    requestUri: $"{typeof(TData).Name}/{id}",
                    value: data);

                return await response.Content.ReadFromJsonAsync<IDocument<TData>>();
            }
            catch { throw; }
        }

        public virtual async Task<List<IDocument<TData>>> PatchMultipleDocuments(List<Dto<TData>> dtos)
        {
            try
            {
                var response = await _httpClient.PatchAsJsonAsync(
                    requestUri: $"{typeof(TData).Name}s",
                    value: dtos);

                return await response.Content.ReadFromJsonAsync<List<IDocument<TData>>>();
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

using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace CruiseBookingApp.Services.Request
{
    public class RequestService : IRequestService
    {
        readonly JsonSerializerSettings _serializerSettings;

        public RequestService()
        {
            _serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                NullValueHandling = NullValueHandling.Ignore
            };

            _serializerSettings.Converters.Add(new StringEnumConverter());
        }

        public Task<TResult> GetAsync<TResult>(string uri, string token = "")
        {
            throw new NotImplementedException();
        }

        public Task<TResult> PostAsync<TResult>(string uri, TResult data, string token = "")
        {
            return PostAsync<TResult, TResult>(uri, data, token);
        }

        public Task<TResult> PostAsync<TRequest, TResult>(string uri, TRequest data, string token = "")
        {
            throw new NotImplementedException();
        }

        public Task<TResult> PutAsync<TResult>(string uri, TResult data, string token = "")
        {
            return PutAsync<TResult, TResult>(uri, data, token);
        }

        public Task<TResult> PutAsync<TRequest, TResult>(string uri, TRequest data, string token = "")
        {
            throw new NotImplementedException();
        }

        HttpClient CreateHttpClient(string token = "")
        {
            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (!string.IsNullOrEmpty(token))
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return httpClient;
        }

        async Task HandleResponse(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == HttpStatusCode.Forbidden || response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new Exception(content);
                }

                throw new HttpRequestException(content);
            }
        }
    }
}

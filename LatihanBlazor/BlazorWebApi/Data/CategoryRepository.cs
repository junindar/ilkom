using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using BlazorWebApi.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;


namespace BlazorWebApi.Data
{
    public class CategoryRepository: ICategoryRepository
    {
        public HttpClient _httpClient { get; }
        public AppSettings _appSettings { get; }
        public CategoryRepository(HttpClient httpClient
            , IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;


            httpClient.BaseAddress = new Uri(_appSettings.PustakaBaseAddress);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));


            _httpClient = httpClient;
        }
        public async Task<List<Category>> GetAll()
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "categories");
            var response = await _httpClient.SendAsync(requestMessage);

            var responseStatusCode = response.StatusCode;

            if (responseStatusCode.ToString() == "OK")
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                return await Task.FromResult(JsonConvert.DeserializeObject<List<Category>>(responseBody));
            }

            return null;

        }

        public async Task<Category> GetById(int categoryId)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"categories/{categoryId}");
            var response = await _httpClient.SendAsync(requestMessage);


            var responseStatusCode = response.StatusCode;

            if (responseStatusCode.ToString() == "OK")
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                return await Task.FromResult(JsonConvert.DeserializeObject<Category>(responseBody));
            }

            return null;
        }
    }
}

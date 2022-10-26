using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MudBlazor.Input.Data.ViewModel;
using Newtonsoft.Json;

namespace MudBlazor.Input.Data
{
    public class ProvinsiRepository : IProvinsiRepository
    {

        public HttpClient _httpClient { get; }
      
        public ProvinsiRepository(HttpClient httpClient)
        {
 

            httpClient.BaseAddress = new Uri("http://www.emsifa.com/api-wilayah-indonesia/api/");
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));


            _httpClient = httpClient;
        }

      

        public async Task<IEnumerable<Provinsi>> GetAll()
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "provinces.json");
            var response = await _httpClient.SendAsync(requestMessage);


            var responseStatusCode = response.StatusCode;

            if (responseStatusCode.ToString() == "OK")
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                return await Task.FromResult(JsonConvert.DeserializeObject<List<Provinsi>>(responseBody));
            }

            return null;
        }

      


    }
}

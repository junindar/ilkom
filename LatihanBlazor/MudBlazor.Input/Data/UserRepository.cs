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
    public class UserRepository : IUserRepository
    {

        public HttpClient _httpClient { get; }
      
        public UserRepository(HttpClient httpClient)
        {
 

            httpClient.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));


            _httpClient = httpClient;
        }

      

        public async Task<IEnumerable<User>> GetAll()
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "users");
            var response = await _httpClient.SendAsync(requestMessage);


            var responseStatusCode = response.StatusCode;

            if (responseStatusCode.ToString() == "OK")
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                return await Task.FromResult(JsonConvert.DeserializeObject<List<User>>(responseBody));
            }

            return null;
        }

      


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using BlazorWebApi.Data;
using BlazorWebApi.Models;

using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace BlazorWebApi.Data
{
    public class BookRepository : IBookRepository
    {

        public HttpClient _httpClient { get; }
        public AppSettings _appSettings { get; }
        public BookRepository(HttpClient httpClient
            , IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;


            httpClient.BaseAddress = new Uri(_appSettings.PustakaBaseAddress);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));


            _httpClient = httpClient;
        }

        public async Task Insert(Book book)
        {
            string serializedUser = JsonConvert.SerializeObject(book);
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "books");
            requestMessage.Content = new StringContent(serializedUser);

            requestMessage.Content.Headers.ContentType
                = new MediaTypeHeaderValue("application/json");

            var response = await _httpClient.SendAsync(requestMessage);


            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Sorry, something went wrong");

            }


        }

        public async Task Update(Book book)
        {
            string serializedUser = JsonConvert.SerializeObject(book);
            var requestMessage = new HttpRequestMessage(HttpMethod.Put, "books");
            requestMessage.Content = new StringContent(serializedUser);

            requestMessage.Content.Headers.ContentType
                = new MediaTypeHeaderValue("application/json");

            var response = await _httpClient.SendAsync(requestMessage);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Sorry, something went wrong");

            }
        }

        public async Task Delete(int bookId)
        {

            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, $"books/{bookId}");




            var response = await _httpClient.SendAsync(requestMessage);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Sorry, something went wrong");

            }
        }

        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "books");
            var response = await _httpClient.SendAsync(requestMessage);


            var responseStatusCode = response.StatusCode;

            if (responseStatusCode.ToString() == "OK")
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                return await Task.FromResult(JsonConvert.DeserializeObject<List<Book>>(responseBody));
            }

            return null;
        }

        public async Task<Book> GetBookById(int bookId)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"books/{bookId}");
            var response = await _httpClient.SendAsync(requestMessage);


            var responseStatusCode = response.StatusCode;

            if (responseStatusCode.ToString() == "OK")
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                return await Task.FromResult(JsonConvert.DeserializeObject<Book>(responseBody));
            }

            return null;
        }


    }
}

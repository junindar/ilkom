using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorWebApi.Data;
using BlazorWebApi.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorWebApi.Pages
{
    public class BookListBase : ComponentBase
    {
        [Inject]
        public IBookRepository BookRepository { get; set; }

        public IEnumerable<Book> Books { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Books = (await BookRepository.GetAllBooks()).ToList();
        }
    }
}

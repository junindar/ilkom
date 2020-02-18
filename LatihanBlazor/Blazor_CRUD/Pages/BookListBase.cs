using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazor_CRUD.Data;
using Blazor_CRUD.Entity;
using Microsoft.AspNetCore.Components;

namespace Blazor_CRUD.Pages
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

using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using BlazorWebApi.Data;
using BlazorWebApi.Models;

namespace BlazorWebApi.Pages
{
    public class BookDetailBase : ComponentBase
    {
        [Parameter]
        public string BookId { get; set; }
        public Book Book { get; set; } = new Book();
        [Inject]
        public IBookRepository BookRepository { get; set; }
        [Inject]
        public ICategoryRepository CategoryRepository { get; set; }
        protected string NamaCategory = string.Empty;
        protected override async Task OnInitializedAsync()
        {
            Book = await BookRepository.GetBookById(int.Parse(BookId));
            var cat = await CategoryRepository.GetById(Book.CategoryID);
            NamaCategory = cat.Nama;
        }
    }
}

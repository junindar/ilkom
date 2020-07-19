using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using BlazorAPP_Login.Data;
using BlazorAPP_Login.Entity;

namespace BlazorApp_Login.Pages
{
    public class BookDetailBase : ComponentBase
    {
        [Parameter]
        public string BookId { get; set; }
        public Book Book { get; set; } = new Book();
        [Inject]
        public IBookRepository BookRepository { get; set; }
        protected string NamaCategory = string.Empty;
        protected override async Task OnInitializedAsync()
        {
            Book = await BookRepository.GetBookById(int.Parse(BookId));
            NamaCategory = Book.Category.NamaCategory;
        }
    }
}

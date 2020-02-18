using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Blazor_CRUD.Data;
using Blazor_CRUD.Entity;
namespace Blazor_CRUD.Pages
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

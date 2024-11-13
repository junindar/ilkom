
using Latihan2.Entity;
using Latihan2.Service;
using Microsoft.AspNetCore.Components;

namespace Latihan2.Components.Pages
{
    public class BookList2Base: ComponentBase
    {
        [Inject]
        public IBookService BookService { get; set; }
        protected override async  Task OnInitializedAsync()
        {
            Books =  await BookService.GetBooksAsync();
             await base.OnInitializedAsync();
        }
        public IEnumerable<Book?>? Books { get; set; }
    }
}

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using BlazorAPP_Login.Data;
using BlazorAPP_Login.Entity;
using Microsoft.AspNetCore.Components;

namespace BlazorApp_Login.Components
{
    public class CardBookBase : ComponentBase
    {
        public string PanelText { get; set; }
        [Parameter]
        public string CategoryName { get; set; }

        [Inject]
        public IBookRepository BookRepository { get; set; }
        public IEnumerable<Book> Books { get; set; }

        protected override async Task OnInitializedAsync()
        {
            PanelText = "DAFTAR BUKU : TEKNOLOGI";
            Books = (await BookRepository.GetRandomBooks(CategoryName)).ToList();
        }

        public async void ShowBookCategoryAsync(string cat)
        {
            PanelText = $"DAFTAR BUKU : {cat.ToUpper()}";
            Books = (await BookRepository.GetRandomBooks(cat)).ToList();
            StateHasChanged();
        }

    }
}

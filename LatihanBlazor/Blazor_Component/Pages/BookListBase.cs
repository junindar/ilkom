using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazor_Component.Components;
using Blazor_Component.Data;
using Blazor_Component.Entity;
using Microsoft.AspNetCore.Components;

namespace Blazor_Component.Pages
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
        protected AddEditBookDialog AddEditBookDialog { get; set; }
        public async void AddEditBookDialog_OnDialogClose()
        {
            Books = (await BookRepository.GetAllBooks()).ToList();
            StateHasChanged();
        }

        public void AddEditBookShow(int bookid)
        {
            AddEditBookDialog.Show(bookid);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorApp_Login.Components;
using BlazorAPP_Login.Data;
using BlazorAPP_Login.Entity;
using Microsoft.AspNetCore.Components;

namespace BlazorApp_Login.Pages
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

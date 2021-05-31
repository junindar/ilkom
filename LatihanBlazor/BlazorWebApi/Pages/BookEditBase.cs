using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using BlazorInputFile;
using BlazorWebApi.Data;
using BlazorWebApi.Models;
using Microsoft.AspNetCore.Components;


namespace BlazorWebApi.Pages
{
    public class BookEditBase : ComponentBase
    {
        [Inject]
        public IFileUpload fileUpload { get; set; }
        [Inject]
        public IBookRepository BookRepository { get; set; }

        [Inject]
        public ICategoryRepository CategoryRepository { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Parameter]
        public string BookId { get; set; }

        public Book Book { get; set; } = new Book();
        public List<Category> Categories { get; set; } = new List<Category>();

        protected string CategoryId = string.Empty;
        protected IFileListEntry file;
        protected MemoryStream fs;
        protected string ImageData = string.Empty;


        protected override async Task OnInitializedAsync()
        {

            Categories = (await CategoryRepository.GetAll()).ToList();

            int.TryParse(BookId, out var bookId);

            if (bookId == 0)
            {
                Book = new Book();
            }
            else
            {

                Book = await BookRepository.GetBookById(int.Parse(BookId));
                ImageData = $"images/{Book.Gambar}";
            }


            CategoryId = Book.CategoryID.ToString();

        }

        protected void NavigateToList()
        {
            NavigationManager.NavigateTo("/BookList");
        }

        protected async Task HandleValidSubmit()
        {
            Book.CategoryID = int.Parse(CategoryId);


            if (Book.ID == 0)
            {
                await BookRepository.Insert(Book);


            }
            else
            {
                await BookRepository.Update(Book);

            }

            if (file != null && file.Data.Length > 0)
            {
                await fileUpload.UploadAsync(fs, Book.Gambar);
            }

            NavigationManager.NavigateTo("/BookList");
        }
        protected async Task FileUpload(IFileListEntry[] files)
        {
            file = files.FirstOrDefault();
            if (file != null)
            {
                var ms = new MemoryStream();
                await file.Data.CopyToAsync(ms);
                fs = ms;
                ImageData = $"data:image/jpg;base64,{Convert.ToBase64String(ms.ToArray())}";

                Book.Gambar = file.Name;

            }
        }

        protected async Task DeleteBook()
        {
            await BookRepository.Delete(Book.ID);
            NavigationManager.NavigateTo("/BookList");

        }
    }
}

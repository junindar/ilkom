using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BlazorAPP_Login.Data;
using BlazorAPP_Login.Entity;
using BlazorInputFile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace BlazorApp_Login.Components
{
    public class AddEditBookDialogBase : ComponentBase
    {
        public bool ShowDialog { get; set; }
        
        [Inject]
        public IFileUpload fileUpload { get; set; }
        [Inject]
        public IBookRepository BookRepository { get; set; }

        [Inject]
        public ICategoryRepository CategoryRepository { get; set; }


        public Book Book { get; set; } = new Book();
        public List<Category> Categories { get; set; } = new List<Category>();

        protected string CategoryId = string.Empty;
        protected IFileListEntry file;
        protected MemoryStream fs;
        protected string ImageData = string.Empty;


        [Parameter]
        public EventCallback<bool> CloseEventCallback { get; set; }
        

        public async void Show(int bookid)
        {
            CategoryId = "";
            Categories = await CategoryRepository.GetAll();

            ImageData = string.Empty;
            fs = null;
            file = null;
            if (bookid == 0)
            {
                Book = new Book();
            }
            else
            {

                Book = await BookRepository.GetBookById(bookid);
                CategoryId = Book.CategoryID.ToString();
                ImageData = $"images/{Book.Gambar}";
            }

            ShowDialog = true;
            StateHasChanged();
        }

       
        public void Close()
        {
            ShowDialog = false;
        }
        protected async Task HandleValidSubmit()
        {
            Book.CategoryID = int.Parse(CategoryId);


            if (Book.BookID == 0)
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

            ShowDialog = false;

            await CloseEventCallback.InvokeAsync(true);
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
            await BookRepository.Delete(Book.BookID);
            ShowDialog = false;

            await CloseEventCallback.InvokeAsync(true);
      

        }

    }
}

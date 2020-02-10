using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LatihanBlazor.Entity;
using Microsoft.AspNetCore.Components;

namespace LatihanBlazor.Pages
{
    public class BookListBase : ComponentBase
    {

        protected override Task OnInitializedAsync()
        {

            InitializeCategories();
            InitializeBooks();

            return base.OnInitializedAsync();
        }
        public IEnumerable<Book> Books { get; set; }

      

        private List<Category> Categories { get; set; }

        private void InitializeCategories()
        {
            Categories = new List<Category>()
            {
                new Category{CategoryID = 1, NamaCategory = "Novel"},
                new Category{CategoryID = 2, NamaCategory = "Cerpen"},
                new Category{CategoryID = 3, NamaCategory = "Fiksi"},
                new Category{CategoryID = 4, NamaCategory = "Komputer"},
                new Category{CategoryID = 5, NamaCategory = "Sosial"},


            };
        }

        private void InitializeBooks()
        {
            var book1 = new Book()
            {
                BookID=1,
                 Judul = "Visual Studio LightSwitch - Edisi Bundling",Penulis="Junindar",
                    Penerbit ="EBOOKUID",Deskripsi="",
                    Status=true,Gambar="Ls4.jpg" ,CategoryID=4

            };

            var book2 = new Book
            {
                BookID = 2,
                Judul = "XAMARIN ANDROID - Mudah Membangung Aplikasi Mobile",
                Penulis = "Junindar",
                Penerbit = "EBOOKUID",
                Deskripsi = "",
                Status = true,
                Gambar = "xamarin-android.jpg",
                CategoryID = 4
            };

            var book3 = new Book
            {
                BookID = 3,
                Judul = "Raga Kayu Jiwa Manusia",
                Penulis = "Sarah Anais Andrieu",
                Penerbit = "Kepustakaan Populer Gramedia",
                Deskripsi = @"Wayang golek purwa kini sangat populer di Tanah Sunda, Jawa Barat, Indonesia. 
                            Praktik yang kompleks dalam dimensi sosial dan artistiknya ini diproklamasikan oleh UNESCO 
                            sebagai Karya Agung Warisan Budaya Lisan dan Takbenda Manusia yang merupakan bagian dari pencalonan umum “Wayang Indonesia”, pada tahun 2003.
                            Buku ini menguraikan dan membahas jalur yang dilalui suatu warisan keluarga hingga menjadi suatu warisan bersama, nasional, dan dunia. 
                            Analisis antropologi ini memadukan kajian politik budaya di tingkat-tingkat tersebut dengan 
                            kajian konsep-konsep global dan studi mendalam mengenai tahapan pencalonan pertama Indonesia pada warisan takbenda UNESCO, 
                            serta realitas etnografi wayang golek. Dari proses warisanisasi resmi (yaitu proses menjadi warisan) itu muncul banyak kepentingan, 
                            seperti pembentukan identitas dan budaya nasional, atau pula spektakularisasi dan folklorisasi wayang golek, perubahannya menjadi sebuah produk ekspor, suatu sumber daya untuk digerakkan dan didayagunakan.",
                Status = true,
                CategoryID = 5,
                Gambar = "sosial1.jpg"
            };
            var book4 = new Book
            {
                BookID = 4,
                Judul = "Generasi Phi",
                Penulis = "Dr.Muhammad Faizal",
                Penerbit = "Republika Penerbit",
                Deskripsi = "",
                Status = true,
                CategoryID = 5,
                Gambar = "sosial2.jpg"
            };

            Books = new List<Book> { book1, book2 ,book3,book4};
        }

    }
}

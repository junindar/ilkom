using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Latihan.Library.Entity;
using static System.Reflection.Metadata.BlobBuilder;

namespace Latihan.Library.Service
{
    public class BookService:IBookService
    {
        public async Task<List<Book>?> GetBooksAsync()
        {
            var result = new List<Book>()
            {
                new()
                {
                    Id = 1,
                    Judul = "Visual Studio LightSwitch - Edisi Bundling",
                    Penulis = "Junindar",
                    Penerbit = "EBOOKUID",
                    Deskripsi = "",
                    Status = true,
                    Gambar = "Ls4.jpg"
                },
                //Detail Sintaks ada di project lampiran
                new ()
                {
                    Id = 2,
                    Judul = "XAMARIN ANDROID - Mudah Membangung Aplikasi Mobile",
                    Penulis = "Junindar",
                    Penerbit = "EBOOKUID",
                    Deskripsi = "",
                    Status = true,
                    Gambar = "xamarin-android.jpg"
                },
                new ()
                {
                    Id = 3,
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
                   Gambar = "sosial1.jpg"
                },
                new ()
                {
                    Id = 4,
                    Judul = "Generasi Phi",
                    Penulis = "Dr.Muhammad Faizal",
                    Penerbit = "Republika Penerbit",
                    Deskripsi = "",
                    Status = true,
                   Gambar = "sosial2.jpg"
                }
            };
            
            await  Task.Delay(50);
           return result;
          
        }

        public async Task<Book?> GetBookByIdAsync(int id)
        {
            Book? book = null;
            var lstBooks = await GetBooksAsync();
            if (lstBooks != null)
            {
                book= lstBooks.FirstOrDefault(x => x.Id == id);
            }
            return book;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using BlazorAPP_Login.Entity;

namespace BlazorAPP_Login.Data
{
    public class DbInitializer
    {
        public static void Seed(PustakaDbContext context)
        {

            if (!context.Users.Any())
            {
                var user = new User
                {
                    Username = "admin",
                    Password = "123",
                    Nama = "Junindar",
                    Role = "Admin",
                    Status = true
                };

                context.Users.Add(user);
                user = new User
                {
                    Username = "user1",
                    Password = "123",
                    Nama = "Andi",
                    Role = "Staff",
                    Status = true
                };

                context.Users.Add(user);

                user = new User
                {
                    Username = "user2",
                    Password = "123",
                    Nama = "Yudi",
                    Role = "General",
                    Status = true
                };

                context.Users.Add(user);
                context.SaveChanges();
            }

            if (!context.Categories.Any())
            {
                var categories = new List<Category>
                {
                    new Category { Nama = "Agama" },
                    new Category { Nama = "Bahasa" },

                };
                categories.ForEach(a => context.Categories.Add(a));
                // Sintaks Detail ada pada lampiran project
                var cat1 = new Category
                {
                    Nama = "Teknologi",
                    Books = new List<Book>()
                {
                        new Book { Judul = "XAMARIN ANDROID - Mudah Membangung Aplikasi Mobile",
                            Penulis ="Junindar",
                            Penerbit ="EBOOKUID",Deskripsi="",
                            Status=true,Gambar="xamarin-android.jpg" },
                        new Book { Judul = "XAMARIN.FORMS - Membangun Aplikasi Mobile Cross-Platform",
                            Penulis ="Junindar",Penerbit ="EBOOKUID",Deskripsi="",
                            Status =true,Gambar="xamarin-form.jpg" },
                        new Book { Judul = "VISUAL BASIC 2015 - Cara Cepat Membangun Aplikasi Interaktif",Penulis="Junindar",
                            Penerbit ="EBOOKUID",Deskripsi="",
                            Status =true,Gambar="vb-2015.jpg" },
                        new Book { Judul = "Membangun Aplikasi Point Of Sale (POS) Lebih Mudah Dan Cepat",
                            Penulis ="Junindar",Penerbit="EBOOKUID",Deskripsi="",
                            Status =true,Gambar="c-dapper.jpg" },
                        new Book { Judul = "Visual Studio LightSwitch Learning By Doing",Penulis="Junindar",
                            Penerbit ="EBOOKUID",Deskripsi="",
                            Status=true,Gambar="Ls1.jpg" },
                        new Book { Judul = "Will Code With LightSwitch",Penulis="Junindar",Penerbit="EBOOKUID",Deskripsi="",
                            Status=true,Gambar="Ls2.jpg" },
                        new Book { Judul = "Visual Studio LightSwitch - HTML Client",Penulis="Junindar",Penerbit="EBOOKUID",
                            Deskripsi ="",
                            Status=true,Gambar="Ls3.jpg" },
                        new Book { Judul = "Visual Studio LightSwitch - Edisi Bundling",Penulis="Junindar",
                            Penerbit ="EBOOKUID",Deskripsi="",
                            Status=true,Gambar="Ls4.jpg" }
                }
                };

                var cat2 = new Category()
                {
                    Nama = "Sosial",
                    Books = new List<Book>()
                {
                     new Book { Judul = "Raga Kayu Jiwa Manusia",
                            Penulis ="Sarah Anais Andrieu",
                            Penerbit ="Kepustakaan Populer Gramedia",
                            Deskripsi =@"Wayang golek purwa kini sangat populer di Tanah Sunda, Jawa Barat, Indonesia. 
                            Praktik yang kompleks dalam dimensi sosial dan artistiknya ini diproklamasikan oleh UNESCO 
                            sebagai Karya Agung Warisan Budaya Lisan dan Takbenda Manusia yang merupakan bagian dari pencalonan umum “Wayang Indonesia”, pada tahun 2003.
                            Buku ini menguraikan dan membahas jalur yang dilalui suatu warisan keluarga hingga menjadi suatu warisan bersama, nasional, dan dunia. 
                            Analisis antropologi ini memadukan kajian politik budaya di tingkat-tingkat tersebut dengan 
                            kajian konsep-konsep global dan studi mendalam mengenai tahapan pencalonan pertama Indonesia pada warisan takbenda UNESCO, 
                            serta realitas etnografi wayang golek. Dari proses warisanisasi resmi (yaitu proses menjadi warisan) itu muncul banyak kepentingan, 
                            seperti pembentukan identitas dan budaya nasional, atau pula spektakularisasi dan folklorisasi wayang golek, perubahannya menjadi sebuah produk ekspor, suatu sumber daya untuk digerakkan dan didayagunakan.",
                            Status=true,Gambar="sosial1.jpg" },
                      new Book { Judul = "Generasi Phi",
                            Penulis ="Dr.Muhammad Faizal",
                            Penerbit ="Republika Penerbit",
                            Deskripsi ="",
                            Status=true,Gambar="sosial2.jpg" }
                }
                };

                context.Categories.Add(cat1);
                context.Categories.Add(cat2);

               
                context.SaveChanges();
            }

        }
    }
}

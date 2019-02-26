using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace LatihanCardView
{
 public  class MakananRepository
    {

        public static List<Makanan> ListMakanan { get; }

        static MakananRepository()
        {
            ListMakanan = new List<Makanan>();
            TambahMakanan();
            TambahMakanan2();
            TambahMakanan2();
            TambahMakanan2();
            TambahMakanan2();
            TambahMakanan2();
            TambahMakanan2();
            TambahMakanan2();
        }

        private static void TambahMakanan()
        {
            ListMakanan.Add(new Makanan()
            {
                NamaMakanan = "Nasi Goreng Ayam",
                Harga = 20000,
                SizeMakanan = "Sedang",
                TanggalRelease = new DateTime(2017, 01, 18)
            });

            ListMakanan.Add(new Makanan()
            {
                NamaMakanan = "Kentang Goreng",
                Harga = 5000,
                SizeMakanan = "Kecil",
                TanggalRelease = new DateTime(2017, 01, 17)
            });


            ListMakanan.Add(new Makanan()
            {
                NamaMakanan = "Sop Ayam",
                Harga = 50000,
                SizeMakanan = "Besar",
                TanggalRelease = new DateTime(2017, 01, 16)
            });

            ListMakanan.Add(new Makanan()
            {
                NamaMakanan = "Ayam Bakar Rica-Rica",
                Harga = 70000,
                SizeMakanan = "Besar",
                TanggalRelease = new DateTime(2017, 01, 15)
            });

            ListMakanan.Add(new Makanan()
            {
                NamaMakanan = "Nasi Uduk",
                Harga = 25000,
                SizeMakanan = "Sedang",
                TanggalRelease = new DateTime(2017, 01, 14)
            });

            ListMakanan.Add(new Makanan()
            {
                NamaMakanan = "Pecel Lele",
                Harga = 20000,
                SizeMakanan = "Sedang",
                TanggalRelease = new DateTime(2017, 01, 13)
            });

        }
        private static void TambahMakanan2()
        {
            ListMakanan.Add(new Makanan()
            {
                NamaMakanan = "Nasi Goreng Ayam",
                Harga = 20000,
                SizeMakanan = "Sedang",
                TanggalRelease = new DateTime(2017, 01, 18)
            });

            ListMakanan.Add(new Makanan()
            {
                NamaMakanan = "Kentang Goreng",
                Harga = 5000,
                SizeMakanan = "Kecil",
                TanggalRelease = new DateTime(2017, 01, 17)
            });


            ListMakanan.Add(new Makanan()
            {
                NamaMakanan = "Sop Ayam",
                Harga = 50000,
                SizeMakanan = "Besar",
                TanggalRelease = new DateTime(2017, 01, 16)
            });

            ListMakanan.Add(new Makanan()
            {
                NamaMakanan = "Ayam Bakar Rica-Rica",
                Harga = 70000,
                SizeMakanan = "Besar",
                TanggalRelease = new DateTime(2017, 01, 15)
            });

            ListMakanan.Add(new Makanan()
            {
                NamaMakanan = "Nasi Uduk",
                Harga = 25000,
                SizeMakanan = "Sedang",
                TanggalRelease = new DateTime(2017, 01, 14)
            });

            ListMakanan.Add(new Makanan()
            {
                NamaMakanan = "Pecel Lele",
                Harga = 20000,
                SizeMakanan = "Sedang",
                TanggalRelease = new DateTime(2017, 01, 13)
            });

        }

    }
}
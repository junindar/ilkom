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

namespace LatihanRecyclerView
{
   public class ProductRepository
    {
        public static List<Product> Products { get; }

        static ProductRepository()
        {
            Products = new List<Product>();
            TambahProduct();
        }

        private static void TambahProduct()
        {
            Products.Add(new Product()
            {
                Id = Resource.Drawable.product1,
                Nama = "Argan Oil Arsyifa 10ml",
                Harga = 65000
            });

            Products.Add(new Product()
            {
                Id = Resource.Drawable.product2,
                Nama = "Argan Oil Arsyifa 50ml",
                Harga = 195000
            });

            Products.Add(new Product()
            {
                Id = Resource.Drawable.product3,
                Nama = "Argan Oil Arsyifa Jumbo",
                Harga = 250000
            });

            Products.Add(new Product()
            {
                Id = Resource.Drawable.product4,
                Nama = "Rose Water Arsyifa 100ml",
                Harga = 200000
            });

            Products.Add(new Product()
            {
                Id = Resource.Drawable.product5,
                Nama = "Arsyifa All Products",
                Harga = 450000
            });
        }
    }
}
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
    public class Product
    {
        public int Id { get; set; }
        public string Nama { get; set; }
        public double Harga { get; set; }

        public override string ToString()
        {
            return Nama + " - harga " + Harga.ToString("C");
        }

    }
}
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
  public  class Makanan
    {
        public string NamaMakanan { get; set; }
        public double Harga { get; set; }
        public string SizeMakanan { get; set; }
        public DateTime TanggalRelease { get; set; }

        public override string ToString()
        {
            return NamaMakanan + " " + SizeMakanan + " - harga " + Harga.ToString("C");
        }
    }
}
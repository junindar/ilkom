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

namespace LatihanListView.DataAccess
{
    public class Size
    {
        public string SizeMakanan { get; set; }
        public List<Makanan> MakananDetails { get; set; } = new List<Makanan>();
        public override string ToString()
        {
            return  "Ukuran Makanan : " + SizeMakanan ;
        }
    }
}
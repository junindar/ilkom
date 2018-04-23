using System;

namespace LatihanListView.DataAccess
{
  public  class Makanan
    {
        public string NamaMakanan { get; set; }
        public double Harga { get; set; }
        public string SizeMakanan { get; set; }
        public DateTime TanggalRelease { get; set; }

        public override string ToString()
        {
            return  NamaMakanan + " " + SizeMakanan + " - harga " + Harga.ToString("C");
        }
    }
}
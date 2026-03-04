using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerVisionIntro.Models
{
    public class ReceiptItem
    {
        public int No { get; set; }
        public string Nama { get; set; } = "";
        public double Harga { get; set; }
        public int Jumlah { get; set; }
        public double Total => Harga * Jumlah;
    }
}

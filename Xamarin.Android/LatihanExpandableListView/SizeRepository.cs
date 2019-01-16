using System.Collections.Generic;


namespace LatihanExpandableListView
{
  public static class SizeRepository
    {

        public static List<Size> ListSize { get; }

        static SizeRepository()
        {
            ListSize = new List<Size>();
            TambahSize();
        }

        private static void TambahSize()
        {
            ListSize.Add(new Size()
            {
                SizeMakanan = "Besar",
                MakananDetails = MakananRepository.ListMakanan
            });
            ListSize.Add(new Size()
            {
                SizeMakanan = "Sedang",
                MakananDetails = MakananRepository.ListMakanan
            });
            ListSize.Add(new Size()
            {
                SizeMakanan = "Kecil",
                MakananDetails = MakananRepository.ListMakanan
            });
        }

    }
    }
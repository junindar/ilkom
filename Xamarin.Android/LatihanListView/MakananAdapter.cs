using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;
using LatihanListView.DataAccess;


// ReSharper disable once CheckNamespace
namespace LatihanListView
{
    public class MakananAdapter : BaseAdapter<Makanan>
    {
        private readonly Activity _context;
        private readonly List<Makanan> _listMakanan;
      
        public MakananAdapter(Activity context, List<Makanan> listmakanan)
        {
            _context = context;
            _listMakanan = listmakanan;
         
        }
        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? _context.LayoutInflater.Inflate
                (Resource.Layout.customRowListView, parent, false);

            var makananTextView = view.FindViewById<TextView>(Resource.Id.makananTextView);
            var hargaTextView = view.FindViewById<TextView>(Resource.Id.hargaTextView);
            var tanggalTextView = view.FindViewById<TextView>(Resource.Id.tanggalTextView);
            var iconImage = view.FindViewById<ImageView>(Resource.Id.item_icon);

            iconImage.SetImageResource(Resource.Drawable.makanan);
            makananTextView.Text = _listMakanan[position].NamaMakanan;
            hargaTextView.Text = "Size : " + _listMakanan[position].SizeMakanan + " - Harga: " + _listMakanan[position].Harga.ToString("C");
            tanggalTextView.Text = "Tanggal : " + _listMakanan[position].TanggalRelease.ToShortDateString();

            return view;
        }


        public override int Count => _listMakanan.Count;

        public override Makanan this[int position] => _listMakanan[position];


        //readonly Java.Lang.Object[] _sectionHeaders = SectionIndexerBuilder.BuildSectionHeaders(MakananRepository.ListMakanan);
        //readonly Dictionary<int, int> _positionForSectionMap = SectionIndexerBuilder.BuildPositionForSectionMap(MakananRepository.ListMakanan);
        //readonly Dictionary<int, int> _sectionForPositionMap = SectionIndexerBuilder.BuildSectionForPositionMap(MakananRepository.ListMakanan);

        //public Java.Lang.Object[] GetSections()
        //{
        //    return _sectionHeaders;
        //}

        //public int GetPositionForSection(int section)
        //{
        //    return _positionForSectionMap[section];
        //}

        //public int GetSectionForPosition(int position)
        //{
        //    return _sectionForPositionMap[position];
        //}

    }
}
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
    public class MakananCardAdapter : BaseAdapter<Makanan>
    {

        private readonly Activity _context;
        private readonly List<Makanan> _listMakanan;
        private readonly List<int> _imageResId;

        public MakananCardAdapter(Activity context, List<Makanan> listmakanan)
        {
            _context = context;
            _listMakanan = listmakanan;
            _imageResId = new List<int> {
                Resource.Drawable.nasi_goreng,
                Resource.Drawable.ketang_goreng,
                Resource.Drawable.sop_ayam,
                Resource.Drawable.ayam_bakar,
                Resource.Drawable.nasi_uduk,
                Resource.Drawable.pecel_lele};
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? _context.LayoutInflater.Inflate
                           (Resource.Layout.CardViewLayout, parent, false);
            var titleTextView = view.FindViewById<TextView>(Resource.Id.lblTitle);
            var imageView = view.FindViewById<ImageView>(Resource.Id.imgView);
            titleTextView.Text = _listMakanan[position].NamaMakanan + " - Harga : " +
                                 _listMakanan[position].Harga.ToString("C");
            imageView.SetImageResource(_imageResId[position]);
            return view;
        }


        public override int Count => _listMakanan.Count;

        public override Makanan this[int position] => _listMakanan[position];
    }
}
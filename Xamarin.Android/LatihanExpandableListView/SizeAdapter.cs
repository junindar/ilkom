using System;
using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;
using Object = Java.Lang.Object;

namespace LatihanExpandableListView
{
    public class SizeAdapter : BaseExpandableListAdapter
    {
        private readonly Activity _context;
        private readonly List<Size> _listSize;

        public SizeAdapter(Activity context, List<Size> listsize)
        {
            _context = context;
            _listSize = listsize;
        }

        public override Object GetChild(int groupPosition, int childPosition)
        {
            throw new System.NotImplementedException();
        }

        public override long GetChildId(int groupPosition, int childPosition)
        {
            return childPosition;
        }

        public override Object GetGroup(int groupPosition)
        {
            return _listSize[groupPosition].ToString();
        }

        public override bool IsChildSelectable(int groupPosition, int childPosition)
        {
            return true;
        }

        public override bool HasStableIds => true;

        public override long GetGroupId(int groupPosition)
        {
            return groupPosition;
        }

        public override int GetChildrenCount(int groupPosition)
        {
            return _listSize[groupPosition].MakananDetails.Count;
        }

        public override View GetChildView(int groupPosition, int childPosition,
            bool isLastChild, View convertView, ViewGroup parent)
        {

            var view = convertView ?? _context.LayoutInflater.Inflate
              (Resource.Layout.customRowListView, parent, false);

            var makananTextView = view.FindViewById<TextView>(Resource.Id.makananTextView);
            var hargaTextView = view.FindViewById<TextView>(Resource.Id.hargaTextView);
            var tanggalTextView = view.FindViewById<TextView>(Resource.Id.tanggalTextView);
            var iconImage = view.FindViewById<ImageView>(Resource.Id.item_icon);

            var makanan = _listSize[groupPosition].MakananDetails[childPosition];

            iconImage.SetImageResource(Resource.Drawable.makanan);
            makananTextView.Text = makanan.NamaMakanan;
            hargaTextView.Text = "Harga: " + makanan.Harga.ToString("C");
            tanggalTextView.Text = "Tanggal : " + makanan.TanggalRelease.ToShortDateString();

            return view;
        }


        public override View GetGroupView(int groupPosition, bool isExpanded,
            View convertView, ViewGroup parent)
        {
            convertView = convertView ?? _context.LayoutInflater.Inflate
                (Resource.Layout.HeaderExpandLayout, null);
            var headerTitle = (string)GetGroup(groupPosition);
            var lblListHeader = (TextView)convertView.FindViewById(Resource.Id.lblLvHeader);
            lblListHeader.Text = headerTitle;

            return convertView;
        }

        public override int GroupCount => _listSize.Count;




    }
}
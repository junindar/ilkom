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
using LatihanListView.DataAccess;


namespace LatihanListView
{
    [Activity(Label = "Daftar Harga Makanan Custom", Icon = "@drawable/search")]
    public class ListViewCustomActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ListViewLayout);
            var listViewMakanan = FindViewById<ListView>(Resource.Id.makananListView);
            var makananAdapter = new MakananAdapter(this, MakananRepository.ListMakanan);
            listViewMakanan.FastScrollEnabled = true;
            listViewMakanan.Adapter = makananAdapter;
            listViewMakanan.ItemClick += listViewMakanan_ItemClick;
        }

        void listViewMakanan_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Toast.MakeText(this, MakananRepository.ListMakanan[e.Position].ToString(),
                ToastLength.Short).Show();

        }
    }
}
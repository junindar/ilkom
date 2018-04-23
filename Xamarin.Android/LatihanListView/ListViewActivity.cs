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
    [Activity(Label = "Daftar Harga Makanan", Icon = "@drawable/makanan")]
    public class ListViewActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.ListViewLayout);
            var listViewMakanan = FindViewById<ListView>(Resource.Id.makananListView);
            var makananAdapter = new ArrayAdapter<Makanan>(this, Android.Resource.Layout.SimpleListItem1, MakananRepository.ListMakanan);

            listViewMakanan.Adapter = makananAdapter;
            listViewMakanan.ItemClick += listViewMakanan_ItemClick;
        }

        void listViewMakanan_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Toast.MakeText(this, MakananRepository.ListMakanan[e.Position].ToString(), ToastLength.Short).Show();

        }
    }
}
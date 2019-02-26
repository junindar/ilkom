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
    //[Activity(Label = "CardView Custom")]
    [Activity(Label = "CardView Custom", Icon = "@drawable/search",
        Theme = "@android:style/Theme.Material.Light.DarkActionBar")]
    public class CardViewCustomActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.CardViewCustomLayout);
            var makananListView = FindViewById<ListView>(Resource.Id.makananListView);
            makananListView.ItemClick += makananListView_ItemClick;
            makananListView.FastScrollEnabled = true;
            var moviesAdapter = new MakananCardAdapter(this, MakananRepository.ListMakanan);
            makananListView.Adapter = moviesAdapter;
        }

        void makananListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Toast.MakeText(this, MakananRepository.ListMakanan[e.Position].ToString(), ToastLength.Short).Show();
        }

    }
}
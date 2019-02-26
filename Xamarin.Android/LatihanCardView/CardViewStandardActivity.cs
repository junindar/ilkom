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
    //[Activity(Label = "CardView Standard")]
    [Activity(Label = "CardView Standard", Icon = "@drawable/search",
        Theme = "@android:style/Theme.Material.Light.DarkActionBar")]

    public class CardViewStandardActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.CardViewLayout);
            var lblTitle = FindViewById<TextView>(Resource.Id.lblTitle);
            var imgView = FindViewById<ImageView>(Resource.Id.imgView);
            lblTitle.Text = "Arsyifa Argan Oil";
            imgView.SetImageResource(Resource.Drawable.gambar_arsyifa);
            // Create your application here
        }
    }
}
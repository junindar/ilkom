using Android.App;
using Android.Widget;
using Android.OS;

namespace LatihanCardView
{
   

    [Activity(Label = "Latihan CardView", MainLauncher = true, Icon = "@drawable/search")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            var btnCardView = FindViewById<Button>(Resource.Id.btnCardView);
            btnCardView.Click += (sender, args) =>
            {
                StartActivity(typeof(CardViewStandardActivity));
            };

            var btnCardViewCustom = FindViewById<Button>(Resource.Id.btnCardViewCustom);
            btnCardViewCustom.Click += (sender, args) =>
            {
                StartActivity(typeof(CardViewCustomActivity));
            };

        }
    }
}


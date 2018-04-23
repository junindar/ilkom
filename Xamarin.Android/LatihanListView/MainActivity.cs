using Android.App;
using Android.Widget;
using Android.OS;

namespace LatihanListView
{
    [Activity(Label = "Latihan ListView", MainLauncher = true, Icon = "@drawable/makanan")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            var btnlistView = FindViewById<Button>(Resource.Id.btnListView);
            btnlistView.Click += (sender, args) =>
            {
                StartActivity(typeof(ListViewActivity));
            };

            var btnlistViewCustom = FindViewById<Button>(Resource.Id.btnListViewCustom);
            btnlistViewCustom.Click += (sender, args) =>
            {
                StartActivity(typeof(ListViewCustomActivity));
            };

            var btnExpandableListView = FindViewById<Button>(Resource.Id.btnExpandableListView);
            btnExpandableListView.Click += (sender, args) =>
            {
                StartActivity(typeof(ExpandableListViewActivity));
            };

        }
    }
}


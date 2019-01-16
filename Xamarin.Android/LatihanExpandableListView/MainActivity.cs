using Android.App;
using Android.Widget;
using Android.OS;

namespace LatihanExpandableListView
{
    [Activity(Label = "Latihan ExpandableListView", MainLauncher = true)]
    public class MainActivity : Activity
    {
        ExpandableListView _expListView;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            _expListView = FindViewById<ExpandableListView>(Resource.Id.expandLV);
            var listAdapter = new SizeAdapter(this, SizeRepository.ListSize);
            _expListView.SetAdapter(listAdapter);
            _expListView.FastScrollEnabled = true;
            ClickEvents();
        }

        void ClickEvents()
        {

            _expListView.ChildClick += delegate (object sender, ExpandableListView.ChildClickEventArgs e)
            {
                Toast.MakeText(this, SizeRepository.ListSize[e.GroupPosition].MakananDetails[e.ChildPosition].ToString(), ToastLength.Short).Show();
            };

            _expListView.GroupCollapse += delegate (object sender, ExpandableListView.GroupCollapseEventArgs e)
            {
                var i = e.GroupPosition;
                if (i == -1) return;
                Toast.MakeText(this, SizeRepository.ListSize[i].ToString(), ToastLength.Short).Show();
            };

            _expListView.GroupExpand += delegate (object sender, ExpandableListView.GroupExpandEventArgs e) {
                int previousGroup = -1;
                if (e.GroupPosition != previousGroup) _expListView.CollapseGroup(previousGroup);
                var i = e.GroupPosition;
                if (i == -1) return;
                Toast.MakeText(this, SizeRepository.ListSize[i].ToString(), ToastLength.Short).Show();
            };


        }

    }
}


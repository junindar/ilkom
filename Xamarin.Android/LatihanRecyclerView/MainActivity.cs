using System;
using System.Collections.Generic;
using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;

namespace LatihanRecyclerView
{
    //[Activity(Label = "LatihanRecyclerView", MainLauncher = true)]


    [Activity(Label = "Latihan RecyclerView", Icon = "@drawable/search",
        Theme = "@android:style/Theme.Material.Light.DarkActionBar", MainLauncher = true)]
    public class MainActivity : Activity
    {
        private RecyclerView _recyclerView;
        private RecyclerView.LayoutManager _layoutManager;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            _recyclerView = FindViewById<RecyclerView>(Resource.Id.rcView);
            _layoutManager = new LinearLayoutManager(this, LinearLayoutManager.Vertical, false);
            _recyclerView.SetLayoutManager(_layoutManager);
            var productAdapter = new ProductAdapter(ProductRepository.Products);
            _recyclerView.SetAdapter(productAdapter);
            productAdapter.ItemClick += ProductAdapter_ItemClick;

            Button btnLayout = FindViewById<Button>(Resource.Id.btnLayout);

            btnLayout.Click += delegate
            {
                if (btnLayout.Text == "Grid Layout")
                {
                    _layoutManager = new GridLayoutManager(this, 2, GridLayoutManager.Vertical, false);
                    _recyclerView.SetLayoutManager(_layoutManager);
                    btnLayout.Text = "Linear Layout";
                }
                else
                {
                    _layoutManager = new LinearLayoutManager(this, LinearLayoutManager.Vertical, false);
                    _recyclerView.SetLayoutManager(_layoutManager);
                    btnLayout.Text = "Grid Layout";
                }
            };


        }

        private void ProductAdapter_ItemClick(object sender, int e)
        {
            Toast.MakeText(this, ProductRepository.Products[e].ToString(), ToastLength.Short).Show();
        }
    }


    public class ProductViewHolder : RecyclerView.ViewHolder
    {
        public TextView ProductNameTextView { get; set; }
        public ImageView ProductImageView { get; set; }

        public ProductViewHolder(View itemView, Action<int> listener) : base(itemView)
        {
            ProductNameTextView = itemView.FindViewById<TextView>(Resource.Id.lblTitle);
            ProductImageView = itemView.FindViewById<ImageView>(Resource.Id.imgView);

            itemView.Click += (s, e) => listener(Position);
        }
    }

    public class ProductAdapter : RecyclerView.Adapter
    {
        public event EventHandler<int> ItemClick;

        private readonly List<Product> _products;

        public ProductAdapter(List<Product> products)
        {
            _products = products;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var productViewHolder = (ProductViewHolder)holder;
            productViewHolder.ProductNameTextView.Text = _products[position].Nama + " - Harga : " + _products[position].Harga.ToString("C");
            productViewHolder.ProductImageView.SetImageResource(_products[position].Id);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var layout = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.CardViewLayout, parent, false);

            return new ProductViewHolder(layout, OnItemClick);

        }

        public override int ItemCount => _products.Count;

        void OnItemClick(int position)
        {
            ItemClick?.Invoke(this, position);
        }

    }

}


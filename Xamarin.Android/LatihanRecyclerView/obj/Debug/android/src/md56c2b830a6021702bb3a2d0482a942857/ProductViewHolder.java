package md56c2b830a6021702bb3a2d0482a942857;


public class ProductViewHolder
	extends android.support.v7.widget.RecyclerView.ViewHolder
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("LatihanRecyclerView.ProductViewHolder, LatihanRecyclerView, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", ProductViewHolder.class, __md_methods);
	}


	public ProductViewHolder (android.view.View p0)
	{
		super (p0);
		if (getClass () == ProductViewHolder.class)
			mono.android.TypeManager.Activate ("LatihanRecyclerView.ProductViewHolder, LatihanRecyclerView, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Views.View, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}

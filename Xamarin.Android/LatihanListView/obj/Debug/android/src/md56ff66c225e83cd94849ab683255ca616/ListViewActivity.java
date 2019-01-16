package md56ff66c225e83cd94849ab683255ca616;


public class ListViewActivity
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("LatihanListView.ListViewActivity, LatihanListView, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", ListViewActivity.class, __md_methods);
	}


	public ListViewActivity ()
	{
		super ();
		if (getClass () == ListViewActivity.class)
			mono.android.TypeManager.Activate ("LatihanListView.ListViewActivity, LatihanListView, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

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

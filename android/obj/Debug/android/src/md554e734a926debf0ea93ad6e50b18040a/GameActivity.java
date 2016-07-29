package md554e734a926debf0ea93ad6e50b18040a;


public class GameActivity
	extends md5741e60a80250ab7e7b6f039198ff6a46.AndroidGameActivity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("FallingCatGame.Games.GameActivity, FallingCatGame, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", GameActivity.class, __md_methods);
	}


	public GameActivity () throws java.lang.Throwable
	{
		super ();
		if (getClass () == GameActivity.class)
			mono.android.TypeManager.Activate ("FallingCatGame.Games.GameActivity, FallingCatGame, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
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

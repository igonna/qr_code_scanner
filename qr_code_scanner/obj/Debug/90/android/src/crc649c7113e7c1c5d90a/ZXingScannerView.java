package crc649c7113e7c1c5d90a;


public class ZXingScannerView
	extends me.dm7.barcodescanner.core.BarcodeScannerView
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onPreviewFrame:([BLandroid/hardware/Camera;)V:GetOnPreviewFrame_arrayBLandroid_hardware_Camera_Handler\n" +
			"";
		mono.android.Runtime.register ("EDMTDev.ZXingXamarinAndroid.ZXingScannerView, ZXingBinding", ZXingScannerView.class, __md_methods);
	}


	public ZXingScannerView (android.content.Context p0)
	{
		super (p0);
		if (getClass () == ZXingScannerView.class)
			mono.android.TypeManager.Activate ("EDMTDev.ZXingXamarinAndroid.ZXingScannerView, ZXingBinding", "Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0 });
	}


	public ZXingScannerView (android.content.Context p0, android.util.AttributeSet p1)
	{
		super (p0, p1);
		if (getClass () == ZXingScannerView.class)
			mono.android.TypeManager.Activate ("EDMTDev.ZXingXamarinAndroid.ZXingScannerView, ZXingBinding", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android", this, new java.lang.Object[] { p0, p1 });
	}


	public void onPreviewFrame (byte[] p0, android.hardware.Camera p1)
	{
		n_onPreviewFrame (p0, p1);
	}

	private native void n_onPreviewFrame (byte[] p0, android.hardware.Camera p1);

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

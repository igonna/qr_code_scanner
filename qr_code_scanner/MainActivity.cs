using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using EDMTDev.ZXingXamarinAndroid;
using Com.Karumi.Dexter;
using Android;
using Com.Karumi.Dexter.Listener.Single;
using Com.Karumi.Dexter.Listener;
using System;
using qr_code_scanner.Model;
using Android.Content;

namespace qr_code_scanner
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, IPermissionListener
    {
        private ZXingScannerView scannerView;
        private TextView txtResult;
        private Button open_link_button;
        private Button break_button;
        static private string UrlString = "";
        //private Button urlButton;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            //Init view
            txtResult = FindViewById<TextView>(Resource.Id.txt_result);
            scannerView = FindViewById<ZXingScannerView>(Resource.Id.zxscan);
            open_link_button = FindViewById<Button>(Resource.Id.link_button);
            break_button = FindViewById<Button>(Resource.Id.break_button);
            //urlButton = FindViewById<Button>(Resource.Id.url_button);
            //Request permission
            Dexter.WithActivity(this)
                .WithPermission(Manifest.Permission.Camera)
                .WithListener(this)
                .Check();
            open_link_button.Click += (sender, e) =>
            {
                if (UrlString != "")
                    Xamarin.Essentials.Browser.OpenAsync(UrlString);
            };
            break_button.Click += (sender, e) =>
            {
                scannerView.SetResultHandler(new MyResultHandler(this));
                scannerView.StartCamera();
            };

        }
        protected override void OnDestroy()
        {
            scannerView.StopCamera();
            base.OnDestroy();
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public void OnPermissionDenied(PermissionDeniedResponse p0)
        {
            Toast.MakeText(this, "Enable this permission for camera", ToastLength.Long).Show();
        }

        public void OnPermissionGranted(PermissionGrantedResponse p0)
        {
            scannerView.SetResultHandler(new MyResultHandler(this));
            scannerView.StartCamera();
        }

        public void OnPermissionRationaleShouldBeShown(PermissionRequest p0, IPermissionToken p1)
        {
            
        }

        private class MyResultHandler : IResultHandler
        {
            private MainActivity mainActivity;

            public MyResultHandler(MainActivity mainActivity)
            {
                this.mainActivity = mainActivity;
            }

            public void HandleResult(ZXing.Result rawResult)
            {
                //mainActivity.txtResult.Text = rawResult.Text;
                ProcessResult(rawResult.Text);
            }

            private void ProcessResult(string text)
            {
                
                if (text.StartsWith("BEGIN:"))
                {
                    var tokens = text.Split("\n");
                    QRVCardModel qRVCardModel = new QRVCardModel();
                    foreach (var item in tokens)
                    {
                        if (item.StartsWith("BEGIN:"))
                            qRVCardModel.type = item.Substring("BEGIN:".Length); //remove BEGIN from String to get type
                        else if (item.StartsWith("N:"))
                            qRVCardModel.name = item.Substring("N:".Length);
                        else if (item.StartsWith("ORG:"))
                            qRVCardModel.org = item.Substring("ORG:".Length);
                        else if (item.StartsWith("TEL:"))
                            qRVCardModel.tel = item.Substring("TEL:".Length);
                        else if (item.StartsWith("URL:"))
                            qRVCardModel.url = item.Substring("URL:".Length);
                        else if (item.StartsWith("EMAIL:"))
                            qRVCardModel.email = item.Substring("EMAIL:".Length);
                        else if (item.StartsWith("ADDR:"))
                            qRVCardModel.addr = item.Substring("ADDR:".Length);
                        else if (item.StartsWith("NOTE:"))
                            qRVCardModel.note = item.Substring("NOTE:".Length);

                        //VEVENT
                        else if (item.StartsWith("SYMMARY:"))
                            qRVCardModel.summary = item.Substring("SYMMARY:".Length);
                        else if (item.StartsWith("DTSTART:"))
                            qRVCardModel.dtstart = item.Substring("DTSTART:".Length);
                        else if (item.StartsWith("DTEND:"))
                            qRVCardModel.dtend = item.Substring("DTEND:".Length);

                    }
                    mainActivity.txtResult.Text = qRVCardModel.type;
                }
                else if (text.StartsWith("http://") || text.StartsWith("https://") || text.StartsWith("www."))
                {
                    QRUrlModel qRUrlModel = new QRUrlModel();
                    qRUrlModel.url = text;
                    mainActivity.txtResult.Text = qRUrlModel.url;
                    UrlString = text;
                    //Xamarin.Essentials.Browser.OpenAsync(text);
                }
                else if (text.StartsWith("geo:"))
                {
                    QRGeoModel qRGeoModel = new QRGeoModel();
                    var delims = "[ , ?q= ]+";
                    var tokens = text.Split(delims);
                    foreach (var item in tokens)
                    {
                        if (item.StartsWith(" geo:"))
                            qRGeoModel.lat = item.Substring("geo:".Length);
                    }
                    qRGeoModel.lat = tokens[0].Substring("geo".Length);
                    qRGeoModel.lng = tokens[1];
                    qRGeoModel.geo_place = tokens[2];
                }
                else
                {
                    mainActivity.txtResult.Text = text;
                }
            }
        }
    }

}
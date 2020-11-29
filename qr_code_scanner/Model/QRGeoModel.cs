using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace qr_code_scanner.Model
{
    public class QRGeoModel
    {
        public string lat { get; set; }
        public string lng { get; set; }
        public string geo_place { get; set; }
    }
}
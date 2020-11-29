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
    public class QRVCardModel
    {
        public string type { get; set; }
        //VCARD
        public string name { get; set; }
        public string org { get; set; }
        public string tel { get; set; }
        public string addr { get; set; }
        public string email { get; set; }
        public string note { get; set; }
        public string url { get; set; }
        //VEVENT
        public string summary { get; set; }
        public string dtstart { get; set; }
        public string dtend { get; set; }


    }
}
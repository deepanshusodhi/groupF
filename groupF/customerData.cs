using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

using Android.App;

using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Realms;       

namespace groupF
{
   public  class UserInfoDB:RealmObject

    {

        public int itemimage { get; set; }

        public int itemquantity { get; set; }

        public string itemimagename { get; set; }

        public string nameofuser { get; set; }

        public string email { get; set; }

        public string password { get; set; }

        public int phonenumber { get; set; }

        public int age { get; set; }

    }
}
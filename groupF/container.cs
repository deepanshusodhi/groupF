using System;
using System.Collections.Generic;
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
    [Activity(Label = "Frame")]
    public class Frame : Activity
    {
        string nameInfo;

        Realm realmDB;
        Fragment[] _FragmentsArray;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            RequestWindowFeature(Android.Views.WindowFeatures.ActionBar);
            this.ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.FrameLayout);

            realmDB = Realm.GetInstance();


            nameInfo = Intent.GetStringExtra("email");


            var userinfodbObj = realmDB.All<UserInfoDB>().Where(d => d.email == nameInfo.ToLower()).First();

            _FragmentsArray = new Fragment[]
            {
                new ScreenOne(userinfodbObj),

                new ScreenTwo(this),

                new ScreenThree(this) //CAUSING ISSUES
            };

            AddTabToActionBar("First Tab"); 


            AddTabToActionBar("Second Tab"); 

            AddTabToActionBar("Third Tab"); 
        }


        void AddTabToActionBar(string tabTitle)
        {
            //WARING COMPILER ISSUES
            Android.App.ActionBar.Tab tab = ActionBar.NewTab();

            tab.SetText(tabTitle);

            tab.SetIcon(Android.Resource.Drawable.IcInputAdd); 
            tab.TabSelected += TabOnTabSelected;

            this.ActionBar.AddTab(tab);
        }

        void TabOnTabSelected(object sender, Android.App.ActionBar.TabEventArgs tabEventArgs)
        {
            //WARNING COMPLILER ISSUES
            Android.App.ActionBar.Tab tab = (Android.App.ActionBar.Tab)sender;
            //Log.Debug(Tag, "The tab {0} has been selected.", tab.Text); 
            Fragment frag = _FragmentsArray[tab.Position];
            tabEventArgs.FragmentTransaction.Replace(Resource.Id.frameLayout1, frag);
        }

    }
}
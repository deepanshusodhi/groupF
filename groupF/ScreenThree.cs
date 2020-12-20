using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Realms;

namespace groupF
{
    public class ScreenThree : Fragment
    {
        Button addToList;

        ListView listView;
        Spinner spinnerView;
        
        Spinner electronicImage;  

        Realm realmDB;
        
        MyCustomAdapter myAdapter;  

        int[]electronicsItemImage = {
            
        }

        int selectedItemImage;
        int selectedItemQuantity;


        List<MyModel> myOwnList = new List<MyModel>();
        List<int> myitemQuantityList = new List<int>();
        List<ItemImageModel> myelectronicItemImageList = new List<ItemImageModel>();
        private Activity context;

        public ScreenThree(Activity passedContext)
        {
            this.context = passedContext;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            realmDB = Realm.GetInstance();

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View myView = inflater.Inflate(Resource.Layout.ScreenThree, container, false); 

            addToList = myView.FindViewById<Button>(Resource.Id.addToListID);
            listView = myView.FindViewById<ListView>(Resource.Id.listViewID);
            spinnerView = myView.FindViewById<Spinner>(Resource.Id.spinnerViewID);
            electronicImage = myView.FindViewById<Spinner>(Resource.Id.electronicImageID);


            ArrayAdapter arrayAdapter = new ArrayAdapter(context, Android.Resource.Layout.SimpleExpandableListItem1, itemQuantity);
            spinnerView.Adapter = arrayAdapter;
            spinnerView.ItemSelected += spinner_ItemSelected; 
            addToList.Click += addToLists;


            ItemImageAdapter itemimageAdapter = new ItemImageAdapter(context, myelectronicItemImageList);
            electronicImage.Adapter = itemimageAdapter;
            electronicImage.ItemSelected += electronicImage_ItemSelected;

            return myView;
        }




        public List<MyModel> getDataFromRealmDB()
        {

            List<MyModel> dbRecordList = new List<MyModel>();

            var resultCollection = realmDB.All<UserInfoDB>();


            //loop to go through each value of the collection 
            foreach (UserInfoDB newObj in resultCollection)
            {
                //get the name and type
                int itemimagefromDB = newObj.itemimage;
                int itemquantityfromDB = newObj.itemquantity;

                //And asign into the list
                MyModel temp = new MyModel(itemimagefromDB, itemquantityfromDB);
                dbRecordList.Add(temp);
            }
            return dbRecordList;
        }



        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)//For Spinner MovieType
        {
            int index = e.Position;
            selectedItemQuantity = itemQuantity[index];
        }

        private void electronicImage_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)//For Spinner Item Quantity
        {
            int index = e.Position;

            ItemImageModel ItemImageModel = myelectronicItemImageList[index];
            selectedItemImage = ItemImageModel.itemimage;
        }



        private void addToLists(object sender, EventArgs e)
        {
            int itemImageInfo = selectedItemImage;
            int itemQuantityInfo = selectedItemQuantity;

            UserInfoDB newObj = new UserInfoDB();
            newObj.itemquantity = itemQuantityInfo;
            newObj.itemimage = itemImageInfo;


            //Saving the info in the DB:
            realmDB.Write(() =>
            {
                realmDB.Add(newObj);
            });


            myOwnList = getDataFromRealmDB();   
            myAdapter = new MyCustomAdapter(context, myOwnList); 
            listView.Adapter = myAdapter;
        }


    }
}

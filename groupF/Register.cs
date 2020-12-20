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
    [Activity(Label = "Register")]
    public class Register : Activity
    {

        EditText nameofuser;
        EditText email;
  
        EditText password;
        Button registerButton;
        Button returnBtn;


        Realm realmDB;
        string nameofuserValue;

        string emailValue;
    
        string passwordValue;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Register);

            realmDB = Realm.GetInstance();

            nameofuser = FindViewById<EditText>(Resource.Id.nameofuserID);

            email = FindViewById<EditText>(Resource.Id.emailID);



            password = FindViewById<EditText>(Resource.Id.passwordID);



            returnBtn.Click += loginPage;

            registerButton = FindViewById<Button>(Resource.Id.registerBtnID);
            registerButton.Click += registerUser;
        }
        private void loginPage(object sender, EventArgs e)

        {
            Intent newMainActivityScreen = new Intent(this, typeof(MainActivity));
            StartActivity(newMainActivityScreen);
        }

        private void registerUser(object sender, EventArgs e)
        {
            emailValue = email.Text;
            passwordValue = password.Text;
            nameofuserValue = nameofuser.Text;

            if (emailValue.Trim() == "" )
            {
                Toast.MakeText(this, "Please Enter Email!",
                    ToastLength.Long).Show();
            }

            else if (nameofuserValue.Trim() == "" )
            {
                Toast.MakeText(this, " Enter User Name!",
                    ToastLength.Long).Show();
            }

            else if (passwordValue.Trim() == "" )
            {
                Toast.MakeText(this, " Enter the Password!",
                    ToastLength.Long).Show();
            }

            else
            {
                var customerData = realmDB.All<UserInfoDB>().Where(d => d.email == emailValue.ToLower());
                var customerCount = customerData.Count();

                if ( customerCount > 0)
                {
                    Toast.MakeText(this, "USER ALREADY EXIST", ToastLength.Long).Show();
                }

                else
                {
                    UserInfoDB storeData = new UserInfoDB();

                    storeData.nameofuser = nameofuserValue;

                    storeData.email = emailValue.ToLower();

                    storeData.password = passwordValue.ToLower();

                    realmDB.Write(() =>
                    {
                        realmDB.Add(storeData);
                    });
               
                    Toast.MakeText(this, "REGISTERED ", ToastLength.Short).Show();
                }
            }
        }
     

    }
}
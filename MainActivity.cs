using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System.Collections.Generic;
using Android.Database.Sqlite;
using Android.Database;
using Android.Views;
using Android.Content;

namespace XamExistingDB
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        //https://www.c-sharpcorner.com/article/xamarin-android-working-with-existing-db/
        DBHelper db;
        SQLiteDatabase sqliteDB;
        LinearLayout container;
        Button btnGetData;
        List<Account> lstUser = new List<Account>();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
           
            db = new DBHelper(this);
            sqliteDB = db.WritableDatabase;
            container = FindViewById<LinearLayout>(Resource.Id.container);
            btnGetData = FindViewById<Button>(Resource.Id.btnLoadData);
            btnGetData.Click += delegate
            {
                Data();
            };
        }
        private void Data()
        {
            ICursor selectData = sqliteDB.RawQuery("select * from Account", new string[] { });
            if (selectData.Count > 0)
            {
                selectData.MoveToFirst();
                do
                {
                    Account user = new Account();
                    user.Name = selectData.GetString(selectData.GetColumnIndex("Name"));
                    user.Email = selectData.GetString(selectData.GetColumnIndex("Email"));
                    user.Mobile = selectData.GetString(selectData.GetColumnIndex("Mobile"));
                    lstUser.Add(user);
                }
                while (selectData.MoveToNext());
                selectData.Close();
            }
            foreach (var item in lstUser)
            {
                LayoutInflater layoutInflater = (LayoutInflater)BaseContext.GetSystemService(Context.LayoutInflaterService);
                View addView = layoutInflater.Inflate(Resource.Layout.row, null);
                TextView txtName = addView.FindViewById<TextView>(Resource.Id.txtName);
                TextView txtEmail = addView.FindViewById<TextView>(Resource.Id.txtEmail);
                TextView txtMobile = addView.FindViewById<TextView>(Resource.Id.txtMobile);
                txtName.Text = item.Name;
                txtEmail.Text = item.Email;
                txtMobile.Text = item.Mobile;
                container.AddView(addView);
            }
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
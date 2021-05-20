using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XamExistingDB
{
    public class Account
    {
        [PrimaryKey]
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
    }
}
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
using SQLite;
using FlashQuizMaster.Droid;
using Xamarin.Forms;
using System.IO;

[assembly: Dependency(typeof(Database_Android))]
namespace FlashQuizMaster.Droid
{
    public class Database_Android: ISQLite
    {
        public Database_Android() { }

        public SQLiteConnection GetConnection()
        {
            var filename = "ItemsSQLite.db";
            string folder =
                System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(folder, filename);
            var connection = new SQLiteConnection(path);
            return connection;
        }
    }
}
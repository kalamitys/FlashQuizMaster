using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FlashQuizMaster.Windows;
using SQLite;
using Xamarin.Forms;
using System.IO;
using Windows.Storage;


//[assembly: Dependency(typeof(Database_Windows))]
namespace FlashQuizMaster.WinPhone
{
    public class Database_Windows : ISQLite
    {
        public Database_Windows() { }

        public SQLiteConnection GetConnection()
        {
            var filename = "ItemsSQLite.db";
            string folder = ApplicationData.Current.LocalFolder.Path;
            //string folder = @"C:\Sandrine\MobileApps\AppDatabase";
            var path = Path.Combine(folder, filename);
            var connection = new SQLiteConnection(path, true);
            return connection;
        }
    }
}

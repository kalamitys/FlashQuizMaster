using FlashQuizMaster.WinPhone;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Xamarin.Forms;




[assembly:Dependency(typeof(Database_WinPhone))]
namespace FlashQuizMaster.WinPhone
{
    public class Database_WinPhone:ISQLite
    {
        public Database_WinPhone() { }

        public SQLiteConnection GetConnection()
        {
            var filename = "ItemsSQLite.db";
            string folder = ApplicationData.Current.LocalFolder.Path;
            //string folder = @"C:\Sandrine\MobileApps\AppDatabase";
            var path = Path.Combine(folder, filename);
            var connection = new SQLiteConnection(path,true);
            return connection;
        }
    }
}

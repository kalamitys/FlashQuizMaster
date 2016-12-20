using System;
using Xamarin.Forms;
using SQLite;
using System.IO;
using FlashQuizMaster.iOS;

[assembly: Dependency(typeof(Database_iOS))]
namespace FlashQuizMaster.iOS
{
    public class Database_iOS: ISQLite
    {
        public Database_iOS() { }
        public SQLiteConnection GetConnection()
        {
            var filename = "ItemsSQLite.db";
            string folder =
                Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libraryFolder = Path.Combine(folder, "..", "Library");
            var path = Path.Combine(libraryFolder, filename);
            var connection = new SQLiteConnection(path);
            return connection;
        }
    }
}
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FlashQuizMaster
{
    public class DatabaseGeneric
    {
        static object locker = new object();

        SQLiteConnection database;

        public DatabaseGeneric()
        {
            //Create database and databade tables
            database = DependencyService.Get<ISQLite>().GetConnection();            
            database.CreateTable<User>();
            database.CreateTable<Topic>();
            database.CreateTable<Chapter>();
            database.CreateTable<Question>();
            
        }

        public IEnumerable<T> GetObjects<T>() where T : IObject, new()
        {
            lock (locker)
            {
                return (from i in database.Table<T>() select i).ToList();
            }
        }        

        public IEnumerable<T> GetFirstObjects<T>() where T : IObject, new()
        {
            lock (locker)
            {
                return database.Query<T>("SELECT * FROM Item WHERE Name = 'First'");
            }
        }

        //SDI Probleme avec cette fonction lorsqu'on passe l'id: a revoir
        public T GetObject<T>(int id) where T : IObject, new()
        {
            lock (locker)
            {
                T dbTble = database.Table<T>().FirstOrDefault();
                return dbTble;
                //return database.Table<T>().FirstOrDefault(x => x.ID == id);
            }
        }

        public int SaveObject<T>(T obj) where T : IObject
        {
            lock (locker)
            {
                if (obj.ID != 0)
                {
                    database.Update(obj);
                    return obj.ID;
                }
                else
                {
                    return database.Insert(obj);
                }
            }
        }

        public int DeleteObject<T>(int id) where T : IObject, new()
        {
            lock (locker)
            {
                return database.Delete<T>(id);
            }
        }

        public void DeleteAllObjects<T>()
        {
            lock (locker)
            {
                database.DropTable<T>();
                database.CreateTable<T>();
            }
        }

        public IEnumerable<T> GetTopicsByUserId<T>(int _UserId) where T : Topic, new()
        {
            lock (locker)
            {
                return (from i in database.Table<T>() select i).Where(t=>t.UserId == _UserId);
            }
        }

        public IEnumerable<T> GetChaptersByTopicId<T>(int _TopicId) where T : Chapter, new()
        {
            lock (locker)
            {
                return (from i in database.Table<T>() select i).Where(t => t.TopicId == _TopicId);
            }
        }

        public T GetChapterById<T>(int _Id) where T : Chapter, new()
        {
            lock (locker)
            {
                //return (from i in database.Table<T>() select i).Where(t => t.ID == _Id);
                T dbTble = database.Table<T>().Where(t => t.ID == _Id).FirstOrDefault();
                return dbTble;
            }
        }

       
        public IEnumerable<T> GetQuestionsByChapterId<T>(int _ChapterId) where T : Question, new()
        {
            lock (locker)
            {
                return (from i in database.Table<T>() select i).Where(t => t.ChaperId == _ChapterId);
            }
        }
    }
}

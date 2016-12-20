using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashQuizMaster
{
    public class DbRepository
    {
        DatabaseGeneric itemDatabase = null;

        public DbRepository()
        {
            itemDatabase = new DatabaseGeneric();
        }

        #region Item
        public Item GetItem(int id)
        {
            return itemDatabase.GetObject<Item>(id);
        }

        public IEnumerable<Item> GetFirstItems()
        {
            return itemDatabase.GetObjects<Item>();
        }

        public IEnumerable<Item> GetItems()
        {
            return itemDatabase.GetObjects<Item>();
        }

        public int SaveItem(Item item)
        {
            return itemDatabase.SaveObject<Item>(item);
        }

        public int DeleteItem(int id)
        {
            return itemDatabase.DeleteObject<Item>(id);
        }

        public void DeleteAllItems()
        {
            itemDatabase.DeleteAllObjects<Item>();
        }
        #endregion

        #region User
        public User GetUser(int id)
        {
            return itemDatabase.GetObject<User>(id);
        }

        public IEnumerable<User> GetUsers()
        {
            return itemDatabase.GetObjects<User>();
        }

        public int SaveUser(User User)
        {
            return itemDatabase.SaveObject<User>(User);
        }

        public int DeleteUserById(int id)
        {
            return itemDatabase.DeleteObject<User>(id);
        }

        public void DeleteAllUsers()
        {
            itemDatabase.DeleteAllObjects<User>();
        }
        #endregion

        #region Topic

        public int SaveTopic(Topic oneTopic)
        {
            return itemDatabase.SaveObject<Topic>(oneTopic);
        }

        public IEnumerable<Topic> GetTopics()
        {
            return itemDatabase.GetObjects<Topic>();
        }

        public IEnumerable<Topic> GetTopicsByUserId(int _conUserId)
        {
            return itemDatabase.GetTopicsByUserId<Topic>(_conUserId);
        }


        public int DeleteTopicById(int id)
        {
            return itemDatabase.DeleteObject<Topic>(id);
        }

        public void DeleteAllTopics()
        {
            itemDatabase.DeleteAllObjects<Topic>();
        }
        #endregion

        #region Chapter
        public int SaveChapter(Chapter oneChapter)
        {
            return itemDatabase.SaveObject<Chapter>(oneChapter);
        }

        public IEnumerable<Chapter> GetChapters()
        {
            return itemDatabase.GetObjects<Chapter>();
        }

        public IEnumerable<Chapter> GetChaptersByTopicId(int _selTopicId)
        {
            return itemDatabase.GetChaptersByTopicId<Chapter>(_selTopicId);
        }

        public Chapter GetChapterByChapterId(int _selChapterId)
        {
            return itemDatabase.GetChapterById<Chapter>(_selChapterId);
        }


        public int DeleteChapterById(int id)
        {
            return itemDatabase.DeleteObject<Chapter>(id);
        }

        public void DeleteAllChapters()
        {
            itemDatabase.DeleteAllObjects<Chapter>();
        }
        #endregion

        #region Questions
        public int SaveQuestion(Question oneQuestion)
        {
            return itemDatabase.SaveObject<Question>(oneQuestion);
        }

        public IEnumerable<Question> GetQuestions()
        {
            return itemDatabase.GetObjects<Question>();
        }

        public IEnumerable<Question> GetQuestionsByChapterId(int _selChapterId)
        {
            return itemDatabase.GetQuestionsByChapterId<Question>(_selChapterId);
        }


        public int DeleteQuestionById(int id)
        {
            return itemDatabase.DeleteObject<Question>(id);
        }

        public void DeleteAllQuestions()
        {
            itemDatabase.DeleteAllObjects<Question>();
        }
        #endregion
    }
}

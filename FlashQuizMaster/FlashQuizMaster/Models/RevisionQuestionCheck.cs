using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashQuizMaster
{
    public class RevisionQuestionCheck : IObject
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int UserId { get; set; }
        public int TopicId { get; set; }
        public int ChapterId { get; set; }
        public int QuestionId { get; set; }
    }
}

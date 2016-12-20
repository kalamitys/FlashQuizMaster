using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace FlashQuizMaster
{
    public class Answer:IObject
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        [MaxLength(8000)]
        public string Text { get; set; }
        public int QuestionId { get; set; }
        public bool IsCorrect { get; set; }
    }
}

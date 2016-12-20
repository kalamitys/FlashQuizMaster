using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace FlashQuizMaster
{
    public class Question:IObject
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        [MaxLength(8000)]
        public string Text { get; set; }
        public int ChaperId { get; set; }
        [MaxLength(8000)]
        public string CorrectAnswer { get; set; }
        [MaxLength(8000)]
        public string FakeAnswer1 { get; set; }
        [MaxLength(8000)]
        public string FakeAnswer2 { get; set; }
        [MaxLength(8000)]
        public string FakeAnswer3 { get; set; }
    }
}

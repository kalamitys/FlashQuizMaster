using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace FlashQuizMaster
{
    public class Topic : IObject
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        [MaxLength(400)]
        public string Name { get; set; }
        public int UserId { get; set; }
        public double CompletionRate { get; set; }
        public double SuccessRate { get; set; }
        public double ConfidenceRate { get; set; }

    }
}

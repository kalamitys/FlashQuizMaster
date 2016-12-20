using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashQuizMaster.Models
{
    public class ConfidenceDegree:IObject
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        
        public int ConfidenceRanking { get; set; }
    }
}

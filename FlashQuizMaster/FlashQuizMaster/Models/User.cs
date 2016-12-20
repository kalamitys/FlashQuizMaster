using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace FlashQuizMaster
{
    public class User : IObject
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        [MaxLength(400)]
        public string FirstName { get; set; }
        [MaxLength(400)]
        public string LastName { get; set; }
        [MaxLength(100)]
        public string LoginName { get; set; }

    }
}

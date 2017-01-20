using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _90_Days_Challenge.Models
{
    class Program
    {
        [PrimaryKey, AutoIncrement]

        public int Program_Id { get; set; }
        public int Plan_Id_Ref { get; set; }
        public int Program_Level { get; set; }

    }
}

using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _90_Days_Challenge.Models
{
    class Set
    {
        [PrimaryKey, AutoIncrement]
        public int Set_Id { get; set; }

        public string Set_type { get; set; }
        public int Set_Duration { get; set; }
        public int Set_Count { get; set; }
        public int Day_Id_Ref { get; set; }
        public int Set_Progress { get; set; }

    }
    
    
}

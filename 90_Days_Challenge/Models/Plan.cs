using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _90_Days_Challenge.Models
{
    class Plan
    {
        [PrimaryKey, AutoIncrement]
        public int Plan_Id { get; set; }
        public string Plan_Name { get; set; }
        public int Plan_Progress { get; set; }
    }
}

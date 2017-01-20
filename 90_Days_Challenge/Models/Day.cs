using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace _90_Days_Challenge.Models
{
    class Day
    {
        [PrimaryKey, AutoIncrement]
        public int Day_Id { get; set; }
        public int Day_Number { get; set; }
        public int Program_Id_Ref { get; set; }
    }
}

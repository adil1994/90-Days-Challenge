using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _90_Days_Challenge.Models
{
    class BmiHistory
    {
        [PrimaryKey, AutoIncrement]
        public int Bmi_Id { get; set; }
        public string Bmi_Date { get; set; }
        public double Bmi_Result { get; set; }

    }
}

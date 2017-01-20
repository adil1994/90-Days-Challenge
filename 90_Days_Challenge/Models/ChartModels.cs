using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _90_Days_Challenge.Models
{
    class ChartModels
    {
        public string DayNumber { get; set ; }
        public double AbsProgress { get; set; }
        public double CompleteProgress { get; set; }
        public double ChestProgress { get; set; }
        public double BackProgress { get; set; }
        public double LegsProgress { get; set; }
        public double ArmsProgress { get; set; }
    }

    class DayProductivity
    {
        public string Day { get; set; }
        public int Productivity { get; set; }
    }


}

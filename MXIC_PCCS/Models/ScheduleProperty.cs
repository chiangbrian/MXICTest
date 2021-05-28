using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MXIC_PCCS.Models
{
    public class ScheduleProperty
    {
        public string PoNo { get; set; }

        public DateTime WorkDate { get; set; }

        public string DayWeek { get; set; }

        public string EmpName { get; set; }

        public string WorkShift { get; set; }

        public string WorkGroup { get; set; }
    }
}
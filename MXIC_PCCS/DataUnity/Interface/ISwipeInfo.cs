using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MXIC_PCCS.DataUnity.Interface
{
    interface ISwipeInfo
    {
        string CheckinList(string VendorName, string EmpID, string EmpName ,DateTime? StartTime, DateTime? EndTime, string AttendTypeSelect);

        string AlarmList(string PoNo, string VendorName, string EmpName, DateTime? StartTime, DateTime? EndTime, string CheckType);

        string SwipeInfoDetail(string EditID);

        string EditSwipe(string EditID, string AttendTypeSelect, string Hour);
        
        //20210514
        string InsertSwipe(string AttendTypeSelect, string CheckType, string EmpName, DateTime SwipeTime, double Hour, string WorkShift);
        //string InsertSwipe(string PoNoSelect, string AttendTypeSelect, string CheckType, string EmpName, DateTime SwipeTime, double Hour, string WorkShift);

        string transform();

        void transform2(string StartTime, string EndTime,string PoNo);

        bool CheckPO(string PO);


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MXIC_PCCS.DataUnity.Interface
{
    interface ISwipeSet
    {

        string UserList(string PoNo, DateTime? Date);

        string EditTimeDetail(string EditID);

        string EditTime(string EditID, string SwipeStartTime, string SwipeEndTime);

    }
}
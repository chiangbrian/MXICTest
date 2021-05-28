using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using MXIC_PCCS.Models;

namespace MXIC_PCCS.DataUnity.Interface
{
    interface IScheduleSetting
    {
        /// <summary>
        /// 班表查詢
        /// </summary>
        /// <param name="SchedulDate"></param>
        /// <param name="PoNo"></param>
        /// <returns></returns>
        string ScheduleList(DateTime? StartTime, DateTime? EndTime, string PoNo);
        /// <summary>
        /// 匯入檔案
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        string ImportSchedul(HttpPostedFileBase file);
        /// <summary>
        /// 新增班表
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        string AddSchedul(ScheduleProperty Model);
        /// <summary>
        /// 清除當月般表
        /// </summary>
        /// <returns></returns>
        void CleanSchedul(string Year, string Month, string PoNo);
        /// <summary>
        /// 產出班表
        /// </summary>
        /// <param name="Year"></param>
        /// <param name="Month"></param>
        /// <param name="PoNo"></param>
        /// <returns></returns>
        MemoryStream ExportSchedul(string Year, string Month, string PoNo);
        /// <summary>
        /// 刪除班表資料
        /// </summary>
        /// <param name="ScheduleDate"></param>
        /// <param name="SchedulePoNo"></param>
        /// <returns></returns>
        string DelSchedule(string ScheduleDate, string SchedulePoNo);
    }
}

using Dapper;
using MXIC_PCCS.DataUnity.Interface;
using MXIC_PCCS.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;

namespace MXIC_PCCS.DataUnity.BusinessUnity
{
    public class SwipeInfo : ISwipeInfo, IDisposable
    {
        public PCCSContext _db = new PCCSContext();

        public MxicTestContext _dbMXIC = new MxicTestContext();

        //關閉資料庫
        #region IDisposable Support
        private bool disposedValue = false; // 偵測多餘的呼叫

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 處置 Managed 狀態 (Managed 物件)。
                    _db.Dispose();
                }

                // TODO: 釋放 Unmanaged 資源 (Unmanaged 物件) 並覆寫下方的完成項。
                // TODO: 將大型欄位設為 null。

                disposedValue = true;
            }
        }

        // TODO: 僅當上方的 Dispose(bool disposing) 具有會釋放 Unmanaged 資源的程式碼時，才覆寫完成項。
        // ~UserManagement() {
        //   // 請勿變更這個程式碼。請將清除程式碼放入上方的 Dispose(bool disposing) 中。
        //   Dispose(false);
        // }

        // 加入這個程式碼的目的在正確實作可處置的模式。
        public void Dispose()
        {
            // 請勿變更這個程式碼。請將清除程式碼放入上方的 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果上方的完成項已被覆寫，即取消下行的註解狀態。
            GC.SuppressFinalize(this);
        }
        #endregion
        public string CheckinList(string VendorName, string EmpID, string EmpName, DateTime? StartTime, DateTime? EndTime, string AttendTypeSelect)
        {
            var _List = _db.MXIC_View_Swipes.Select(x => new { x.PoNo, x.VendorName, x.EmpID, x.CheckType, x.EmpName, x.SwipeTime, x.EditID, x.AttendType, x.WorkShift }).OrderBy(x => new { x.PoNo, x.EmpID, x.SwipeTime });

            if (!string.IsNullOrWhiteSpace(VendorName))
            {
                _List = _List.Where(x => x.VendorName.ToLower().Contains(VendorName.ToLower())).OrderBy(x => new { x.PoNo, x.EmpID, x.SwipeTime });
            }

            if (!string.IsNullOrWhiteSpace(EmpID))
            {
                _List = _List.Where(x => x.EmpID.ToLower().Contains(EmpID.ToLower())).OrderBy(x => new { x.PoNo, x.EmpID, x.SwipeTime });
            }

            if (!string.IsNullOrWhiteSpace(EmpName))
            {
                _List = _List.Where(x => x.EmpName.ToLower().Contains(EmpName.ToLower())).OrderBy(x => new { x.PoNo, x.EmpID, x.SwipeTime });
            }

            if (!string.IsNullOrWhiteSpace(StartTime.ToString()) && !string.IsNullOrWhiteSpace(EndTime.ToString()))
            {   //日期加一天 再減去一秒
                DateTime End2 = EndTime.Value.AddDays(1).AddSeconds(-1);
                _List = _List.Where(x => x.SwipeTime >= StartTime && x.SwipeTime <= End2).OrderBy(x => new { x.PoNo, x.EmpID, x.SwipeTime });
            }

            if (!string.IsNullOrWhiteSpace(AttendTypeSelect))
            {
                _List = _List.Where(x => x.AttendType == AttendTypeSelect).OrderBy(x => new { x.PoNo, x.EmpID, x.SwipeTime });
            }

            string Str = JsonConvert.SerializeObject(_List, Formatting.Indented);

            return (Str);
        }

        public string AlarmList(string PoNo, string VendorName, string EmpName, DateTime? StartTime, DateTime? EndTime, string CheckType)
        {
            var _List = _db.View_Swipe_Doubles.Where(x => x.CheckSum != "1010").Select(x => new { x.PoNo, x.VendorName, x.EmpID, x.CheckType, x.EmpName, x.SwipeTime, x.WorkShift }).OrderBy(x => new { x.PoNo, x.EmpID, x.SwipeTime });

            if (!string.IsNullOrWhiteSpace(PoNo))
            {
                _List = _List.Where(x => x.PoNo.ToLower().Contains(PoNo.ToLower())).OrderBy(x => new { x.PoNo, x.EmpID, x.SwipeTime });
            }

            if (!string.IsNullOrWhiteSpace(VendorName))
            {
                _List = _List.Where(x => x.VendorName.ToLower().Contains(VendorName.ToLower())).OrderBy(x => new { x.PoNo, x.EmpID, x.SwipeTime });
            }

            if (!string.IsNullOrWhiteSpace(EmpName))
            {
                _List = _List.Where(x => x.EmpName.ToLower().Contains(EmpName.ToLower())).OrderBy(x => new { x.PoNo, x.EmpID, x.SwipeTime });
            }

            if (!string.IsNullOrWhiteSpace(StartTime.ToString()) && !string.IsNullOrWhiteSpace(EndTime.ToString()))
            {   //日期加一天 再減去一秒
                DateTime End2 = EndTime.Value.AddDays(1).AddSeconds(-1);
                _List = _List.Where(x => x.SwipeTime >= StartTime && x.SwipeTime <= End2).OrderBy(x => new { x.PoNo, x.EmpID, x.SwipeTime });
            }

            if (!string.IsNullOrWhiteSpace(CheckType))
            {
                _List = _List.Where(x => x.CheckType.ToLower().Contains(CheckType.ToLower())).OrderBy(x => new { x.PoNo, x.EmpID, x.SwipeTime });
            }

            string Str;

            if (_List.Any())
            {
                Str = JsonConvert.SerializeObject(_List, Formatting.Indented);
            }
            else
            {
                Str = "[\r\n  {\r\n    \"PoNo\": \"0000000000\",\r\n    \"VendorName\": \"測試廠商\",\r\n    \"EmpID\": \"00000000\",\r\n    \"CheckType\": \"CheckType\",\r\n    \"EmpName\": \"測試人員\",\r\n    \"SwipeTime\": \"9999-01-01T12:00:00\",\r\n    \"WorkShift\": \"測試班別\"\r\n  }\r\n]";
            }

            return (Str);
        }

        public string SwipeInfoDetail(string EditID)
        {
            var SwipeInfoDetail = _db.MXIC_SwipeInfos.Where(x => x.EditID.ToString() == EditID).Select(x => new { x.AttendType, x.Hour });

            string Str = JsonConvert.SerializeObject(SwipeInfoDetail, Formatting.Indented);

            return (Str);
        }

        public string EditSwipe(string EditID, string AttendTypeSelect, string Hour)
        {
            string str = "修改失敗";

            try
            {
                var EditSwipeInfo = _db.MXIC_SwipeInfos.Where(x => x.EditID.ToString() == EditID).FirstOrDefault();

                double time;

                if (double.TryParse(Hour, out time))
                {
                    EditSwipeInfo.AttendType = AttendTypeSelect;

                    EditSwipeInfo.Hour = time;
                }
                else
                {
                    str = "時數格式錯誤";
                }

                _db.SaveChanges();

                str = "修改成功";
            }
            catch (Exception e)
            {
                str = e.ToString();
            }

            return (str);
        }

        public string InsertSwipe(string AttendTypeSelect, string CheckType, string EmpName, DateTime SwipeTime, double Hour, string WorkShift)
        {
            //20210514
            //public string InsertSwipe(string PoNoSelect, string AttendTypeSelect, string CheckType, string EmpName, DateTime SwipeTime, double Hour, string WorkShift)
            // {
            string Str;
            try
            {
                //先確認班表有 (View_Swipe會有東西)
                var CheckSchedule = _db.MXIC_ScheduleSettings.Where(x => x.Date == SwipeTime.Date && x.EmpName == EmpName);
                
                //再Insert到刷卡資料去
                if (CheckSchedule.Any())
                {
                    var InsertSwipeInfo = new Models.SwipeInfo
                    {
                        SwipID = Guid.NewGuid(),
                        valid = "true",
                        EmpName = EmpName,
                        CheckType = CheckType,
                        SwipeTime = SwipeTime,
                        EditID = Guid.NewGuid(),
                        AttendType = AttendTypeSelect,
                        //PoNo = PoNoSelect,//20210514
                        Hour = Hour,
                        WORK_DATETIME = SwipeTime.Date
                    };

                    _db.MXIC_SwipeInfos.Add(InsertSwipeInfo);
                    _db.SaveChanges();

                    Str = "新增成功!";
                }
                else
                {
                    Str = "輸入資料與班表不相符!";
                }
            }
            catch (Exception ex)
            {
                Str = "新增失敗!" + ex.ToString();
            }
            return (Str);
        }

        public string transform()
        {
            var ATTENDLIST = _dbMXIC.FAC_ATTENDLISTs;

            string Str = JsonConvert.SerializeObject(ATTENDLIST, Formatting.Indented);

            return (Str);
        }

        public void transform2(string StartTime, string EndTime, string PoNo)
        {
            //DateTime Date = DateTime.Parse(UserDate);
            //DateTime Date = Convert.ToDateTime("2020-08-01");
            //DateTime TheMonthStart = new DateTime(Date.Year, Date.Month, 1);//本月初1號
            //DateTime TheMonthEnd = new DateTime(Date.Year, Date.Month, DateTime.DaysInMonth(Date.Year, Date.Month));//本月初月底

            // 設定查詢月份
            //DateTime StartDate = Convert.ToDateTime("2020-08-10");
            //DateTime StartDate = TheMonthStart;
            //DateTime EndDate = Convert.ToDateTime("2020-09-08");
            // DateTime EndDate = TheMonthEnd.AddDays(1).AddSeconds(-1);
            //IQueryable<Models.SwipeInfo> history = _db.MXIC_SwipeInfos;
            //IQueryable<Models.ScheduleSetting> UserSchedule = _db.MXIC_ScheduleSettings;
            //DateTime StartDate;
            //DateTime EndDate;
            //DateTime Today=DateTime.Now.Date.AddSeconds(-1);
            //if (!string.IsNullOrWhiteSpace(StartTime))
            //{
            //    StartDate = DateTime.Parse(StartTime);

            //    history = history.Where(x => x.WORK_DATETIME >= StartDate && x.WORK_DATETIME <= Today);

            //    UserSchedule = UserSchedule.Where(x => x.Date >= StartDate&&x.PoNo==PoNo);

            //}

            //if (!string.IsNullOrWhiteSpace(EndTime))
            //{
            //    EndDate = DateTime.Parse(EndTime);

            //    history = history.Where(x => x.WORK_DATETIME < EndDate);

            //    UserSchedule = UserSchedule.Where(x =>  x.Date < EndDate && x.PoNo == PoNo);

            //}

            DateTime StartDate = DateTime.Parse("1900/01/01");

            DateTime EndDate = DateTime.Parse("2999/12/31");

            IQueryable<Models.ScheduleSetting> UserSchedule = _db.MXIC_ScheduleSettings;

            if (!string.IsNullOrWhiteSpace(StartTime) && !string.IsNullOrWhiteSpace(EndTime))
            {
                StartDate = DateTime.Parse(StartTime);

                EndDate = DateTime.Parse(EndTime).AddDays(1).AddSeconds(-1);
            }

            //如果PONO為空
            if (string.IsNullOrWhiteSpace(PoNo))
            {
                var history = _db.MXIC_SwipeInfos.Where(x => x.WORK_DATETIME >= StartDate && x.WORK_DATETIME < EndDate).ToList();
                if (history.Any())
                {
                    foreach (var item in history)
                    {
                        _db.MXIC_SwipeInfos.Remove(item);
                    }
                    _db.SaveChanges();
                }
                UserSchedule = UserSchedule.Where(x => x.Date >= StartDate && x.Date < EndDate);
            }
            else
            {
                var UserList = _db.MXIC_ScheduleSettings.Where(x => x.PoNo == PoNo);
                UserSchedule = UserSchedule.Where(x => x.Date >= StartDate && x.Date < EndDate && x.PoNo == PoNo);
                foreach (var item in UserList)
                {
                    // Stored Procedure
                    using (SqlConnection Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["MXIC_PCCS"].ConnectionString))
                    {
                        // 準備參數
                        var parameters = new DynamicParameters();
                        //Stored Procedure 變數 ,程式變數,資料型態,輸入
                        parameters.Add("@EmpName", item.EmpName, DbType.String, ParameterDirection.Input);
                        parameters.Add("@StratTime", StartDate, DbType.DateTime, ParameterDirection.Input);
                        parameters.Add("@EndTime", EndDate, DbType.DateTime, ParameterDirection.Input);
                        Conn.Execute("SP_DelSwipeInfo", parameters, commandType: CommandType.StoredProcedure);
                    }
                }
            }

            // 班表
            //IQueryable<Models.ScheduleSetting> UserSchedule = _db.MXIC_ScheduleSettings.Where(x => x.Date >= StartDate && x.Date < EndDate);
            //var UserSchedule = _db.MXIC_ScheduleSettings;
            foreach (var item in UserSchedule)
            {
                string InAttendType = "正常";

                string OutAttendType = "正常";

                //某人某天的打卡紀錄
                var ATTENDLIST = _dbMXIC.FAC_ATTENDLISTs.Where(x => x.WORKER_NAME == item.EmpName && x.WORK_DATETIME == item.Date && x.TBM_DATETIME != null && (x.ENTRANCE_DATETIME != null || x.EXIT_DATETIME != null));

                //沒有刷臉紀錄&班別為休
                if (ATTENDLIST.Any() == false && item.WorkShift == "休")
                {

                }
                else
                {
                    DateTime ENTRANCE;

                    DateTime EXIT;

                    //如果某人某天無打卡紀錄又不是休假的話出席狀態曠職
                    if (ATTENDLIST.Any() == false && item.WorkShift != "休")
                    {
                        ENTRANCE = item.Date;

                        EXIT = item.Date;

                        //InAttendType = "曠職";
                        //OutAttendType = "曠職";

                        //var SwipeIN = new Models.SwipeInfo();
                        //SwipeIN.CheckType = "CHECKIN";
                        //SwipeIN.SwipeTime = ENTRANCE;
                        //SwipeIN.EmpName = item.EmpName;
                        //SwipeIN.EditID = Guid.NewGuid();
                        //SwipeIN.SwipID = Guid.NewGuid();
                        //SwipeIN.Hour = 0;
                        //SwipeIN.AttendType = InAttendType;
                        //SwipeIN.valid = "true";
                        //SwipeIN.WORK_DATETIME = item.Date;
                        //_db.MXIC_SwipeInfos.Add(SwipeIN);

                        //var SwipeOUT = new Models.SwipeInfo();
                        //SwipeOUT.CheckType = "CHECKOUT";
                        //SwipeOUT.SwipeTime = EXIT;
                        //SwipeOUT.EmpName = item.EmpName;
                        //SwipeOUT.EditID = Guid.NewGuid();
                        //SwipeOUT.SwipID = Guid.NewGuid();
                        //SwipeOUT.Hour = 0;
                        //SwipeOUT.AttendType = OutAttendType;
                        //SwipeOUT.valid = "true";
                        //SwipeOUT.WORK_DATETIME = item.Date;
                        //_db.MXIC_SwipeInfos.Add(SwipeOUT);

                        if (item.WorkShift == "排休")
                        {
                            InAttendType = "排休";
                            OutAttendType = "排休";

                            var SwipeIN = new Models.SwipeInfo();
                            SwipeIN.CheckType = "CHECKIN";
                            SwipeIN.SwipeTime = ENTRANCE;
                            SwipeIN.EmpName = item.EmpName;
                            SwipeIN.EditID = Guid.NewGuid();
                            SwipeIN.SwipID = Guid.NewGuid();
                            SwipeIN.Hour = 8;
                            SwipeIN.AttendType = InAttendType;
                            SwipeIN.valid = "true";
                            SwipeIN.WORK_DATETIME = item.Date;
                            _db.MXIC_SwipeInfos.Add(SwipeIN);

                            var SwipeOUT = new Models.SwipeInfo();
                            SwipeOUT.CheckType = "CHECKOUT";
                            SwipeOUT.SwipeTime = EXIT;
                            SwipeOUT.EmpName = item.EmpName;
                            SwipeOUT.EditID = Guid.NewGuid();
                            SwipeOUT.SwipID = Guid.NewGuid();
                            SwipeOUT.Hour = 8;
                            SwipeOUT.AttendType = OutAttendType;
                            SwipeOUT.valid = "true";
                            SwipeOUT.WORK_DATETIME = item.Date;
                            _db.MXIC_SwipeInfos.Add(SwipeOUT);
                        }
                        else
                        {
                            InAttendType = "曠職";
                            OutAttendType = "曠職";

                            var SwipeIN = new Models.SwipeInfo();
                            SwipeIN.CheckType = "CHECKIN";
                            SwipeIN.SwipeTime = ENTRANCE;
                            SwipeIN.EmpName = item.EmpName;
                            SwipeIN.EditID = Guid.NewGuid();
                            SwipeIN.SwipID = Guid.NewGuid();
                            SwipeIN.Hour = 0;
                            SwipeIN.AttendType = InAttendType;
                            SwipeIN.valid = "true";
                            SwipeIN.WORK_DATETIME = item.Date;
                            _db.MXIC_SwipeInfos.Add(SwipeIN);

                            var SwipeOUT = new Models.SwipeInfo();
                            SwipeOUT.CheckType = "CHECKOUT";
                            SwipeOUT.SwipeTime = EXIT;
                            SwipeOUT.EmpName = item.EmpName;
                            SwipeOUT.EditID = Guid.NewGuid();
                            SwipeOUT.SwipID = Guid.NewGuid();
                            SwipeOUT.Hour = 0;
                            SwipeOUT.AttendType = OutAttendType;
                            SwipeOUT.valid = "true";
                            SwipeOUT.WORK_DATETIME = item.Date;
                            _db.MXIC_SwipeInfos.Add(SwipeOUT);
                        }
                    }
                    //有打卡紀錄
                    else
                    {
                        var MonthFirstDay = item.Date.AddDays(-item.Date.Day + 1);
                        var SettingList = _db.SwipeSets.Where(x => x.PoNo == item.PoNo && x.Date == MonthFirstDay);
                        var DaySwipeSet = SettingList.Where(x => x.WorkShift == "日班").FirstOrDefault();
                        var MornSwipeSet = SettingList.Where(x => x.WorkShift == "早班").FirstOrDefault();
                        var NightSwipeSet = SettingList.Where(x => x.WorkShift == "夜班").FirstOrDefault();

                        foreach (var AttendanceRecord in ATTENDLIST)
                        {
                            //上班時間                      
                            //如果CHECKINtime不是空
                            if (!string.IsNullOrWhiteSpace(AttendanceRecord.ENTRANCE_DATETIME.ToString()))
                            {
                                DateTime CHECKINtime = Convert.ToDateTime(AttendanceRecord.ENTRANCE_DATETIME.Value.ToString("HH:mm"));
                                ENTRANCE = AttendanceRecord.ENTRANCE_DATETIME.Value;

                                DateTime LateTimeDay;
                                InAttendType = "正常";

                                switch (item.WorkShift)
                                {   //如果此人是早班
                                    case "早":
                                        LateTimeDay = Convert.ToDateTime(MornSwipeSet.SwipeStartTime);
                                        // 上班時間比07:00晚=遲到
                                        if (CHECKINtime > LateTimeDay)
                                        {
                                            InAttendType = "異常";
                                        }
                                        break;
                                    //如果此人是常日班
                                    case "日":
                                        LateTimeDay = Convert.ToDateTime(DaySwipeSet.SwipeStartTime);
                                        // 上班時間比08:30晚=遲到
                                        if (CHECKINtime > LateTimeDay)
                                        {
                                            InAttendType = "異常";
                                        }
                                        break;
                                    //如果此人是夜班
                                    case "夜":
                                        LateTimeDay = Convert.ToDateTime(NightSwipeSet.SwipeStartTime);
                                        // 上班時間比19:00晚=遲到
                                        if (CHECKINtime > LateTimeDay)
                                        {
                                            InAttendType = "異常";
                                        }
                                        break;
                                    //如果此人是代早班
                                    case "代早":
                                        InAttendType = "代早";
                                        LateTimeDay = Convert.ToDateTime(MornSwipeSet.SwipeStartTime);
                                        // 上班時間比07:00晚=遲到
                                        if (CHECKINtime > LateTimeDay)
                                        {
                                            InAttendType = "異常";
                                        }
                                        break;
                                    //如果此人是代常日班
                                    case "代日":
                                        InAttendType = "代日";
                                        LateTimeDay = Convert.ToDateTime(DaySwipeSet.SwipeStartTime);
                                        // 上班時間比08:30晚=遲到
                                        if (CHECKINtime > LateTimeDay)
                                        {
                                            InAttendType = "異常";
                                        }
                                        break;
                                    //如果此人是代夜班
                                    case "代夜":
                                        InAttendType = "代夜";
                                        LateTimeDay = Convert.ToDateTime(NightSwipeSet.SwipeStartTime);
                                        // 上班時間比19:00晚=遲到
                                        if (CHECKINtime > LateTimeDay)
                                        {
                                            InAttendType = "異常";
                                        }
                                        break;
                                    default:
                                        InAttendType = "異常";
                                        break;
                                }

                                var SwipeIN = new Models.SwipeInfo();
                                SwipeIN.CheckType = "CHECKIN";
                                SwipeIN.SwipeTime = ENTRANCE;
                                SwipeIN.EmpName = item.EmpName;
                                SwipeIN.EditID = Guid.NewGuid();
                                SwipeIN.SwipID = Guid.NewGuid();
                                SwipeIN.Hour = 0;
                                SwipeIN.AttendType = InAttendType;
                                SwipeIN.valid = "true";
                                SwipeIN.WORK_DATETIME = item.Date;
                                _db.MXIC_SwipeInfos.Add(SwipeIN);
                            }
                            else
                            {
                                InAttendType = "異常";
                                ENTRANCE = item.Date;
                                var SwipeIN = new Models.SwipeInfo();
                                SwipeIN.CheckType = "CHECKIN";
                                SwipeIN.SwipeTime = ENTRANCE;
                                SwipeIN.EmpName = item.EmpName;
                                SwipeIN.EditID = Guid.NewGuid();
                                SwipeIN.SwipID = Guid.NewGuid();
                                SwipeIN.Hour = 0;
                                SwipeIN.AttendType = InAttendType;
                                SwipeIN.valid = "true";
                                SwipeIN.WORK_DATETIME = item.Date;
                                _db.MXIC_SwipeInfos.Add(SwipeIN);
                            }
                            //下班時間
                            if (!string.IsNullOrWhiteSpace(AttendanceRecord.EXIT_DATETIME.ToString()))
                            {
                                DateTime CHECKOUTtime = Convert.ToDateTime(AttendanceRecord.EXIT_DATETIME.Value.ToString("HH:mm"));
                                EXIT = AttendanceRecord.EXIT_DATETIME.Value;

                                DateTime EarlyTimeDay;
                                OutAttendType = "正常";

                                switch (item.WorkShift)
                                {   //如果此人是早班
                                    case "早":
                                        EarlyTimeDay = Convert.ToDateTime(MornSwipeSet.SwipeEndTime);
                                        //下班時間比19:30晚=加班 下班時間比19:00早=早退
                                        if (CHECKOUTtime < EarlyTimeDay)
                                        {
                                            OutAttendType = "異常";
                                        }
                                        break;
                                    //如果此人是常日班
                                    case "日":
                                        EarlyTimeDay = Convert.ToDateTime(DaySwipeSet.SwipeEndTime);
                                        //下班時間比18:00晚=加班 下班時間比17:30早=早退
                                        if (CHECKOUTtime < EarlyTimeDay)
                                        {
                                            OutAttendType = "異常";
                                        }
                                        break;
                                    //如果此人是夜班
                                    case "夜":
                                        EarlyTimeDay = Convert.ToDateTime(NightSwipeSet.SwipeEndTime);
                                        //下班時間比07:30晚=加班 下班時間比07:00早=早退
                                        if (CHECKOUTtime < EarlyTimeDay)
                                        {
                                            OutAttendType = "異常";
                                        }
                                        break;
                                    //如果此人是代早班
                                    case "代早":
                                        EarlyTimeDay = Convert.ToDateTime(MornSwipeSet.SwipeEndTime);
                                        OutAttendType = "代早";
                                        //下班時間比19:30晚=加班 下班時間比19:00早=早退
                                        if (CHECKOUTtime < EarlyTimeDay)
                                        {
                                            OutAttendType = "異常";
                                        }
                                        break;
                                    //如果此人是代常日班
                                    case "代日":
                                        EarlyTimeDay = Convert.ToDateTime(DaySwipeSet.SwipeEndTime);
                                        OutAttendType = "代日";
                                        //下班時間比18:00晚=加班 下班時間比17:30早=早退
                                        if (CHECKOUTtime < EarlyTimeDay)
                                        {
                                            OutAttendType = "異常";
                                        }
                                        break;
                                    //如果此人是代夜班
                                    case "代夜":
                                        EarlyTimeDay = Convert.ToDateTime(NightSwipeSet.SwipeEndTime);
                                        OutAttendType = "代夜";
                                        //下班時間比07:30晚=加班 下班時間比07:00早=早退
                                        if (CHECKOUTtime < EarlyTimeDay)
                                        {
                                            OutAttendType = "異常";
                                        }
                                        break;
                                    //如果此人是代早代晚或是班表休假但是有打卡紀錄
                                    default:
                                        OutAttendType = "異常";
                                        break;
                                }

                                var SwipeOUT = new Models.SwipeInfo();
                                SwipeOUT.CheckType = "CHECKOUT";
                                SwipeOUT.SwipeTime = EXIT;
                                SwipeOUT.EmpName = item.EmpName;
                                SwipeOUT.EditID = Guid.NewGuid();
                                SwipeOUT.SwipID = Guid.NewGuid();
                                SwipeOUT.Hour = 0;
                                SwipeOUT.AttendType = OutAttendType;
                                SwipeOUT.valid = "true";
                                SwipeOUT.WORK_DATETIME = item.Date;
                                _db.MXIC_SwipeInfos.Add(SwipeOUT);
                            }
                            else
                            {
                                EXIT = item.Date;
                                OutAttendType = "異常";
                                var SwipeOUT = new Models.SwipeInfo();
                                SwipeOUT.CheckType = "CHECKOUT";
                                SwipeOUT.SwipeTime = EXIT;
                                SwipeOUT.EmpName = item.EmpName;
                                SwipeOUT.EditID = Guid.NewGuid();
                                SwipeOUT.SwipID = Guid.NewGuid();
                                SwipeOUT.Hour = 0;
                                SwipeOUT.AttendType = OutAttendType;
                                SwipeOUT.valid = "true";
                                SwipeOUT.WORK_DATETIME = item.Date;
                                _db.MXIC_SwipeInfos.Add(SwipeOUT);
                            }
                        }
                    }
                }
            }
            _db.SaveChanges();
        }

        public bool CheckPO(string PO)
        {
            var WithPO = _db.MXIC_Quotations.Where(x => x.PoNo == PO).Any();
            return WithPO;
        }
    }
}
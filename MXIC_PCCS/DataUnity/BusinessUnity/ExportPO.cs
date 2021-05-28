using MXIC_PCCS.DataUnity.Interface;
using MXIC_PCCS.Models;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Configuration;
using Spire.Xls;

namespace MXIC_PCCS.DataUnity.BusinessUnity
{
    public class ExportPO : IExportPO, IDisposable
    {
        //開啟資料庫連結
        public PCCSContext _db = new PCCSContext();

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

        private void DownloadExcel(string PoNo, string VendorName, DateTime Date, List<CalculationQuotation> QuotationList)
        {
            //Step 1. 寫入EXCEL 
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var ExcelPath = ConfigurationManager.AppSettings["DowloadDirectory"]; 
            var DownloadPath = new FileInfo(ExcelPath);
            using (var Excel = new ExcelPackage(DownloadPath))
            {
                ExcelWorksheet WorkSheet = Excel.Workbook.Worksheets["PO報價"];
                WorkSheet.PrinterSettings.FitToPage = true;
                WorkSheet.PrinterSettings.FitToHeight = 0;
                WorkSheet.PrinterSettings.FitToWidth = 1;

                //Step 1-1.把數量寫入計價單   
                foreach (var QuotationItem in QuotationList)
                {
                    if (QuotationItem.Count != 0)
                    {
                        switch (QuotationItem.PoClassID)
                        {
                            #region 把數量寫入計價單
                            case "10-1":
                                WorkSheet.Cells[9, 7].Value = QuotationItem.Count;
                                break;
                            case "10-2":
                                WorkSheet.Cells[10, 7].Value = QuotationItem.Count;
                                break;
                            case "10-3":
                                WorkSheet.Cells[11, 7].Value = QuotationItem.Count;
                                break;
                            case "10-4":
                                WorkSheet.Cells[12, 7].Value = QuotationItem.Count;
                                break;
                            case "10-5":
                                WorkSheet.Cells[13, 7].Value = QuotationItem.Count;
                                break;
                            case "10-6":
                                WorkSheet.Cells[14, 7].Value = QuotationItem.Count;
                                break;
                            case "10-7":
                                WorkSheet.Cells[15, 7].Value = QuotationItem.Count;
                                break;
                            case "10-8":
                                WorkSheet.Cells[16, 7].Value = QuotationItem.Count;
                                break;
                            case "10-9":
                                WorkSheet.Cells[17, 7].Value = QuotationItem.Count;
                                break;
                            case "10-10":
                                WorkSheet.Cells[18, 7].Value = QuotationItem.Count;
                                break;
                            case "10-11":
                                WorkSheet.Cells[19, 7].Value = QuotationItem.Count;
                                break;
                            case "10-12":
                                WorkSheet.Cells[20, 7].Value = QuotationItem.Count;
                                break;
                            case "10-13":
                                WorkSheet.Cells[21, 7].Value = QuotationItem.Count;
                                break;
                            case "10-14":
                                WorkSheet.Cells[22, 7].Value = QuotationItem.Count;
                                break;
                            case "10-15":
                                WorkSheet.Cells[23, 7].Value = QuotationItem.Count;
                                break;
                            case "10-16":
                                WorkSheet.Cells[24, 7].Value = QuotationItem.Count;
                                break;
                            case "10-17":
                                WorkSheet.Cells[25, 7].Value = QuotationItem.Count;
                                break;
                            case "10-18":
                                WorkSheet.Cells[26, 7].Value = QuotationItem.Count;
                                break;
                            case "10-19":
                                WorkSheet.Cells[27, 7].Value = QuotationItem.Count;
                                break;
                            case "10-20":
                                WorkSheet.Cells[28, 7].Value = QuotationItem.Count;
                                break;
                            case "10-21":
                                WorkSheet.Cells[29, 7].Value = QuotationItem.Count;
                                break;
                            case "10-22":
                                WorkSheet.Cells[30, 7].Value = QuotationItem.Count;
                                break;
                            case "10-23":
                                WorkSheet.Cells[31, 7].Value = QuotationItem.Count;
                                break;
                            case "10-24":
                                WorkSheet.Cells[32, 7].Value = QuotationItem.Count;
                                break;
                            case "10-25":
                                WorkSheet.Cells[33, 7].Value = QuotationItem.Count;
                                break;
                            default:
                                break;
                                #endregion
                        }
                    }
                }

                //Vendor
                WorkSheet.Cells[5, 8].Value = VendorName;
                //PO
                WorkSheet.Cells[7, 8].Value = PoNo;
                //申請月份
                //WorkSheet.Cells[9, 1].Value = Date.ToString("yyyy/MM");

                Excel.Save();

                MemoryStream fileStream = new MemoryStream();
                using (var workbook = new Workbook())
                {

                    workbook.LoadFromStream(Excel.Stream as MemoryStream);
                    workbook.SaveToStream(fileStream, Spire.Xls.FileFormat.PDF);
                    workbook.SaveToFile(ConfigurationManager.AppSettings["DowloadPDF"]);
                }
                Excel.Dispose();
                fileStream.Position = 0;
            }
        }

        private void UsualFunction(string PoNo, DateTime StartDate, DateTime EndDate, string ProcessCode, IQueryable<Models.SwipeInfo> SwipeData, List<CalculationQuotation> QuotationItem)
        {
            var EmpList = _db.MXIC_ScheduleSettings.Where(x => x.PoNo == PoNo && x.Date >= StartDate && x.Date <= EndDate).Select(x => new { x.EmpName, x.WorkGroup }).Distinct();

            var LisenceList = _db.MXIC_LisenceManagements.Where(x => x.PoNo == PoNo);

            switch (ProcessCode)
            {
                #region 正常
                case "正常":
                    foreach (var Emp in EmpList)
                    {
                        var WithLisence = LisenceList.Where(x => x.EmpName == Emp.EmpName && x.EndDate < EndDate);
                        //var WithLisence = LisenceList.Where(x => x.EmpName == Emp.EmpName && x.EndDate > EndDate);

                        if (WithLisence.Count() == 0)             //有證照 20210517
                        //if (WithLisence.Count() >= 2)
                        {
                            switch (Emp.WorkGroup)                //班別
                            {
                                case "組長":
                                    //Console.WriteLine("10-1");
                                    QuotationItem[1 - 1].Count = QuotationItem[1 - 1].Count + 1;
                                    break;
                                case "常日":
                                    //Console.WriteLine("10-10");
                                    QuotationItem[10 - 1].Count = QuotationItem[10 - 1].Count + 1;
                                    break;
                                case "早":
                                    //Console.WriteLine("10-8");
                                    QuotationItem[8 - 1].Count = QuotationItem[8 - 1].Count + 1;
                                    break;
                                case "夜":
                                    //Console.WriteLine("10-9");
                                    QuotationItem[9 - 1].Count = QuotationItem[9 - 1].Count + 1;
                                    break;
                                default:
                                    break;
                            }
                        }
                        else                                        //無證照
                        {
                            switch (Emp.WorkGroup)                  //班別
                            {
                                case "組長":
                                    //Console.WriteLine("10-13");
                                    QuotationItem[13 - 1].Count = QuotationItem[13 - 1].Count + 1;
                                    break;
                                case "常日":
                                    //Console.WriteLine("10-13");
                                    QuotationItem[13 - 1].Count = QuotationItem[13 - 1].Count + 1;
                                    break;
                                case "早":
                                    //Console.WriteLine("10-11");
                                    QuotationItem[11 - 1].Count = QuotationItem[11 - 1].Count + 1;
                                    break;
                                case "夜":
                                    //Console.WriteLine("10-12");
                                    QuotationItem[12 - 1].Count = QuotationItem[12 - 1].Count + 1;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    break;
                #endregion

                #region 加項
                case "加項":
                    foreach (var SwipeRowData in SwipeData)
                    {
                        var WithLisence = LisenceList.Where(x => x.EmpName == SwipeRowData.EmpName && x.EndDate < EndDate);
                        var EmpInfo = EmpList.Where(x => x.EmpName == SwipeRowData.EmpName);

                        foreach (var EmpRowData in EmpInfo)
                        {
                            if (SwipeRowData.AttendType.Contains("加"))       //支援
                            {
                                if (WithLisence.Count() == 0)                 //有證照
                                {
                                    switch (EmpRowData.WorkGroup)             //班別
                                    {
                                        case "組長":
                                            //Console.WriteLine("10-14(填入時數)"); //SwipeRowData.Hour
                                            QuotationItem[14 - 1].Count = QuotationItem[14 - 1].Count + SwipeRowData.Hour;
                                            break;
                                        case "常日":
                                            //Console.WriteLine("10-14(填入時數)"); //SwipeRowData.Hour
                                            QuotationItem[14 - 1].Count = QuotationItem[14 - 1].Count + SwipeRowData.Hour;
                                            break;
                                        case "早":
                                            //Console.WriteLine("10-14(填入時數)"); //SwipeRowData.Hour
                                            QuotationItem[14 - 1].Count = QuotationItem[14 - 1].Count + SwipeRowData.Hour;
                                            break;
                                        case "夜":
                                            //Console.WriteLine("10-15(填入時數)"); //SwipeRowData.Hour
                                            QuotationItem[15 - 1].Count = QuotationItem[15 - 1].Count + SwipeRowData.Hour;
                                            break;
                                        default:
                                            break;
                                    }
                                }
                                else                                          //沒證照
                                {
                                    switch (EmpRowData.WorkGroup)             //班別
                                    {
                                        case "組長":
                                            //Console.WriteLine("10-16(填入時數)"); //SwipeRowData.Hour
                                            QuotationItem[16 - 1].Count = QuotationItem[16 - 1].Count + SwipeRowData.Hour;
                                            break;
                                        case "常日":
                                            //Console.WriteLine("10-16(填入時數)"); //SwipeRowData.Hour
                                            QuotationItem[16 - 1].Count = QuotationItem[16 - 1].Count + SwipeRowData.Hour;
                                            break;
                                        case "早":
                                            //Console.WriteLine("10-16(填入時數)"); //SwipeRowData.Hour
                                            QuotationItem[16 - 1].Count = QuotationItem[16 - 1].Count + SwipeRowData.Hour;
                                            break;
                                        case "夜":
                                            //Console.WriteLine("10-17(填入時數)"); //SwipeRowData.Hour
                                            QuotationItem[17 - 1].Count = QuotationItem[17 - 1].Count + SwipeRowData.Hour;
                                            break;
                                        default:
                                            break;
                                    }
                                }
                            }
                            else                                              //加班
                            {
                                if (WithLisence.Count() == 0)                 //有證照
                                {
                                    switch (EmpRowData.WorkGroup)             //班別
                                    {
                                        case "組長":
                                            //Console.WriteLine("10-4(填入時數)"); //SwipeRowData.Hour
                                            QuotationItem[4 - 1].Count = QuotationItem[4 - 1].Count + SwipeRowData.Hour;
                                            break;
                                        case "常日":
                                            //Console.WriteLine("10-4(填入時數)"); //SwipeRowData.Hour
                                            QuotationItem[4 - 1].Count = QuotationItem[4 - 1].Count + SwipeRowData.Hour;
                                            break;
                                        case "早":
                                            //Console.WriteLine("10-2(填入時數)"); //SwipeRowData.Hour
                                            QuotationItem[2 - 1].Count = QuotationItem[2 - 1].Count + SwipeRowData.Hour;
                                            break;
                                        case "夜":
                                            //Console.WriteLine("10-3(填入時數)"); //SwipeRowData.Hour
                                            QuotationItem[3 - 1].Count = QuotationItem[3 - 1].Count + SwipeRowData.Hour;
                                            break;
                                        default:
                                            break;

                                    }
                                }
                                else                                          //沒證照
                                {
                                    switch (EmpRowData.WorkGroup)             //班別
                                    {
                                        case "組長":
                                            //Console.WriteLine("10-7(填入時數)"); //SwipeRowData.Hour
                                            QuotationItem[7 - 1].Count = QuotationItem[7 - 1].Count + SwipeRowData.Hour;
                                            break;
                                        case "常日":
                                            //Console.WriteLine("10-7(填入時數)"); //SwipeRowData.Hour
                                            QuotationItem[7 - 1].Count = QuotationItem[7 - 1].Count + SwipeRowData.Hour;
                                            break;
                                        case "早":
                                            //Console.WriteLine("10-5(填入時數)"); //SwipeRowData.Hour
                                            QuotationItem[5 - 1].Count = QuotationItem[5 - 1].Count + SwipeRowData.Hour;
                                            break;
                                        case "夜":
                                            //Console.WriteLine("10-6(填入時數)"); //SwipeRowData.Hour
                                            QuotationItem[6 - 1].Count = QuotationItem[6 - 1].Count + SwipeRowData.Hour;
                                            break;
                                        default:
                                            break;
                                    }
                                }
                            }
                        }
                    }
                    break;
                #endregion

                #region 扣項
                case "扣項":
                    foreach (var Emp in EmpList)
                    {
                        var WithUnusual = SwipeData.Where(x => x.EmpName == Emp.EmpName);
                        var WithLisence = LisenceList.Where(x => x.EmpName == Emp.EmpName && x.EndDate < EndDate);

                        if (WithUnusual.Count() == 0)                 //正常
                        {
                            if (WithLisence.Count() == 0)             //有證照
                            {
                                switch (Emp.WorkGroup)                //班別
                                {
                                    case "組長":
                                        //Console.WriteLine("10-1");
                                        QuotationItem[1 - 1].Count = QuotationItem[1 - 1].Count + 1;
                                        break;
                                    case "常日":
                                        //Console.WriteLine("10-10");
                                        QuotationItem[10 - 1].Count = QuotationItem[10 - 1].Count + 1;
                                        break;
                                    case "早":
                                        //Console.WriteLine("10-8");
                                        QuotationItem[8 - 1].Count = QuotationItem[8 - 1].Count + 1;
                                        break;
                                    case "夜":
                                        //Console.WriteLine("10-9");
                                        QuotationItem[9 - 1].Count = QuotationItem[9 - 1].Count + 1;
                                        break;
                                    default:
                                        break;
                                }
                            }
                            else                                        //沒證照
                            {
                                switch (Emp.WorkGroup)                  //班別
                                {
                                    case "組長":
                                        //Console.WriteLine("10-13");
                                        QuotationItem[13 - 1].Count = QuotationItem[13 - 1].Count + 1;
                                        break;
                                    case "常日":
                                        //Console.WriteLine("10-13");
                                        QuotationItem[13 - 1].Count = QuotationItem[13 - 1].Count + 1;
                                        break;
                                    case "早":
                                        //Console.WriteLine("10-11");
                                        QuotationItem[11 - 1].Count = QuotationItem[11 - 1].Count + 1;
                                        break;
                                    case "夜":
                                        //Console.WriteLine("10-12");
                                        QuotationItem[12 - 1].Count = QuotationItem[12 - 1].Count + 1;
                                        break;
                                    default:
                                        break;
                                }
                            }

                        }
                        else
                        {
                            var WorkingHour = 240.0;

                            foreach (var UnusualData in WithUnusual)
                            {
                                WorkingHour = WorkingHour - UnusualData.Hour;
                            }

                            if (WithLisence.Count() == 0)             //有證照
                            {
                                switch (Emp.WorkGroup)                //班別
                                {
                                    case "組長":
                                        //Console.WriteLine("10-18");
                                        QuotationItem[18 - 1].Count = QuotationItem[18 - 1].Count + Math.Floor(WorkingHour);
                                        break;
                                    case "常日":
                                        //Console.WriteLine("10-18");
                                        QuotationItem[18 - 1].Count = QuotationItem[18 - 1].Count + Math.Floor(WorkingHour);
                                        break;
                                    case "早":
                                        //Console.WriteLine("10-18");
                                        QuotationItem[18 - 1].Count = QuotationItem[18 - 1].Count + Math.Floor(WorkingHour);
                                        break;
                                    case "夜":
                                        //Console.WriteLine("10-20");
                                        QuotationItem[20 - 1].Count = QuotationItem[20 - 1].Count + Math.Floor(WorkingHour);
                                        break;
                                    default:
                                        break;
                                }
                                if (!Emp.WorkGroup.Contains("常日備用") && WorkingHour.ToString().Contains(".5"))
                                {
                                    if (Emp.WorkGroup.Contains("夜"))
                                    {
                                        QuotationItem[21 - 1].Count = QuotationItem[21 - 1].Count + 1;
                                    }
                                    else
                                    {
                                        QuotationItem[19 - 1].Count = QuotationItem[19 - 1].Count + 1;
                                    }
                                }
                            }
                            else                                      //沒證照
                            {
                                switch (Emp.WorkGroup)                //班別
                                {
                                    case "組長":
                                        //Console.WriteLine("10-18");
                                        QuotationItem[22 - 1].Count = QuotationItem[22 - 1].Count + Math.Floor(WorkingHour);
                                        break;
                                    case "常日":
                                        //Console.WriteLine("10-18");
                                        QuotationItem[22 - 1].Count = QuotationItem[22 - 1].Count + Math.Floor(WorkingHour);
                                        break;
                                    case "早":
                                        //Console.WriteLine("10-18");
                                        QuotationItem[22 - 1].Count = QuotationItem[22 - 1].Count + Math.Floor(WorkingHour);
                                        break;
                                    case "夜":
                                        //Console.WriteLine("10-20");
                                        QuotationItem[24 - 1].Count = QuotationItem[24 - 1].Count + Math.Floor(WorkingHour);
                                        break;
                                    default:
                                        break;
                                }
                                if (!Emp.WorkGroup.Contains("常日備用") && WorkingHour.ToString().Contains(".5"))
                                {
                                    if (Emp.WorkGroup.Contains("夜"))
                                    {
                                        QuotationItem[25 - 1].Count = QuotationItem[25 - 1].Count + 1;
                                    }
                                    else
                                    {
                                        QuotationItem[23 - 1].Count = QuotationItem[23 - 1].Count + 1;
                                    }
                                }
                            }
                        }
                    }
                    break;
                #endregion

                default:
                    break;
            }
        }

        public string CalcuationPO(string PoNo, DateTime Date)
        {
            //這個月一半有證照 一半沒有的(X) 還不會寫 QQ 

            string reponseStr = "寫入成功";

            string PoNumber = "", VendorName = "";

            //本月初
            DateTime TheMonthStart = new DateTime(Date.Year, Date.Month, 1);//1號

            //本月底
            DateTime TheMonthEnd = new DateTime(Date.Year, Date.Month, DateTime.DaysInMonth(Date.Year, Date.Month)).AddDays(1).AddSeconds(-1);//月底

            //刷卡資料有異常未修改的狀態，就不給匯出計價單!!
            #region 刷卡資料有異常未修改的狀態，就不給匯出計價單!!

            var AttendError = _db.MXIC_View_Swipes.Where(x => x.AttendType == "異常" && x.Date >= TheMonthStart && x.Date <= TheMonthEnd);

            var Order = _db.MXIC_Quotations.Where(x => x.PoNo == PoNo);

            if (AttendError.Any())
            {
                reponseStr = "刷卡有異常資料未修改";
            }
            else if (!Order.Any())
            {
                reponseStr = "無此PO";
            }

            #endregion
            else
            {
                try
                {
                    //撈出報價單的項目資料
                    #region 撈出報價單的項目資料
                    var QuotationList = _db.MXIC_Quotations.OrderBy(x => x.Sequence).Where(x => x.PoNo == PoNo).Select(x => new { x.PoNo, x.VendorName, x.PoClassID, x.Amount });

                    List<CalculationQuotation> QuotationItem_ListModel = new List<CalculationQuotation>();

                    foreach (var QuotationRow in QuotationList)
                    {
                        CalculationQuotation QuotationItem_Model = new CalculationQuotation();
                        QuotationItem_Model.PoClassID = QuotationRow.PoClassID;
                        QuotationItem_Model.Amount = QuotationRow.Amount;
                        QuotationItem_Model.Count = 0.0;
                        QuotationItem_ListModel.Add(QuotationItem_Model);
                        PoNumber = QuotationRow.PoNo;
                        VendorName = QuotationRow.VendorName;
                    }
                    #endregion

                    // 設定查詢月份
                    DateTime StartDate = TheMonthStart;
                    DateTime EndDate = TheMonthEnd;

                    // 查出整個月含有不正常的刷卡資料
                    var IsSwipeUnusual = _db.MXIC_SwipeInfos.Where(x => x.valid == "true" && (x.AttendType == "遲到" || x.AttendType == "早退") && x.SwipeTime >= StartDate && x.SwipeTime <= EndDate);
                    var WithOverTime = _db.MXIC_SwipeInfos.Where(x => x.valid == "true" && (x.AttendType == "加日" || x.AttendType == "加早" || x.AttendType == "加夜" || x.AttendType == "支援") && x.SwipeTime >= StartDate && x.SwipeTime <= EndDate);

                    if (IsSwipeUnusual.Count() == 0)
                    {
                        UsualFunction(PoNo, StartDate, EndDate, "正常", null, QuotationItem_ListModel);
                    }
                    else
                    {
                        UsualFunction(PoNo, StartDate, EndDate, "扣項", IsSwipeUnusual, QuotationItem_ListModel);
                    }


                    if (WithOverTime.Count() > 0)
                    {
                        UsualFunction(PoNo, StartDate, EndDate, "加項", WithOverTime, QuotationItem_ListModel);
                    }

                    #region 舊城市
                    //// 給他查起來
                    //foreach (var Emp in EmpList)
                    //{
                    //    // 先查詢結果
                    //    var IsUnusual = _db.MXIC_SwipeInfos.Where(x => x.valid == "true" && x.EmpName == Emp.EmpName && (x.AttendType != "正常") && x.SwipeTime >= StartDate && x.SwipeTime <= EndDate);
                    //    var WithLisence = _db.MXIC_LisenceManagements.Where(x => x.EmpName == Emp.EmpName && x.EndDate > EndDate);
                    //    var WorkGroup = Emp.WorkGroup;

                    //    // 之後再判斷
                    //    if (IsUnusual.Count() == 0)                   // 正常
                    //    {
                    //        UsualFunction(WithLisence.Count(), WorkGroup, CalculationQuotation_ListModel);
                    //    }
                    //    else                                          // 異常 
                    //    {
                    //        var WorkHour = 240.0;
                    //        foreach (var UnusualRow in IsUnusual)
                    //        {
                    //            #region 遲到+早退 時間計算
                    //            if (UnusualRow.AttendType == "遲到" || UnusualRow.AttendType == "早退")
                    //            {
                    //                WorkHour = WorkHour - UnusualRow.Hour;
                    //            }
                    //            #endregion
                    //            else
                    //            {
                    //                switch (UnusualRow.AttendType)
                    //                {
                    //                    #region 加班
                    //                    case "加班":
                    //                        if (WithLisence.Count() == 0)                    //有證照
                    //                        {
                    //                            switch (WorkGroup)                           //班別
                    //                            {
                    //                                case "組長":
                    //                                    //Console.WriteLine("10-4(填入時數)"); //UnusualRow.Hour
                    //                                    CalculationQuotation_ListModel[4 - 1].Count = CalculationQuotation_ListModel[4 - 1].Count + UnusualRow.Hour;
                    //                                    break;
                    //                                case "常日":
                    //                                    //Console.WriteLine("10-4(填入時數)"); //UnusualRow.Hour
                    //                                    CalculationQuotation_ListModel[4 - 1].Count = CalculationQuotation_ListModel[4 - 1].Count + UnusualRow.Hour;
                    //                                    break;
                    //                                case "早":
                    //                                    //Console.WriteLine("10-2(填入時數)"); //UnusualRow.Hour
                    //                                    CalculationQuotation_ListModel[2 - 1].Count = CalculationQuotation_ListModel[2 - 1].Count + UnusualRow.Hour;
                    //                                    break;
                    //                                case "夜":
                    //                                    //Console.WriteLine("10-3(填入時數)"); //UnusualRow.Hour
                    //                                    CalculationQuotation_ListModel[3 - 1].Count = CalculationQuotation_ListModel[3 - 1].Count + UnusualRow.Hour;
                    //                                    break;
                    //                                default:
                    //                                    break;
                    //                            }
                    //                        }
                    //                        else                                             //無證照
                    //                        {
                    //                            switch (WorkGroup)                           //班別
                    //                            {
                    //                                case "組長":
                    //                                    //Console.WriteLine("10-7(填入時數)"); //UnusualHour
                    //                                    CalculationQuotation_ListModel[7 - 1].Count = CalculationQuotation_ListModel[7 - 1].Count + UnusualRow.Hour;
                    //                                    break;
                    //                                case "常日":
                    //                                    //Console.WriteLine("10-7(填入時數)"); //UnusualHour
                    //                                    CalculationQuotation_ListModel[7 - 1].Count = CalculationQuotation_ListModel[7 - 1].Count + UnusualRow.Hour;
                    //                                    break;
                    //                                case "早":
                    //                                    // Console.WriteLine("10-5(填入時數)"); //UnusualHour
                    //                                    CalculationQuotation_ListModel[5 - 1].Count = CalculationQuotation_ListModel[5 - 1].Count + UnusualRow.Hour;
                    //                                    break;
                    //                                case "夜":
                    //                                    //Console.WriteLine("10-6(填入時數)"); //UnusualHour
                    //                                    CalculationQuotation_ListModel[6 - 1].Count = CalculationQuotation_ListModel[6 - 1].Count + UnusualRow.Hour;
                    //                                    break;
                    //                                default:
                    //                                    break;
                    //                            }
                    //                        }
                    //                        break;
                    //                    #endregion

                    //                    #region 代早
                    //                    case "代早":
                    //                        if (WithLisence.Count() == 0)                    //有證照
                    //                        {
                    //                            //Console.WriteLine("10-14");
                    //                            CalculationQuotation_ListModel[14 - 1].Count = CalculationQuotation_ListModel[14 - 1].Count + UnusualRow.Hour;

                    //                        }
                    //                        else                                             //無證照
                    //                        {
                    //                            //Console.WriteLine("10-16");
                    //                            CalculationQuotation_ListModel[16 - 1].Count = CalculationQuotation_ListModel[16 - 1].Count + UnusualRow.Hour;
                    //                        }
                    //                        break;
                    //                    #endregion

                    //                    #region 代晚
                    //                    case "代晚":
                    //                        if (WithLisence.Count() == 0)                    //有證照
                    //                        {
                    //                            //Console.WriteLine("10-15");
                    //                            CalculationQuotation_ListModel[15 - 1].Count = CalculationQuotation_ListModel[15 - 1].Count + UnusualRow.Hour;

                    //                        }
                    //                        else                                             //無證照
                    //                        {
                    //                            //Console.WriteLine("10-17");
                    //                            CalculationQuotation_ListModel[17 - 1].Count = CalculationQuotation_ListModel[17 - 1].Count + UnusualRow.Hour;
                    //                        }
                    //                        break;
                    //                        #endregion
                    //                }
                    //            }
                    //        }

                    //        #region 遲到+早退 or 加班/支援 給本薪 金額計算
                    //        if (WorkHour == 240.0)                                            //代表他都是加班或支援
                    //        {
                    //            UsualFunction(WithLisence.Count(), WorkGroup, CalculationQuotation_ListModel);
                    //        }
                    //        else                                                              //遲到早退
                    //        {
                    //            if (WithLisence.Count() == 0)                                 //有證照
                    //            {
                    //                if (WorkHour.ToString().Contains(".5"))
                    //                {
                    //                    if (WorkGroup == "夜")
                    //                    {
                    //                        // Console.WriteLine("10-21");                        //+1
                    //                        CalculationQuotation_ListModel[21 - 1].Count = CalculationQuotation_ListModel[21 - 1].Count + 1;
                    //                    }
                    //                    else
                    //                    {
                    //                        //Console.WriteLine("10-19");                        //+1
                    //                        CalculationQuotation_ListModel[19 - 1].Count = CalculationQuotation_ListModel[19 - 1].Count + 1;
                    //                    }

                    //                }
                    //                switch (WorkGroup)                                         //班別
                    //                {
                    //                    case "組長":
                    //                        //Console.WriteLine("10-18(填入時數)"); //+Math.Floor(WorkHour);
                    //                        CalculationQuotation_ListModel[18 - 1].Count = CalculationQuotation_ListModel[18 - 1].Count + Math.Floor(WorkHour);
                    //                        break;
                    //                    case "常日":
                    //                        //Console.WriteLine("10-18(填入時數)"); //+Math.Floor(WorkHour);
                    //                        CalculationQuotation_ListModel[18 - 1].Count = CalculationQuotation_ListModel[18 - 1].Count + Math.Floor(WorkHour);
                    //                        break;
                    //                    case "早":
                    //                        //Console.WriteLine("10-18(填入時數)"); //+Math.Floor(WorkHour);
                    //                        CalculationQuotation_ListModel[18 - 1].Count = CalculationQuotation_ListModel[18 - 1].Count + Math.Floor(WorkHour);
                    //                        break;
                    //                    case "夜":
                    //                        // Console.WriteLine("10-20(填入時數)"); //+Math.Floor(WorkHour);
                    //                        CalculationQuotation_ListModel[20 - 1].Count = CalculationQuotation_ListModel[20 - 1].Count + Math.Floor(WorkHour);
                    //                        break;
                    //                    default:
                    //                        break;
                    //                }
                    //            }
                    //            else                                              //無證照
                    //            {
                    //                if (WorkHour.ToString().Contains(".5"))
                    //                {
                    //                    if (WorkGroup == "夜")
                    //                    {
                    //                        //Console.WriteLine("10-25"); //+1
                    //                        CalculationQuotation_ListModel[25 - 1].Count = CalculationQuotation_ListModel[25 - 1].Count + 1;
                    //                    }
                    //                    else
                    //                    {
                    //                        //Console.WriteLine("10-23"); //+1
                    //                        CalculationQuotation_ListModel[23 - 1].Count = CalculationQuotation_ListModel[23 - 1].Count + 1;
                    //                    }

                    //                }
                    //                switch (WorkGroup)                            //班別
                    //                {
                    //                    case "組長":
                    //                        //Console.WriteLine("10-22(填入時數)"); //+Math.Floor(WorkHour);
                    //                        CalculationQuotation_ListModel[22 - 1].Count = CalculationQuotation_ListModel[22 - 1].Count + Math.Floor(WorkHour);
                    //                        break;
                    //                    case "常日":
                    //                        //Console.WriteLine("10-22(填入時數)"); //+Math.Floor(WorkHour);
                    //                        CalculationQuotation_ListModel[22 - 1].Count = CalculationQuotation_ListModel[22 - 1].Count + Math.Floor(WorkHour);
                    //                        break;
                    //                    case "早":
                    //                        //Console.WriteLine("10-22(填入時數)"); //+Math.Floor(WorkHour);
                    //                        CalculationQuotation_ListModel[22 - 1].Count = CalculationQuotation_ListModel[22 - 1].Count + Math.Floor(WorkHour);
                    //                        break;
                    //                    case "夜":
                    //                        //Console.WriteLine("10-24(填入時數)"); //+Math.Floor(WorkHour);
                    //                        CalculationQuotation_ListModel[24 - 1].Count = CalculationQuotation_ListModel[24 - 1].Count + Math.Floor(WorkHour);
                    //                        break;
                    //                    default:
                    //                        break;
                    //                }
                    //            }

                    //        }
                    //        #endregion
                    //    }
                    //}
                    #endregion

                    DownloadExcel(PoNumber, VendorName, Date, QuotationItem_ListModel);
                }
                catch (Exception ex)
                {
                    reponseStr = "寫入失敗";
                }
            }
            return (reponseStr);
        }
    }
}
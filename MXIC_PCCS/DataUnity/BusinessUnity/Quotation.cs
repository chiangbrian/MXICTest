using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MXIC_PCCS.DataUnity.Interface;
using MXIC_PCCS.Models;
using Newtonsoft.Json;
using OfficeOpenXml;
using System.Web.Mvc;
using System.Text;

namespace MXIC_PCCS.DataUnity.BusinessUnity
{
    public class Quotation : IQuotation, IDisposable
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

        public string SearchQuotation(string VendorName, string PoNo, string PoClassID)
        {
            var _QuotationSearchCondition = _db.MXIC_Quotations.OrderBy(x => new { x.PoNo ,x.Sequence }).Select(x=>new { x.PoNo , x.PoClassID, x.PoClassName, x.Unit, x.Amount, x.VendorName});
            
            //如果VendorName不為空
            if (!string.IsNullOrWhiteSpace(VendorName))
            {
                _QuotationSearchCondition = _QuotationSearchCondition.Where(x => x.VendorName.Contains(VendorName));

            }
            //如果PoNo不為空
            if (!string.IsNullOrWhiteSpace(PoNo))
            {
                _QuotationSearchCondition = _QuotationSearchCondition.Where(x => x.PoNo.Contains(PoNo));

            }
            //如果PoClassID不為空
            if (!string.IsNullOrWhiteSpace(PoClassID))
            {
                _QuotationSearchCondition = _QuotationSearchCondition.Where(x => x.PoClassID.ToLower().Contains(PoClassID.ToLower()));

            }

            //_SearchCondition轉換成json字串
            string responseStr = JsonConvert.SerializeObject(_QuotationSearchCondition, Formatting.Indented);

            return (responseStr);
        }

        public string ImportQuotation(string VendorName, string PoNo, List<QuotationProperty> Property_ListModel)
        {
            string MessageStr = "匯入成功";

            for (int i=0; i< Property_ListModel.Count;i++)
            {
                var AddQuotationItem = new Models.Quotation()
                {
                    QuotationID = Guid.NewGuid(),
                    PoNo = PoNo,
                    VendorName = VendorName,
                    PoClassID = Property_ListModel[i].PoClassID,
                    PoClassName = Property_ListModel[i].PoClassName,
                    LicPossess = "有",
                    Unit = Property_ListModel[i].Unit,
                    Amount = Property_ListModel[i].Amount,
                    Sequence = 1+ i
                };

                _db.MXIC_Quotations.Add(AddQuotationItem);
            }
            _db.SaveChanges();

            return (MessageStr);
        }

        public string ClearTableCheck(HttpPostedFileBase file)
        {
            //StringBuilder SB = new StringBuilder();
            string Result = "0";
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var excelPkg = new ExcelPackage(file.InputStream))
            {
                string PoNo;
                ExcelWorksheet sheet = excelPkg.Workbook.Worksheets["PO報價"];
                if (sheet.Cells[7, 7].Text.Contains("PO NO.") && !string.IsNullOrWhiteSpace(sheet.Cells[7, 9].Text))
                {
                    PoNo = sheet.Cells[7, 9].Text;

                    var Rows = _db.MXIC_Quotations.Where(x => x.PoNo == PoNo);

                    if (Rows.Count() > 0)
                    {
                        //Po已存在
                        Result ="1";
                    }
                  
                }
                else
                {
                    Result = "0";

                }

                

                return (Result);


            }

            
        }

        public string ClearTable(string PoNo)
        {
            string MessageStr = "判讀結束!";
            try
            {
                var Rows = _db.MXIC_Quotations.Where(x => x.PoNo == PoNo);

                if (Rows.Count() > 0)
                {
                    foreach (var DataRow in Rows)
                    {
                        _db.MXIC_Quotations.Remove(DataRow);
                    }
                    _db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageStr = ex.ToString();
            }
            return (MessageStr);
        }


        public string DelQuotation(string PoNo)
        {
            string MessageStr = "刪除失敗";
            try
            {
                var Rows = _db.MXIC_Quotations.Where(x => x.PoNo == PoNo);

                if (Rows.Count() > 0)
                {
                    foreach (var DataRow in Rows)
                    {
                        _db.MXIC_Quotations.Remove(DataRow);
                    }
                    _db.SaveChanges();
                    MessageStr = "刪除成功";
                }
                else
                {
                    MessageStr = "查無此PoNo";

                }
               
            }
            catch (Exception ex)
            {
                MessageStr = ex.ToString();
            }
            return (MessageStr);

        }
    }
}
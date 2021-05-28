using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MXIC_PCCS.DataUnity.Interface;
using MXIC_PCCS.Models;
using Newtonsoft.Json;

namespace MXIC_PCCS.DataUnity.BusinessUnity
{
    public class VendorManagement : IVendorManagement, IDisposable
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

        public string AddVendor(string PoNo, string VendorName, string EmpID, string EmpName, string Shifts)
        {
            string Str = "新增成功";
            if (!string.IsNullOrWhiteSpace(PoNo) && !string.IsNullOrWhiteSpace(VendorName) && !string.IsNullOrWhiteSpace(EmpID) && !string.IsNullOrWhiteSpace(EmpName))
            {
                var OriginalEmp = _db.MXIC_VendorManagements.Where(x => x.EmpID == EmpID);
                if (OriginalEmp.Any())
                {
                    Str = "此駐廠人員編號已存在";
                }
                else
                { 
                    var AddUser = new Models.VendorManagement()
                    {
                        VenID = Guid.NewGuid(),
                        PoNo = PoNo,
                        VendorName = VendorName,
                        EmpID = EmpID,
                        EmpName = EmpName,
                        Shifts = Shifts,
                        DeleteID = Guid.NewGuid(),
                        EditID = Guid.NewGuid()
                    };
                    _db.MXIC_VendorManagements.Add(AddUser);
                    _db.SaveChanges();
                }
            }
            else
            {
                Str = "新增失敗，請輸入所有資料。";
            }
            return (Str);
        }

        public string DeleteVendor(string DeleteID)
        {
            string Str = "刪除失敗";
            try
            {
                if (!string.IsNullOrWhiteSpace(DeleteID))
                {
                    string[] DeleteIDList = null;
                    DeleteID = DeleteID.Replace("jqg_grid_gb1_", "").TrimEnd(',');
                    DeleteIDList = DeleteID.Split(',');
                    foreach (var item in DeleteIDList)
                    {
                        Models.VendorManagement Vendor = _db.MXIC_VendorManagements.Where(x => x.DeleteID.ToString() == item).FirstOrDefault();
                        //User.UserDisable = false;
                        _db.MXIC_VendorManagements.Remove(Vendor);
                    }
                    _db.SaveChanges();
                    Str = "刪除成功";
                }
                else
                {
                    Str = "刪除失敗!請勾選刪除資料。";
                }
            }
            catch (Exception e)
            {
                Str = e.ToString();
            }
            return (Str);
        }

        public string EditVendor(string EditID, string PoNo, string VendorName, string EmpID, string EmpName, string Shifts)
        {
            string Str = "修改成功";

            var EditVendor = _db.MXIC_VendorManagements.Where(x => x.EditID.ToString() == EditID).FirstOrDefault();

            try
            {
                if (!string.IsNullOrWhiteSpace(PoNo))
                {
                    EditVendor.PoNo = PoNo;
                }

                if (!string.IsNullOrWhiteSpace(VendorName))
                {
                    EditVendor.VendorName = VendorName;
                }

                if (!string.IsNullOrWhiteSpace(EmpID))
                {
                    EditVendor.EmpID = EmpID;
                }

                if (!string.IsNullOrWhiteSpace(EmpName))
                {
                    EditVendor.EmpName = EmpName;
                }

                if (!string.IsNullOrWhiteSpace(Shifts))
                {
                    EditVendor.Shifts = Shifts;
                }

                _db.SaveChanges();
            }
            catch (Exception e)
            {
                Str = e.ToString();
            }
            return (Str);
        }

        public string VendorList( string PoNo, string VendorName, string EmpID, string EmpName, string Shifts)
        {
            var _VenderList = _db.MXIC_VendorManagements.OrderBy(x => x.EmpID).Select(x => new { x.PoNo,x.VendorName,x.EmpID,x.EmpName,x.EditID,x.DeleteID});

            //如果PoNo不為空
            if (!string.IsNullOrWhiteSpace(PoNo))
            {
                _VenderList = _VenderList.Where(x => x.PoNo.ToLower().Contains(PoNo.ToLower()));
            }
            //如果VendorName不為空
            if (!string.IsNullOrWhiteSpace(VendorName))
            {
                _VenderList = _VenderList.Where(x => x.VendorName.ToLower().Contains(VendorName.ToLower()));
            }
            //如果EmpID不為空
            if (!string.IsNullOrWhiteSpace(EmpID))
            {
                _VenderList = _VenderList.Where(x => x.EmpID.ToLower().Contains(EmpID.ToLower()));
            }
            //如果EmpName不為空
            if (!string.IsNullOrWhiteSpace(EmpName))
            {
                _VenderList = _VenderList.Where(x => x.EmpName.ToLower().Contains(EmpName.ToLower()));
            }
            //如果Shifts不為空
            //if (!string.IsNullOrWhiteSpace(Shifts))
            //{
            //    _VenderList = _VenderList.Where(x => x.Shifts.ToLower().Contains(Shifts.ToLower()));
            //}

            string Str = JsonConvert.SerializeObject(_VenderList, Formatting.Indented);

            return (Str);
        }

        public string EditVendorDetail (string EditID)
        {
            var VendorDetail = _db.MXIC_VendorManagements.Where(x => x.EditID.ToString() == EditID).Select(x => new { x.PoNo, x.VendorName, x.EmpID, x.EmpName,x.Shifts });

            string Str = JsonConvert.SerializeObject(VendorDetail, Formatting.Indented);

            return (Str);
        }

        public string Shifts()
        {
            List<SelectViewModel> SelectListx = new List<SelectViewModel>();

           
            var ShiftsList = _db.MXIC_VendorManagements.Select(x => x.Shifts).Distinct();
            foreach (var item in ShiftsList)
            {
                var SelectItem = new SelectViewModel()
                {
                    name = item,
                    value = item
                };
                SelectListx.Add(SelectItem);
            }
         

            string Str = JsonConvert.SerializeObject(SelectListx, Formatting.Indented);

            return (Str);

        }
    }
}
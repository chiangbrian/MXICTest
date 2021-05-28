using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MXIC_PCCS.DataUnity.Interface;
using MXIC_PCCS.Models;
using Newtonsoft.Json;

namespace MXIC_PCCS.DataUnity.BusinessUnity
{
    public class UserManagement : IUserManagement, IDisposable
    {
        //開啟資料庫連結
        public PCCSContext _db = new PCCSContext();

        //關閉資料庫
        //public void Dispose()
        //{
        //    ((IDisposable)_db).Dispose();
        //}

        public string UserList(string DepNo, string DepName, string UserID, string UserName)
        {
            var _UserList = _db.MXIC_UserManagements.Where(x => x.UserDisable == true).Select(x => new { x.DepNo, x.DepName, x.UserID, x.UserName, x.Admin, x.EditID, x.DeleteID });

            //如果DepNo不為空
            if (!string.IsNullOrWhiteSpace(DepNo))
            {
                _UserList = _UserList.Where(x => x.DepNo.ToLower().Contains(DepNo.ToLower()));

            }
            //如果DepName不為空
            if (!string.IsNullOrWhiteSpace(DepName))
            {
                _UserList = _UserList.Where(x => x.DepName.ToLower().Contains(DepName.ToLower()));

            }
            //如果UserID不為空
            if (!string.IsNullOrWhiteSpace(UserID))
            {
                _UserList = _UserList.Where(x => x.UserID.ToLower().Contains(UserID.ToLower()));

            }
            //如果UserName不為空
            if (!string.IsNullOrWhiteSpace(UserName))
            {
                _UserList = _UserList.Where(x => x.UserName.ToLower().Contains(UserName.ToLower()));

            }

            //_UserList轉換成json字串
            string Str = JsonConvert.SerializeObject(_UserList, Formatting.Indented);

            return (Str);
        }

        public string DeleteUser(string DeleteID)
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
                        Models.UserManagement User = _db.MXIC_UserManagements.Where(x => x.DeleteID.ToString() == item).FirstOrDefault();
                        //User.UserDisable = false;
                        _db.MXIC_UserManagements.Remove(User);
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

        public string AddUser(string DepNo, string DepName, string UserID, string UserName, string Admin, string PassWord)
        {
            string Str = "新增成功";
            if(!string.IsNullOrWhiteSpace(DepNo) && !string.IsNullOrWhiteSpace(DepName) && !string.IsNullOrWhiteSpace(UserID) && !string.IsNullOrWhiteSpace(UserName) && !string.IsNullOrWhiteSpace(PassWord))
            {

                var OriginalUser = _db.MXIC_UserManagements.Where(x => x.UserID == UserID);

                if (OriginalUser.Any())
                {
                    Str = "此人員編號已存在";
                }
                else
                { 
            //SHA1加密
            string Hash = GetSHA1.GetSHA1Hash(PassWord);

            var AddUser = new Models.UserManagement()
            {
                UserListID = Guid.NewGuid(),
                DepNo = DepNo,
                DepName = DepName,
                UserID = UserID,
                UserName = UserName,
                Admin = Admin,
                PassWord = Hash,
                UserDisable = true,
                EditID = Guid.NewGuid(),
                DeleteID = Guid.NewGuid()
            };

            _db.MXIC_UserManagements.Add(AddUser);
            _db.SaveChanges();
            }
            }
            else
            {
                Str = "新增失敗，請輸入所有資料。";
            }
           
            return (Str);
        }

        public string EditUserDetail(string EditID)
        {
            var UserDetail = _db.MXIC_UserManagements.Where(x => x.EditID.ToString() == EditID).Select(x => new { x.DepNo, x.DepName, x.UserID, x.UserName, x.Admin });

            string Str = JsonConvert.SerializeObject(UserDetail, Formatting.Indented);

            return (Str);
        }

        public string EditUser(string EditID, string DepNo, string DepName, string UserID, string UserName, string Admin, string PassWord)
        {
            string Str = "修改成功";

            var EditUser = _db.MXIC_UserManagements.Where(x => x.EditID.ToString() == EditID).FirstOrDefault();

            try
            {
                if (!string.IsNullOrWhiteSpace(PassWord))
                {
                    string hash = GetSHA1.GetSHA1Hash(PassWord);
                    EditUser.PassWord = hash;
                }
                EditUser.DepNo = DepNo;
                EditUser.DepName = DepName;
                EditUser.UserID = UserID;
                EditUser.UserName = UserName;
                EditUser.Admin = Admin;
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                Str = e.ToString();
            }

            return (Str);
        }

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
    }
}
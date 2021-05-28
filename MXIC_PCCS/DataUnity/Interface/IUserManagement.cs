using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MXIC_PCCS.DataUnity.Interface
{
    interface IUserManagement
    {/// <summary>
    /// 人員列表
    /// </summary>
    /// <returns></returns>
        string UserList(string DepNo ,string DepName, string UserID ,string UserName); 
        /// <summary>
        /// 刪除使用者
        /// </summary>
        /// <param name="UserListID"></param>
        /// <returns></returns>
        string DeleteUser(string DeleteID);
        /// <summary>
        /// 新增使用者
        /// </summary>
        /// <param name="DepNo"></param>
        /// <param name="DepName"></param>
        /// <param name="UserID"></param>
        /// <param name="UserName"></param>
        /// <param name="Admin"></param>
        /// <param name="PassWord"></param>
        /// <returns></returns>
        string AddUser(string DepNo, string DepName, string UserID, string UserName, string Admin,string PassWord );
        /// <summary>
        /// 取得修改員工資料
        /// </summary>
        /// <param name="EditID"></param>
        /// <returns></returns>
        string EditUserDetail(string EditID);
        /// <summary>
        /// 修改後資料
        /// </summary>
        /// <param name="EditID"></param>
        /// <param name="DepNo"></param>
        /// <param name="DepName"></param>
        /// <param name="UserID"></param>
        /// <param name="UserName"></param>
        /// <param name="Admin"></param>
        /// <param name="PassWord"></param>
        /// <returns></returns>
        string EditUser(string EditID, string DepNo, string DepName, string UserID, string UserName, string Admin, string PassWord);
    }
}

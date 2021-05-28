using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MXIC_PCCS.DataUnity.Interface
{
    interface IDepartmentManagement
    {
        /// <summary>
        /// 查詢(甚麼部門管理甚麼廠商)
        /// </summary>
        /// <param name="DepNo"></param>
        /// <param name="DepName"></param>
        /// <returns></returns>
        string SearchDepToVen(string DepNo, string DepName);

        //string AddDepToVen(string DepNo, string DepName, string VendorName);

        //string EditDepToVen(string EditID);

        /// <summary>
        /// 刪除(甚麼部門管理甚麼廠商)
        /// </summary>
        /// <param name="DeleteID"></param>
        /// <returns></returns>
        string DeleteDepToVen(string DeleteID);
        /// <summary>
        /// 新增視窗grid資料
        /// </summary>
        /// <returns></returns>
        string Venderdata();

        /// <summary>
        /// 部門管理供應商
        /// </summary>
        /// <param name="DepName"></param>
        /// <param name="VendorName"></param>
        /// <returns></returns>
        string AddDepToVen(string DepName, string VendorName);
    }
}

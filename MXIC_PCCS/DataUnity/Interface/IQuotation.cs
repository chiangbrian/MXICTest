using MXIC_PCCS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MXIC_PCCS.DataUnity.Interface
{
    interface IQuotation
    {
        /// <summary>
        /// 報價單查詢
        /// </summary>
        /// <param name="VendorName"></param>
        /// <param name="PoNo"></param>
        /// <param name="PoClassID"></param>
        /// <returns></returns>
        string SearchQuotation(string VendorName, string PoNo, string PoClassID);
        /// <summary>
        /// 匯入報價單
        /// </summary>
        /// <param name="VendorName"></param>
        /// <param name="PoNo"></param>
        /// <param name="Property_ListModel"></param>
        /// <returns></returns>
        string ImportQuotation(string VendorName, string PoNo, List<QuotationProperty> Property_ListModel);
       /// <summary>
       /// 清空Table
       /// </summary>
       /// <param name="PoNo"></param>
       /// <returns></returns>
        string ClearTable(string PoNo);
        /// <summary>
        /// 匯入之前判斷資料是否重複
        /// </summary>
        /// <param name="PoNo"></param>
        /// <returns></returns>
        string ClearTableCheck(HttpPostedFileBase file);
        /// <summary>
        /// 刪除報價單
        /// </summary>
        /// <param name="PoNo"></param>
        /// <returns></returns>
        string DelQuotation(string PoNo);
    }
}

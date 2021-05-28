using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace MXIC_PCCS.Models
{
    public class GetSHA1
    {
        public static string GetSHA1Hash(string uInput)     //*** 固定的數學公式。 可以使用static（靜態）方法
        {
            ////** 方法一 ****************************************************************
            //SHA1 SHA1Hasher = SHA1.Create();
            ////-- SHA1必須搭配 System.Security.Cryptography命名空間

            //Byte[] data = SHA1Hasher.ComputeHash(Encoding.Default.GetBytes(uInput));
            ////-- SHA1的 .ComputeHash(Byte[]) 方法，計算指定位元組陣列的雜湊值。
            ////  （字串轉成Byte[]）  System.Text.Encoding.Default.GetBytes(uInput)

            //** 方法二 ****************************************************************
            Byte[] data;
            SHA1CryptoServiceProvider sha1SP = new SHA1CryptoServiceProvider();
            //-- 使用密碼編譯服務提供者 (CSP) 所提供之實作，計算輸入資料的 SHA1 雜湊值。
            //-- http://msdn.microsoft.com/zh-tw/library/system.security.cryptography.sha1cryptoserviceprovider.aspx
            data = sha1SP.ComputeHash(Encoding.Default.GetBytes(uInput));
            StringBuilder sBuilder = new StringBuilder();   //-- StringBuilder必須搭配 System.Text命名空間

            // Loop through each byte of the hashed data and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));  //--變成十六進位
            }
            return sBuilder.ToString();
        }
    }
}
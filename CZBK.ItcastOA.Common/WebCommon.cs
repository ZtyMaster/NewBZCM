﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CZBK.ItcastOA.Common
{
   public class WebCommon
    {
       /// <summary>
       /// 对字符串进行MD5运算
       /// </summary>
       /// <param name="str"></param>
       /// <returns></returns>
       public static string GetMd5String(string str)
       {
           MD5 md5 = MD5.Create();
           byte[] buffer = Encoding.UTF8.GetBytes(str);
           byte[]md5Buffer=md5.ComputeHash(buffer);
           StringBuilder sb = new StringBuilder();
           foreach (byte b in md5Buffer)
           {
               sb.Append(b.ToString("x2"));
           }
           md5.Clear();
           return sb.ToString();
       }
       public static void GoPage()
       {
           HttpContext.Current.Response.Redirect("/Login/Index/?returnUrl=" + HttpContext.Current.Request.Url.ToString());
       }
    }
}

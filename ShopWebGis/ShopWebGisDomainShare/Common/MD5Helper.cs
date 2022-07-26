/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisDomainShare.Common

 *文件名：  MD5Helper

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2022/6/13 22:55:57

 *描述：MD5帮助类

/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ShopWebGisDomainShare.Common
{
    public class MD5Helper
    {
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="str">原文</param>
        /// <param name="key">密钥</param>
        /// <returns></returns>
        public static string Encrypt(string str, string key)
        {
            str += key;
            MD5 md5 = MD5.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(str);
            byte[] md5Bytes = md5.ComputeHash(bytes);
            StringBuilder sb = new StringBuilder();
            foreach (byte b in md5Bytes)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();



        }
    }
}

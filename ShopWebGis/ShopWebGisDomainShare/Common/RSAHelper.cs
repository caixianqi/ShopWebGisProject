/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisDomainShare.Common

 *文件名：  RSAHelper

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  智慧环保部-蔡显麒

 *创建时间：2022/6/21 14:10:26

 *描述：RSA帮助类

/************************************************************************************/

using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using ShopWebGisDomainShare.Const;
using ShopWebGisDomainShare.CustomException;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ShopWebGisDomainShare.Common
{
    public class RSAHelper
    {
        public const string privateKey = @"-----BEGIN RSA PRIVATE KEY-----
MIICXgIBAAKBgQCeQ4k4RImsowVBBhBwaCjQ/8y2cwoU1G1yaDWQY190oCafzGDK
LsVhTeMmOkWY9PyTMvNEE8cFEaHuc+9uAQC9PQDF/86+jtcERkTjhDjTN59H6Tbp
2dz8XpYCx3TFOOLIfsGyG5iDYAOgAuJMyXGJl9VZJyEBeiXu1IQkTPek2wIDAQAB
AoGAdn0FvRTDJnceteH+aMny1RbOg0J5p8Kq9l7Cy4k6rCxC/pybUoPVztXrXHGA
LdYtS1939d4AwPDElaxC887YZ6hXDNCxtoN3KKGgWGMTRZUC4OHHNxBi5Vfe5xKw
gtRKKhw2VhvWRXGrBseZjiVDsJ0tQtjDjKN6Hlxq3gUczPECQQDMbp2QubbJ5FOn
JfeyFaLXC2LoaAeVM6vh2BdtprLqthctTVC3gHW4HEOxkYGmoMcoaWKKEzbWkR1Z
MUFj7TOvAkEAxi+QEIH289gh7ikFqSaeXs0myrvd9e4aU3XEcOulhwzX2jZf2yzi
nLRiOHU2dHWMaLvNAmKmx3vj3YKPrnRwlQJBAJrCfpvKS7tJE57s2jfBs1XSc9z2
rA7iYBOHsCy9TqLqmF8eMaXJJNTs6L4rJhhLjJlmNkfKxe/nSW11IUiRkFcCQQCX
ksrhBTUlW9jfeDpfGy0mnkqb5UEmFTvHNxvNrTxE5Jq1xr5bN6H9baxtN6A1Q63m
cDOkLmUVzngA8xkGuQFdAkEAjdL9i39FMNxL4Ij4fCB+4B9zFDrKWqDvv3vtIVlF
NjRzfMZli/EHClU/uOhcogIqOFndaQy+E7Q1Dnk/T+HEWQ==
-----END RSA PRIVATE KEY-----
";
        public const string publicKey = @"-----BEGIN PUBLIC KEY-----
MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCeQ4k4RImsowVBBhBwaCjQ/8y2
cwoU1G1yaDWQY190oCafzGDKLsVhTeMmOkWY9PyTMvNEE8cFEaHuc+9uAQC9PQDF
/86+jtcERkTjhDjTN59H6Tbp2dz8XpYCx3TFOOLIfsGyG5iDYAOgAuJMyXGJl9VZ
JyEBeiXu1IQkTPek2wIDAQAB
-----END PUBLIC KEY-----
-
";
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="encryptstring"></param>
        /// <returns></returns>
        public static string Encrypt(string encryptstring)
        {
            using (TextReader reader = new StringReader(publicKey))
            {
                AsymmetricKeyParameter key = new PemReader(reader).ReadObject() as AsymmetricKeyParameter;
                Pkcs1Encoding pkcs1 = new Pkcs1Encoding(new RsaEngine());
                pkcs1.Init(true, key);//加密是true；解密是false;
                byte[] entData = Encoding.UTF8.GetBytes(encryptstring);
                entData = pkcs1.ProcessBlock(entData, 0, entData.Length);
                return Convert.ToBase64String(entData);
            }
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="decryptstring"></param>
        /// <returns></returns>
        public static string Decrypt(string decryptstring)
        {
            using (TextReader reader = new StringReader(privateKey))
            {
                try
                {
                    dynamic key = new PemReader(reader).ReadObject();
                    var rsaDecrypt = new Pkcs1Encoding(new RsaEngine());
                    if (key is AsymmetricKeyParameter)
                    {
                        key = (AsymmetricKeyParameter)key;
                    }
                    else if (key is AsymmetricCipherKeyPair)
                    {
                        key = ((AsymmetricCipherKeyPair)key).Private;
                    }
                    rsaDecrypt.Init(false, key);  //这里加密是true；解密是false  

                    byte[] entData = Convert.FromBase64String(decryptstring);
                    entData = rsaDecrypt.ProcessBlock(entData, 0, entData.Length);
                    return Encoding.UTF8.GetString(entData);
                }catch(Exception ex)
                {
                    throw new ShopWebGisCustomException($"解析失败!");
                }
            }
        }


    }



}

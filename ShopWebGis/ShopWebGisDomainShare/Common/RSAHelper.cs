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

using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ShopWebGisDomainShare.Common
{
    public class RSAHelper
    {
        private readonly RSA _privateKeyRsaProvider;
        private readonly RSA _publicKeyRsaProvider;
        private readonly HashAlgorithmName _hashAlgorithmName;
        private readonly Encoding _encoding;
        public const string privateKey = @"MIIEpQIBAAKCAQEAk74QpHjISjlsf/xAZ+3dG5+RgXE0hUynMOfnQ9fsfv7NWkSlorSh90dehToI50UBgt/siZyGrY8M2sca83HwL4a9oJIAszQNI5FZENVjg4NMZK0YYGxZEShMtHxJo++0RP0fd+m6LoyeOe/3fsOKzPzqoCqQkBKszoqgj3uDHjeomagqz8dq6QOKVIdGDomYE4uEgbLb4+r3w5wJZfPEJ0IovxzKkhzRmEgSDRYUXBxs9p8uyXe+HvFxrs3IhWARwfxwq59DcGjZgqWwzumoFqzXBDXRsNtqSesJhehwXNDjvdMMKRyR6Ow0iWL53aNOG5umj6vx8f2nVWKwEjJa9wIDAQABAoIBAEHEUlk8sQAlhtCERFFfV9Vjl1yVPal3AVfqa47OUCcKGvSraUY//Xd7rC5HMs5sb+tH7d4mMOeSrci36B3louMtKj2PsMMVESI7ofe13doduQR570eBA7b5Bwgy6X8SBd/OA9OX1jrBeu/UjApAVonArlsFB5wyy/0XRbkJZMkukTzXKX5ceTeUwu9MS3UBHxmVC/lT/uD9ooizPkHdtbef0cFZ/NxlBuASwG+XkzANeDiO4kGvQ1o+f57CKBOt1o9GAKCKz9DDgvFGzw3E4pxR6oPSfFRK/lVu17B9KV7Z9eZS5/6HDQf4ASa338zH6NAclPqEeDnkqHsCYjgsDoUCgYEA8n31shVH0OEXaAafHsHcn9B9TnUmglwxrFXvXOX4yfeIH3oV+7f45ouMwRqvMqoJjirw+kOIAkPXotPpTcMD6bexcqW1O0Zrf6FnwweqwSwvISGemvUTo2sXgPP8yRqQpjRmt5D2DNPn/TDVkFos40SIMHcudQgv0SYYwINPD/MCgYEAm/jvfrjAozauPkCgzREqrkXjediwEAD7cyZ5tEBy9DUpnMyd+p1P+Js6rp9PfBr04Qh0sMPAnHwf/cOwUUWlEiZXiqFcapkP1GHF1Z/LH+QMxs0oU3pvEYDQF7KRzVVyqylWcU14PeGAg6XRGl/BQT3dKie6hDug3G5dhi6Zze0CgYEAiGobweb+058ND9RJx3+/oZgkJfL5ivRabVyan2QwJU4/IZYIr+a6+tceg7ODQ7ksTvIRRnifFscbk9oqsTg4B07zYE+gOIxRBSfDu4+gm7NDgBvtobalKZWhT0Xyux9aqGVqM3I1ONos8955Bg/0mJWBF+K0G9pTLBGGcyZrX8kCgYEAghpNVCwybDKxd7YbK9N1AbfEfx/0BPV8ydm/TU8xVGmAE45O/Pz590ss0PqMp94ohAm0pLo00ZNscBYq3jA0+IXJIhdlannQzXzdNEu9eRWWXsJFMBUBztzfN9U5b8kmBIZP1+UiNqQCuk23tcNIBe7frElFeQXwMc9R7hqzKyUCgYEA8VmIB9e0rjFwb/9Fdgg6AjUJ8+7cGmYtO/e1GtlFdCwYzYg7+5GI/0Lg9DnpwngHCKOIZBuU5Oofa1FSizbm/gRpEizOytgXxouGyZwo3mFlcI+hylQphJtEhIM0iFe3M4Nmy/LM351pmnMdMNC3c51le2Iu417QkCcZG6maMys=";
        public const string publicKey = @"MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAk74QpHjISjlsf/xAZ+3dG5+RgXE0hUynMOfnQ9fsfv7NWkSlorSh90dehToI50UBgt/siZyGrY8M2sca83HwL4a9oJIAszQNI5FZENVjg4NMZK0YYGxZEShMtHxJo++0RP0fd+m6LoyeOe/3fsOKzPzqoCqQkBKszoqgj3uDHjeomagqz8dq6QOKVIdGDomYE4uEgbLb4+r3w5wJZfPEJ0IovxzKkhzRmEgSDRYUXBxs9p8uyXe+HvFxrs3IhWARwfxwq59DcGjZgqWwzumoFqzXBDXRsNtqSesJhehwXNDjvdMMKRyR6Ow0iWL53aNOG5umj6vx8f2nVWKwEjJa9wIDAQAB";
        /// <summary>
        /// 实例化RSAHelper
        /// </summary>
        /// <param name="rsaType">加密算法类型 RSA SHA1;RSA2 SHA256 密钥长度至少为2048</param>
        /// <param name="encoding">编码类型</param>
        /// <param name="privateKey">私钥</param>
        /// <param name="publicKey">公钥</param>
        public RSAHelper()
        {
            _encoding = Encoding.UTF8;
            if (!string.IsNullOrEmpty(privateKey))
            {
                _privateKeyRsaProvider = CreateRsaProviderFromPrivateKey(privateKey);
            }

            if (!string.IsNullOrEmpty(publicKey))
            {
                _publicKeyRsaProvider = CreateRsaProviderFromPublicKey(publicKey);
            }

            _hashAlgorithmName = HashAlgorithmName.SHA256;
        }

        #region 使用私钥签名

        /// <summary>
        /// 使用私钥签名
        /// </summary>
        /// <param name="data">原始数据</param>
        /// <returns></returns>
        public string Sign(string data)
        {
            byte[] dataBytes = _encoding.GetBytes(data);

            var signatureBytes = _privateKeyRsaProvider.SignData(dataBytes, _hashAlgorithmName, RSASignaturePadding.Pkcs1);

            return Convert.ToBase64String(signatureBytes);
        }

        #endregion

        #region 使用公钥验证签名

        /// <summary>
        /// 使用公钥验证签名
        /// </summary>
        /// <param name="data">原始数据</param>
        /// <param name="sign">签名</param>
        /// <returns></returns>
        public bool Verify(string data, string sign)
        {
            byte[] dataBytes = _encoding.GetBytes(data);
            byte[] signBytes = Convert.FromBase64String(sign);

            var verify = _publicKeyRsaProvider.VerifyData(dataBytes, signBytes, _hashAlgorithmName, RSASignaturePadding.Pkcs1);

            return verify;
        }

        #endregion

        #region 解密

        public string Decrypt(string cipherText)
        {
            if (_privateKeyRsaProvider == null)
            {
                throw new Exception("_privateKeyRsaProvider is null");
            }
            return Encoding.UTF8.GetString(_privateKeyRsaProvider.Decrypt(Convert.FromBase64String(cipherText), RSAEncryptionPadding.Pkcs1));
        }

        #endregion

        #region 加密

        public string Encrypt(string text)
        {
            if (_publicKeyRsaProvider == null)
            {
                throw new Exception("_publicKeyRsaProvider is null");
            }
            return Convert.ToBase64String(_publicKeyRsaProvider.Encrypt(Encoding.UTF8.GetBytes(text), RSAEncryptionPadding.Pkcs1));
        }

        #endregion

        #region 使用私钥创建RSA实例

        public RSA CreateRsaProviderFromPrivateKey(string privateKey)
        {
            var privateKeyBits = Convert.FromBase64String(privateKey);

            var rsa = RSA.Create();
            var rsaParameters = new RSAParameters();

            using (BinaryReader binr = new BinaryReader(new MemoryStream(privateKeyBits)))
            {
                byte bt = 0;
                ushort twobytes = 0;
                twobytes = binr.ReadUInt16();
                if (twobytes == 0x8130)
                    binr.ReadByte();
                else if (twobytes == 0x8230)
                    binr.ReadInt16();
                else
                    throw new Exception("Unexpected value read binr.ReadUInt16()");

                twobytes = binr.ReadUInt16();
                if (twobytes != 0x0102)
                    throw new Exception("Unexpected version");

                bt = binr.ReadByte();
                if (bt != 0x00)
                    throw new Exception("Unexpected value read binr.ReadByte()");

                rsaParameters.Modulus = binr.ReadBytes(GetIntegerSize(binr));
                rsaParameters.Exponent = binr.ReadBytes(GetIntegerSize(binr));
                rsaParameters.D = binr.ReadBytes(GetIntegerSize(binr));
                rsaParameters.P = binr.ReadBytes(GetIntegerSize(binr));
                rsaParameters.Q = binr.ReadBytes(GetIntegerSize(binr));
                rsaParameters.DP = binr.ReadBytes(GetIntegerSize(binr));
                rsaParameters.DQ = binr.ReadBytes(GetIntegerSize(binr));
                rsaParameters.InverseQ = binr.ReadBytes(GetIntegerSize(binr));
            }

            rsa.ImportParameters(rsaParameters);
            return rsa;
        }

        #endregion

        #region 使用公钥创建RSA实例

        public RSA CreateRsaProviderFromPublicKey(string publicKeyString)
        {
            // encoded OID sequence for  PKCS #1 rsaEncryption szOID_RSA_RSA = "1.2.840.113549.1.1.1"
            byte[] seqOid = { 0x30, 0x0D, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x01, 0x01, 0x05, 0x00 };
            byte[] seq = new byte[15];

            var x509Key = Convert.FromBase64String(publicKeyString);

            // ---------  Set up stream to read the asn.1 encoded SubjectPublicKeyInfo blob  ------
            using (MemoryStream mem = new MemoryStream(x509Key))
            {
                using (BinaryReader binr = new BinaryReader(mem))  //wrap Memory Stream with BinaryReader for easy reading
                {
                    byte bt = 0;
                    ushort twobytes = 0;

                    twobytes = binr.ReadUInt16();
                    if (twobytes == 0x8130) //data read as little endian order (actual data order for Sequence is 30 81)
                        binr.ReadByte();    //advance 1 byte
                    else if (twobytes == 0x8230)
                        binr.ReadInt16();   //advance 2 bytes
                    else
                        return null;

                    seq = binr.ReadBytes(15);       //read the Sequence OID
                    if (!CompareBytearrays(seq, seqOid))    //make sure Sequence for OID is correct
                        return null;

                    twobytes = binr.ReadUInt16();
                    if (twobytes == 0x8103) //data read as little endian order (actual data order for Bit String is 03 81)
                        binr.ReadByte();    //advance 1 byte
                    else if (twobytes == 0x8203)
                        binr.ReadInt16();   //advance 2 bytes
                    else
                        return null;

                    bt = binr.ReadByte();
                    if (bt != 0x00)     //expect null byte next
                        return null;

                    twobytes = binr.ReadUInt16();
                    if (twobytes == 0x8130) //data read as little endian order (actual data order for Sequence is 30 81)
                        binr.ReadByte();    //advance 1 byte
                    else if (twobytes == 0x8230)
                        binr.ReadInt16();   //advance 2 bytes
                    else
                        return null;

                    twobytes = binr.ReadUInt16();
                    byte lowbyte = 0x00;
                    byte highbyte = 0x00;

                    if (twobytes == 0x8102) //data read as little endian order (actual data order for Integer is 02 81)
                        lowbyte = binr.ReadByte();  // read next bytes which is bytes in modulus
                    else if (twobytes == 0x8202)
                    {
                        highbyte = binr.ReadByte(); //advance 2 bytes
                        lowbyte = binr.ReadByte();
                    }
                    else
                        return null;
                    byte[] modint = { lowbyte, highbyte, 0x00, 0x00 };   //reverse byte order since asn.1 key uses big endian order
                    int modsize = BitConverter.ToInt32(modint, 0);

                    int firstbyte = binr.PeekChar();
                    if (firstbyte == 0x00)
                    {   //if first byte (highest order) of modulus is zero, don't include it
                        binr.ReadByte();    //skip this null byte
                        modsize -= 1;   //reduce modulus buffer size by 1
                    }

                    byte[] modulus = binr.ReadBytes(modsize);   //read the modulus bytes

                    if (binr.ReadByte() != 0x02)            //expect an Integer for the exponent data
                        return null;
                    int expbytes = (int)binr.ReadByte();        // should only need one byte for actual exponent data (for all useful values)
                    byte[] exponent = binr.ReadBytes(expbytes);

                    // ------- create RSACryptoServiceProvider instance and initialize with public key -----
                    var rsa = RSA.Create();
                    RSAParameters rsaKeyInfo = new RSAParameters
                    {
                        Modulus = modulus,
                        Exponent = exponent
                    };
                    rsa.ImportParameters(rsaKeyInfo);

                    return rsa;
                }

            }
        }

        #endregion

        #region 导入密钥算法

        private int GetIntegerSize(BinaryReader binr)
        {
            byte bt = 0;
            int count = 0;
            bt = binr.ReadByte();
            if (bt != 0x02)
                return 0;
            bt = binr.ReadByte();

            if (bt == 0x81)
                count = binr.ReadByte();
            else
            if (bt == 0x82)
            {
                var highbyte = binr.ReadByte();
                var lowbyte = binr.ReadByte();
                byte[] modint = { lowbyte, highbyte, 0x00, 0x00 };
                count = BitConverter.ToInt32(modint, 0);
            }
            else
            {
                count = bt;
            }

            while (binr.ReadByte() == 0x00)
            {
                count -= 1;
            }
            binr.BaseStream.Seek(-1, SeekOrigin.Current);
            return count;
        }

        private bool CompareBytearrays(byte[] a, byte[] b)
        {
            if (a.Length != b.Length)
                return false;
            int i = 0;
            foreach (byte c in a)
            {
                if (c != b[i])
                    return false;
                i++;
            }
            return true;
        }

        #endregion

    }

    /// <summary>
    /// RSA算法类型
    /// </summary>
    public enum RSAType
    {
        /// <summary>
        /// SHA1
        /// </summary>
        RSA = 0,
        /// <summary>
        /// RSA2 密钥长度至少为2048
        /// SHA256
        /// </summary>
        RSA2
    }
}
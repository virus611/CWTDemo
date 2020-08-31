using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CWTDemo.Utils
{
    public class AESTools
    {
        /// <summary>
        /// AES加密算法
        /// </summary>
        /// <param name="plainText">明文</param>
        /// <param name="strKey">16位密钥字符串</param>
        /// <param name="iv">16位插值字符串</param>
        /// <returns>返回加密后的密文字节数组</returns>
        public static string AESEncrypt(string plainText, string strKey = "aaabbbccc1234567", string iv = "12345678ABCDEFGH")
        {
            if (string.IsNullOrWhiteSpace(plainText) || string.IsNullOrWhiteSpace(strKey) || string.IsNullOrWhiteSpace(iv))
            {
                return "";
            }

            if (strKey.Length != 16)
            {
                return "";
            }
            if (iv.Length != 16)
            {
                return "";
            }

            //分组加密算法
            SymmetricAlgorithm des = Rijndael.Create();
            byte[] inputByteArray = Encoding.UTF8.GetBytes(plainText);//得到需要加密的字节数组
            //设置密钥及密钥向量
            des.Key = Encoding.UTF8.GetBytes(strKey);
            des.IV = Encoding.UTF8.GetBytes(iv);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            byte[] cipherBytes = ms.ToArray();//得到加密后的字节数组
            cs.Close();
            ms.Close();
            return Convert.ToBase64String(cipherBytes);
        }



        /// <summary>
        ///  AES解密
        /// </summary>
        /// <param name="cipherStr">待解码的串</param>
        /// <param name="strKey">16位密钥字符串</param>
        /// <param name="iv">8位插值字符串</param>
        /// <returns>返回解密后的字符串</returns>
        public static string AESDecrypt(string cipherStr, string strKey = "aaabbbccc1234567", string iv = "12345678ABCDEFGH")
        {
            if (string.IsNullOrWhiteSpace(cipherStr) || string.IsNullOrWhiteSpace(strKey) || string.IsNullOrWhiteSpace(iv))
            {
                return "";
            }

            if (strKey.Length != 16)
            {
                return "";
            }
            if (iv.Length != 16)
            {
                return "";
            }

            try
            {
                byte[] cipherText = Convert.FromBase64String(cipherStr);

                SymmetricAlgorithm des = Rijndael.Create();
                des.Key = Encoding.UTF8.GetBytes(strKey);
                des.IV = Encoding.UTF8.GetBytes(iv);
                byte[] decryptBytes = new byte[cipherText.Length];
                MemoryStream ms = new MemoryStream(cipherText);
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Read);
                cs.Read(decryptBytes, 0, decryptBytes.Length);
                cs.Close();
                ms.Close();
                return System.Text.Encoding.UTF8.GetString(decryptBytes);
            }
            catch
            {
                return "";
            }
        }
    }
}


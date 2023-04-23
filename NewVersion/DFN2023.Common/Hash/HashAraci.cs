using System;
using System.Security.Cryptography;
using System.Text;

namespace DFN2023.Common.Hash
{
    public class HashAraci
    {
        public static string SifreyeHashUygula(string Sifre)
        {
            Byte[] originalBytes;
            Byte[] encodedBytes;
            MD5 md5;
            md5 = new MD5CryptoServiceProvider();
            originalBytes = ASCIIEncoding.Default.GetBytes(Sifre);
            encodedBytes = md5.ComputeHash(originalBytes);
            return BitConverter.ToString(encodedBytes);
        }
    }
}

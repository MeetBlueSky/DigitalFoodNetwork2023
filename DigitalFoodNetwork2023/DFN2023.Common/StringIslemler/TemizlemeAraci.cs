using System;
using System.Collections.Generic;
using System.Text;

namespace DFN2023.Common.StringIslemler
{
    public class TemizlemeAraci
    {
        public static string TurkceKarakterTemizle(string text)
        {
            text = text.Trim().ToLower();
            char[] oldValue = new char[] { 'ö', 'ü', 'ç', 'ı', 'ğ', 'ş', ' ' };
            char[] newValue = new char[] { 'o', 'u', 'c', 'i', 'g', 's', '_' };
            for (int sayac = 0; sayac < oldValue.Length; sayac++)
            {
                text = text.Replace(oldValue[sayac], newValue[sayac]);
            }
            return text;
        }
        public static string KullaniciAdiOlustur(string text, string sayac)
        {
            text = text.Trim().ToLower();
            char[] oldValue = new char[] { 'ö', 'ü', 'ç', 'ı', 'ğ', 'ş', ' ' };
            char[] newValue = new char[] { 'o', 'u', 'c', 'i', 'g', 's', '_' };
            for (int i = 0; i < oldValue.Length; i++)
            {
                text = text.Replace(oldValue[i], newValue[i]);
            }
            text = text + sayac;
            return text;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DFN2023.Web.Helpers
{
    public class XamarinUtils
    {
        public static String SifreCoz(string par)
        {
            par = par.Replace(" ", "+");
            string icerik = KulanAc(par, "KG2KLDsdhjsdf723423g2gdfsjlkh212");
            return icerik;
        }
        public static String Sifrele(string par)
        {
            byte[] sbytes = KullanYap(par, "KG2KLDsdhjsdf723423g2gdfsjlkh212");
            string sonuc = Convert.ToBase64String(sbytes);
            return sonuc;
        }
        private static byte[] KullanYap(string b_sifrelenecekMetin, String anahtar)
        {
            KriptXamarin crypto = new KriptXamarin();
            return crypto.Yap(b_sifrelenecekMetin, anahtar);
        }
        private static string KulanAc(string b_sifreliMetin, String anahtar)
        {
            KriptXamarin crypto = new KriptXamarin();
            return crypto.Ac(b_sifreliMetin, anahtar);
        }
    }
    public class KriptXamarin
    {
        public byte[] Yap(string plainBytes, String password)
        {
            byte[] key = Convert.FromBase64String(password);
            BitConverter.ToString(key);
            var bytes = Crypto.EncryptAes(plainBytes, password, key);
            return bytes;
        }
        public string Ac(string encryptedData, String password)
        {
            byte[] key = Convert.FromBase64String(password);
            byte[] bytes = Convert.FromBase64String(encryptedData);
            var str = Crypto.DecryptAes(bytes, password, key);
            return str;
        }
    }
}
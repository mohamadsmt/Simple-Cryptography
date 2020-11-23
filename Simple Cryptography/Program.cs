using System;
using System.Security.Cryptography;
using System.IO;

namespace Simple_Cryptography
{
    class Program
    {
        static void Main(string[] args)
        {
            DESCryptoServiceProvider key = new DESCryptoServiceProvider();
            Console.WriteLine("Enter your message: ");
            string plainText = Console.ReadLine();
            string cipherText = Encrypt(plainText, key);
            Console.WriteLine("Encrypted message is: " + cipherText);
            Console.WriteLine("pres Enter to Decrypt...");
            Console.ReadLine();
            Console.WriteLine("Decrypted message is: " + Decrypt(cipherText, key));

        }

        public static string Encrypt(string plainText, SymmetricAlgorithm Key)
        {
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, Key.CreateEncryptor(), CryptoStreamMode.Write);
            StreamWriter sw = new StreamWriter(cs);
            sw.Write(plainText);
            sw.Flush();
            cs.FlushFinalBlock();
            return Convert.ToBase64String(ms.GetBuffer(), 0, (int) ms.Length);
        }

        public static string Decrypt(string cipherText, SymmetricAlgorithm key)
        {
            MemoryStream ms = new MemoryStream(Convert.FromBase64String(cipherText));
            CryptoStream cs = new CryptoStream(ms, key.CreateDecryptor(), CryptoStreamMode.Read);
            StreamReader sr = new StreamReader(cs);
            return sr.ReadToEnd();
        }
    }
}

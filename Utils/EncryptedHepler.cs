using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace eSprzedazZadanieRekrutacyjne.Utils
{
    //https://learn.microsoft.com/pl-pl/dotnet/api/system.security.cryptography.aesmanaged?view=net-9.0
    internal class EncryptedHepler
    {
        // Klucz i IV muszą mieć odpowiednią długość (16 bajtów = 128 bit)
        private static readonly byte[] key = Encoding.UTF8.GetBytes("16characterskey!"); // musi mieć 16 bajtów (128 bit)
        private static readonly byte[] iv = Encoding.UTF8.GetBytes("16charactervect!"); // 16 bajtów

        public static String EncryptStringAES(string textToEncrypt)
        {
            String encryptedText;
            using Aes aesAlgoritm = Aes.Create();
            {
                aesAlgoritm.Key = key;
                aesAlgoritm.IV = iv;

                ICryptoTransform encryptor = aesAlgoritm.CreateEncryptor(aesAlgoritm.Key, aesAlgoritm.IV);

                using (MemoryStream memoryStreamEncrypt = new MemoryStream())
                {
                    using (CryptoStream cryptoStreamEncrypt = new CryptoStream(memoryStreamEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriterEncrypt = new StreamWriter(cryptoStreamEncrypt))
                        {
                            streamWriterEncrypt.Write(textToEncrypt);
                        }
                    }
                    // konwersja byte[] do String
                    encryptedText= Convert.ToBase64String(memoryStreamEncrypt.ToArray());
                }
            }            
            return encryptedText;
        }

        public static String DecryptStringAES(string textToDecrypt)
        {
            string decryptedText;
            using Aes aesAlgoritm = Aes.Create();
            {
                aesAlgoritm.Key = key;
                aesAlgoritm.IV = iv;

                ICryptoTransform decryptor = aesAlgoritm.CreateDecryptor(aesAlgoritm.Key, aesAlgoritm.IV);

                //konwersja z string do byte[]
                using (MemoryStream memoryStreamDecrypt = new MemoryStream(Convert.FromBase64String(textToDecrypt)))
                {
                    using (CryptoStream cryptoStreamDecrypt = new CryptoStream(memoryStreamDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReaderDecrypt = new StreamReader(cryptoStreamDecrypt))
                        {
                            decryptedText = streamReaderDecrypt.ReadToEnd();
                        }
                    }
                }            
            }
            return decryptedText;
        }
    }            
}


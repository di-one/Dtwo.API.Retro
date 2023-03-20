using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace D_One.Core.DofusBehavior.Cryptography
{
    public class Hash
    {
        internal static string Encrypt(string clearText)
        {
            string EncryptionKey = "abc123";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        public static string Decrypt(string cipherText)
        {
            string EncryptionKey = "abc123";
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }


        public static char[] charactersArray = new char[]
           {
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p',
            'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F',
            'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V',
            'W', 'X', 'Y', 'Z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '-', '_'
           };

        public static string EncryptPassword(string password, string secretKey)
        {
            StringBuilder str = new StringBuilder().Append("#1");
            for (int i = 0; i < password.Length; i++)
            {
                char ch = password[i];
                char ch2 = secretKey[i];
                int num2 = ch / 16;
                int num3 = ch % 16;
                int index = (num2 + ch2) % charactersArray.Length;
                int num5 = (num3 + ch2) % charactersArray.Length;
                str.Append(charactersArray[index]).Append(charactersArray[num5]);
            }
            return str.ToString();
        }

        public static string DecryptIP(string packet)
        {
            StringBuilder ip = new StringBuilder();

            for (int i = 0; i < 8; i += 2)
            {
                int ascii1 = packet[i] - 48;
                int ascii2 = packet[i + 1] - 48;

                if (i != 0)
                    ip.Append('.');

                ip.Append(((ascii1 & 15) << 4) | (ascii2 & 15));
            }
            return ip.ToString();
        }

        public static int DecryptPort(char[] chars)
        {
            if (chars.Length != 3)
                throw new ArgumentOutOfRangeException("The port must be encrypted in 3 characters");

            int port = 0;
            for (int i = 0; i < 2; i++)
                port += (int)(Math.Pow(64, 2 - i) * GetHash(chars[i]));

            port += GetHash(chars[2]);
            return port;
        }

        public static short GetHash(char ch)
        {
            for (short i = 0; i < charactersArray.Length; i++)
                if (charactersArray[i] == ch)
                    return i;

            throw new IndexOutOfRangeException(ch + " it is not in the hash array");
        }

        public static short GetCellIdFromHash(string str)
        {
            char char1 = str[0], char2 = str[1];
            short code1 = 0, code2 = 0, a = 0;

            while (a < charactersArray.Length)
            {
                if (charactersArray[a] == char1)
                    code1 = (short)(a * 64);

                if (charactersArray[a] == char2)
                    code2 = a;

                a++;
            }
            return (short)(code1 + code2);
        }

        public static string GetCellChar(short cellId) => charactersArray[cellId / 64] + "" + charactersArray[cellId % 64];
    }
}

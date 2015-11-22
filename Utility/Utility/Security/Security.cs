using System;
using System.Security.Cryptography;
using System.Text;

namespace Utility.Security
{
    /// <summary>
    /// Provides methods for treating security stuff
    /// </summary>
    public static class Security
    {
        #region Public Methods

        /// <summary>
        /// Returns an encrypted string by a key
        /// </summary>
        /// <remarks>
        /// If something is broken this method returns an empty string
        /// </remarks>
        /// <param name="text">Text to encrypt</param>
        /// <param name="key">Key to encrypt</param>
        /// <param name="encoding" >Encoding to get bytes. UTF8 by default.</ param >
        /// <returns></returns>
        public static String Encript(string text, string key, Encoding encoding = null)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(text) || String.IsNullOrWhiteSpace(key))
                    return String.Empty;

                if (encoding == null)
                    encoding = UTF8Encoding.UTF8;

                byte[] keyBytes;           
                byte[] bytesNoEncrypted = encoding.GetBytes(text);
                
                //Create a MD5 object to obtain a hash
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();

                //Get the key hash in bytes
                keyBytes = hashmd5.ComputeHash(encoding.GetBytes(key)); 
                hashmd5.Clear();

                //Create a Triple DES object to encrypt
                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();

                tdes.Key = keyBytes;
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = tdes.CreateEncryptor();

                byte[] resultArray = cTransform.TransformFinalBlock(bytesNoEncrypted, 0, bytesNoEncrypted.Length);
                tdes.Clear();

                return Convert.ToBase64String(resultArray, 0, resultArray.Length);
            }
            catch (Exception)
            {
                return String.Empty;
            }
        }

        /// <summary>
        /// Returns a decrypted string by a key
        /// </summary>
        /// <remarks>
        /// If something is broken this method returns an empty string
        /// </remarks>
        /// <param name="text">Text to decrypt</param>
        /// <param name="key">Key to decrypt</param>
        /// <param name="encoding" >Encoding to get bytes. UTF8 by default.</ param >
        /// <returns></returns>
        public static String Decrypt(String textoEncriptado, String clave, Encoding encoding = null)
        {
            try
            {
                if (String.IsNullOrEmpty(textoEncriptado) || String.IsNullOrEmpty(clave))
                    return String.Empty;

                byte[] keyBytes;           
                byte[] encryptedBytes = Convert.FromBase64String(textoEncriptado);

                //Create a MD5 object to obtain a hash
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();

                keyBytes = hashmd5.ComputeHash(encoding.GetBytes(clave));
                hashmd5.Clear();

                //Create a Triple DES object to decrypt
                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
                 
                tdes.Key = keyBytes;
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;
                
                ICryptoTransform cTransform = tdes.CreateDecryptor();
                
                byte[] resultArray = cTransform.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
                tdes.Clear();

                return encoding.GetString(resultArray);
            }
            catch (Exception)
            {
                return String.Empty;
            }
        }
        #endregion
    }
}

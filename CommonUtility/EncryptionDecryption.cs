using System.Security.Cryptography;
using System.Text;

namespace WellMI.CommonUtility
{
    public class EncryptionDecryption
    {
        private static readonly byte [] key = Encoding.UTF8.GetBytes ( "ruJRn-gee%8c2g=m" );
        private static readonly byte [] iv = Encoding.UTF8.GetBytes ( "uA%frBlR9tCv;f&h" );

        public static string EncryptData ( string plainText )
        {
            byte [] encrypted;
            using ( var rijAlg = new RijndaelManaged () )
            {
                rijAlg.Mode = CipherMode.CBC;
                rijAlg.Padding = PaddingMode.PKCS7;
                rijAlg.FeedbackSize = 128;

                rijAlg.Key = key;
                rijAlg.IV = iv;

                var encryptor = rijAlg.CreateEncryptor ();

                using ( var msEncrypt = new MemoryStream () )
                {
                    using ( var csEncrypt = new CryptoStream ( msEncrypt, encryptor, CryptoStreamMode.Write ) )
                    {
                        using ( var swEncrypt = new StreamWriter ( csEncrypt ) )
                        {
                            swEncrypt.Write ( plainText );
                        }
                        encrypted = msEncrypt.ToArray ();
                    }
                }
            }
            return Convert.ToBase64String ( encrypted );
        }

        public static string DecryptData ( string cipherText )
        {
            try
            {
                var encrypted = Convert.FromBase64String ( cipherText );
                return DecryptStringFromBytes ( encrypted, key, iv );
            }
            catch ( CryptographicException ex )
            {
                // Log the exception
                // Log.Error("Decryption failed: " + ex.Message);

                // Handle the error more gracefully
                return "Decryption failed. Please check your input.";
            }
        }

        private static string DecryptStringFromBytes ( byte [] cipherText, byte [] key, byte [] iv )
        {
            string plaintext = null;
            using ( var rijAlg = new RijndaelManaged () )
            {
                try
                {
                    rijAlg.Mode = CipherMode.CBC;
                    rijAlg.Padding = PaddingMode.PKCS7;
                    rijAlg.FeedbackSize = 128;

                    rijAlg.Key = key;
                    rijAlg.IV = iv;

                    using ( var msDecrypt = new MemoryStream ( cipherText ) )
                    {
                        using ( var csDecrypt = new CryptoStream ( msDecrypt, rijAlg.CreateDecryptor (), CryptoStreamMode.Read ) )
                        {
                            using ( var srDecrypt = new StreamReader ( csDecrypt ) )
                            {
                                plaintext = srDecrypt.ReadToEnd ();
                            }
                        }
                    }
                }
                catch ( CryptographicException ex )
                {
                    // Log the exception
                    // Log.Error("Decryption failed: " + ex.Message);

                    // Handle the error more gracefully
                    return "Decryption failed. Please check your input.";
                }
            }
            return plaintext;
        }

        public static IDictionary<string, dynamic> DecryptQueryStringParameters ( IDictionary<string, string>? queryStringParameters )
        {
            if ( queryStringParameters != null && queryStringParameters.Count > 0 )
            {
                IDictionary<string, dynamic> parameters = new Dictionary<string, dynamic> ();
                foreach ( var param in queryStringParameters )
                    parameters.Add ( param.Key, DecryptData ( param.Value ) );
                return parameters;
            }
            throw new ArgumentNullException ( nameof ( queryStringParameters ), "Query string parameters cannot be null." );
        }

    }
}

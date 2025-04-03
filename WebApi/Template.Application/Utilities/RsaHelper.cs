using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Template.Application.Utilities
{
    public class RsaHelper
    {
        private readonly string _privateKeyFilePath;
        private readonly string _passphrase;

        public RsaHelper(string privateKeyFilePath, string passphrase)
        {
            _privateKeyFilePath = privateKeyFilePath;
            _passphrase = passphrase;
        }

        public RSACryptoServiceProvider GetPrivateKeyFromPemFile()
        {
            using (TextReader privateKeyTextReader = new StringReader(File.ReadAllText(_privateKeyFilePath)))
            {
                // Read the PEM file content
                PemReader pemReader = new PemReader(privateKeyTextReader, new PasswordFinder(_passphrase));
                object keyObject = pemReader.ReadObject();

                // Check if the keyObject is an AsymmetricCipherKeyPair or RsaPrivateCrtKeyParameters
                RsaPrivateCrtKeyParameters privateKeyParameters;
                if (keyObject is AsymmetricCipherKeyPair keyPair)
                {
                    privateKeyParameters = keyPair.Private as RsaPrivateCrtKeyParameters;
                }
                else if (keyObject is RsaPrivateCrtKeyParameters rsaPrivate)
                {
                    privateKeyParameters = rsaPrivate;
                }
                else
                {
                    throw new InvalidOperationException("Invalid key format");
                }

                RSAParameters rsaParams = DotNetUtilities.ToRSAParameters(privateKeyParameters);
                RSACryptoServiceProvider csp = new RSACryptoServiceProvider();
                csp.ImportParameters(rsaParams);
                return csp;
            }
        }

        public string Decrypt(string encryptedData)
        {
            try
            {
                // Load the private key
                var privateKey = GetPrivateKeyFromPemFile();

                // Convert the encrypted data from Base64
                var encryptedBytes = Convert.FromBase64String(encryptedData);

                // Attempt to decrypt the data
                var decryptedBytes = privateKey.Decrypt(encryptedBytes, false);

                // Convert the decrypted bytes to a UTF-8 string
                var decryptedValue = Encoding.UTF8.GetString(decryptedBytes);


                return decryptedValue;

            }
            catch
            {
                // Handle any exceptions
                return null;
            }
        }



        internal class PasswordFinder : IPasswordFinder
        {
            private readonly string _password;

            public PasswordFinder(string password)
            {
                _password = password;
            }

            public char[] GetPassword()
            {
                return _password.ToCharArray();
            }
        }
    }


}

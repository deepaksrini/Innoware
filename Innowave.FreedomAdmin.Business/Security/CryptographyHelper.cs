using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Innowave.FreedomAdmin.Business.Security
{
    public class CryptographyHelper : ICryptographyHelper
    {
        private readonly string key;
        private readonly string vector;
        private readonly UTF8Encoding encoder = new UTF8Encoding();
        private readonly Func<SymmetricAlgorithm> provideAlgorithm;

        public CryptographyHelper(Func<SymmetricAlgorithm> provideAlgorithm, string key, string vector)
        {
            this.key = key;
            this.vector = vector;
            this.provideAlgorithm = provideAlgorithm;
        }

        public string Decrypt(string encrypted)
        {
            return encoder.GetString(Decrypt(Convert.FromBase64String(encrypted)));
        }

        public string Encrypt(string unencrypted)
        {
            return Convert.ToBase64String(Encrypt(encoder.GetBytes(unencrypted)));
        }

        private byte[] Encrypt(byte[] buffer)
        {
            var symmetricAlgorithm = provideAlgorithm();
            var encryptor = symmetricAlgorithm.CreateEncryptor(GetKey(symmetricAlgorithm), GetVector(symmetricAlgorithm));

            return Transform(buffer, encryptor);
        }

        private byte[] Decrypt(byte[] buffer)
        {
            var symmetricAlgorithm = provideAlgorithm();
            var decryptor = symmetricAlgorithm.CreateDecryptor(GetKey(symmetricAlgorithm), GetVector(symmetricAlgorithm));

            return Transform(buffer, decryptor);
        }

        private byte[] Transform(byte[] buffer, ICryptoTransform transform)
        {
            using (var stream = new MemoryStream())
            {
                using (var cs = new CryptoStream(stream, transform, CryptoStreamMode.Write))
                {
                    cs.Write(buffer, 0, buffer.Length);
                }

                return stream.ToArray();
            }
        }

        private byte[] GetKey(SymmetricAlgorithm algorithm)
        {
            return encoder.GetBytes(key).Take(algorithm.KeySize / 8).ToArray();
        }

        private byte[] GetVector(SymmetricAlgorithm algorithm)
        {
            return encoder.GetBytes(vector).Take(algorithm.BlockSize / 8).ToArray();
        }
    }
}

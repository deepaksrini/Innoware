namespace Innowave.FreedomAdmin.Business.Security
{
    public interface ICryptographyHelper
    {
        string Encrypt(string unencrypted);

        string Decrypt(string encrypted);
    }
}

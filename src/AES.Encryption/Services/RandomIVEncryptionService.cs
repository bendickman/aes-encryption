using AES.Encryption.Interfaces;
using AES.Encryption.Utilities;
using System.Security.Cryptography;

namespace AES.Encryption.Services;
public class RandomIVEncryptionService : IEncryptionService
{
    private byte[] _encryptionKey;

    public RandomIVEncryptionService(
        byte[] encryptionKey)
    {
        _encryptionKey = encryptionKey;
    }

    public string Decrypt(
        string input)
    {
        var cbcCiphertext = AesCbcCipherText.FromBase64String(input);
        using var aes = Aes.Create();
        var decryptor = aes.CreateDecryptor(_encryptionKey, cbcCiphertext.Iv);
        using (var memoryStream = new MemoryStream(cbcCiphertext.CiphertextBytes))
        {
            using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
            {
                using (var streamReader = new StreamReader(cryptoStream))
                {
                    return streamReader.ReadToEnd();
                }
            }
        }
    }

    public string Encrypt(
        string input)
    {
        byte[] cyphertextBytes;
        using var aes = Aes.Create();
        var encryptor = aes.CreateEncryptor(_encryptionKey, aes.IV);
        using (var memoryStream = new MemoryStream())
        {
            using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
            {
                using (var streamWriter = new StreamWriter(cryptoStream))
                {
                    streamWriter.Write(input);
                }
            }
            cyphertextBytes = memoryStream.ToArray();

            return new AesCbcCipherText(aes.IV, cyphertextBytes).ToString();
        }
    }
}

using AES.Encryption.Interfaces;
using AES.Encryption.Utilities;
using System.Security.Cryptography;
using System.Text;

namespace AES.Encryption.Services;

public class AuthenticatedEncryptionService : IEncryptionService
{
    private readonly byte[] _encryptionKey;

    public AuthenticatedEncryptionService(byte[] encryptionKey)
    {
        _encryptionKey = encryptionKey;
    }

    public string Encrypt(string plaintext)
    {
        using var aes = new AesCcm(_encryptionKey);

        var nonce = new byte[AesGcm.NonceByteSizes.MaxSize];
        RandomNumberGenerator.Fill(nonce);
        var plaintextBytes = Encoding.UTF8.GetBytes(plaintext);
        var ciphertextBytes = new byte[plaintextBytes.Length];
        var tag = new byte[AesGcm.TagByteSizes.MaxSize];

        aes.Encrypt(nonce, plaintextBytes, ciphertextBytes, tag);
        return new AesGcmCipherText(nonce, tag, ciphertextBytes).ToString();
    }

    public string Decrypt(string cipherText)
    {
        var gcmCiphertext = AesGcmCipherText.FromBase64String(cipherText);

        using var aes = new AesCcm(_encryptionKey);

        var plaintextBytes = new byte[gcmCiphertext.CiphertextBytes.Length];

        aes.Decrypt(gcmCiphertext.Nonce, gcmCiphertext.CiphertextBytes, gcmCiphertext.Tag, plaintextBytes);

        return Encoding.UTF8.GetString(plaintextBytes);
    }
}

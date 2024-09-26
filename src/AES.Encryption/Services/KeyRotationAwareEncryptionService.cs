using AES.Encryption.Interfaces;
using AES.Encryption.Utilities;

namespace AES.Encryption.Services;

public class KeyRotationAwareEncryptionService : IEncryptionService
{
    private readonly IEncryptionKeyProvider _encryptionKeyProvider;

    public KeyRotationAwareEncryptionService(IEncryptionKeyProvider encryptionKeyProvider)
    {
        _encryptionKeyProvider = encryptionKeyProvider;
    }

    public string Encrypt(string plainText)
    {
        var encryptionKey = _encryptionKeyProvider.GetCurrentEncryptionKey();

        var encryptionService = new AuthenticatedEncryptionService(encryptionKey.Data);
        var base64EncryptedText = encryptionService.Encrypt(plainText);
        return new VersionedAesGcmCiphertext(encryptionKey.Id, base64EncryptedText).ToString();
    }

    public string Decrypt(string cipherText)
    {
        var versionedCiphertext = VersionedAesGcmCiphertext.FromString(cipherText);
        var encryptionKey = _encryptionKeyProvider.GetEncryptionKeyById(versionedCiphertext.KeyId);
        var encryptionService = new AuthenticatedEncryptionService(encryptionKey.Data);
        return encryptionService.Decrypt(versionedCiphertext.Ciphertext);
    }

    public string UpgradeCiphertextWithCurrentEncryptionKey(string cipherText)
    {
        return Encrypt(Decrypt(cipherText));
    }
}

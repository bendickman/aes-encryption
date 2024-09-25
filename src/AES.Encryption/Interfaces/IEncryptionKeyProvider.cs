using AES.Encryption.Providers;

namespace AES.Encryption.Interfaces;

public interface IEncryptionKeyProvider
{
    EncryptionKey GetCurrentEncryptionKey();

    EncryptionKey GetEncryptionKeyById(string keyId);

    void RotateKey(byte[] newKeyData);
}
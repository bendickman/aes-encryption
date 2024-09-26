using AES.Encryption.Interfaces;

namespace AES.Encryption.Providers;

public class EncryptionKeyProvider : IEncryptionKeyProvider
{
    private readonly IList<byte[]> _encryptionKeys = Array.Empty<byte[]>().ToList();

    public EncryptionKey GetCurrentEncryptionKey()
    {
        return new EncryptionKey($"v{_encryptionKeys.Count - 1}", _encryptionKeys.Last());
    }

    public EncryptionKey GetEncryptionKeyById(string keyId)
    {
        var keyIndex = int.Parse(keyId[1..]);

        return new EncryptionKey(keyId, _encryptionKeys[keyIndex]);
    }

    public void RotateKey(byte[] newKeyData)
    {
        _encryptionKeys.Add(newKeyData);
    }
}

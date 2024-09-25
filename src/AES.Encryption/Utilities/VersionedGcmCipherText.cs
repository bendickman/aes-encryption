namespace AES.Encryption.Utilities;

public class VersionedAesGcmCiphertext
{
    public string KeyId { get; }

    public string Ciphertext { get; }

    public VersionedAesGcmCiphertext(string keyId, string ciphertext)
    {
        KeyId = keyId;
        Ciphertext = ciphertext;
    }

    public static VersionedAesGcmCiphertext FromString(string versionedCiphertext)
    {
        var parts = versionedCiphertext.Split('$');
        return new VersionedAesGcmCiphertext(parts[0], parts[1]);
    }

    public override string ToString()
    {
        return $"{KeyId}${Ciphertext}";
    }
}

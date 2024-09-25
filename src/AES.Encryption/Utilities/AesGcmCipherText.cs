using System.Security.Cryptography;

namespace AES.Encryption.Utilities;

public class AesGcmCipherText
{
    public byte[] Nonce { get; }

    public byte[] Tag { get; }

    public byte[] CiphertextBytes { get; }

    public static AesGcmCipherText FromBase64String(
        string data)
    {
        var dataBytes = Convert.FromBase64String(data);

        return new AesGcmCipherText(
            dataBytes.Take(AesGcm.NonceByteSizes.MaxSize).ToArray(),
            dataBytes[^AesGcm.TagByteSizes.MaxSize..],
            dataBytes[AesGcm.NonceByteSizes.MaxSize..^AesGcm.TagByteSizes.MaxSize]
        );
    }

    public AesGcmCipherText(
        byte[] nonce,
        byte[] tag,
        byte[] ciphertextBytes)
    {
        Nonce = nonce;
        Tag = tag;
        CiphertextBytes = ciphertextBytes;
    }

    public override string ToString()
    {
        return Convert.ToBase64String(Nonce.Concat(CiphertextBytes).Concat(Tag).ToArray());
    }
}

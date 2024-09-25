namespace AES.Encryption.Utilities;

public class AesCbcCipherText
{
    public byte[] Iv { get; }

    public byte[] CiphertextBytes { get; }

    public static AesCbcCipherText FromBase64String(
        string data)
    {
        var dataBytes = Convert.FromBase64String(data);

        return new AesCbcCipherText(
            dataBytes.Take(16).ToArray(),
            dataBytes.Skip(16).ToArray()
        );
    }

    public AesCbcCipherText(
        byte[] iv,
        byte[] ciphertextBytes)
    {
        Iv = iv;
        CiphertextBytes = ciphertextBytes;
    }

    public override string ToString()
    {
        return Convert.ToBase64String(Iv.Concat(CiphertextBytes).ToArray());
    }
}

namespace AES.Encryption.Providers;

public class EncryptionKey
{
    public EncryptionKey(
        string id, 
        byte[] data)
    {
        Id = id;
        Data = data;
    }

    public string Id { get; }

    public byte[] Data { get; }
}

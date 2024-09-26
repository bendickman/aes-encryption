using AES.Encryption.Interfaces;

namespace AES.Encryption.Processors;

public class EncryptionProcessor : IEncryptionProcessor
{
    private readonly IEnumerable<IEncryptionService> _encryptionServices;

    public EncryptionProcessor(
        IEnumerable<IEncryptionService> encryptionServices)
    {
        _encryptionServices = encryptionServices;
    }

    public void Process(string input)
    {
        foreach (var service in _encryptionServices)
        {
            var encryptedResult = service.Encrypt(input);

            WriteToConsole($"Encrypted: {encryptedResult}");

            var decryptedResult = service.Decrypt(encryptedResult);

            WriteToConsole($"Decrypted (serialised): {decryptedResult}");
        }
    }

    private void WriteToConsole(string value)
    {
        Console.WriteLine(value);
    }
}

namespace AES.Encryption.Interfaces;

public interface IEncryptionService
{
    string Encrypt(string input);

    string Decrypt(string input);
}

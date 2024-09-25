namespace AES.Encryption.Interfaces;

internal interface IEncryptionService
{
    string Encrypt(string input);

    string Decrypt(string input);
}

using AES.Encryption.Providers;
using AES.Encryption.Services;
using System.Text;
using System.Text.Json;

var encryptionKey = "gE3M8kWsVtnkMtJW";
var input = Encoding.UTF8.GetBytes(encryptionKey);

var employee = new Employee
{
    Id = 1,
    Name = "John Smith",
};

var employeeSerialized = JsonSerializer.Serialize(employee);

// Random IV Encryption

var RandomIvEncryptionService = new RandomIVEncryptionService(input);

var encrypted = RandomIvEncryptionService.Encrypt(employeeSerialized);
var decrypted = RandomIvEncryptionService.Decrypt(encrypted);

var result = JsonSerializer.Deserialize<Employee>(decrypted);

Console.WriteLine(encrypted);
Console.WriteLine($"{result.Id} -  {result.Name}");

// Authenticated Encryption

var authenticatedEncryptionService = new AuthenticatedEncryptionService(input);

var encrypted2 = authenticatedEncryptionService.Encrypt(employeeSerialized);
var decrypted2 = authenticatedEncryptionService.Decrypt(encrypted2);

var result2 = JsonSerializer.Deserialize<Employee>(decrypted2);

Console.WriteLine(encrypted2);
Console.WriteLine($"{result2.Id} -  {result2.Name}");

// Key Rotation Encryption

var keyProvider = new EncryptionKeyProvider(new List<byte[]>());
var keyRotationAwareEncryptionService = new KeyRotationAwareEncryptionService(keyProvider);

keyProvider.RotateKey(input);

var encrypted3 = keyRotationAwareEncryptionService.Encrypt(employeeSerialized);
var decrypted3 = keyRotationAwareEncryptionService.Decrypt(encrypted3);

var result3 = JsonSerializer.Deserialize<Employee>(decrypted3);

Console.WriteLine(encrypted3);
Console.WriteLine($"{result3.Id} -  {result3.Name}");

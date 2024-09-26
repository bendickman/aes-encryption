using AES.Encryption.Interfaces;
using AES.Encryption.Processors;
using AES.Encryption.Providers;
using AES.Encryption.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using System.Text.Json;

var encryptionKey = "gE3M8kWsVtnkMtJW";
var encryptionKeyBytes = Encoding.UTF8.GetBytes(encryptionKey);

var employee = new Employee
{
    Id = 1,
    Name = "John Smith",
};

var employeeSerialized = JsonSerializer.Serialize(employee);

var serviceProvider = GetServiceProvider(encryptionKeyBytes);
var keyRotationProvider = serviceProvider.GetRequiredService<IEncryptionKeyProvider>();

keyRotationProvider.RotateKey(encryptionKeyBytes);
var processor = serviceProvider.GetRequiredService<IEncryptionProcessor>();

processor.Process(employeeSerialized);

IServiceProvider GetServiceProvider(byte[] encryptionKey)
{
    var serviceCollection = new ServiceCollection();

    serviceCollection.AddSingleton<IEncryptionService>(_ =>
    {
        return new AuthenticatedEncryptionService(encryptionKeyBytes!);
    });

    serviceCollection.AddSingleton<IEncryptionService>(_ =>
    {
        return new RandomIVEncryptionService(encryptionKeyBytes!);
    });

    serviceCollection.AddSingleton<IEncryptionService, KeyRotationAwareEncryptionService>();
    serviceCollection.AddSingleton<IEncryptionKeyProvider, EncryptionKeyProvider>();
    serviceCollection.AddSingleton<IEncryptionProcessor, EncryptionProcessor>();

    return serviceCollection.BuildServiceProvider();
}
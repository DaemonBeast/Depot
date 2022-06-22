using Depot.Core;

namespace Depot.Storage.Multi;

public class MultiFile : IFile
{
    private readonly MultiStorage _multiStorage;

    public MultiFile(MultiStorage multiStorage)
    {
        this._multiStorage = multiStorage;
    }

    public async Task<Stream> OpenReadAsync(string filePath)
    {
        return await this._multiStorage.Execute(
            storage => storage.File.OpenReadAsync(filePath),
            "Failed to open stream for specified file in any available storage");
    }

    public async Task<byte[]> ReadAsync(string filePath)
    {
        return await this._multiStorage.Execute(
            storage => storage.File.ReadAsync(filePath),
            "Failed to read bytes of specified file in any available storage");
    }

    public async Task<string> ReadTextAsync(string filePath)
    {
        return await this._multiStorage.Execute(
            storage => storage.File.ReadTextAsync(filePath),
            "Failed to read text of specified file in any available storage");
    }

    public async Task WriteAsync(string filePath, Stream data)
    {
        await this._multiStorage.Execute(
            storage => storage.File.WriteAsync(filePath, data),
            "Failed to write stream to specified file in any available storage");
    }

    public async Task WriteAsync(string filePath, byte[] data)
    {
        await this._multiStorage.Execute(
            storage => storage.File.WriteAsync(filePath, data),
            "Failed to write bytes to specified file in any available storage");
    }

    public async Task CreateAsync(string filePath)
    {
        await this._multiStorage.Execute(
            storage => storage.File.CreateAsync(filePath),
            "Failed to create specified file in any available storage");
    }

    public async Task DeleteAsync(string filePath)
    {
        await this._multiStorage.Execute(
            storage => storage.File.DeleteAsync(filePath),
            "Failed to delete specified file in any available storage");
    }

    public async Task<bool> ExistsAsync(string filePath)
    {
        return await this._multiStorage.Execute(
            storage => storage.File.ExistsAsync(filePath),
            "Failed to check if specified file exists in any available storage");
    }
}
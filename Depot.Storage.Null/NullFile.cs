using Depot.Core;

namespace Depot.Storage.Null;

public class NullFile : IFile
{
    public Task<Stream> OpenReadAsync(string filePath)
    {
        throw new FileNotFoundException();
    }

    public Task<byte[]> ReadAsync(string filePath)
    {
        throw new FileNotFoundException();
    }

    public Task<string> ReadTextAsync(string filePath)
    {
        throw new FileNotFoundException();
    }

    public Task WriteAsync(string filePath, Stream data)
    {
        return Task.CompletedTask;
    }

    public Task WriteAsync(string filePath, byte[] data)
    {
        return Task.CompletedTask;
    }

    public Task CreateAsync(string filePath)
    {
        return Task.CompletedTask;
    }

    public Task DeleteAsync(string filePath)
    {
        throw new FileNotFoundException();
    }

    public Task<bool> ExistsAsync(string filePath)
    {
        return Task.FromResult(false);
    }
}
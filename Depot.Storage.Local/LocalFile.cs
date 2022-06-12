using Depot.Core;

namespace Depot.Storage.Local;

public class LocalFile : IFile
{
    public Task<Stream> OpenReadAsync(string filePath)
    {
        return Task.FromResult<Stream>(new FileInfo(filePath).OpenRead());
    }

    public async Task<byte[]> ReadAsync(string filePath)
    {
        return await File.ReadAllBytesAsync(filePath);
    }

    public async Task<string> ReadTextAsync(string filePath)
    {
        return await File.ReadAllTextAsync(filePath);
    }

    public async Task WriteAsync(string filePath, Stream data)
    {
        var fileStream = new FileInfo(filePath).OpenWrite();
        
        await data.CopyToAsync(fileStream);
        
        fileStream.Close();
    }

    public async Task WriteAsync(string filePath, byte[] data)
    {
        await File.WriteAllBytesAsync(filePath, data);
    }

    public async Task CreateAsync(string filePath)
    {
        await File.Create(filePath).DisposeAsync();
    }

    public Task DeleteAsync(string filePath)
    {
        File.Delete(filePath);
        
        return Task.CompletedTask;
    }

    public Task<bool> ExistsAsync(string filePath)
    {
        return Task.FromResult(File.Exists(filePath));
    }
}
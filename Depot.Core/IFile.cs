namespace Depot.Core;

public interface IFile
{
    public Task<Stream> OpenReadAsync(string filePath);

    public Task<byte[]> ReadAsync(string filePath);

    public Task<string> ReadTextAsync(string filePath);

    public Task WriteAsync(string filePath, Stream data);

    public Task WriteAsync(string filePath, byte[] data);

    public Task CreateAsync(string filePath);

    public Task DeleteAsync(string filePath);

    public Task<bool> ExistsAsync(string filePath);
}
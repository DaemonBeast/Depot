using Depot.Core;

namespace Depot.Storage.Local;

public class LocalDirectory : IDirectory
{
    public Task CreateAsync(string directoryPath)
    {
        Directory.CreateDirectory(directoryPath);

        return Task.CompletedTask;
    }

    public Task DeleteAsync(string directoryPath)
    {
        Directory.Delete(directoryPath, true);
        
        return Task.CompletedTask;
    }

    public Task<bool> ExistsAsync(string directoryPath)
    {
        return Task.FromResult(Directory.Exists(directoryPath));
    }
}
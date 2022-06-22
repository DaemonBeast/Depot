using Depot.Core;

namespace Depot.Storage.Null;

public class NullDirectory : IDirectory
{
    public Task CreateAsync(string directoryPath)
    {
        return Task.CompletedTask;
    }

    public Task DeleteAsync(string directoryPath)
    {
        throw new FileNotFoundException();
    }

    public Task<bool> ExistsAsync(string directoryPath)
    {
        return Task.FromResult(false);
    }
}
namespace Depot.Core;

public interface IDirectory
{
    public Task CreateAsync(string directoryPath);

    public Task DeleteAsync(string directoryPath);

    public Task<bool> ExistsAsync(string directoryPath);
}
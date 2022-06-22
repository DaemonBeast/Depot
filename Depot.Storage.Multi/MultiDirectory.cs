using Depot.Core;

namespace Depot.Storage.Multi;

public class MultiDirectory : IDirectory
{
    private readonly MultiStorage _multiStorage;

    public MultiDirectory(MultiStorage multiStorage)
    {
        this._multiStorage = multiStorage;
    }

    public async Task CreateAsync(string directoryPath)
    {
        await this._multiStorage.Execute(
            storage => storage.Directory.CreateAsync(directoryPath),
            "Failed to create specified directory in any available storage");
    }

    public async Task DeleteAsync(string directoryPath)
    {
        await this._multiStorage.Execute(
            storage => storage.Directory.DeleteAsync(directoryPath),
            "Failed to delete specified directory in any available storage");
    }

    public async Task<bool> ExistsAsync(string directoryPath)
    {
        return await this._multiStorage.Execute(
            storage => storage.Directory.ExistsAsync(directoryPath),
            "Failed to check if specified directory exists in any available storage");
    }
}
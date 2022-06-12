using Xunit;
using Xunit.Sdk;

namespace Depot.Storage.Tests;

public abstract class DirectoryTestBase<T> : IClassFixture<T> where T : StorageFixtureBase
{
    private readonly T _fixture;

    public DirectoryTestBase(T fixture)
    {
        this._fixture = fixture;
    }

    [Fact]
    public async void DirectoryNotExistsTest()
    {
        var dirName = Path.GetRandomFileName();
        
        var actual = await this._fixture.Storage.Directory.ExistsAsync(dirName);
        Assert.False(actual, "Directory reported as existent when shouldn't be");
    }

    [Fact]
    public async void DirectoryExistsTest()
    {
        var dirName = Path.GetRandomFileName();
        var directory = this._fixture.Storage.Directory;

        try
        {
            await directory.CreateAsync(dirName);
        }
        catch
        {
            throw new XunitException("Failed to create directory");
        }

        var actual = await directory.ExistsAsync(dirName);
        Assert.True(actual, "Directory reported as non-existent when should be");
        
        try
        {
            await directory.DeleteAsync(dirName);
        }
        catch
        {
            throw new XunitException("Failed to delete directory");
        }
        
        actual = await directory.ExistsAsync(dirName);
        Assert.False(actual, "Directory reported as existent when shouldn't be");
    }
}
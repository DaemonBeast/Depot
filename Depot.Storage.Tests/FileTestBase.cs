using Xunit;
using Xunit.Sdk;

namespace Depot.Storage.Tests;

public abstract class FileTestBase<T> : IClassFixture<T> where T : StorageFixtureBase
{
    private readonly T _fixture;

    public FileTestBase(T fixture)
    {
        this._fixture = fixture;
    }
    
    [Fact]
    public async void FileNotExistsTest()
    {
        var fileName = Path.GetRandomFileName();
        
        var actual = await this._fixture.Storage.File.ExistsAsync(fileName);
        Assert.False(actual, "File reported as existent when shouldn't be");
    }

    [Fact]
    public async void FileExistsTest()
    {
        var fileName = Path.GetRandomFileName();
        var file = this._fixture.Storage.File;

        try
        {
            await file.CreateAsync(fileName);
        }
        catch
        {
            throw new XunitException("Failed to create file");
        }

        var actual = await file.ExistsAsync(fileName);
        Assert.True(actual, "File reported as non-existent when should be");
        
        try
        {
            await file.DeleteAsync(fileName);
        }
        catch
        {
            throw new XunitException("Failed to delete file");
        }
        
        actual = await file.ExistsAsync(fileName);
        Assert.False(actual, "File reported as existent when shouldn't be");
    }

    [Fact]
    public async void FileOpenMissingStreamTest()
    {
        var fileName = Path.GetRandomFileName();
        var file = this._fixture.Storage.File;

        async Task Act()
        {
            await using var _ = await file.OpenReadAsync(fileName);
        }

        await Assert.ThrowsAnyAsync<Exception>(Act);
    }
    
    [Fact]
    public async void FileOpenMissingTest()
    {
        var fileName = Path.GetRandomFileName();
        var file = this._fixture.Storage.File;

        async Task Act()
        {
            await file.ReadAsync(fileName);
        }

        await Assert.ThrowsAnyAsync<Exception>(Act);
    }
    
    [Fact]
    public async void FileOpenMissingTextTest()
    {
        var fileName = Path.GetRandomFileName();
        var file = this._fixture.Storage.File;

        async Task Act()
        {
            await file.ReadTextAsync(fileName);
        }

        await Assert.ThrowsAnyAsync<Exception>(Act);
    }
    
    [Fact]
    public async void FileWriteStreamTest()
    {
        var fileName = Path.GetRandomFileName();
        var expected = new byte[] { 0x00, 0x01, 0x02 };

        using var memoryStream = new MemoryStream(expected);
        await this._fixture.Storage.File.WriteAsync(fileName, memoryStream);

        var actual = await this._fixture.Storage.File.ReadAsync(fileName);

        await this.TryCleanUpFile(fileName);
        
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public async void FileWriteTest()
    {
        var fileName = Path.GetRandomFileName();
        var expected = new byte[] { 0x00, 0x01, 0x02 };

        await this._fixture.Storage.File.WriteAsync(fileName, expected);

        var actual = await this._fixture.Storage.File.ReadAsync(fileName);

        await this.TryCleanUpFile(fileName);

        Assert.Equal(expected, actual);
    }

    private async Task TryCleanUpFile(string fileName)
    {
        try
        {
            await this._fixture.Storage.File.DeleteAsync(fileName);
        }
        catch
        {
            // ignored
        }
    }
}
namespace Depot.Core;

public interface IStorage
{
    public IDirectory Directory { get; }
    
    public IFile File { get; }
}
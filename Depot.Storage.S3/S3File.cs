using System.Net;
using System.Text;
using Amazon.S3;
using Amazon.S3.Model;
using Depot.Core;

namespace Depot.Storage.S3;

public class S3File : IFile
{
    private readonly S3Storage _s3Storage;
    
    public S3File(S3Storage s3Storage)
    {
        this._s3Storage = s3Storage;
    }
    
    public async Task<Stream> OpenReadAsync(string filePath)
    {
        var s3Object = await this._s3Storage.S3Client.GetObjectAsync(this._s3Storage.BucketName, filePath);

        return s3Object.ResponseStream;
    }

    public async Task<byte[]> ReadAsync(string filePath)
    {
        var s3Object = await this._s3Storage.S3Client.GetObjectAsync(this._s3Storage.BucketName, filePath);

        using var memoryStream = new MemoryStream();
        await s3Object.ResponseStream.CopyToAsync(memoryStream);

        return memoryStream.ToArray();
    }

    public async Task<string> ReadTextAsync(string filePath)
    {
        return await this.ReadTextAsync(filePath, Encoding.UTF8);
    }

    public async Task<string> ReadTextAsync(string filePath, Encoding encoding)
    {
        return encoding.GetString(await this.ReadAsync(filePath));
    }

    public async Task WriteAsync(string filePath, Stream data)
    {
        await this._s3Storage.CreateBucketIfNotExistsAsync();
        
        var request = new PutObjectRequest()
        {
            BucketName = this._s3Storage.BucketName,
            InputStream = data,
            Key = filePath
        };

        await this._s3Storage.S3Client.PutObjectAsync(request);
    }

    public async Task WriteAsync(string filePath, byte[] data)
    {
        await this._s3Storage.CreateBucketIfNotExistsAsync();
        
        var request = new PutObjectRequest()
        {
            BucketName = this._s3Storage.BucketName,
            InputStream = new MemoryStream(data),
            Key = filePath
        };

        await this._s3Storage.S3Client.PutObjectAsync(request);
    }

    public async Task CreateAsync(string filePath)
    {
        await this._s3Storage.CreateBucketIfNotExistsAsync();

        var request = new PutObjectRequest()
        {
            BucketName = this._s3Storage.BucketName,
            Key = filePath
        };

        await this._s3Storage.S3Client.PutObjectAsync(request);
    }

    public async Task DeleteAsync(string filePath)
    {
        await this._s3Storage.S3Client.DeleteObjectAsync(this._s3Storage.BucketName, filePath);
    }

    public async Task<bool> ExistsAsync(string filePath)
    {
        try
        {
            await this._s3Storage.S3Client.GetObjectMetadataAsync(this._s3Storage.BucketName, filePath);

            return true;
        }
        catch (AmazonS3Exception e)
        {
            if (e.StatusCode == HttpStatusCode.NotFound)
            {
                return false;
            }
            
            throw;
        }
    }
}
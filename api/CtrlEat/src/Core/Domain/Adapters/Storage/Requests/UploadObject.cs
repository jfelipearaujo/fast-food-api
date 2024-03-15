namespace Domain.Adapters.Storage.Requests;

public class UploadObjectRequest
{
    public string BucketName { get; set; }
    public string Name { get; set; }
    public MemoryStream InputStream { get; set; }
}

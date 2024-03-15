namespace Domain.Adapters.Storage.Requests;

public class DownloadObjectRequest
{
    public string BucketName { get; set; }
    public string Name { get; set; }
}
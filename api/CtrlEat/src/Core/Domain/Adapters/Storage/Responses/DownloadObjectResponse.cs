namespace Domain.Adapters.Storage.Responses;

public class DownloadObjectResponse
{
    public string Url { get; set; }
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public byte[] FileData { get; set; }
}
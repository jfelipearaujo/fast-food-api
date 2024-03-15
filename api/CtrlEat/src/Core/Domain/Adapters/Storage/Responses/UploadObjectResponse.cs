namespace Domain.Adapters.Storage.Responses;

public class UploadObjectResponse
{
    public string Url { get; set; }
    public int StatusCode { get; set; }
    public string Message { get; set; }
}

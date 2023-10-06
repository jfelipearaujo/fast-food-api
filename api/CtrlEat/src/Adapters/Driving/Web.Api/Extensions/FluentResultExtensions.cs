using FluentResults;

namespace Web.Api.Extensions;

public class ApiError
{
    public List<string> Errors { get; } = new();
}

public static class FluentResultExtensions
{
    public static ApiError ToApiError<T>(this Result<T> result)
    {
        var apiError = new ApiError();

        foreach (var error in result.Errors)
        {
            apiError.Errors.Add(error.Message);
        }

        return apiError;
    }
}

using FluentResults;

namespace Web.Api.Extensions;

public class ApiError
{
    public List<string> Errors { get; } = new();
}

public static class FluentResultExtensions
{
    public static bool HasNotFound<T>(this Result<T> result)
    {
        return HasStatusCode<T>(result, StatusCodes.Status404NotFound);
    }

    public static bool HasBadRequest<T>(this Result<T> result)
    {
        return HasStatusCode(result, StatusCodes.Status400BadRequest);
    }

    public static bool HasStatusCode<T>(this Result<T> result, int statusCode)
    {
        return result.Errors
            .SelectMany(x => x.Reasons)
            .SelectMany(x => x.Metadata)
            .Any(x => x.Key == "status_code" && (int)x.Value == statusCode);
    }

    public static ApiError ToApiError(this Result result)
    {
        var apiError = new ApiError();

        foreach (var error in result.Errors)
        {
            apiError.Errors.Add(error.Message);
        }

        return apiError;
    }

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
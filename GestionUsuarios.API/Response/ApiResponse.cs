namespace GestionUsuarios.API.Response;

/// <summary>
/// ApiResponse
/// </summary>
/// <typeparam name="T"></typeparam>
public class ApiResponse<T> : ApiResponseBase<T>
{
    /// <summary>
    /// constructor
    /// </summary>
    public ApiResponse()
    {
    }

    /// <summary>
    /// ApiResponse
    /// </summary>
    /// <param name="isSuccessful"></param>
    /// <param name="isError"></param>
    /// <param name="errorMessage"></param>
    /// <param name="messages"></param>
    /// <param name="result"></param>
    public ApiResponse(bool isSuccessful, bool isError, string? errorMessage, IEnumerable<string>? messages, T? result)
    {
        IsSuccessful = isSuccessful;
        IsError = isError;
        ErrorMessage = errorMessage;
        Messages = messages;
        Result = result;
    }
    /// <summary>
    /// CreateSuccessful
    /// </summary>
    /// <param name="result"></param>
    /// <returns></returns>
    public static ApiResponse<T> CreateSuccessful(T result) => new(isSuccessful: true, isError: false, null, null, result);

    /// <summary>
    /// CreateUnsuccessful
    /// </summary>
    /// <param name="messages"></param>
    /// <returns></returns>
    public static ApiResponse<T> CreateUnsuccessful(IEnumerable<string> messages) => new(isSuccessful: false, isError: false, null, messages, default);

    /// <summary>
    /// CreateError
    /// </summary>
    /// <param name="errorMessage"></param>
    /// <returns></returns>
    public static ApiResponse<T> CreateError(string errorMessage) => new(isSuccessful: false, isError: true, errorMessage, null, default);
}
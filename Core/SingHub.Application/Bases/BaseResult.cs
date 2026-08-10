using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace SingHub.Application.Bases;

public class BaseResult<T>
{
    public T? Data { get; set; }
    public IEnumerable<Error>? Errors { get; set; }

    [JsonIgnore]
    public bool IsSuccess => Errors == null || !Errors.Any();

    [JsonIgnore]
    public bool IsFailure => !IsSuccess;

    public static BaseResult<T> Success(T data)
    {
        return new BaseResult<T> { Data = data };
    }

    public static BaseResult<T> Failure(string message)
    {
        return new BaseResult<T> { Errors = [new Error { Message = message }] };
    }

    public static BaseResult<T> Failure()
    {
        return new BaseResult<T> { Errors = [new Error { Message = "An Error Occurred" }] };
    }

    public static BaseResult<T> Failure(IEnumerable<string> errors)
    {
        return new BaseResult<T> { Errors = errors.Select(e => new Error { Message = e }) };
    }

    public static BaseResult<T> Failure(IEnumerable<IdentityError> errors)
    {
        return new BaseResult<T> { Errors = errors.Select(e => new Error { Code = e.Code, Message = e.Description }) };
    }

}
public class Error
{
    public string? Code { get; set; }
    public string? Message { get; set; }
}

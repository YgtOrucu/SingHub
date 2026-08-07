using System.Net;

namespace SignHub.Application.Exceptions;

public abstract class BaseException(string message, HttpStatusCode statusCode) : Exception(message)
{
    public HttpStatusCode StatusCode { get; } = statusCode;
}

public class NotFoundException(string message) : BaseException(message, HttpStatusCode.NotFound);
public class BadRequestException(string message) : BaseException(message, HttpStatusCode.BadRequest);
public class UnAuthorizationException(string message) : BaseException(message, HttpStatusCode.Unauthorized);

using System.Net;

namespace EzShare.Application.Common;

public class Result<T>
{
    public bool Succeeded { get; }
    public T? Data { get; }
    public object? Error { get; }
    public HttpStatusCode StatusCode { get; }
    protected Result(bool succeeded, T? data, object? error, HttpStatusCode httpStatusCode)
    {
        Succeeded = succeeded;
        Data = data;
        Error = error;
        StatusCode = httpStatusCode;
    }
    
    public static Result<T> Success(T data, HttpStatusCode httpStatusCode)
    {
        return new Result<T>(true, data, null, httpStatusCode);
    }
    
    public static Result<T> Failure(object error, HttpStatusCode httpStatusCode)
    {
        return new Result<T>(false, default, error, httpStatusCode);
    }
}
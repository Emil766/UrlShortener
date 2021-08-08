using System;
using System.Net;

namespace Models
{
    public class ServiceResult
    {
        public HttpStatusCode StatusCode { get; set; }
        public Enum ExceptionCode { get; set; }
        public string ExceptionDescritpion { get; set; }

        public bool IsSuccessResult => StatusCode == HttpStatusCode.OK && ExceptionCode == null;

        public ServiceResult() { }
        public ServiceResult(HttpStatusCode code)
        {
            StatusCode = code;
        }
        public ServiceResult(ServiceResult result)
        {
            ExceptionCode = result.ExceptionCode;
            ExceptionDescritpion = result.ExceptionDescritpion;
            StatusCode = result.StatusCode;
        }

        public static ServiceResult Ok() => new ServiceResult { StatusCode = HttpStatusCode.OK };

        public static ServiceResult Forbid() => new ServiceResult { StatusCode = HttpStatusCode.Forbidden };

        public static ServiceResult Forbid(Enum exceptionCode, string exceptionDescription = null) =>
            new ServiceResult
            {
                StatusCode = HttpStatusCode.Forbidden,
                ExceptionCode = exceptionCode,
                ExceptionDescritpion = exceptionDescription
            };

        public static ServiceResult NotFound() => new ServiceResult { StatusCode = HttpStatusCode.NotFound };

        public static ServiceResult NotFound(Enum exceptionCode, string exceptionDescription = null) =>
            new ServiceResult
            {
                StatusCode = HttpStatusCode.NotFound,
                ExceptionCode = exceptionCode,
                ExceptionDescritpion = exceptionDescription
            };
        public static ServiceResult BadRequest() => new ServiceResult { StatusCode = HttpStatusCode.BadRequest };

        public static ServiceResult BadRequest(Enum exceptionCode, string exceptionDescription = null) =>
            new ServiceResult
            {
                StatusCode = HttpStatusCode.BadRequest,
                ExceptionCode = exceptionCode,
                ExceptionDescritpion = exceptionDescription
            };

        public static ServiceResult InternalServerError() => new ServiceResult { StatusCode = HttpStatusCode.InternalServerError };

        public static ServiceResult InternalServerError(Enum exceptionCode, string exceptionDescription = null) =>
            new ServiceResult
            {
                StatusCode = HttpStatusCode.InternalServerError,
                ExceptionCode = exceptionCode,
                ExceptionDescritpion = exceptionDescription
            };
    }

    public class ServiceResult<TObject> : ServiceResult
    {
        public ServiceResult() { }

        public ServiceResult(ServiceResult result)
        {
            ExceptionCode = result.ExceptionCode;
            ExceptionDescritpion = result.ExceptionDescritpion;
            StatusCode = result.StatusCode;
        }

        public TObject Model { get; set; }

        public static ServiceResult<TObject> Ok(TObject model) =>
            new ServiceResult<TObject> { Model = model, StatusCode = HttpStatusCode.OK };

        public static ServiceResult<TObject> NoContent() => new ServiceResult<TObject> { StatusCode = HttpStatusCode.NoContent };

        public static new ServiceResult<TObject> Forbid() =>
            new ServiceResult<TObject> { StatusCode = HttpStatusCode.Forbidden };

        public static new ServiceResult<TObject> Forbid(Enum exceptionCode, string exceptionDescription = null) =>
            new ServiceResult<TObject>
            {
                StatusCode = HttpStatusCode.Forbidden,
                ExceptionCode = exceptionCode,
                ExceptionDescritpion = exceptionDescription
            };

        public static new ServiceResult<TObject> NotFound() =>
            new ServiceResult<TObject> { StatusCode = HttpStatusCode.NotFound };

        public static new ServiceResult<TObject> NotFound(Enum exceptionCode, string exceptionDescription = null) =>
            new ServiceResult<TObject>
            {
                StatusCode = HttpStatusCode.NotFound,
                ExceptionCode = exceptionCode,
                ExceptionDescritpion = exceptionDescription
            };

        public static new ServiceResult<TObject> BadRequest() =>
            new ServiceResult<TObject> { StatusCode = HttpStatusCode.BadRequest };

        public static new ServiceResult<TObject> BadRequest(Enum exceptionCode, string exceptionDescription = null) =>
            new ServiceResult<TObject>
            {
                StatusCode = HttpStatusCode.BadRequest,
                ExceptionCode = exceptionCode,
                ExceptionDescritpion = exceptionDescription
            };

        public static new ServiceResult<TObject> InternalServerError() =>
            new ServiceResult<TObject> { StatusCode = HttpStatusCode.InternalServerError };

        public new static ServiceResult<TObject> InternalServerError(Enum exceptionCode, string exceptionDescription = null) =>
            new ServiceResult<TObject>
            {
                StatusCode = HttpStatusCode.InternalServerError,
                ExceptionCode = exceptionCode,
                ExceptionDescritpion = exceptionDescription
            };
    }
}

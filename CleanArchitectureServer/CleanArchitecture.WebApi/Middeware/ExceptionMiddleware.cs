using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infrastracture.Data.Context;
using CleanArchitecture.WebApi.Middeware;
using FluentValidation;

namespace CleanArchitecture.WebApi.Middleware;

public sealed class ExceptionMiddleware : IMiddleware
{
    private readonly AppDbContext _context;
    private readonly IUnitOfWork _unitOfWork;

    public ExceptionMiddleware(AppDbContext context, IUnitOfWork unitOfWork)
    {
        _context = context;
        _unitOfWork = unitOfWork;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await LogExceptionToDatabaseAsync(ex, context.Request);
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = 500;

        if (ex.GetType() == typeof(ValidationException))
        {
            return context.Response.WriteAsync(new ValidationErrorDetails
            {
                Errors = ((ValidationException)ex).Errors.Select(s => s.PropertyName),
                StatusCode = 403
            }.ToString());
        }

        return context.Response.WriteAsync(new ErrorResult
        {
            Message = ex.Message,
            StatusCode = context.Response.StatusCode
        }.ToString());
    }

    private async Task LogExceptionToDatabaseAsync(Exception ex, HttpRequest request)
    {
        ErrorLog errorLog = new()
        {
            ErrorMessage = ex.Message,
            StackTrace = ex.StackTrace,
            RequestPath = request.Path,
            RequestMethod = request.Method,
            Timestamp = DateTime.Now,
        };


        await using var transaction = await _unitOfWork.BeginTransactionAsync();

        try
        {

            await _unitOfWork.Repository<ErrorLog>().AddAsync(errorLog, default);

            await _unitOfWork.SaveChangesAsync(default);

            await _unitOfWork.CommitTransactionAsync(default);
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync(default);
            throw;
        }
    }
}

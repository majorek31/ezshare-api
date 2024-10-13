using System.Net;
using EzShare.Application.Common;
using FluentValidation;
using MediatR;

namespace EzShare.Application.PipelineBehaviors;

public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse>
    where TResponse : Result<Unit>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);
        var results = await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, cancellationToken)));
        var failures = results.SelectMany(r => r.Errors).Where(f => f != null).ToList();

        if (failures.Count == 0) return await next();
        
        var errorResult = Result<Unit>.Failure(failures.ToList(), HttpStatusCode.BadRequest);
        return (errorResult as TResponse)!;
    }
}
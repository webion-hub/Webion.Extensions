using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Webion.Extensions.AspNetCore.FluentValidation;

public static class FluentValidationExtension
{
    public static async Task ValidateModelAsync<T>(this AbstractValidator<T> validator,
        T request,
        ModelStateDictionary modelState,
        CancellationToken cancellationToken = default
    )
    {
        var result = await validator.ValidateAsync(request, cancellationToken);
        result.AddToModelState(modelState);
    }
}
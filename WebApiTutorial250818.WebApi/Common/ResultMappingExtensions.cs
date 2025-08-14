using Microsoft.AspNetCore.Mvc;

namespace WebApiTutorial250818.WebApi.Common
{
    public static class ResultMappingExtensions
    {
        public static IActionResult ToActionResult<T>(
            this Result<T> result,
            ControllerBase c,
            Func<T, IActionResult> onSuccess)
        {
            if (result.IsSuccess)
            {
                return onSuccess(result.Value!);
            }

            var e = result.Error!;
            return e.Type switch
            {
                ErrorType.Validation => c.BadRequest(ToProblemDetails(c, 400, e)),
                ErrorType.NotFound => c.NotFound(ToProblemDetails(c, 404, e)),
                ErrorType.Conflict => c.Conflict(ToProblemDetails(c, 409, e)),
                ErrorType.Forbidden => c.Forbid(),
                ErrorType.Unauthorized => c.Unauthorized(),
                _ => c.Problem(ToProblemDetails(c, 500, e).Detail),
            };
        }

        public static IActionResult ToActionResult(this Result result, ControllerBase c)
        {
            if (result.IsSuccess)
            {
                return c.NoContent();
            }

            var e = result.Error!;
            return e.Type switch
            {
                ErrorType.Validation => c.BadRequest(ToProblemDetails(c, 400, e)),
                ErrorType.NotFound => c.NotFound(ToProblemDetails(c, 404, e)),
                ErrorType.Conflict => c.Conflict(ToProblemDetails(c, 409, e)),
                ErrorType.Forbidden => c.Forbid(),
                ErrorType.Unauthorized => c.Unauthorized(),
                _ => c.Problem(detail: e.Message),
            };
        }

        private static ProblemDetails ToProblemDetails(ControllerBase c, int status, Error e) =>
            new()
            {
                Title = e.Code,
                Detail = e.Message,
                Status = status,
                Type = $"https://httpstatuses.com/{status}",
                Instance = c.HttpContext.TraceIdentifier
            };
    }

}

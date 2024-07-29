using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Sang.AspNetCore.CommonLibraries.Models;
using System.Diagnostics;

namespace Sang.AspNetCore.CommonLibraries.Filter
{
    internal class ModelValidateActionFilterAttribute : ActionFilterAttribute
    {
        private readonly ModelValidationExceptionFilterConfig _config;

        public ModelValidateActionFilterAttribute(ModelValidationExceptionFilterConfig config)
        {
            _config = config;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {

                //获取验证失败的模型字段
                var errors = context.ModelState.Select(kvp => new FieldError(
                    kvp.Key,
                    kvp.Value!.Errors.Select(e => e.ErrorMessage).ToArray()
                )).ToList();

                context.Result = new ContentResult
                {
                    StatusCode = _config.StatusCode,
                    ContentType = "application/json;charset=utf-8",
                    Content = new MessageModel<IEnumerable<FieldError>>
                    {
                        Status = _config.Status,
                        Msg = _config.Message,
                        Data= errors,
                        TraceId = _config.WithTraceId ? (Activity.Current?.Id ?? context.HttpContext.TraceIdentifier) : null
                    }.ToString()
                };
            }
        }
    }

}

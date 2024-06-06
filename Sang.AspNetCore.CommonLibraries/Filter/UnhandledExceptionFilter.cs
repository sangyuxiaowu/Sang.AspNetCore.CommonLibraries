using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Sang.AspNetCore.CommonLibraries.Models;
using System.Diagnostics;

namespace Sang.AspNetCore.CommonLibraries.Filter
{
    /// <summary>
    /// 未知异常处理
    /// </summary>
    public class UnhandledExceptionFilter : IExceptionFilter
    {
        private readonly IHostEnvironment _environment;
        private readonly UnhandledExceptionFilterConfig _config;

        /// <summary>
        /// 未知异常处理
        /// </summary>
        /// <param name="environment">运行环境</param>
        /// <param name="config"></param>
        public UnhandledExceptionFilter(IHostEnvironment environment, UnhandledExceptionFilterConfig config)
        {
            _environment = environment;
            _config = config;
        }

        /// <summary>
        /// 未知异常处理
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public void OnException(ExceptionContext context)
        {
            context.Result = new ContentResult
            {
                StatusCode = _config.StatusCode,
                ContentType = "application/json;charset=utf-8",
                Content = new MessageModel<string>
                {
                    Status = _config.Status,
                    Msg = _environment.IsDevelopment() ? context.Exception.ToString() : "服务端异常，请稍后重试！",
                    TraceId = _config.WithTraceId ? (Activity.Current?.Id ?? context.HttpContext.TraceIdentifier) : null
                    
                }.ToString()
            };
            context.ExceptionHandled = true;
        }
    }
}

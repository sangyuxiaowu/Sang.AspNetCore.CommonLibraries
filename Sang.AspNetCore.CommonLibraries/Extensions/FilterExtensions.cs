using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Sang.AspNetCore.CommonLibraries.Filter;

namespace Sang.AspNetCore.CommonLibraries
{
    /// <summary>
    /// 筛选器扩展
    /// </summary>
    public static class FilterExtensions
    {
        /// <summary>
        /// 添加未知异常处理
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configureOptions"></param>
        public static void AddUnhandledExceptionFilter(this IServiceCollection services, Action<UnhandledExceptionFilterConfig> configureOptions)
        {
            var options = new UnhandledExceptionFilterConfig();
            configureOptions(options);
            services.AddSingleton(options);
            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add<UnhandledExceptionFilter>();
            });
        }

        /// <summary>
        /// 添加模型验证异常处理
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configureOptions"></param>
        public static void AddModelValidationExceptionFilter(this IServiceCollection services, Action<ModelValidationExceptionFilterConfig> configureOptions)
        {
            var options = new ModelValidationExceptionFilterConfig();
            configureOptions(options);
            services.AddSingleton(options);
            //关闭默认模型验证
            services.Configure<ApiBehaviorOptions>(opt => opt.SuppressModelStateInvalidFilter = true);
            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add<ModelValidateActionFilterAttribute>();
            });
        }
    }
}

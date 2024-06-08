namespace Sang.AspNetCore.CommonLibraries.Filter
{
    /// <summary>
    /// 异常处理配置
    /// </summary>
    public class ExceptionFilterConfig
    {
        /// <summary>
        /// 异常状态码设置
        /// </summary>
        public int Status { get; set; } = 500;
        
        /// <summary>
        /// HTTP 状态码设置
        /// </summary>
        public int StatusCode { get; set; } = 500;

        /// <summary>
        /// 是否返回跟踪 ID
        /// </summary>
        public bool WithTraceId { get; set; } = true;

        /// <summary>
        /// 通用异常信息
        /// ModelValidationException 将使用 Data 属性返回详细内容
        /// </summary>
        public string Message { get; set; } = string.Empty;
    }

    /// <summary>
    /// 未知异常处理配置
    /// </summary>
    public class UnhandledExceptionFilterConfig : ExceptionFilterConfig { }

    /// <summary>
    /// 验证异常处理配置
    /// </summary>
    public class ModelValidationExceptionFilterConfig : ExceptionFilterConfig { }

}

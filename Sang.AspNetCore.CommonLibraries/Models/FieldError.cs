using System.Text.Json.Serialization;

namespace Sang.AspNetCore.CommonLibraries.Models
{
    /// <summary>
    /// 错误字段信息
    /// </summary>
    public record class FieldError
    {
        /// <summary>
        /// 字段
        /// <summary>
        [JsonPropertyName("field")]
        public string Field { get; set; }

        /// <summary>
        /// 错误
        /// <summary>
        [JsonPropertyName("err")]
        public string[] Err { get; set; }

        /// <summary>
        /// 错误字段信息
        /// </summary>
        /// <param name="field">字段</param>
        /// <param name="err">错误</param>
        public FieldError(string field, string[] err)
        {
            Field = field;
            Err = err;
        }
    }

}

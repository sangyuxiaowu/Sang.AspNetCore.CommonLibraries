using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;

namespace Sang.AspNetCore.CommonLibraries.Models
{
    /// <summary>
    /// 通用返回信息类
    /// </summary>
    public record class MessageModel<T>
    {
        /// <summary>
        /// 状态码
        /// </summary>
        [JsonPropertyName("status")]
        public int Status { get; set; } = 0;

        /// <summary>
        /// 返回信息
        /// </summary>
        [JsonPropertyName("msg")]
        public string Msg { get; set; } = "";
        /// <summary>
        /// 返回数据集合
        /// </summary>
        [JsonPropertyName("data")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public T? Data { get; set; }

        /// <summary>
        /// 跟踪 ID
        /// </summary>
        [JsonPropertyName("traceId")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? TraceId { get; set; }


        /// <summary>
        /// 返回成功
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="data">数据</param>
        /// <param name="trace">跟踪ID</param>
        /// <returns></returns>
        public static MessageModel<T> Ok(string msg = "ok", T data = default!, string? trace = null)
        {
            return Message(0, msg, data, trace);
        }

        /// <summary>
        /// 返回成功
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="data">数据</param>
        /// <param name="trace">跟踪ID</param>
        /// <returns></returns>
        public static MessageModel<T> Success(string msg = "ok", T data = default!, string? trace = null)
        {
            return Message(0, msg, data, trace);
        }

        /// <summary>
        /// 返回失败
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="status">状态</param>
        /// <param name="data">数据</param>
        /// <param name="trace">跟踪ID</param>
        /// <returns></returns>
        public static MessageModel<T> Fail(string msg, int status = -1, T data = default!, string? trace = null)
        {
            return Message(status, msg, data, trace);
        }

        /// <summary>
        /// 返回失败
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="status">状态</param>
        /// <param name="data">数据</param>
        /// <param name="trace">跟踪ID</param>
        /// <returns></returns>
        public static MessageModel<T> Error(string msg, int status = -1, T data = default!, string? trace = null)
        {
            return Message(status, msg, data, trace);
        }


        /// <summary>
        /// 返回消息
        /// </summary>
        /// <param name="status">状态码，0 表示正确返回</param>
        /// <param name="msg">消息</param>
        /// <param name="data">数据</param>
        /// <param name="trace">跟踪ID</param>
        /// <returns></returns>
        public static MessageModel<T> Message(int status, string msg, T data, string? trace = null)
        {
            return new MessageModel<T>() { Msg = msg, Data = data, Status = status, TraceId = trace };
        }

        /// <summary>
        /// 转为 JSON 字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var options = new JsonSerializerOptions { Encoder = JavaScriptEncoder.Create(UnicodeRanges.All) };
            return JsonSerializer.Serialize(this, options);
        }
    }
}

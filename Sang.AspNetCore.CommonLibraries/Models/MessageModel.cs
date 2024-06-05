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
        /// 返回成功
        /// </summary>
        /// <param name="msg">消息</param>
        /// <returns></returns>
        public static MessageModel<T> Ok(string msg = "ok")
        {
            return Message(0, msg, default!);
        }

        /// <summary>
        /// 返回成功
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="data">数据</param>
        /// <returns></returns>
        public static MessageModel<T> Ok(string msg, T data)
        {
            return Message(0, msg, data);
        }

        /// <summary>
        /// 返回失败
        /// </summary>
        /// <param name="status">状态</param>
        /// <param name="msg">消息</param>
        /// <returns></returns>
        public static MessageModel<T> Fail(int status, string msg)
        {
            return Message(status, msg, default!);
        }
        /// <summary>
        /// 返回失败
        /// </summary>
        /// <param name="status">状态</param>
        /// <param name="msg">消息</param>
        /// <param name="data">数据</param>
        /// <returns></returns>
        public static MessageModel<T> Fail(int status, string msg, T data)
        {
            return Message(status, msg, data);
        }

        /// <summary>
        /// 返回消息
        /// </summary>
        /// <param name="status">状态码，0 表示正确返回</param>
        /// <param name="msg">消息</param>
        /// <param name="data">数据</param>
        /// <returns></returns>
        public static MessageModel<T> Message(int status, string msg, T data)
        {
            return new MessageModel<T>() { Msg = msg, Data = data, Status = status };
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

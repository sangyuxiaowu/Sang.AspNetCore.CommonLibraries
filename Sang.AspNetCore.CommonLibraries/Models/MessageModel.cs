using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;

namespace Sang.AspNetCore.CommonLibraries.Models
{
    /// <summary>
    /// ͨ�÷�����Ϣ��
    /// </summary>
    public record class MessageModel<T>
    {
        /// <summary>
        /// ״̬��
        /// </summary>
        [JsonPropertyName("status")]
        public int Status { get; set; } = 0;

        /// <summary>
        /// ������Ϣ
        /// </summary>
        [JsonPropertyName("msg")]
        public string Msg { get; set; } = "";
        /// <summary>
        /// �������ݼ���
        /// </summary>
        [JsonPropertyName("data")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public T? Data { get; set; }

        /// <summary>
        /// ���سɹ�
        /// </summary>
        /// <param name="msg">��Ϣ</param>
        /// <returns></returns>
        public static MessageModel<T> Ok(string msg = "ok")
        {
            return Message(0, msg, default!);
        }

        /// <summary>
        /// ���سɹ�
        /// </summary>
        /// <param name="msg">��Ϣ</param>
        /// <param name="data">����</param>
        /// <returns></returns>
        public static MessageModel<T> Ok(string msg, T data)
        {
            return Message(0, msg, data);
        }

        /// <summary>
        /// ����ʧ��
        /// </summary>
        /// <param name="status">״̬</param>
        /// <param name="msg">��Ϣ</param>
        /// <returns></returns>
        public static MessageModel<T> Fail(int status, string msg)
        {
            return Message(status, msg, default!);
        }
        /// <summary>
        /// ����ʧ��
        /// </summary>
        /// <param name="status">״̬</param>
        /// <param name="msg">��Ϣ</param>
        /// <param name="data">����</param>
        /// <returns></returns>
        public static MessageModel<T> Fail(int status, string msg, T data)
        {
            return Message(status, msg, data);
        }

        /// <summary>
        /// ������Ϣ
        /// </summary>
        /// <param name="status">״̬�룬0 ��ʾ��ȷ����</param>
        /// <param name="msg">��Ϣ</param>
        /// <param name="data">����</param>
        /// <returns></returns>
        public static MessageModel<T> Message(int status, string msg, T data)
        {
            return new MessageModel<T>() { Msg = msg, Data = data, Status = status };
        }

        /// <summary>
        /// תΪ JSON �ַ���
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var options = new JsonSerializerOptions { Encoder = JavaScriptEncoder.Create(UnicodeRanges.All) };
            return JsonSerializer.Serialize(this, options);
        }
    }
}

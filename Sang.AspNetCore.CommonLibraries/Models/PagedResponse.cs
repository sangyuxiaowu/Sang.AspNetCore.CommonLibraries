using System.Text.Json.Serialization;

namespace Sang.AspNetCore.CommonLibraries.Models
{

    /// <summary>
    /// 分页数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public record class PagedResponse<T>
    {
        /// <summary>
        /// 数据
        /// </summary>
        [JsonPropertyName("data")]
        public IEnumerable<T> Data { get; set; }

        /// <summary>
        /// 总数
        /// </summary>
        [JsonPropertyName("count")]
        public int TotalCount { get; set; }

        /// <summary>
        /// 页码
        /// </summary>
        [JsonPropertyName("page")]
        public int PageIndex { get; set; }

        /// <summary>
        /// 每页大小
        /// </summary>
        [JsonPropertyName("size")]
        public int PageSize { get; set; }

        /// <summary>
        /// 分页数据
        /// </summary>
        /// <param name="data"></param>
        /// <param name="totalCount"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        public PagedResponse(IEnumerable<T> data, int totalCount, int pageIndex, int pageSize)
        {
            Data = data;
            TotalCount = totalCount;
            PageIndex = pageIndex;
            PageSize = pageSize;
        }
    }
}

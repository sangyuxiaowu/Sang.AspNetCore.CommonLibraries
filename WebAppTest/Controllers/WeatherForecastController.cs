using Microsoft.AspNetCore.Mvc;
using Sang.AspNetCore.CommonLibraries.Models;
using System.ComponentModel.DataAnnotations;

namespace WebAppTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<string> Get()
        {
            throw new Exception("测试异常");
        }

        [HttpPost(Name = "PostWeatherForecast")]
        public MessageModel<IEnumerable<string>> Post(TestModel model)
        {
            return MessageModel<IEnumerable<string>>.Success(new string[] { "value1", "value2" });
        }


        [HttpGet("/page")]
        public MessageModel<PagedResponse<string>> PageResponse()
        {
            var data = new List<string> { "value1", "value2" };
            var response = new PagedResponse<string>(data, data.Count, 1, 10);
            return MessageModel<PagedResponse<string>>.Success(response);
        }


        [HttpGet("/")]
        public ContentResult Page()
        {
            var page = new MessagePage
            {
                Icon = MsgIcon.Safe_Warn,
                HtmlTitle = "网页标题",
                Title = "消息标题",
                Desc = "内容详情，可根据实际需要安排，如果换行则不超过规定长度，居中展现[超链接支持](http://www.baidu.com)",
                DescInfo = "内容详情，可根据实际需要安排，如果换行则不超过规定长度，居中展现",
                Custom = "<textarea>自定义 html 区域</textarea>",
                ListInfo = new List<string> { "列表提示项", "列表提示项", "列表提示项" },
                KeyValues = new Dictionary<string, string> { { "姓名", "张三" }, { "微信号", "123" } },
                ListUrl = new List<UrlInfo> { new("链接1", "javascript:"), new("链接2", "javascript:") },
                TipsPre = "内容详情，可根据实际需要安排，如果换行则不超过规定长度，居中展现[超链接支持](http://www.baidu.com)",
                TipsNext = "内容详情，可根据实际需要安排，如果换行则不超过规定长度，居中展现[超链接支持](http://www.baidu.com)",
                OprBtn = new List<UrlInfo> { new("链接1", "javascript:"), new("链接2", "javascript:", "default") },
                FooterLink = new UrlInfo("底部链接", "javascript:"),
                CopyRight = "桑榆肖物 版权信息<br>© 2014-2021 Sang. All Rights Reserved."
            };
            return new ContentResult
            {
                ContentType = "text/html",
                StatusCode = 200,
                Content = page.Render()
            };
        }

        public class TestModel
        {
            /// <summary>
            /// 名称
            /// </summary>
            [Required]
            [StringLength(10, MinimumLength = 5, ErrorMessage = "Err info set")]
            public string Name { get; set; }

            [Required]
            [Range(1, 100, ErrorMessage = "Age 1-100")]
            public int Age { get; set; }
        }

    }
}

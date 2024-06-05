using System.Text;
using System.Text.RegularExpressions;

namespace Sang.AspNetCore.CommonLibraries.Models
{
    /// <summary>
    /// WeUI 单页 HTML 消息页
    /// https://github.com/Tencent/weui/wiki
    /// </summary>
    public class MessagePage
    {

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="title">页面标题</param>
        public MessagePage(string title)
        {
            Title = title;
        }


        private static readonly Regex MarkdownLinkRegex = new Regex(@"\[(.*?)\]\((.*?)\)", RegexOptions.Compiled);

        /// <summary>
        /// 网页标题
        /// </summary>
        public string HtmlTitle { get; set; } = "";

        private string _title = "";
        /// <summary>
        /// 页面标题
        /// </summary>
        public string Title
        {
            get { return _title; }
            set
            {
                if (string.IsNullOrEmpty(HtmlTitle)) HtmlTitle = value;
                _title = $"<h2 class=\"weui-msg__title\">{value}</h2>";
            }
        }

        /// <summary>
        /// 显示图标
        /// </summary>
        public MsgIcon Icon { get; set; } = MsgIcon.None;

        private string _desc = "";
        /// <summary>
        /// 内容详情，可根据实际需要安排，如果换行则不超过规定长度，居中展现
        /// 可添加 Markdown 的 a 标签
        /// </summary>
        public string Desc
        {
            get { return _desc; }
            set
            {
                if(string.IsNullOrEmpty(value)) return;
                _desc = ReplaceMarkdownLinks(value);
            }
        }

        private string desc_info = "";
        /// <summary>
        /// 内容详情，小字描述信息
        /// </summary>
        public string DescInfo
        {
            get { return desc_info; }
            set
            {
                if (string.IsNullOrEmpty(value)) return;
                desc_info = $"<p class=\"weui-msg__desc-primary\">{value}</p>";
            }
        }

        /// <summary>
        /// key-value 展示
        /// </summary>
        public Dictionary<string, string> KeyValues { get; set; } = new();

        /// <summary>
        /// 描述列表 展示
        /// </summary>
        public List<string> ListInfo = new();

        /// <summary>
        /// 跳转列表 展示
        /// </summary>
        public List<UrlInfo> ListUrl { get; set; } = new();

        /// <summary>
        /// 自定义数据展示
        /// </summary>
        public string Custom = "";


        private string tips_pre = "";
        /// <summary>
        /// 按钮操作区，前方提示
        /// 可添加 Markdown 的 a 标签
        /// </summary>
        public string TipsPre
        {
            get { return tips_pre; }
            set
            {
                if (string.IsNullOrEmpty(value)) return;
                tips_pre = ReplaceMarkdownLinks(value);
            }
        }

        private string tips_next = "";
        /// <summary>
        /// 按钮操作区，后方提示
        /// 可添加 Markdown 的 a 标签
        /// </summary>
        public string Tips_Next
        {
            get { return tips_next; }
            set
            {
                if (string.IsNullOrEmpty(value)) return;
                tips_next = ReplaceMarkdownLinks(value);
            }
        }

        /// <summary>
        /// 操作按钮
        /// </summary>
        public UrlInfo[] OprBtn = new UrlInfo[] { };


        /// <summary>
        /// 底部链接
        /// </summary>
        public UrlInfo FooterLink = new("", "");

        /// <summary>
        /// 底部版权信息
        /// </summary>
        public string CopyRight = "";

        /// <summary>
        /// 返回消息页面 HTML 数据
        /// </summary>
        /// <returns></returns>
        public string Render()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(HeadMake());
            sb.Append("<div class=\"weui-msg\">");
            sb.Append(MsgIconMake());
            sb.Append("<div class=\"weui-msg__text-area\">");
            sb.Append(Title);
            sb.Append(Desc);
            sb.Append(DescInfo);
            sb.Append(CustomMake());
            sb.Append("</div>");
            sb.Append(TipsPre);
            sb.Append(OprBtnMake());
            sb.Append(Tips_Next);
            sb.Append(FooterMake());
            sb.Append("</div></div></body></html>");
            return sb.ToString();
        }

        private string FooterMake()
        {
            if (CopyRight == "" && FooterLink.Text == "") return "";
            var link = FooterLink.Text == "" ? "" : $"<p class=\"weui-footer__links\"> <a href=\"{FooterLink.Url}\" class=\"weui-wa-hotarea weui-footer__link\">{FooterLink.Text}</a> </p>";
            var cop = CopyRight == "" ? "" : $" <p class=\"weui-footer__text\">{CopyRight}</p>";
            return $"<div class=\"weui-msg__extra-area\"> <div class=\"weui-footer\"> {link}{cop} </div> </div>";
        }

        /// <summary>
        /// 按钮操作区
        /// </summary>
        /// <returns></returns>
        private string OprBtnMake()
        {
            if (OprBtn.Length == 0) return "";
            StringBuilder sb = new StringBuilder();
            foreach (var it in OprBtn)
            {
                sb.Append("<div class=\"weui-msg__opr-area\"><p class=\"weui-btn-area\"> <a href=\"");
                sb.Append(it.Url);
                sb.Append("\" role=\"button\" class=\"weui-btn weui-btn_");
                sb.Append(it.Type);
                sb.Append("\">");
                sb.Append(it.Text);
                sb.Append("</a> </p></div>");
            }
            return sb.ToString();
        }


        /// <summary>
        /// 自定义展示区
        /// </summary>
        /// <returns></returns>
        private string CustomMake()
        {
            if (string.IsNullOrWhiteSpace(Custom) && KeyValues.Count == 0 && ListInfo.Count == 0 && ListUrl.Count == 0)
            {
                return "";
            }
            var kv = "";
            if (KeyValues.Count > 0)
            {
                foreach (var it in KeyValues)
                {
                    kv += $"<li role=\"option\" class=\"weui-form-preview__item\"><label class=\"weui-form-preview__label\">{it.Key}</label><p class=\"weui-form-preview__value\">{it.Value}</p></li>";
                }
                kv = $"<ul class=\"weui-form-preview__list\">{kv}</ul>";
            }

            var li = "";
            if (ListInfo.Count > 0)
            {
                foreach (var it in ListInfo)
                {
                    li += $"<li role=\"option\" class=\"weui-list-tips__item\">{it}</li>";
                }
                li = $"<ul class=\"weui-list-tips\">{li}</ul>";
            }

            var ur = "";
            if (ListUrl.Count > 0)
            {
                foreach (var it in ListUrl)
                {
                    ur += $"<a class=\"weui-cell weui-cell_access\" href=\"{it.Url}\"> <span class=\"weui-cell__bd\">{it.Text}</span> <span class=\"weui-cell__ft\"></span> </a>";
                }
                ur = $"<ul class=\"weui-cells__group weui-cells__group_form\"><div class=\"weui-cells\">{ur}</div></ul>";
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("<div class=\"weui-msg__custom-area\">");
            sb.Append(Custom);
            sb.Append(kv);
            sb.Append(li);
            sb.Append(ur);
            sb.Append("</div>");
            return sb.ToString();
        }

        /// <summary>
        /// 图标信息生成
        /// </summary>
        /// <returns></returns>
        private string MsgIconMake()
        {
            if (Icon == MsgIcon.None) return "";
            return $"<div class=\"weui-msg__icon-area\"><i class=\"weui-icon-{Enum.GetName(typeof(MsgIcon), Icon)!.ToLower().Replace("_", "-")} weui-icon_msg\"></i></div>";
        }


        /// <summary>
        /// 页面头部信息
        /// </summary>
        /// <returns></returns>
        private string HeadMake()
        {
            return $@"
<!DOCTYPE html>
<html lang=""zh"">
<head>
    <meta charset=""utf-8"">
    <meta name=""viewport"" content=""width=device-width,initial-scale=1,user-scalable=0,viewport-fit=cover"">
    <title>{HtmlTitle}</title>
    <link rel=""stylesheet"" href=""https://res.wx.qq.com/t/wx_fed/weui-source/res/2.5.11/weui.min.css"">
</head>
<body>
    <div style=""position:absolute;top:0;right:0;bottom:0;left:0;overflow:hidden;color:var(--weui-FG-0)"">
";
        }


        /// <summary>
        /// 替换 Markdown 链接
        /// </summary>
        /// <param name="input">输入</param>
        /// <returns></returns>
        private string ReplaceMarkdownLinks(string input)
        {
            return MarkdownLinkRegex.Replace(input, "<a href='$2'>$1</a>");
        }
    }

    public enum MsgIcon
    {
        /// <summary>
        /// 无图标
        /// </summary>
        None,
        /// <summary>
        /// 成功
        /// </summary>
        Success,
        /// <summary>
        /// 失败
        /// </summary>
        Warn,
        /// <summary>
        /// 提示
        /// </summary>
        Info,
        /// <summary>
        /// 警告
        /// </summary>
        Safe_Warn,
        /// <summary>
        /// 等待
        /// </summary>
        Waiting
    }

    /// <summary>
    /// 链接或按钮详情
    /// javascript:history.back();
    /// javascript:WeixinJSBridge.invoke('closeWindow')
    /// </summary>
    /// <param name="Text">描述</param>
    /// <param name="Url">跳转地址</param>
    /// <param name="Type">按钮类型 primary/default</param>
    public record UrlInfo(string Text, string Url, string Type = "primary");
}

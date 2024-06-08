# Sang.AspNetCore.CommonLibraries

[![NuGet version (Sang.AspNetCore.CommonLibraries)](https://img.shields.io/nuget/v/Sang.AspNetCore.CommonLibraries.svg?style=flat-square)](https://www.nuget.org/packages/Sang.AspNetCore.CommonLibraries/)

这是一个用于 ASP.NET Core 的通用库，提供了一些常用的功能和工具。

[English](./README.md) | 简体中文

## 功能

- 异常处理：提供了未处理异常的过滤器，可以自定义异常处理的行为（参见 FilterExtensions.cs）。
- 模型验证：提供了模型验证的过滤器，可以自定义模型验证失败时的行为（参见 FilterExtensions.cs）。
- 消息模型：定义了一个通用的消息模型，用于 API 的响应（参见 MessageModel.cs）。
- 消息页面：定义了一个用于生成 HTML 消息页面的类，支持 Markdown 链接（参见 MessagePage.cs）。

## 使用

```
Install-Package Sang.AspNetCore.CommonLibraries
```

或者

```
dotnet add package Sang.AspNetCore.CommonLibraries

```


### 通用的一致性返回模型

修改你的 API 控制器的返回类型为 MessageModel<T>，其中 T 为你的返回数据类型。例如：

```csharp
[HttpGet]
public MessageModel<string> Get()
{
	return new MessageModel<string>
	{
		Status = 0,
		Msg = "Success",
		Data = "Hello, World!"
	};
}
```

为未处理异常和模型验证失败添加过滤器：

```csharp
builder.Services.AddUnhandledExceptionFilter(config =>
{
    config.Status = 500;
    config.StatusCode = 500;
    config.WithTraceId = false; //是否显示 TraceId
    config.Message = "Unhandled Exception";
});
```

为模型验证失败添加过滤器：

```csharp
builder.Services.AddModelValidationExceptionFilter(config =>
{
	config.Status = 400; //业务状态码
	config.StatusCode = 400; //HTTP 状态码
    config.Message = "Model Validation Exception"; //自定义消息
});
```

### 生成 HTML 消息页面

为了生成 HTML 消息页面，你可以使用 MessagePage 类，这个类提供了一些属性用于设置页面的内容，然后调用 Render 方法生成 HTML 页面。

页面UI采用了 WeUI 2.5.11 版本，微信公众号的风格，包括标题、图标、内容、列表、链接、按钮、底部链接等。

```csharp
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
        OprBtn = new List<UrlInfo> { new("链接1", "javascript:"), new ("链接2", "javascript:", "default") },
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
```
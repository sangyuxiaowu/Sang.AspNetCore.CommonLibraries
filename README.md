# Sang.AspNetCore.CommonLibraries

[![NuGet version (Sang.AspNetCore.CommonLibraries)](https://img.shields.io/nuget/v/Sang.AspNetCore.CommonLibraries.svg?style=flat-square)](https://www.nuget.org/packages/Sang.AspNetCore.CommonLibraries/)

This is a general library for ASP.NET Core, providing some common functionalities and tools.

English | [简体中文](./README_CN.md)

## Features

- Exception Handling: Provides a filter for unhandled exceptions, allowing customization of exception handling behavior (see FilterExtensions.cs).
- Model Validation: Provides a filter for model validation, allowing customization of behavior when model validation fails (see FilterExtensions.cs).
- Message Model: Defines a general message model for API responses (see MessageModel.cs).
- Message Page: Defines a class for generating HTML message pages, supporting Markdown links (see MessagePage.cs).

## Usage

```
Install-Package Sang.AspNetCore.CommonLibraries
```

Or

```
dotnet add package Sang.AspNetCore.CommonLibraries
```

### General Consistent Return Model

Change the return type of your API controller to MessageModel<T>, where T is the type of data you are returning. For example:

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

Paged return example:
```csharp
[HttpGet("/page")]
public MessageModel<PagedResponse<string>> PageResponse()
{
    var data = new List<string> { "value1", "value2" };
    var response = new PagedResponse<string>(data, data.Count, 1, 10);
    return MessageModel<PagedResponse<string>>.Success(response);
}
```

Add filters for unhandled exceptions and model validation failures:

```csharp
builder.Services.AddUnhandledExceptionFilter(config =>
{
    config.Status = 500;
    config.StatusCode = 500;
    config.WithTraceId = false; //Whether to return TraceId
    config.Message = "Unhandled Exception";
});
```

Add a filter for model validation failure:

```csharp
builder.Services.AddModelValidationExceptionFilter(config =>
{
	config.Status = 400; //Business status code
	config.StatusCode = 400; //HTTP status code
    config.Message = "Model Validation Exception"; //Custom message
});
```

### Json Example


```json
{
  "status": 0,
  "msg": "ok",
  "data": [
    "value1",
    "value2"
  ]
}
```

```json
{
  "status": 400,
  "msg": "Bad Request",
  "data": [
    {
      "field": "Age",
      "err": [
        "Age 1-100"
      ]
    },
    {
      "field": "Name",
      "err": [
        "Err info set"
      ]
    }
  ],
  "traceId": "00-6e7bf0a442c4787f3d17d2124c50017d-7f5bf597c46d289f-00"
}
```

```json
{
  "status": 500,
  "msg": "System.Exceptione",
  "traceId": "00-7a0900c0de5accfbffd699081facf718-5ab7a3bcfc4fff6c-00"
}
```


```json
{
  "status": 0,
  "msg": "ok",
  "data": {
    "data": [
      "value1",
      "value2"
    ],
    "count": 2,
    "page": 1,
    "size": 10
  }
}
```



### Generating HTML Message Pages

To generate HTML message pages, you can use the MessagePage class. This class provides some properties for setting the content of the page, and then you can call the Render method to generate the HTML page.

The page UI adopts WeUI version 2.5.11, styled after WeChat official accounts, including title, icon, content, list, links, buttons, and footer links.

```csharp
public ContentResult Page()
{
    var page = new MessagePage
    {
        Icon = MsgIcon.Safe_Warn,
        HtmlTitle = "Webpage Title",
        Title = "Message Title",
        Desc = "Detail content, can be arranged according to actual needs. If it wraps, it should not exceed the specified length and should be centered [hyperlink support](http://www.baidu.com)",
        DescInfo = "Detail content, can be arranged according to actual needs. If it wraps, it should not exceed the specified length and should be centered",
        Custom = "<textarea>Custom html area</textarea>",
        ListInfo = new List<string> { "List hint item", "List hint item", "List hint item" },
        KeyValues = new Dictionary<string, string> { { "Name", "Zhang San" }, { "WeChat ID", "123" } },
        ListUrl = new List<UrlInfo> { new("Link 1", "javascript:"), new("Link 2", "javascript:") },
        TipsPre = "Detail content, can be arranged according to actual needs. If it wraps, it should not exceed the specified length and should be centered [hyperlink support](http://www.baidu.com)",
        TipsNext = "Detail content, can be arranged according to actual needs. If it wraps, it should not exceed the specified length and should be centered [hyperlink support](http://www.baidu.com)",
        OprBtn = new List<UrlInfo> { new("Link 1", "javascript:"), new ("Link 2", "javascript:", "default") },
        FooterLink = new UrlInfo("Footer Link", "javascript:"),
        CopyRight = "Sang Late Autumn Copyright Information<br>© 2014-2021 Sang. All Rights Reserved."
    };
    return new ContentResult
    {
        ContentType = "text/html",
        StatusCode = 200,
        Content = page.Render()
    };
}
```
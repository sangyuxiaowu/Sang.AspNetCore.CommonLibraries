using System.Text.Json.Serialization;
using Sang.AspNetCore.CommonLibraries.Models;

namespace WebAppAotTest;

[JsonSerializable(typeof(MessageModel<global::WeatherForecast[]>))]
[JsonSerializable(typeof(global::WeatherForecast[]))]
internal partial class WebAppAotTestJsonContext : JsonSerializerContext
{
}

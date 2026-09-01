using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

[JsonConverter(typeof(StringEnumConverter))]
public enum Gamemodes
{
	Challange = 0,
	Chill = 1
}

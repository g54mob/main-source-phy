using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

[JsonConverter(typeof(StringEnumConverter))]
public enum LeaderboardPeriod
{
	Daily = 0,
	AllTime = 1
}

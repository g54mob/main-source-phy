using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json.Linq;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class UploadMappingResults : BaseResult
	{
		public string Message { get; set; }

		public Dictionary<string, string> Mappings { get; set; }

		public string NextCursor { get; set; }

		internal override void SetValues(JToken source)
		{
			base.SetValues(source);
			if (Mappings == null)
			{
				Mappings = new Dictionary<string, string>();
			}
			if (source == null)
			{
				return;
			}
			string message = source.Value<string>("message") ?? string.Empty;
			Message = message;
			JToken jToken = source["mappings"];
			if (jToken != null)
			{
				foreach (JToken item in jToken.Children())
				{
					Mappings.Add(item["folder"].ToString(), item["template"].ToString());
				}
			}
			string text = source.Value<string>("folder") ?? string.Empty;
			string value = source.Value<string>("template") ?? string.Empty;
			if (!string.IsNullOrEmpty(text))
			{
				Mappings.Add(text, value);
			}
			NextCursor = source.Value<string>("next_cursor") ?? string.Empty;
		}
	}
}

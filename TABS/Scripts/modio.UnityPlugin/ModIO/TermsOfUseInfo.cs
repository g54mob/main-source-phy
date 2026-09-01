using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ModIO
{
	[Serializable]
	public class TermsOfUseInfo
	{
		[Serializable]
		public struct LinkData
		{
			[JsonProperty("text")]
			public string buttonText;

			[JsonProperty("url")]
			public string URL;

			[JsonProperty("required")]
			public bool required;
		}

		private struct ButtonInfo
		{
			public string text;
		}

		[JsonProperty("plaintext")]
		public string terms;

		[JsonProperty("html")]
		public string terms_HTML;

		[JsonProperty("buttons.agree.text")]
		public string buttonText_agree;

		[JsonProperty("buttons.disagree.text")]
		public string buttonText_disagree;

		[JsonProperty("links")]
		public Dictionary<string, LinkData> links;

		private const string APIOBJECT_LINKKEY_WEBSITE = "website";

		private const string APIOBJECT_LINKKEY_TERMS = "terms";

		private const string APIOBJECT_LINKKEY_PRIVACY = "privacy";

		private const string APIOBJECT_LINKKEY_ACCOUNT = "manage";

		[JsonExtensionData]
		private IDictionary<string, JToken> m_extensionData;

		[OnDeserialized]
		private void OnDeserialized(StreamingContext context)
		{
			if (m_extensionData != null && m_extensionData.Count > 0 && m_extensionData.TryGetValue("buttons", out var value))
			{
				buttonText_agree = (string)value["agree"]["text"];
				buttonText_disagree = (string)value["disagree"]["text"];
			}
		}
	}
}

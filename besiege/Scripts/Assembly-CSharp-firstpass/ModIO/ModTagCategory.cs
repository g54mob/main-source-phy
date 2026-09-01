using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ModIO
{
	[Serializable]
	public class ModTagCategory
	{
		public const string APIOBJECT_VALUESTRING_ISSINGLETAG = "DROPDOWN";

		public const string APIOBJECT_VALUESTRING_ISMULTITAG = "CHECKBOXES";

		[JsonProperty("name")]
		public string name;

		[JsonProperty("multiple_tags")]
		public bool isMultiTagCategory;

		[JsonProperty("hidden")]
		public bool isHidden;

		[JsonProperty("tags")]
		public string[] tags;

		[JsonProperty("type")]
		private string _typeString;

		[OnDeserialized]
		private void OnDeserialized(StreamingContext context)
		{
			if (!string.IsNullOrEmpty(_typeString))
			{
				isMultiTagCategory = "CHECKBOXES".Equals(_typeString.ToUpper());
				_typeString = null;
			}
		}
	}
}

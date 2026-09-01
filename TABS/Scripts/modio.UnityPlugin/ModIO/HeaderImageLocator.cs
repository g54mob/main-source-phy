using System;
using Newtonsoft.Json;

namespace ModIO
{
	[Serializable]
	public class HeaderImageLocator : IImageLocator
	{
		[JsonProperty("filename")]
		public string fileName;

		[JsonProperty("original")]
		public string url;

		public string GetFileName()
		{
			return fileName;
		}

		public string GetURL()
		{
			return url;
		}
	}
}

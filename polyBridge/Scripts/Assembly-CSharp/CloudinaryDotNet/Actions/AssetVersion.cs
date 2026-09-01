using System;
using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class AssetVersion
	{
		[DataMember(Name = "version_id")]
		public string VersionId { get; set; }

		[DataMember(Name = "version")]
		public string Version { get; set; }

		[DataMember(Name = "size")]
		public string Size { get; set; }

		[DataMember(Name = "time")]
		public DateTime Time { get; set; }

		[DataMember(Name = "restorable")]
		public bool Restorable { get; set; }

		[DataMember(Name = "url")]
		public string Url { get; set; }
	}
}

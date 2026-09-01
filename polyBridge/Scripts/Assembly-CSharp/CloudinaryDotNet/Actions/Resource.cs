using System;
using System.Runtime.Serialization;
using Newtonsoft.Json.Linq;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class Resource : UploadResult
	{
		[DataMember(Name = "resource_type")]
		public string ResourceType { get; set; }

		[DataMember(Name = "type")]
		public string Type { get; set; }

		[Obsolete("Property Created is deprecated, please use CreatedAt instead")]
		public string Created
		{
			get
			{
				return CreatedAt;
			}
			set
			{
				CreatedAt = value;
			}
		}

		[DataMember(Name = "created_at")]
		public string CreatedAt { get; set; }

		[DataMember(Name = "width")]
		public int Width { get; set; }

		[DataMember(Name = "height")]
		public int Height { get; set; }

		[DataMember(Name = "tags")]
		public string[] Tags { get; set; }

		[DataMember(Name = "backup")]
		public bool? Backup { get; set; }

		[DataMember(Name = "moderation_status")]
		public ModerationStatus? ModerationStatus { get; set; }

		[DataMember(Name = "context")]
		public JToken Context { get; set; }

		public string FullyQualifiedPublicId => ResourceType + "/" + Type + "/" + base.PublicId;

		[DataMember(Name = "access_mode")]
		public string AccessMode { get; set; }
	}
}

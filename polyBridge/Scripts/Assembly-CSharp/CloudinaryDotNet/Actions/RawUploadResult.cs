using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class RawUploadResult : UploadResult
	{
		[DataMember(Name = "signature")]
		public string Signature { get; set; }

		[DataMember(Name = "type")]
		public string Type { get; set; }

		[DataMember(Name = "resource_type")]
		public string ResourceType { get; set; }

		[DataMember(Name = "moderation")]
		public List<Moderation> Moderation { get; set; }

		[DataMember(Name = "created_at")]
		public DateTime CreatedAt { get; set; }

		[DataMember(Name = "tags")]
		public string[] Tags { get; set; }

		[DataMember(Name = "access_control")]
		public List<AccessControlRule> AccessControl { get; set; }

		public string FullyQualifiedPublicId => ResourceType + "/" + Type + "/" + base.PublicId;

		[DataMember(Name = "access_mode")]
		public string AccessMode { get; set; }

		[DataMember(Name = "etag")]
		public string Etag { get; set; }

		[DataMember(Name = "placeholder")]
		public bool Placeholder { get; set; }

		[DataMember(Name = "original_filename")]
		public string OriginalFilename { get; set; }
	}
}

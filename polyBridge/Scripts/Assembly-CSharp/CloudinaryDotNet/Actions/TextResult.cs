using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class TextResult : BaseResult
	{
		[DataMember(Name = "resource_type")]
		protected string m_resourceType;

		[DataMember(Name = "width")]
		public int Width { get; set; }

		[DataMember(Name = "height")]
		public int Height { get; set; }

		[DataMember(Name = "public_id")]
		public string PublicId { get; set; }

		[DataMember(Name = "version")]
		public string Version { get; set; }

		[DataMember(Name = "signature")]
		public string Signature { get; set; }

		[DataMember(Name = "format")]
		public string Format { get; set; }

		public ResourceType ResourceType => ApiShared.ParseCloudinaryParam<ResourceType>(m_resourceType);

		[DataMember(Name = "created_at")]
		public string CreatedAt { get; set; }

		[DataMember(Name = "tags")]
		public string[] Tags { get; set; }

		[DataMember(Name = "bytes")]
		public long Bytes { get; set; }

		[DataMember(Name = "type")]
		public string Type { get; set; }

		[DataMember(Name = "placeholder")]
		public bool Placeholder { get; set; }

		[DataMember(Name = "url")]
		public string Url { get; set; }

		[DataMember(Name = "secure_url")]
		public string SecureUrl { get; set; }

		[DataMember(Name = "access_mode")]
		public string AccessMode { get; set; }
	}
}

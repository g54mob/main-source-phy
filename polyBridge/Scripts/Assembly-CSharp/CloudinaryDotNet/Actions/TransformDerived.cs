using System;
using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class TransformDerived
	{
		[DataMember(Name = "resource_type")]
		public string m_resourceType;

		[DataMember(Name = "public_id")]
		public string PublicId { get; set; }

		public ResourceType ResourceType => ApiShared.ParseCloudinaryParam<ResourceType>(m_resourceType);

		[DataMember(Name = "type")]
		public string Type { get; set; }

		[DataMember(Name = "format")]
		public string Format { get; set; }

		[DataMember(Name = "url")]
		public string Url { get; set; }

		[DataMember(Name = "secure_url")]
		public string SecureUrl { get; set; }

		[Obsolete("Property Length is deprecated, please use Bytes instead")]
		public long Length
		{
			get
			{
				return Bytes;
			}
			set
			{
				Bytes = value;
			}
		}

		[DataMember(Name = "bytes")]
		public long Bytes { get; set; }

		[DataMember(Name = "id")]
		public string Id { get; set; }
	}
}

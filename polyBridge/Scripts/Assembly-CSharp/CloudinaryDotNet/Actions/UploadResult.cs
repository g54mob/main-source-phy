using System;
using System.Runtime.Serialization;
using Newtonsoft.Json.Linq;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public abstract class UploadResult : BaseResult
	{
		[DataMember(Name = "public_id")]
		public string PublicId { get; set; }

		[DataMember(Name = "version")]
		public string Version { get; set; }

		[Obsolete("Property Uri is deprecated, please use Url instead")]
		public Uri Uri
		{
			get
			{
				return Url;
			}
			set
			{
				Url = value;
			}
		}

		[DataMember(Name = "url")]
		public Uri Url { get; set; }

		[Obsolete("Property SecureUri is deprecated, please use SecureUrl instead")]
		public Uri SecureUri
		{
			get
			{
				return SecureUrl;
			}
			set
			{
				SecureUrl = value;
			}
		}

		[DataMember(Name = "secure_url")]
		public Uri SecureUrl { get; set; }

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

		[DataMember(Name = "format")]
		public string Format { get; set; }

		[DataMember(Name = "metadata")]
		public JToken MetadataFields { get; set; }
	}
}

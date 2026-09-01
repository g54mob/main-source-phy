using System;
using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class MultiResult : BaseResult
	{
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

		[DataMember(Name = "public_id")]
		public string PublicId { get; set; }

		[DataMember(Name = "version")]
		public string Version { get; set; }
	}
}

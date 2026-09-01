using System;
using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class Eager
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

		[DataMember(Name = "transformation")]
		public string Transformation { get; set; }

		[DataMember(Name = "width")]
		public int Width { get; set; }

		[DataMember(Name = "height")]
		public int Height { get; set; }

		[DataMember(Name = "bytes")]
		public long Bytes { get; set; }

		[DataMember(Name = "format")]
		public string Format { get; set; }
	}
}

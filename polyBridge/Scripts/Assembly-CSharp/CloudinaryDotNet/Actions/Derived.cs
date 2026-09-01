using System;
using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class Derived
	{
		[DataMember(Name = "transformation")]
		public string Transformation { get; set; }

		[DataMember(Name = "format")]
		public string Format { get; set; }

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

		[DataMember(Name = "url")]
		public string Url { get; set; }

		[DataMember(Name = "secure_url")]
		public string SecureUrl { get; set; }
	}
}

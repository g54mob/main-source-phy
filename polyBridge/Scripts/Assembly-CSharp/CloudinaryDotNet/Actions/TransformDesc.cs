using System;
using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class TransformDesc
	{
		[DataMember(Name = "name")]
		public string Name { get; set; }

		[Obsolete("Property Strict is deprecated, please use AllowedForStrict instead")]
		public bool Strict
		{
			get
			{
				return AllowedForStrict;
			}
			set
			{
				AllowedForStrict = value;
			}
		}

		[DataMember(Name = "allowed_for_strict")]
		public bool AllowedForStrict { get; set; }

		[DataMember(Name = "used")]
		public bool Used { get; set; }

		[DataMember(Name = "named")]
		public bool Named { get; set; }
	}
}

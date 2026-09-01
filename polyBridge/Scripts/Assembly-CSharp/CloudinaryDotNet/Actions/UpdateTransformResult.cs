using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class UpdateTransformResult : BaseResult
	{
		[DataMember(Name = "name")]
		public string Name { get; set; }

		[DataMember(Name = "allowed_for_strict")]
		public bool AllowedForStrict { get; set; }

		[DataMember(Name = "used")]
		public bool Used { get; set; }

		[DataMember(Name = "info")]
		public Dictionary<string, string>[] Info { get; set; }

		[DataMember(Name = "derived")]
		public TransformDerived[] Derived { get; set; }

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

		[DataMember(Name = "message")]
		public string Message { get; set; }
	}
}

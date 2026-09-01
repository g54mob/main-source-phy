using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class MetadataValidationResult : BaseResult
	{
		[DataMember(Name = "type")]
		public string Type { get; set; }

		[DataMember(Name = "value")]
		public object Value { get; set; }

		[DataMember(Name = "equals")]
		public bool? IsEqual { get; set; }

		[DataMember(Name = "min")]
		public int? Min { get; set; }

		[DataMember(Name = "max")]
		public int? Max { get; set; }

		[DataMember(Name = "rules")]
		public List<MetadataValidationResult> Rules { get; set; }
	}
}

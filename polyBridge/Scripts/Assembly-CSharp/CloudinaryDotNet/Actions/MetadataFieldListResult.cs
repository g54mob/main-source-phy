using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class MetadataFieldListResult : BaseResult
	{
		[DataMember(Name = "metadata_fields")]
		public IEnumerable<MetadataFieldResult> MetadataFields { get; set; }
	}
}

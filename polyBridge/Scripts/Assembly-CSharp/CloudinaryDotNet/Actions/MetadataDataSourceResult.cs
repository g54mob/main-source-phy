using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class MetadataDataSourceResult : BaseResult
	{
		[DataMember(Name = "values")]
		public List<EntryResult> Values { get; set; }
	}
}

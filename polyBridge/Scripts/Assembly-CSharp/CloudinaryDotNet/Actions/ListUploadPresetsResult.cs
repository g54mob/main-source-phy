using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class ListUploadPresetsResult : BaseResult
	{
		[DataMember(Name = "presets")]
		public List<GetUploadPresetResult> Presets { get; set; }

		[DataMember(Name = "next_cursor")]
		public string NextCursor { get; set; }
	}
}

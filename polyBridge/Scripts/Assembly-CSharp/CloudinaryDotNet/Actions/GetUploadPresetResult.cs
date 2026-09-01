using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class GetUploadPresetResult : BaseResult
	{
		[DataMember(Name = "name")]
		public string Name { get; set; }

		[DataMember(Name = "unsigned")]
		public bool Unsigned { get; set; }

		[DataMember(Name = "settings")]
		public UploadSettings Settings { get; set; }

		[DataMember(Name = "eval")]
		public string Eval { get; set; }
	}
}

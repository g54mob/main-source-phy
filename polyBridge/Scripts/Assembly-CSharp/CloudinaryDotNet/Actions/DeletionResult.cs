using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class DeletionResult : BaseResult
	{
		[DataMember(Name = "result")]
		public string Result { get; set; }
	}
}

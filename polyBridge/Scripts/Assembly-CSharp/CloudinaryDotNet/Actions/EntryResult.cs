using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class EntryResult
	{
		[DataMember(Name = "external_id")]
		public string ExternalId { get; set; }

		[DataMember(Name = "value")]
		public string Value { get; set; }

		[DataMember(Name = "state")]
		public string State { get; set; }
	}
}

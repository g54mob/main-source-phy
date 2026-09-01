using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class Moderation
	{
		[DataMember(Name = "status")]
		public ModerationStatus Status;

		[DataMember(Name = "kind")]
		public string Kind;

		[DataMember(Name = "response")]
		[JsonConverter(typeof(ModerationResponseConverter))]
		public ModerationResponse Response;

		[DataMember(Name = "updated_at")]
		public DateTime UpdatedAt;
	}
}

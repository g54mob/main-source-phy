using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class Representation
	{
		[DataMember(Name = "transformation")]
		[JsonConverter(typeof(RepresentationsConverter))]
		public Transformation Transformation;
	}
}

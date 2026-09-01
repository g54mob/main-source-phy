using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class Coordinates
	{
		[DataMember(Name = "custom")]
		public int[][] Custom { get; set; }

		[DataMember(Name = "faces")]
		public int[][] Faces { get; set; }
	}
}

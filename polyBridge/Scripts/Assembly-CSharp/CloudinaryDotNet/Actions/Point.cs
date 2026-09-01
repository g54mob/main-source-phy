using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class Point
	{
		[DataMember(Name = "x")]
		public double X { get; set; }

		public double Y { get; set; }
	}
}

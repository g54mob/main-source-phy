using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class AccessibilityAnalysis
	{
		[DataMember(Name = "colorblind_accessibility_analysis")]
		public ColorblindAccessibilityAnalysis ColorblindAccessibilityAnalysis { get; set; }

		[DataMember(Name = "colorblind_accessibility_score")]
		public double ColorblindAccessibilityScore { get; set; }
	}
}

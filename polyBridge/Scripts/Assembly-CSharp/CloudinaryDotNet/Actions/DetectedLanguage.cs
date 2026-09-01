using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class DetectedLanguage
	{
		[DataMember(Name = "languageCode")]
		public string LanguageCode { get; set; }
	}
}

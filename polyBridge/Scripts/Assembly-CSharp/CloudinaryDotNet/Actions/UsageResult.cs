using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class UsageResult : BaseResult
	{
		[DataMember(Name = "plan")]
		public string Plan { get; set; }

		[DataMember(Name = "last_updated")]
		public DateTime LastUpdated { get; set; }

		[DataMember(Name = "objects")]
		public Usage Objects { get; set; }

		[DataMember(Name = "bandwidth")]
		public Usage Bandwidth { get; set; }

		[DataMember(Name = "storage")]
		public Usage Storage { get; set; }

		[DataMember(Name = "requests")]
		public long Requests { get; set; }

		[DataMember(Name = "resources")]
		public int Resources { get; set; }

		[DataMember(Name = "derived_resources")]
		public int DerivedResources { get; set; }

		[DataMember(Name = "transformations")]
		public Usage Transformations { get; set; }

		[DataMember(Name = "webpurify")]
		public Usage Webpurify { get; set; }

		[DataMember(Name = "adv_ocr")]
		public Usage AdvOcr { get; set; }

		[DataMember(Name = "aws_rek_moderation")]
		public Usage AwsRekModeration { get; set; }

		[DataMember(Name = "search_api")]
		public Usage SearchApi { get; set; }

		[DataMember(Name = "url2png")]
		public Usage Url2png { get; set; }

		[DataMember(Name = "aspose")]
		public Usage Aspose { get; set; }

		[DataMember(Name = "style_transfer")]
		public Usage StyleTransfer { get; set; }

		[DataMember(Name = "azure_video_indexer")]
		public Usage AzureVideoIndexer { get; set; }

		[DataMember(Name = "object_detection")]
		public Usage ObjectDetection { get; set; }

		[DataMember(Name = "credits")]
		public Usage Credits { get; set; }

		[DataMember(Name = "media_limits")]
		public Dictionary<string, long> MediaLimits { get; set; }
	}
}

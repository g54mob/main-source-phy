using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class SpriteResult : BaseResult
	{
		[Obsolete("Property CssUri is deprecated, please use CssUrl instead")]
		public Uri CssUri
		{
			get
			{
				return CssUrl;
			}
			set
			{
				CssUrl = value;
			}
		}

		[DataMember(Name = "css_url")]
		public Uri CssUrl { get; set; }

		[Obsolete("Property SecureCssUri is deprecated, please use SecureCssUrl instead")]
		public Uri SecureCssUri
		{
			get
			{
				return SecureCssUrl;
			}
			set
			{
				SecureCssUrl = value;
			}
		}

		[DataMember(Name = "secure_css_url")]
		public Uri SecureCssUrl { get; set; }

		[Obsolete("Property ImageUri is deprecated, please use ImageUrl instead")]
		public Uri ImageUri
		{
			get
			{
				return ImageUrl;
			}
			set
			{
				ImageUrl = value;
			}
		}

		[DataMember(Name = "image_url")]
		public Uri ImageUrl { get; set; }

		[DataMember(Name = "secure_image_url")]
		public Uri SecureImageUrl { get; set; }

		[Obsolete("Property JsonUri is deprecated, please use JsonUrl instead")]
		public Uri JsonUri
		{
			get
			{
				return JsonUrl;
			}
			set
			{
				JsonUrl = value;
			}
		}

		[DataMember(Name = "json_url")]
		public Uri JsonUrl { get; set; }

		[DataMember(Name = "secure_json_url")]
		public Uri SecureJsonUrl { get; set; }

		[DataMember(Name = "public_id")]
		public string PublicId { get; set; }

		[DataMember(Name = "version")]
		public string Version { get; set; }

		[DataMember(Name = "image_infos")]
		public Dictionary<string, ImageInfo> ImageInfos { get; set; }
	}
}

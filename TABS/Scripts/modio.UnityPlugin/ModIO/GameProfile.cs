using System;
using Newtonsoft.Json;

namespace ModIO
{
	[Serializable]
	public class GameProfile
	{
		public const int NULL_ID = 0;

		[JsonProperty("id")]
		public int id;

		[JsonProperty("status")]
		public GameStatus status;

		[JsonProperty("name")]
		public string name;

		[JsonProperty("name_id")]
		public string nameId;

		[JsonProperty("summary")]
		public string summary;

		[JsonProperty("instructions")]
		public string instructions;

		[JsonProperty("instructions_url")]
		public string instructionsURL;

		[JsonProperty("submitted_by")]
		public UserProfile submittedBy;

		[JsonProperty("date_added")]
		public int dateAdded;

		[JsonProperty("date_updated")]
		public int dateUpdated;

		[JsonProperty("date_live")]
		public int dateLive;

		[JsonProperty("ugc_name")]
		public string ugcName;

		[JsonProperty("presentation_option")]
		public GameModGalleryPresentation modGalleryPresentation;

		[JsonProperty("submission_option")]
		public GameModSubmissionPermission modSubmissionPermission;

		[JsonProperty("curation_option")]
		public GameModCuration modCuration;

		[JsonProperty("community_options")]
		public GameCommunityFeatures communityFeatures;

		[JsonProperty("revenue_options")]
		public GameModRevenuePermissions modRevenuePermissions;

		[JsonProperty("api_access_options")]
		public GameAPIPermissions apiPermissions;

		[JsonProperty("maturity_options")]
		public GameModContentPermission contentPermission;

		[JsonProperty("icon")]
		public IconImageLocator iconLocator;

		[JsonProperty("logo")]
		public LogoImageLocator logoLocator;

		[JsonProperty("header")]
		public HeaderImageLocator headerImageLocator;

		[JsonProperty("profile_url")]
		public string profileURL;

		[JsonProperty("tag_options")]
		public ModTagCategory[] tagCategories;
	}
}

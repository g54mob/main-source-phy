using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ModIO
{
	[Serializable]
	public class ModProfile
	{
		public const int NULL_ID = 0;

		[JsonProperty("id")]
		public int id;

		[JsonProperty("game_id")]
		public int gameId;

		[JsonProperty("status")]
		public ModStatus status;

		[JsonProperty("visible")]
		public ModVisibility visibility;

		[JsonProperty("submitted_by")]
		public UserProfile submittedBy;

		[JsonProperty("date_added")]
		public int dateAdded;

		[JsonProperty("date_updated")]
		public int dateUpdated;

		[JsonProperty("date_live")]
		public int dateLive;

		[JsonProperty("maturity_option")]
		public ModContentWarnings contentWarnings;

		[JsonProperty("logo")]
		public LogoImageLocator logoLocator;

		[JsonProperty("homepage_url")]
		public string homepageURL;

		[JsonProperty("name")]
		public string name;

		[JsonProperty("name_id")]
		public string nameId;

		[JsonProperty("summary")]
		public string summary;

		[JsonProperty("description")]
		public string descriptionAsHTML;

		[JsonProperty("description_plaintext")]
		public string descriptionAsText;

		[JsonProperty("metadata_blob")]
		public string metadataBlob;

		[JsonProperty("profile_url")]
		public string profileURL;

		[JsonProperty("modfile")]
		public Modfile currentBuild;

		[JsonProperty("media")]
		public ModMediaCollection media;

		[JsonProperty("metadata_kvp")]
		public MetadataKVP[] metadataKVPs;

		[JsonProperty("tags")]
		public ModTag[] tags;

		[JsonProperty("stats")]
		public ModStatistics statistics;

		[JsonIgnore]
		public IEnumerable<string> tagNames
		{
			get
			{
				if (tags != null)
				{
					ModTag[] array = tags;
					foreach (ModTag tag in array)
					{
						yield return tag.name;
					}
				}
			}
		}
	}
}

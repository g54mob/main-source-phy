using System;

namespace ModIO.UI
{
	[Serializable]
	[Obsolete("No longer supported.")]
	public struct ModProfileDisplayData
	{
		public int modId;

		public int gameId;

		public ModStatus status;

		public ModVisibility visibility;

		public int dateAdded;

		public int dateUpdated;

		public int dateLive;

		public ModContentWarnings contentWarnings;

		public string homepageURL;

		public string name;

		public string nameId;

		public string summary;

		public string descriptionAsHTML;

		public string descriptionAsText;

		public string metadataBlob;

		public string profileURL;

		public MetadataKVP[] metadataKVPs;

		public static ModProfileDisplayData CreateFromProfile(ModProfile profile)
		{
			return new ModProfileDisplayData
			{
				modId = profile.id,
				gameId = profile.gameId,
				status = profile.status,
				visibility = profile.visibility,
				dateAdded = profile.dateAdded,
				dateUpdated = profile.dateUpdated,
				dateLive = profile.dateLive,
				contentWarnings = profile.contentWarnings,
				homepageURL = profile.homepageURL,
				name = profile.name,
				nameId = profile.nameId,
				summary = profile.summary,
				descriptionAsHTML = profile.descriptionAsHTML,
				descriptionAsText = profile.descriptionAsText,
				metadataBlob = profile.metadataBlob,
				profileURL = profile.profileURL,
				metadataKVPs = profile.metadataKVPs
			};
		}
	}
}

using System;
using System.Linq;

namespace ModIO
{
	[Serializable]
	public class EditableModProfile
	{
		public EditableModStatusField status = new EditableModStatusField();

		public EditableModVisibilityField visibility = new EditableModVisibilityField();

		public EditableStringField name = new EditableStringField();

		public EditableStringField nameId = new EditableStringField();

		public EditableStringField summary = new EditableStringField();

		public EditableStringField descriptionAsHTML = new EditableStringField();

		public EditableStringField homepageURL = new EditableStringField();

		public EditableStringArrayField tags = new EditableStringArrayField();

		public EditableStringField metadataBlob = new EditableStringField();

		public EditableKVPArrayField metadataKVPs = new EditableKVPArrayField();

		public EditableImageLocatorField logoLocator = new EditableImageLocatorField();

		public EditableStringArrayField youTubeURLs = new EditableStringArrayField();

		public EditableStringArrayField sketchfabURLs = new EditableStringArrayField();

		public EditableImageLocatorArrayField galleryImageLocators = new EditableImageLocatorArrayField();

		public static EditableModProfile CreateFromProfile(ModProfile profile)
		{
			EditableModProfile editableModProfile = new EditableModProfile();
			editableModProfile.ApplyBaseProfileChanges(profile);
			return editableModProfile;
		}

		public void ApplyBaseProfileChanges(ModProfile profile)
		{
			if (!status.isDirty)
			{
				status.value = profile.status;
			}
			if (!visibility.isDirty)
			{
				visibility.value = profile.visibility;
			}
			if (!name.isDirty)
			{
				name.value = profile.name;
			}
			if (!nameId.isDirty)
			{
				nameId.value = profile.nameId;
			}
			if (!summary.isDirty)
			{
				summary.value = profile.summary;
			}
			if (!descriptionAsHTML.isDirty)
			{
				descriptionAsHTML.value = profile.descriptionAsHTML;
			}
			if (!homepageURL.isDirty)
			{
				homepageURL.value = profile.homepageURL;
			}
			if (!metadataBlob.isDirty)
			{
				metadataBlob.value = profile.metadataBlob;
			}
			if (!metadataBlob.isDirty)
			{
				metadataKVPs.value = profile.metadataKVPs;
			}
			if (!tags.isDirty)
			{
				tags.value = profile.tagNames.ToArray();
			}
			if (!logoLocator.isDirty)
			{
				logoLocator.value.fileName = profile.logoLocator.fileName;
				logoLocator.value.url = profile.logoLocator.GetURL();
			}
			if (!youTubeURLs.isDirty)
			{
				youTubeURLs.value = profile.media.youTubeURLs;
			}
			if (!sketchfabURLs.isDirty)
			{
				sketchfabURLs.value = profile.media.sketchfabURLs;
			}
			if (!galleryImageLocators.isDirty)
			{
				Utility.SafeMapArraysOrZero(profile.media.galleryImageLocators, (GalleryImageLocator l) => ImageLocatorData.CreateFromImageLocator(l), out galleryImageLocators.value);
			}
		}
	}
}

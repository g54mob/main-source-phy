using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ModIO.API;
using UnityEngine;

namespace ModIO
{
	internal class ModManager_SubmitModOperation
	{
		public Action<ModProfile> onSuccess;

		public Action<WebRequestError> onError;

		private int modId;

		private EditableModProfile eModProfile;

		private AddModParameters addModParams;

		private string logoPath;

		private byte[] logoData;

		private byte[] imageArchiveData;

		private List<string> removedImageFileNames;

		private List<string> removedYouTubeURLs;

		private List<string> removedSketchfabURLs;

		private List<string> addedImageFilePaths;

		private List<string> addedYouTubeURLs;

		private List<string> addedSketchfabURLs;

		private List<string> removedTags;

		private List<string> addedTags;

		private Dictionary<string, string> removedKVPs;

		private Dictionary<string, string> addedKVPs;

		public void SubmitNewMod(EditableModProfile newModProfile)
		{
			string text = null;
			if (string.IsNullOrEmpty(newModProfile.name.value))
			{
				text = "Mod Profile needs to be named before it can be uploaded";
			}
			else if (string.IsNullOrEmpty(newModProfile.summary.value))
			{
				text = "Mod Profile needs to be given a summary before it can be uploaded";
			}
			if (text == null)
			{
				addModParams = new AddModParameters();
				addModParams.name = newModProfile.name.value;
				addModParams.summary = newModProfile.summary.value;
				if (newModProfile.visibility.isDirty)
				{
					addModParams.visibility = newModProfile.visibility.value;
				}
				if (newModProfile.nameId.isDirty)
				{
					addModParams.nameId = newModProfile.nameId.value;
				}
				if (newModProfile.descriptionAsHTML.isDirty)
				{
					addModParams.descriptionAsHTML = newModProfile.descriptionAsHTML.value;
				}
				if (newModProfile.homepageURL.isDirty)
				{
					addModParams.nameId = newModProfile.homepageURL.value;
				}
				if (newModProfile.metadataBlob.isDirty)
				{
					addModParams.metadataBlob = newModProfile.metadataBlob.value;
				}
				if (newModProfile.nameId.isDirty)
				{
					addModParams.nameId = newModProfile.nameId.value;
				}
				if (newModProfile.tags.isDirty)
				{
					addModParams.tags = newModProfile.tags.value;
				}
				if (newModProfile.youTubeURLs.isDirty || newModProfile.sketchfabURLs.isDirty || newModProfile.galleryImageLocators.isDirty || newModProfile.metadataKVPs.isDirty)
				{
					eModProfile = new EditableModProfile();
					eModProfile.youTubeURLs = newModProfile.youTubeURLs;
					eModProfile.sketchfabURLs = newModProfile.sketchfabURLs;
					eModProfile.galleryImageLocators = newModProfile.galleryImageLocators;
					eModProfile.metadataKVPs = newModProfile.metadataKVPs;
				}
				DataStorage.ReadFile(newModProfile.logoLocator.value.url, SubmitNewMod_OnReadLogo);
			}
			else
			{
				SubmissionError_Local(text);
			}
		}

		public void SubmitModChanges(int modId, EditableModProfile modEdits)
		{
			eModProfile = modEdits;
			if (eModProfile.status.isDirty || eModProfile.visibility.isDirty || eModProfile.name.isDirty || eModProfile.nameId.isDirty || eModProfile.summary.isDirty || eModProfile.descriptionAsHTML.isDirty || eModProfile.homepageURL.isDirty || eModProfile.metadataBlob.isDirty)
			{
				EditModParameters editModParameters = new EditModParameters();
				if (eModProfile.status.isDirty)
				{
					editModParameters.status = eModProfile.status.value;
				}
				if (eModProfile.visibility.isDirty)
				{
					editModParameters.visibility = eModProfile.visibility.value;
				}
				if (eModProfile.name.isDirty)
				{
					editModParameters.name = eModProfile.name.value;
				}
				if (eModProfile.nameId.isDirty)
				{
					editModParameters.nameId = eModProfile.nameId.value;
				}
				if (eModProfile.summary.isDirty)
				{
					editModParameters.summary = eModProfile.summary.value;
				}
				if (eModProfile.descriptionAsHTML.isDirty)
				{
					editModParameters.descriptionAsHTML = eModProfile.descriptionAsHTML.value;
				}
				if (eModProfile.homepageURL.isDirty)
				{
					editModParameters.homepageURL = eModProfile.homepageURL.value;
				}
				if (eModProfile.metadataBlob.isDirty)
				{
					editModParameters.metadataBlob = eModProfile.metadataBlob.value;
				}
				APIClient.EditMod(modId, editModParameters, SubmitModChanges_Internal, onError);
			}
			else
			{
				ModManager.GetModProfile(modId, SubmitModChanges_Internal, onError);
			}
		}

		private void SubmitModChanges_Internal(ModProfile profile)
		{
			if (profile == null)
			{
				SubmissionError_Local("Profile parameter passed to ModManager_SubmitModOperation.SubmitModChanges_Internal was null. This was an unexpected error, please try submitting the mod again.");
				return;
			}
			if (profile.id == 0)
			{
				SubmissionError_Local("Profile parameter passed to ModManager_SubmitModOperation.SubmitModChanges_Internal has a NULL_ID. This was an unexpected error, please try submitting the mod again.");
				return;
			}
			modId = profile.id;
			if (eModProfile.logoLocator.isDirty && !string.IsNullOrEmpty(eModProfile.logoLocator.value.url))
			{
				logoPath = eModProfile.logoLocator.value.url;
			}
			if (eModProfile.galleryImageLocators.isDirty)
			{
				removedImageFileNames = new List<string>();
				GalleryImageLocator[] galleryImageLocators = profile.media.galleryImageLocators;
				foreach (GalleryImageLocator galleryImageLocator in galleryImageLocators)
				{
					removedImageFileNames.Add(galleryImageLocator.fileName);
				}
				ImageLocatorData[] value = eModProfile.galleryImageLocators.value;
				for (int j = 0; j < value.Length; j++)
				{
					ImageLocatorData imageLocatorData = value[j];
					removedImageFileNames.Remove(imageLocatorData.fileName);
				}
				addedImageFilePaths = new List<string>();
				ImageLocatorData[] value2 = eModProfile.galleryImageLocators.value;
				for (int k = 0; k < value2.Length; k++)
				{
					ImageLocatorData imageLocatorData2 = value2[k];
					addedImageFilePaths.Add(imageLocatorData2.url);
				}
				GalleryImageLocator[] galleryImageLocators2 = profile.media.galleryImageLocators;
				foreach (GalleryImageLocator galleryImageLocator2 in galleryImageLocators2)
				{
					addedImageFilePaths.Remove(galleryImageLocator2.GetURL());
				}
			}
			if (eModProfile.sketchfabURLs.isDirty)
			{
				removedSketchfabURLs = new List<string>(profile.media.sketchfabURLs);
				string[] value3 = eModProfile.sketchfabURLs.value;
				foreach (string item in value3)
				{
					removedSketchfabURLs.Remove(item);
				}
				addedSketchfabURLs = new List<string>(eModProfile.sketchfabURLs.value);
				string[] sketchfabURLs = profile.media.sketchfabURLs;
				foreach (string item2 in sketchfabURLs)
				{
					addedSketchfabURLs.Remove(item2);
				}
			}
			if (eModProfile.youTubeURLs.isDirty)
			{
				removedYouTubeURLs = new List<string>(profile.media.youTubeURLs);
				string[] value4 = eModProfile.youTubeURLs.value;
				foreach (string item3 in value4)
				{
					removedYouTubeURLs.Remove(item3);
				}
				addedYouTubeURLs = new List<string>(eModProfile.youTubeURLs.value);
				string[] youTubeURLs = profile.media.youTubeURLs;
				foreach (string item4 in youTubeURLs)
				{
					addedYouTubeURLs.Remove(item4);
				}
			}
			if (eModProfile.tags.isDirty)
			{
				removedTags = new List<string>(profile.tagNames);
				string[] value5 = eModProfile.tags.value;
				foreach (string item5 in value5)
				{
					removedTags.Remove(item5);
				}
				addedTags = new List<string>(eModProfile.tags.value);
				foreach (string tagName in profile.tagNames)
				{
					addedTags.Remove(tagName);
				}
			}
			if (eModProfile.metadataKVPs.isDirty)
			{
				removedKVPs = MetadataKVP.ArrayToDictionary(profile.metadataKVPs);
				MetadataKVP[] value6 = eModProfile.metadataKVPs.value;
				foreach (MetadataKVP metadataKVP in value6)
				{
					string value7;
					if (removedKVPs.TryGetValue(metadataKVP.key, out value7) && value7 == metadataKVP.value)
					{
						removedKVPs.Remove(metadataKVP.key);
					}
				}
				addedKVPs = MetadataKVP.ArrayToDictionary(eModProfile.metadataKVPs.value);
				MetadataKVP[] metadataKVPs = profile.metadataKVPs;
				foreach (MetadataKVP metadataKVP2 in metadataKVPs)
				{
					string value8;
					if (addedKVPs.TryGetValue(metadataKVP2.key, out value8) && value8 == metadataKVP2.value)
					{
						addedKVPs.Remove(metadataKVP2.key);
					}
				}
			}
			if (logoPath != null)
			{
				DataStorage.ReadFile(logoPath, SubmitModChanges_Internal_OnReadLogo);
			}
			else
			{
				SubmitModChanges_Internal_ZipImages();
			}
		}

		private void SubmissionError_Local(string errorMessage)
		{
			if (this != null && onError != null)
			{
				WebRequestError obj = WebRequestError.GenerateLocal(errorMessage);
				onError(obj);
			}
		}

		private void SubmitNewMod_OnReadLogo(string path, bool success, byte[] data)
		{
			if (!success)
			{
				SubmissionError_Local("Mod Profile logo file could not be read for uploading.\nLogo Path: " + path);
				return;
			}
			addModParams.logo = BinaryUpload.Create(Path.GetFileName(path), data);
			if (eModProfile == null)
			{
				APIClient.AddMod(addModParams, onSuccess, onError);
			}
			else
			{
				APIClient.AddMod(addModParams, SubmitModChanges_Internal, onError);
			}
		}

		private void SubmitModChanges_Internal_OnReadLogo(string path, bool success, byte[] data)
		{
			if (success)
			{
				logoData = data;
				SubmitModChanges_Internal_ZipImages();
			}
			else
			{
				SubmissionError_Local("Mod Profile logo file could not be read for uploading.\nLogo Path: " + path);
			}
		}

		private void SubmitModChanges_Internal_ZipImages()
		{
			if (addedImageFilePaths != null && addedImageFilePaths.Count > 0)
			{
				string imageArchivePath = IOUtilities.CombinePath(Application.temporaryCachePath, "modio", "imageGallery_" + DateTime.Now.ToFileTime() + ".zip");
				DataStorage.CreateDirectory(Path.GetDirectoryName(imageArchivePath), delegate(string path, bool success)
				{
					if (success)
					{
						if (CompressionModule.CompressFileCollection(null, addedImageFilePaths, imageArchivePath))
						{
							DataStorage.ReadFile(imageArchivePath, SubmitModChanges_Internal_OnReadImageArchive);
						}
						else
						{
							SubmissionError_Local("Unable to zip image gallery prior to uploading.");
						}
					}
					else
					{
						SubmissionError_Local("Unable to create temp directory for image gallery prior to uploading.");
					}
				});
			}
			else
			{
				SubmitNextParameter();
			}
		}

		private void SubmitModChanges_Internal_OnReadImageArchive(string path, bool success, byte[] data)
		{
			if (success)
			{
				imageArchiveData = data;
				SubmitNextParameter();
			}
		}

		private void SubmitNextParameter()
		{
			SubmitNextParameter(null);
		}

		private void SubmitNextParameter(object o)
		{
			if ((removedImageFileNames != null && removedImageFileNames.Count > 0) || (removedSketchfabURLs != null && removedSketchfabURLs.Count > 0) || (removedYouTubeURLs != null && removedYouTubeURLs.Count > 0))
			{
				DeleteModMediaParameters deleteModMediaParameters = new DeleteModMediaParameters();
				if (removedImageFileNames != null)
				{
					deleteModMediaParameters.images = removedImageFileNames.ToArray();
				}
				if (removedSketchfabURLs != null)
				{
					deleteModMediaParameters.sketchfab = removedSketchfabURLs.ToArray();
				}
				if (removedYouTubeURLs != null)
				{
					deleteModMediaParameters.youtube = removedYouTubeURLs.ToArray();
				}
				APIClient.DeleteModMedia(modId, deleteModMediaParameters, SubmitNextParameter, onError);
				removedImageFileNames = null;
				removedSketchfabURLs = null;
				removedYouTubeURLs = null;
			}
			else if (logoData != null || imageArchiveData != null || (addedSketchfabURLs != null && addedSketchfabURLs.Count > 0) || (addedYouTubeURLs != null && addedYouTubeURLs.Count > 0))
			{
				AddModMediaParameters addModMediaParameters = new AddModMediaParameters();
				if (logoData != null)
				{
					addModMediaParameters.logo = BinaryUpload.Create(Path.GetFileName(logoPath), logoData);
				}
				if (imageArchiveData != null)
				{
					addModMediaParameters.galleryImages = BinaryUpload.Create("images.zip", imageArchiveData);
				}
				if (addedSketchfabURLs != null && addedSketchfabURLs.Count > 0)
				{
					addModMediaParameters.sketchfab = addedSketchfabURLs.ToArray();
				}
				if (addedYouTubeURLs != null && addedYouTubeURLs.Count > 0)
				{
					addModMediaParameters.youtube = addedYouTubeURLs.ToArray();
				}
				APIClient.AddModMedia(modId, addModMediaParameters, SubmitNextParameter, onError);
				logoData = null;
				imageArchiveData = null;
				addedSketchfabURLs = null;
				addedYouTubeURLs = null;
			}
			else if (removedTags != null && removedTags.Count > 0)
			{
				DeleteModTagsParameters deleteModTagsParameters = new DeleteModTagsParameters();
				deleteModTagsParameters.tagNames = removedTags.ToArray();
				APIClient.DeleteModTags(modId, deleteModTagsParameters, SubmitNextParameter, onError);
				removedTags = null;
			}
			else if (addedTags != null && addedTags.Count > 0)
			{
				AddModTagsParameters addModTagsParameters = new AddModTagsParameters();
				addModTagsParameters.tagNames = addedTags.ToArray();
				APIClient.AddModTags(modId, addModTagsParameters, SubmitNextParameter, onError);
				addedTags = null;
			}
			else if (removedKVPs != null && removedKVPs.Count > 0)
			{
				DeleteModKVPMetadataParameters deleteModKVPMetadataParameters = new DeleteModKVPMetadataParameters();
				deleteModKVPMetadataParameters.metadataKeys = removedKVPs.Keys.ToArray();
				APIClient.DeleteModKVPMetadata(modId, deleteModKVPMetadataParameters, SubmitNextParameter, onError);
				removedKVPs = null;
			}
			else if (addedKVPs != null && addedKVPs.Count > 0)
			{
				string[] metadata = AddModKVPMetadataParameters.ConvertMetadataKVPsToAPIStrings(MetadataKVP.DictionaryToArray(addedKVPs));
				AddModKVPMetadataParameters addModKVPMetadataParameters = new AddModKVPMetadataParameters();
				addModKVPMetadataParameters.metadata = metadata;
				APIClient.AddModKVPMetadata(modId, addModKVPMetadataParameters, SubmitNextParameter, onError);
				addedKVPs = null;
			}
			else if (o != null && o is ModProfile && ((ModProfile)o).id == modId)
			{
				if (onSuccess != null)
				{
					onSuccess((ModProfile)o);
				}
			}
			else
			{
				RequestCache.Clear();
				APIClient.GetMod(modId, onSuccess, onError);
			}
		}
	}
}

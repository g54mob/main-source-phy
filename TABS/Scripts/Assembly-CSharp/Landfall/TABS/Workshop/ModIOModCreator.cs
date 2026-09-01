using System;
using System.Collections.Generic;
using System.IO;
using DM;
using Ionic.Zip;
using ModIO;
using ModIO.API;
using TFBGames;
using UnityEngine;
using UnityEngine.U2D;

namespace Landfall.TABS.Workshop
{
	public class ModIOModCreator
	{
		private const string LocalizedUploadFailedText = "POPUP_UPLOADFAILED";

		private int m_ModID;

		private Modfile m_ModFile;

		private Action mOnCreateAction;

		private Action mOnItemUpdtedAction;

		public void CreateNewMod(List<GenericCustomContentWrapper> contentToUpload, string modName, List<string> tags, string description, ContentTypeFilter contentType, ModVisibility visibility, SpriteAtlas factionAtlas, Action doneCallback)
		{
			AddModParameters addModParameters = new AddModParameters();
			addModParameters.name = modName;
			addModParameters.visibility = visibility;
			addModParameters.summary = description;
			addModParameters.tags = tags.ToArray();
			byte[] modLogoData = GetModLogoData(contentToUpload, modName, contentType, factionAtlas);
			if (modLogoData == null)
			{
				doneCallback?.Invoke();
				return;
			}
			addModParameters.logo = BinaryUpload.Create("logo.png", modLogoData);
			OnItemCreatedAction(delegate
			{
				UpdateMod(GetModID(), contentToUpload.ToArray(), modName, description);
			});
			APIClient.AddMod(addModParameters, OnModCreatedSuccess, OnModCreatedFailed);
			doneCallback?.Invoke();
		}

		public static byte[] GetModLogoData(List<GenericCustomContentWrapper> contentToUpload, string modName, ContentTypeFilter contentType, SpriteAtlas factionAtlas)
		{
			byte[] returnData = null;
			Vector2Int textureSize = Vector2Int.zero;
			switch (contentType)
			{
			case ContentTypeFilter.Battles:
				textureSize = new Vector2Int(640, 360);
				break;
			case ContentTypeFilter.Campaigns:
				textureSize = new Vector2Int(640, 360);
				break;
			case ContentTypeFilter.Units:
				textureSize = new Vector2Int(512, 512);
				break;
			case ContentTypeFilter.Factions:
				textureSize = new Vector2Int(512, 512);
				break;
			case ContentTypeFilter.Maps:
				textureSize = new Vector2Int(1270, 720);
				break;
			default:
				textureSize = new Vector2Int(512, 512);
				break;
			}
			string text = "";
			ContentTypeFilter contentTypeFilter = contentType;
			text = ((contentTypeFilter != ContentTypeFilter.Units) ? "Picture.png" : "icon.png");
			FileIOWrapper service = ServiceLocator.GetService<FileIOWrapper>();
			string path = Path.Combine(contentToUpload[0].DirectoryPath, text);
			if (File.Exists(path))
			{
				service.ReadAllBytes(path, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(byte[] data, Exception exception)
				{
					if (exception != null)
					{
						Debug.LogFormat("Failed to load: {0}\n{1}", path, exception);
					}
					else
					{
						if (contentType == ContentTypeFilter.Units)
						{
							Texture2D texture2D = new Texture2D(textureSize.x, textureSize.y);
							texture2D.LoadImage(data);
							texture2D.Apply();
							data = Resize(texture2D, textureSize.x, textureSize.y).EncodeToPNG();
						}
						returnData = data;
					}
				});
			}
			else if (contentType == ContentTypeFilter.Factions)
			{
				Texture2D coloredFactionIcon = CustomFactionHandler.GetColoredFactionIcon(modName, factionAtlas);
				TextureScale.Bilinear(coloredFactionIcon, textureSize.x, textureSize.y);
				coloredFactionIcon.Apply();
				byte[] array = coloredFactionIcon.EncodeToPNG();
				returnData = array;
			}
			else if (contentType == ContentTypeFilter.Maps)
			{
				Texture2D texture = ContentDatabase.Instance().GetUserMap(contentToUpload[0].ID).Entity.SpriteIcon.texture;
				TextureScale.Bilinear(texture, textureSize.x, textureSize.y);
				texture.Apply();
				byte[] array2 = texture.EncodeToPNG();
				returnData = array2;
			}
			else if (contentType == ContentTypeFilter.Battles)
			{
				Texture2D texture2 = ContentDatabase.Instance().GetCampaignLevel(contentToUpload[0].ID).Entity.SpriteIcon.texture;
				TextureScale.Bilinear(texture2, textureSize.x, textureSize.y);
				texture2.Apply();
				byte[] array3 = texture2.EncodeToPNG();
				returnData = array3;
			}
			return returnData;
		}

		private static Texture2D Resize(Texture2D texture2D, int targetX, int targetY)
		{
			RenderTexture active = RenderTexture.active;
			RenderTexture renderTexture = (RenderTexture.active = new RenderTexture(targetX, targetY, 24));
			Graphics.Blit(texture2D, renderTexture);
			Texture2D texture2D2 = new Texture2D(targetX, targetY);
			texture2D2.ReadPixels(new Rect(0f, 0f, targetX, targetY), 0, 0);
			texture2D2.Apply();
			renderTexture.Release();
			RenderTexture.active = active;
			return texture2D2;
		}

		public int GetModID()
		{
			return m_ModID;
		}

		private Modfile GetModFile()
		{
			return m_ModFile;
		}

		public void UpdateMod(int modID, GenericCustomContentWrapper[] contentToUpload, string modName, string description, bool updatingExisting = false)
		{
			FileIOWrapper fileIO = ServiceLocator.GetService<FileIOWrapper>();
			int directoriesToCheck = contentToUpload.Length;
			bool abort = false;
			GenericCustomContentWrapper[] array = contentToUpload;
			foreach (GenericCustomContentWrapper item in array)
			{
				DataStorage.GetDirectoryExists(item.DirectoryPath, delegate(string directoryPath, bool directoryExists)
				{
					int num = directoriesToCheck - 1;
					directoriesToCheck = num;
					if (!directoryExists)
					{
						Debug.LogError("Given folder: " + item.DirectoryPath + " Does not exist when zipping for ModIO!");
						abort = true;
					}
					Proceed();
				});
			}
			void Proceed()
			{
				AddModfileParameters param;
				byte[] zipData;
				ZipFile newZip;
				MemoryStream stream;
				int filesToAdd;
				if (!abort && directoriesToCheck <= 0)
				{
					Debug.Log("Updating mod: " + modName + " ID: " + modID);
					param = new AddModfileParameters();
					zipData = new byte[0];
					newZip = new ZipFile();
					stream = new MemoryStream();
					filesToAdd = contentToUpload.Length;
					GenericCustomContentWrapper[] array2 = contentToUpload;
					foreach (GenericCustomContentWrapper item2 in array2)
					{
						Debug.Log("Adding file to zip: " + item2.FullFilePath);
						string filePath = item2.FullFilePath;
						fileIO.FileExists(filePath, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(bool fileExists)
						{
							Debug.Log("zip-file: " + filePath + "   EXISTS: " + fileExists);
							if (fileExists)
							{
								fileIO.ReadAllBytes(filePath, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(byte[] fileBytes, Exception readBytesException)
								{
									int num2 = filesToAdd - 1;
									filesToAdd = num2;
									string fileName = Path.GetFileName(item2.FullFilePath);
									string text = Path.Combine(item2.ContentType.ToString(), item2.ID.m_ID.ToString(), fileName);
									Debug.LogError("ENTRY PATH: " + text);
									newZip.AddEntry(text, fileBytes);
									WriteZipProceed();
								});
							}
							else
							{
								int num = filesToAdd - 1;
								filesToAdd = num;
								WriteZipProceed();
							}
						});
					}
				}
				void PostZipProceed()
				{
					param.zippedBinaryData = BinaryUpload.Create(modName + ".zip", zipData);
					EditModParameters parameters = new EditModParameters
					{
						name = modName,
						summary = description
					};
					APIClient.EditMod(modID, parameters, OnModEditSuccess, OnModEditFail);
					APIClient.AddModfile(modID, param, OnAddModFileSuccess, OnAddModFileFail);
					Action a = ((!ModIOUploadHandler.UploadingSeqence) ? ((Action)delegate
					{
						Reset();
					}) : ((Action)delegate
					{
					}));
					OnItemUpdatedAction(a);
				}
				void WriteZipProceed()
				{
					if (filesToAdd <= 0)
					{
						newZip.Save(stream);
						zipData = stream.ToArray();
						PostZipProceed();
					}
				}
			}
		}

		private void OnModEditFail(WebRequestError obj)
		{
		}

		private void OnModEditSuccess(ModProfile obj)
		{
		}

		private void SubscribeToItem(int modID)
		{
			APIClient.SubscribeToMod(modID, OnSubSuccess, OnSubFail);
		}

		private void DownloadItem(Modfile modFile)
		{
			Debug.Log("Downloading mod: " + modFile.fileName + " ID: " + modFile.id);
			ModManager.DownloadAndUpdateMod(modFile.modId, OnDownloadSuccess, OnDownloadError);
		}

		private void OnDownloadError(WebRequestError obj)
		{
			Debug.Log("DownloadItem: FAIL: " + obj.errorMessage);
		}

		private void OnDownloadSuccess()
		{
			Debug.Log("DownloadItem: SUCCESS!");
			ServiceLocator.GetService<CustomContentLoaderModIO>().QuickRefresh(WorkshopContentType.Any, null);
		}

		private void OnSubFail(WebRequestError obj)
		{
			Debug.LogError("Subbing failed: " + obj.errorMessage + " : " + obj.displayMessage);
		}

		private void OnSubSuccess(ModProfile obj)
		{
			Debug.Log("SubSuccess: " + obj.name);
			ServiceLocator.GetService<CustomContentLoaderModIO>().QuickRefresh(WorkshopContentType.Any, null);
		}

		private void OnAddModFileSuccess(Modfile file)
		{
			Debug.Log("Add Mod File SUCCESS: " + file.fileName + " ID: " + file.id);
			m_ModFile = file;
			m_ModID = file.modId;
			mOnItemUpdtedAction?.Invoke();
		}

		private void OnAddModFileFail(WebRequestError err)
		{
			Debug.LogError("ModIO Mod Edit FAILED: " + err.displayMessage);
			if (err.isAuthenticationInvalid)
			{
				ServiceLocator.GetService<CustomContentLoaderModIO>().RequestLoginTicket();
				ServiceLocator.GetService<ModalPanel>().PopUp("POPUP_AUTHENTICATION_FAILED");
			}
			else
			{
				ServiceLocator.GetService<ModalPanel>().PopUp("POPUP_UPLOADFAILED", Localizer.GetSinglePhrase(err.displayMessage));
			}
		}

		private void OnModCreatedSuccess(ModProfile profile)
		{
			Debug.Log("ModIO Mod Created: " + profile.id + " Name: " + profile.name);
			m_ModID = profile.id;
			mOnCreateAction?.Invoke();
		}

		private void OnModCreatedFailed(WebRequestError err)
		{
			Debug.LogError("ModIO Mod Create FAILED: " + err.displayMessage);
			if (err.isAuthenticationInvalid)
			{
				ServiceLocator.GetService<CustomContentLoaderModIO>().RequestLoginTicket();
				ServiceLocator.GetService<ModalPanel>().PopUp("POPUP_AUTHENTICATION_FAILED");
			}
			else
			{
				ServiceLocator.GetService<ModalPanel>().PopUp("POPUP_UPLOADFAILED", Localizer.GetSinglePhrase(err.displayMessage));
			}
		}

		public void Reset()
		{
			mOnItemUpdtedAction = null;
			mOnCreateAction = null;
			Debug.Log("Reset!!!!");
		}

		public void OnItemCreatedAction(Action a)
		{
			mOnCreateAction = (Action)Delegate.Combine(mOnCreateAction, a);
		}

		public void OnItemUpdatedAction(Action a)
		{
			mOnItemUpdtedAction = (Action)Delegate.Combine(mOnItemUpdtedAction, a);
		}
	}
}

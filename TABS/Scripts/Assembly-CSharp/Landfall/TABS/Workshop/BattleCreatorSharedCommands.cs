using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DM;
using ModIO;
using TFBGames;
using UnityEngine;
using UnityEngine.U2D;

namespace Landfall.TABS.Workshop
{
	public class BattleCreatorSharedCommands
	{
		private class AsyncLoadData
		{
			public int ContentIndex;

			public GenericCustomContentWrapper Content;
		}

		private static BattleCreatorTabsUIHandler m_UIHandler;

		public static void AssignUI(BattleCreatorTabsUIHandler uihandler)
		{
			m_UIHandler = uihandler;
		}

		public static void LoadContent(BattleCreatorAssetUICellBase contentCell, Action onFinished = null)
		{
			if (contentCell.ContentType == ContentTypeFilter.Battles)
			{
				Debug.Log("Loading Content: " + contentCell.FullPath, contentCell);
				CampaignHandler.LoadLayoutFromDisk(contentCell.FullPath, delegate
				{
					m_UIHandler.Close();
					onFinished?.Invoke();
				});
			}
			else if (contentCell.ContentType == ContentTypeFilter.Campaigns)
			{
				m_UIHandler.OpenNewScreen(BattleCreatorScreenState.TwoList, BattleCreatorState.CampaignCreator, contentCell.CampaignAsset);
				onFinished?.Invoke();
			}
		}

		public static void OpenUploadScreen(BattleCreatorAssetUICellBase contentCell)
		{
			m_UIHandler.OpenNewScreen(BattleCreatorScreenState.Upload, BattleCreatorState.Upload, contentCell);
		}

		public static void OpenUpdateScreen(UpdateableWorkshopContentPack contentPack)
		{
			m_UIHandler.OpenNewScreen(BattleCreatorScreenState.Upload, BattleCreatorState.Update, contentPack);
		}

		public static void UpdateContent(UpdateableWorkshopContentPack updatePack, BattleCreatorAssetUICellBase originalContent, Action onFinish, string desc)
		{
			GenericCustomContentWrapper[] array = updatePack.CustomContent.ToArray();
			Debug.Log("Updating files: " + array.Length + " : " + originalContent.ContentName + " WIth: " + updatePack.AssetCellUI.ContentName);
			ServiceLocator.GetService<ModalPanel>().WaitPopUp("POPUP_UPDATING", -1f, null, null);
			int modID = 0;
			switch (originalContent.ContentType)
			{
			case ContentTypeFilter.Battles:
				modID = originalContent.ModID;
				break;
			case ContentTypeFilter.Campaigns:
				modID = originalContent.ModID;
				break;
			case ContentTypeFilter.Units:
				modID = originalContent.UnitBluePrint.ModID;
				break;
			}
			ModIOUploadHandler.UpdateExistingItem(modID, array, updatePack.AssetCellUI.ContentName, desc);
			ModIOUploadHandler.SetOnItemUpdatedAction(onFinish);
			onFinish?.Invoke();
		}

		public static void UpdateContent(List<GenericCustomContentWrapper> allContent, ModProfile originalContent, Action onFinish, ContentTypeFilter contentType, string desc)
		{
			GenericCustomContentWrapper[] array = allContent.ToArray();
			Debug.Log("Updating files: " + array.Length + " : " + originalContent.name + " WIth: " + array[0].ItemName);
			ServiceLocator.GetService<ModalPanel>().WaitPopUp("POPUP_UPDATING", -1f, null, null);
			ModIOUploadHandler.UpdateExistingItem(originalContent.id, array, array[0].ItemName, desc);
			ModIOUploadHandler.SetOnItemUpdatedAction(onFinish);
		}

		public static void UploadContent(List<GenericCustomContentWrapper> allContent, List<string> tags, ModVisibility visibility, Action onFinish, ContentTypeFilter contentType, string description, SpriteAtlas factionAtlas)
		{
			Debug.Log("Uploading files: " + allContent.Count);
			ServiceLocator.GetService<ModalPanel>().WaitPopUp("POPUP_UPLOADING", -1f, null, null);
			ModIOUploadHandler.CreateNewItem(allContent, allContent[0].ItemName, tags, description, contentType, visibility, factionAtlas, delegate
			{
				ModIOUploadHandler.SetOnCreateItemAction(onFinish);
			});
		}

		public static void RenameContent(BattleCreatorAssetUICellBase contentCell, Action onFinish)
		{
			Debug.Log("Renaming content: " + contentCell.ContentName);
			string text = ((contentCell.ContentType == ContentTypeFilter.Battles) ? "Renaming Battle: " : "Renaming Campaign: ");
			ServiceLocator.GetService<ModalPanel>().Inputfield(text + contentCell.ContentName, contentCell, delegate
			{
				onFinish?.Invoke();
			}, RenameContentTo);
		}

		public static void RenameUnit(UnitBlueprint unit, string newName)
		{
			unit.Entity.Name = newName;
			CustomUnitHandler.OverrideUnit(unit);
		}

		private static void RenameContentTo(BattleCreatorAssetUICellBase cell, string newName, Action doneCallback)
		{
			switch (cell.ContentType)
			{
			case ContentTypeFilter.Battles:
			{
				TABSCampaignLevelAsset levelAsset = cell.LevelAsset;
				levelAsset.Entity.Name = newName;
				CampaignHandler.OverwriteLayout(levelAsset, delegate
				{
					doneCallback?.Invoke();
				});
				break;
			}
			case ContentTypeFilter.Campaigns:
			{
				TABSCampaignAsset campaignAsset = cell.CampaignAsset;
				campaignAsset.Entity.Name = newName;
				CampaignHandler.OverwriteCampaign(campaignAsset, delegate
				{
					doneCallback?.Invoke();
				});
				break;
			}
			default:
				doneCallback?.Invoke();
				break;
			}
		}

		public static void DeleteContent(BattleCreatorAssetUICellBase contentCell, Action onFinish)
		{
			string text = "POPUP_DELETECONFIRM";
			ContentDatabase contentDatabase = ContentDatabase.Instance();
			IEnumerable<TABSCampaignAsset> enumerable = from cmp in contentDatabase.GetUserCampaigns()
				where cmp.LevelsInCampaign.Contains(contentCell.LevelAsset)
				select cmp;
			if (contentCell.ContentType == ContentTypeFilter.Battles && contentDatabase != null && enumerable != null && enumerable.Count() > 0)
			{
				text = "POPUP_DELETECONFIRM_INUSE";
			}
			ServiceLocator.GetService<ModalPanel>().Choice("POPUP_DELETE_TITLE", text.ToString(), delegate
			{
				DeleteContentFolder(contentCell, delegate
				{
					onFinish?.Invoke();
				});
			}, delegate
			{
				onFinish?.Invoke();
			}, "", "", true, enumerable?.Count().ToString(), contentCell.ContentName);
		}

		public static void DeleteCampaign(TABSCampaignAsset campaign, Action doneCallback)
		{
			FileIOWrapper fileIO = ServiceLocator.GetService<FileIOWrapper>();
			DirectoryInfo directoryInfo = new DirectoryInfo(campaign.FolderPath);
			string path = directoryInfo.FullName;
			Debug.LogFormat("Deleting Campaign: {0}", path);
			fileIO.DirectoryExists(path, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(bool exists)
			{
				if (!exists)
				{
					Debug.LogErrorFormat("Error deleting campaign: {0}     Does not exist", path);
					doneCallback?.Invoke();
				}
				else
				{
					fileIO.DeleteDirectory(path, recursive: true, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(Exception exception)
					{
						if (exception != null)
						{
							Debug.LogErrorFormat("Error deleting campaign: {0}\n{1}", path, exception);
							doneCallback?.Invoke();
						}
						else
						{
							ContentDatabase.Instance().RemoveUserCampaign(campaign.Entity.GUID);
							doneCallback?.Invoke();
						}
					});
				}
			});
		}

		public static void DeleteContentFolder(CustomContentDataPackage contentData, Action doneCallback)
		{
			FileIOWrapper fileIO = ServiceLocator.GetService<FileIOWrapper>();
			DirectoryInfo directoryInfo = new DirectoryInfo(contentData.folderPath);
			string path = directoryInfo.FullName;
			Debug.LogFormat("Deleting Content: {0}", path);
			fileIO.DirectoryExists(path, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(bool exists)
			{
				if (!exists)
				{
					Debug.LogErrorFormat("Error deleting content: {0}     Does not exist", path);
					doneCallback?.Invoke();
				}
				else
				{
					fileIO.DeleteDirectory(path, recursive: true, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(Exception exception)
					{
						if (exception != null)
						{
							Debug.LogErrorFormat("Error deleting content: {0}\n{1}", path, exception);
							doneCallback?.Invoke();
						}
						else
						{
							switch (contentData.contentType)
							{
							case ContentTypeFilter.Battles:
								ContentDatabase.Instance().RemoveUserCampaignLevel(contentData.id, doneCallback);
								break;
							case ContentTypeFilter.Campaigns:
								ContentDatabase.Instance().RemoveUserCampaign(contentData.id);
								doneCallback?.Invoke();
								break;
							case ContentTypeFilter.Units:
								ContentDatabase.Instance().RemoveUserUnitBlueprintAndEmptyFactionsCreated(contentData.id);
								doneCallback?.Invoke();
								break;
							case ContentTypeFilter.Factions:
								ContentDatabase.Instance().RemoveUserFaction(contentData.id);
								doneCallback?.Invoke();
								break;
							case ContentTypeFilter.Maps:
								ContentDatabase.Instance().RemoveUserMap(contentData.id, doneCallback);
								break;
							}
						}
					});
				}
			});
		}

		public static void DeleteContentFolder(BattleCreatorAssetUICellBase cell, Action doneCallback)
		{
			DatabaseID id = default(DatabaseID);
			switch (cell.ContentType)
			{
			case ContentTypeFilter.Battles:
				id = cell.LevelAsset.Entity.GUID;
				break;
			case ContentTypeFilter.Campaigns:
				id = cell.CampaignAsset.Entity.GUID;
				break;
			}
			DeleteContentFolder(new CustomContentDataPackage(id, cell.FolderPath, cell.ContentType), doneCallback);
		}
	}
}

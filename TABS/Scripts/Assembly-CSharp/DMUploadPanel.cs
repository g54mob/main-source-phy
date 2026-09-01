using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using DM;
using GamepadUI.StateManager.Core;
using Landfall.TABS;
using Landfall.TABS.Workshop;
using LevelCreator;
using ModIO;
using ModIO.API;
using ModIO.UI;
using TFBGames;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class DMUploadPanel : MonoBehaviour
{
	[Header("General")]
	[SerializeField]
	private DMWorkshopHandler m_workshopHandler;

	[SerializeField]
	private SpriteAtlas m_factionIconAtlas;

	[Header("Upload")]
	[SerializeField]
	private GameObject m_modPreviewObject;

	[SerializeField]
	private GameObject m_UploadUITogglesParent;

	[SerializeField]
	private NavigableTMPTextInput m_description;

	[Header("Update")]
	[SerializeField]
	private GameObject m_localModPreviewObject;

	[SerializeField]
	private GameObject m_uploadedModPreviewObject;

	[SerializeField]
	private LocalizeText m_updateText;

	[SerializeField]
	private UISubMenu m_updateScreen;

	[SerializeField]
	private GameTagCategoryDisplay m_updateTags;

	[SerializeField]
	private NavigableTMPTextInput m_updateDescription;

	private List<string> m_selectedTags = new List<string>();

	private WorkshopContentType m_contentType;

	private ModVisibility m_visibility = ModVisibility.Public;

	private object contentToUpload;

	private ModProfile modToUpdate;

	private bool requestSent;

	private ModalPanel m_modalPanel;

	private void Start()
	{
		m_modalPanel = ServiceLocator.GetService<ModalPanel>();
	}

	public void Close()
	{
		m_workshopHandler.Back();
	}

	public void OpenToUpload()
	{
		m_workshopHandler.OpenLocalContentBrowser(enableTabs: true, WorkshopContentType.Any, delegate(object data)
		{
			OpenPanel(data, isUpload: true);
		});
	}

	public void OpenToUpdate()
	{
		Action<ModProfile> overrideItemAction = delegate(ModProfile profile)
		{
			modToUpdate = profile;
			if (!string.IsNullOrEmpty(GetModTypeTag(modToUpdate, out var contentType)))
			{
				m_workshopHandler.OpenLocalContentBrowser(enableTabs: false, contentType, delegate(object data)
				{
					OpenPanel(data, isUpload: false);
				});
			}
			m_modalPanel.CloseWaitPopup();
		};
		ViewManager.instance.explorerView.ClearAllFilters();
		m_workshopHandler.SetSearchMethod(2, overrideItemAction, enableTabs: true, ViewManager.instance.explorerView.defaultTab);
		m_workshopHandler.OpenSubMenu(ViewManager.instance.explorerView.GetComponent<UISubMenu>());
	}

	public void OpenToUpdateWithSelectedLocalContent(object localContent)
	{
		if (localContent == null)
		{
			FinishUploadUpdate("POPUP_ERROR", 1);
			return;
		}
		Action<ModProfile> overrideItemAction = delegate(ModProfile profile)
		{
			modToUpdate = profile;
			OpenPanel(localContent, isUpload: false);
			m_modalPanel.CloseWaitPopup();
		};
		string defaultTab = GetDataType(localContent).ToString();
		m_workshopHandler.SetSearchMethod(2, overrideItemAction, enableTabs: false, defaultTab);
		m_workshopHandler.OpenSubMenu(ViewManager.instance.explorerView.GetComponent<UISubMenu>());
	}

	public void OpenPanel(object uploadData, bool isUpload)
	{
		ClearPreviousUploadValues();
		contentToUpload = uploadData;
		if (contentToUpload == null)
		{
			Debug.LogError("Selected local data is null");
			return;
		}
		Sprite localSprite = null;
		Type type = contentToUpload.GetType();
		bool flag = false;
		if (type == typeof(UnitBlueprint))
		{
			m_contentType = WorkshopContentType.Unit;
			flag = true;
			GetDataEntity(contentToUpload, m_contentType)?.GetSpriteIconAsync(delegate(Sprite sprite)
			{
				if (sprite != null)
				{
					UpdateModePreview(isUpload, sprite);
				}
			});
		}
		else if (type == typeof(Faction))
		{
			m_contentType = WorkshopContentType.Faction;
			localSprite = UIUtilities.CreateSpriteFromTexture(CustomFactionHandler.GetColoredFactionIcon(GetDataEntity(contentToUpload, m_contentType)?.Name, m_factionIconAtlas));
		}
		else if (type == typeof(TABSCampaignLevelAsset))
		{
			m_contentType = WorkshopContentType.Battle;
			flag = true;
			CampaignHandler.GetBattleSprite((TABSCampaignLevelAsset)contentToUpload, delegate(Sprite sprite)
			{
				UpdateModePreview(isUpload, sprite);
			});
		}
		else if (type == typeof(TABSCampaignAsset))
		{
			m_contentType = WorkshopContentType.Campaign;
			flag = true;
			CampaignHandler.GetCampaignSprite((TABSCampaignAsset)contentToUpload, delegate(Sprite sprite)
			{
				UpdateModePreview(isUpload, sprite);
			});
		}
		else if (type == typeof(CustomMap))
		{
			m_contentType = WorkshopContentType.Map;
			CustomMap obj = (CustomMap)contentToUpload;
			flag = true;
			obj.Entity.GetSpriteIconAsync(delegate(Sprite sprite)
			{
				if (sprite != null)
				{
					UpdateModePreview(isUpload, sprite);
				}
			});
		}
		else
		{
			m_contentType = WorkshopContentType.Any;
		}
		if (!flag)
		{
			UpdateModePreview(isUpload, localSprite);
		}
	}

	private void ClearPreviousUploadValues()
	{
		m_description.text = string.Empty;
		if (m_UploadUITogglesParent != null)
		{
			Toggle[] componentsInChildren = m_UploadUITogglesParent.GetComponentsInChildren<Toggle>();
			if (componentsInChildren != null && componentsInChildren.Length != 0)
			{
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					componentsInChildren[i].isOn = false;
				}
			}
		}
		m_selectedTags.Clear();
	}

	private void UpdateModePreview(bool isUpload, Sprite localSprite)
	{
		if (isUpload)
		{
			m_workshopHandler.OpenSubMenu(GetComponent<UISubMenu>());
			UpdatePreviewObject(m_modPreviewObject, GetDataEntity(contentToUpload, m_contentType)?.Name, localSprite, m_contentType);
			return;
		}
		m_workshopHandler.OpenSubMenu(m_updateScreen);
		DatabaseEntity dataEntity = GetDataEntity(contentToUpload, m_contentType);
		UpdatePreviewObject(m_localModPreviewObject, dataEntity?.Name, localSprite, m_contentType);
		m_updateText.Args = new string[3] { dataEntity.Name, modToUpdate.name, "\n" };
		m_updateText.LocaleID = "LABEL_UPDATEEXISTING_WARNING";
		m_updateDescription.text = modToUpdate.summary;
		m_selectedTags.AddRange(modToUpdate.tagNames);
		m_updateTags.UpdateTagStates(m_selectedTags);
		ImageRequestManager.instance.RequestModLogo(modToUpdate.id, modToUpdate.logoLocator, LogoSize.Thumbnail_640x360, AssignIcon, AssignIcon, WebRequestError.LogAsWarning);
		void AssignIcon(Texture2D tex)
		{
			Sprite icon = UIUtilities.CreateSpriteFromTexture(tex);
			UpdatePreviewObject(m_uploadedModPreviewObject, modToUpdate.name, icon);
		}
	}

	private void UpdatePreviewObject(GameObject previewObject, string name, Sprite icon, WorkshopContentType contentType = WorkshopContentType.Any)
	{
		previewObject.GetComponentInChildren<TMP_Text>().text = name;
		DMModItemImageFitter componentInChildren = previewObject.GetComponentInChildren<DMModItemImageFitter>();
		componentInChildren.UpdateAspectRatio(contentType.ToString());
		componentInChildren.GetComponentInChildren<Image>().sprite = icon;
	}

	private WorkshopContentType GetDataType(object data)
	{
		Type type = data.GetType();
		if (type == typeof(UnitBlueprint))
		{
			return m_contentType = WorkshopContentType.Unit;
		}
		if (type == typeof(Faction))
		{
			return m_contentType = WorkshopContentType.Faction;
		}
		if (type == typeof(TABSCampaignLevelAsset))
		{
			return m_contentType = WorkshopContentType.Battle;
		}
		if (type == typeof(TABSCampaignAsset))
		{
			return m_contentType = WorkshopContentType.Campaign;
		}
		if (type == typeof(CustomMap))
		{
			return m_contentType = WorkshopContentType.Map;
		}
		return m_contentType = WorkshopContentType.Any;
	}

	private DatabaseEntity GetDataEntity(object data, WorkshopContentType contentType)
	{
		switch (contentType)
		{
		case WorkshopContentType.Unit:
			return (data as UnitBlueprint).Entity;
		case WorkshopContentType.Faction:
			return (data as Faction).Entity;
		case WorkshopContentType.Battle:
			return (data as TABSCampaignLevelAsset).Entity;
		case WorkshopContentType.Campaign:
			return (data as TABSCampaignAsset).Entity;
		case WorkshopContentType.Map:
			return (data as CustomMap).Entity;
		default:
			return null;
		}
	}

	private void GetDataFilePathAsync(object data, WorkshopContentType contentType, Action<string> doneCallback)
	{
		switch (contentType)
		{
		case WorkshopContentType.Unit:
		{
			UnitBlueprint unitBlueprint = data as UnitBlueprint;
			doneCallback?.Invoke(unitBlueprint.FilePath);
			break;
		}
		case WorkshopContentType.Faction:
			Faction.GetCustomFactionPathAsync(data as Faction, delegate(string factionFilePath)
			{
				doneCallback?.Invoke(factionFilePath);
			});
			break;
		case WorkshopContentType.Battle:
		{
			TABSCampaignLevelAsset tABSCampaignLevelAsset = data as TABSCampaignLevelAsset;
			doneCallback?.Invoke(tABSCampaignLevelAsset.FilePath);
			break;
		}
		case WorkshopContentType.Campaign:
		{
			TABSCampaignAsset tABSCampaignAsset = data as TABSCampaignAsset;
			doneCallback?.Invoke(tABSCampaignAsset.FilePath);
			break;
		}
		case WorkshopContentType.Map:
		{
			CustomMap customMap = data as CustomMap;
			doneCallback?.Invoke(customMap.FilePath);
			break;
		}
		default:
			doneCallback?.Invoke(null);
			break;
		}
	}

	private string GetDescription(bool isUpdate)
	{
		return (isUpdate ? m_updateDescription : m_description).text;
	}

	private void GetCustomContentAsync(object data, WorkshopContentType contentType, Action<GenericCustomContentWrapper> doneCallback)
	{
		if (data == null)
		{
			doneCallback?.Invoke(null);
			return;
		}
		DatabaseEntity dataEntity = GetDataEntity(data, contentType);
		if (dataEntity == null)
		{
			doneCallback?.Invoke(null);
			return;
		}
		DatabaseID id = dataEntity.GUID;
		string entityName = dataEntity.Name;
		GetDataFilePathAsync(data, contentType, delegate(string path)
		{
			if (string.IsNullOrEmpty(entityName) || string.IsNullOrEmpty(path))
			{
				doneCallback?.Invoke(null);
				return;
			}
			FileIOWrapper service = ServiceLocator.GetService<FileIOWrapper>();
			try
			{
				service.FileExists(path, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(bool success)
				{
					if (!success)
					{
						doneCallback?.Invoke(null);
					}
					else
					{
						GenericCustomContentWrapper obj = new GenericCustomContentWrapper(entityName, path, id, contentType);
						doneCallback?.Invoke(obj);
					}
				});
			}
			catch (Exception ex)
			{
				Debug.LogError("Failed to Get Custom Content with exception " + ex.Message);
				doneCallback?.Invoke(null);
			}
		});
	}

	private void GetAllCustomContentAsync(object data, WorkshopContentType contentType, Action<List<GenericCustomContentWrapper>> doneCallback)
	{
		if (data == null)
		{
			doneCallback?.Invoke(null);
			return;
		}
		List<GenericCustomContentWrapper> customContentList = new List<GenericCustomContentWrapper>();
		try
		{
			GetCustomContentAsync(data, contentType, delegate(GenericCustomContentWrapper genericCustomContentWrapper)
			{
				if (genericCustomContentWrapper != null)
				{
					customContentList.Add(genericCustomContentWrapper);
				}
				ProcessCustomContent(data, contentType, doneCallback, customContentList);
			});
		}
		catch (Exception ex)
		{
			Debug.LogError("Failed to Get All Custom Content with exception " + ex.Message);
			OnDoneGetAllCustomContentAsync(customContentList, doneCallback);
		}
	}

	private void OnDoneGetAllCustomContentAsync(List<GenericCustomContentWrapper> customContentList, Action<List<GenericCustomContentWrapper>> doneCallback)
	{
		try
		{
			if (customContentList.Count == 0)
			{
				doneCallback?.Invoke(null);
				return;
			}
			customContentList = RemoveDuplicates(customContentList);
			doneCallback?.Invoke(customContentList);
		}
		catch (Exception ex)
		{
			Debug.LogError("Unexpected error in final processing of CustomContentList with error " + ex.Message);
			doneCallback?.Invoke(null);
		}
	}

	private void ProcessCustomContent(object data, WorkshopContentType contentType, Action<List<GenericCustomContentWrapper>> doneCallback, List<GenericCustomContentWrapper> customContentList)
	{
		switch (contentType)
		{
		case WorkshopContentType.Unit:
			AddUnitIconAsync(data, delegate(GenericCustomContentWrapper unitIconWrapper)
			{
				if (unitIconWrapper != null)
				{
					customContentList.Add(unitIconWrapper);
				}
				AddRidersAsync(data, customContentList, delegate
				{
					OnDoneGetAllCustomContentAsync(customContentList, doneCallback);
				});
			});
			break;
		case WorkshopContentType.Faction:
			AddUnitsAsync(data, customContentList, delegate
			{
				OnDoneGetAllCustomContentAsync(customContentList, doneCallback);
			});
			break;
		case WorkshopContentType.Battle:
			AddBattleIconAsync(data, delegate(GenericCustomContentWrapper battleIconWrapper)
			{
				if (battleIconWrapper != null)
				{
					customContentList.Add(battleIconWrapper);
				}
				AddFactionsAsync(data, customContentList, delegate
				{
					AddMapFromBattleAsync(data, customContentList, delegate
					{
						OnDoneGetAllCustomContentAsync(customContentList, doneCallback);
					});
				});
			});
			break;
		case WorkshopContentType.Campaign:
			AddBattlesAsync(data, customContentList, delegate
			{
				OnDoneGetAllCustomContentAsync(customContentList, doneCallback);
			});
			break;
		case WorkshopContentType.Map:
			AddAllMapFiles(data, customContentList);
			OnDoneGetAllCustomContentAsync(customContentList, doneCallback);
			break;
		case WorkshopContentType.Layout:
		case WorkshopContentType.Any:
			break;
		}
	}

	private void AddMapFromBattleAsync(object battleData, List<GenericCustomContentWrapper> customContentList, System.Action doneCallback)
	{
		try
		{
			TABSCampaignLevelAsset tABSCampaignLevelAsset = battleData as TABSCampaignLevelAsset;
			CustomMap map = ContentDatabase.Instance().GetUserMap(tABSCampaignLevelAsset.CustomMap);
			if (map == null)
			{
				foreach (CustomMap userMap in ContentDatabase.Instance().GetUserMaps())
				{
					if (userMap.Entity.GUID.m_ID == tABSCampaignLevelAsset.CustomMap.m_ID)
					{
						map = userMap;
						break;
					}
				}
			}
			GetCustomContentAsync(map, WorkshopContentType.Map, delegate(GenericCustomContentWrapper wrapper)
			{
				customContentList.Add(wrapper);
				AddAllMapFiles(map, customContentList);
				doneCallback?.Invoke();
			});
		}
		catch (Exception ex)
		{
			Debug.LogError("Error occured when adding map from battle with error " + ex.Message);
		}
	}

	private void AddAllMapFiles(object mapData, List<GenericCustomContentWrapper> customContentList)
	{
		try
		{
			CustomMap customMap = mapData as CustomMap;
			if (!(customMap == null))
			{
				DatabaseID gUID = customMap.Entity.GUID;
				GenericCustomContentWrapper item = new GenericCustomContentWrapper(gUID.ToString(), customMap.IconPath, gUID, WorkshopContentType.Map);
				GenericCustomContentWrapper item2 = new GenericCustomContentWrapper(gUID.ToString(), customMap.LevelPath, gUID, WorkshopContentType.Map);
				customContentList.Add(item);
				customContentList.Add(item2);
			}
		}
		catch (Exception ex)
		{
			Debug.LogError("Failed to add all map files with error " + ex.Message);
		}
	}

	private void AddFactionsAsync(object battleData, List<GenericCustomContentWrapper> customContentList, Action<Exception> doneCallBack)
	{
		TABSCampaignLevelAsset tABSCampaignLevelAsset = battleData as TABSCampaignLevelAsset;
		List<Faction> factions = new List<Faction>();
		try
		{
			GetFactions(tABSCampaignLevelAsset.BlueUnits);
			GetFactions(tABSCampaignLevelAsset.RedUnits);
			if (tABSCampaignLevelAsset.AllowedFactions != null)
			{
				factions.AddRange(tABSCampaignLevelAsset.AllowedFactions);
			}
			int factionsToAddCount = factions.Count;
			if (factionsToAddCount <= 0)
			{
				doneCallBack?.Invoke(null);
				return;
			}
			foreach (Faction faction in factions)
			{
				if (faction == null || !faction.IsCustom)
				{
					int num = factionsToAddCount;
					factionsToAddCount = num - 1;
					if (factionsToAddCount <= 0)
					{
						doneCallBack?.Invoke(null);
					}
					continue;
				}
				GetCustomContentAsync(faction, WorkshopContentType.Faction, delegate(GenericCustomContentWrapper factionWrapper)
				{
					if (factionWrapper != null)
					{
						customContentList.Add(factionWrapper);
					}
					AddUnitsAsync(faction, customContentList, delegate
					{
						factionsToAddCount--;
						if (factionsToAddCount <= 0)
						{
							doneCallBack?.Invoke(null);
						}
					});
				});
			}
		}
		catch (Exception ex)
		{
			Debug.LogError("Failed to add custom factions with error " + ex.Message);
			doneCallBack?.Invoke(ex);
		}
		void GetFactions(TABSCampaignLevelAsset.TABSLayoutUnit[] units)
		{
			foreach (TABSCampaignLevelAsset.TABSLayoutUnit tABSLayoutUnit in units)
			{
				if (tABSLayoutUnit != null && tABSLayoutUnit.m_unitBlueprint.IsCustomUnit)
				{
					Faction factionByUnitBlueprint = ContentDatabase.Instance().GetFactionByUnitBlueprint(tABSLayoutUnit.m_unitBlueprint.Entity.GUID);
					if (factionByUnitBlueprint != null && !factions.Contains(factionByUnitBlueprint))
					{
						factions.Add(factionByUnitBlueprint);
					}
				}
			}
		}
	}

	private void AddRidersAsync(object unitData, List<GenericCustomContentWrapper> customContentList, Action<Exception> doneCallBack)
	{
		UnitBlueprint baseUnit = unitData as UnitBlueprint;
		List<UnitBlueprint> riders = new List<UnitBlueprint>();
		try
		{
			AddRidersRecursive(baseUnit);
			int ridersToAddCount = riders.Count;
			if (ridersToAddCount <= 0)
			{
				doneCallBack?.Invoke(null);
				return;
			}
			foreach (UnitBlueprint rider in riders)
			{
				GetCustomContentAsync(rider, WorkshopContentType.Unit, delegate(GenericCustomContentWrapper riderWrapper)
				{
					if (riderWrapper != null)
					{
						customContentList.Add(riderWrapper);
					}
					AddUnitIconAsync(rider, delegate(GenericCustomContentWrapper riderIconWrapper)
					{
						ridersToAddCount--;
						if (riderIconWrapper != null)
						{
							customContentList.Add(riderIconWrapper);
						}
						if (ridersToAddCount <= 0)
						{
							doneCallBack?.Invoke(null);
						}
					});
				});
			}
		}
		catch (Exception ex)
		{
			Debug.LogError("Failed to add custom riders with error " + ex.Message);
			doneCallBack?.Invoke(ex);
		}
		void AddRidersRecursive(UnitBlueprint unitBlueprint)
		{
			if (!(unitBlueprint == null) && unitBlueprint.UnitRiders != null)
			{
				UnitBlueprint[] unitRiders = unitBlueprint.UnitRiders;
				foreach (UnitBlueprint unitBlueprint2 in unitRiders)
				{
					if (!(unitBlueprint2 == null))
					{
						riders.Add(unitBlueprint2);
						AddRidersRecursive(unitBlueprint2);
					}
				}
			}
		}
	}

	private void AddUnitsAsync(object factionData, List<GenericCustomContentWrapper> customContentList, Action<Exception> doneCallBack)
	{
		Faction faction = factionData as Faction;
		int unitsToAddCount = faction.Units.Length;
		try
		{
			if (unitsToAddCount <= 0)
			{
				doneCallBack?.Invoke(null);
				return;
			}
			UnitBlueprint[] units = faction.Units;
			foreach (UnitBlueprint unit in units)
			{
				if (unit == null)
				{
					int num = unitsToAddCount;
					unitsToAddCount = num - 1;
					if (unitsToAddCount <= 0)
					{
						doneCallBack?.Invoke(null);
					}
					continue;
				}
				GetCustomContentAsync(unit, WorkshopContentType.Unit, delegate(GenericCustomContentWrapper unitWrapper)
				{
					if (unitWrapper != null)
					{
						customContentList.Add(unitWrapper);
					}
					AddUnitIconAsync(unit, delegate(GenericCustomContentWrapper unitIconWrapper)
					{
						if (unitIconWrapper != null)
						{
							customContentList.Add(unitIconWrapper);
						}
						AddRidersAsync(unit, customContentList, delegate
						{
							unitsToAddCount--;
							if (unitsToAddCount <= 0)
							{
								doneCallBack?.Invoke(null);
							}
						});
					});
				});
			}
		}
		catch (Exception ex)
		{
			Debug.LogError("Failed to add custom units with error " + ex.Message);
			doneCallBack?.Invoke(ex);
		}
	}

	private void AddBattlesAsync(object campaignData, List<GenericCustomContentWrapper> customContentList, Action<Exception> doneCallBack)
	{
		TABSCampaignAsset tABSCampaignAsset = campaignData as TABSCampaignAsset;
		Path.GetDirectoryName(tABSCampaignAsset.FilePath);
		int battlesToAddCount = tABSCampaignAsset.LevelsInCampaign.Length;
		if (battlesToAddCount <= 0)
		{
			doneCallBack?.Invoke(null);
			return;
		}
		try
		{
			TABSCampaignLevelAsset[] levelsInCampaign = tABSCampaignAsset.LevelsInCampaign;
			foreach (TABSCampaignLevelAsset battle in levelsInCampaign)
			{
				if (battle == null)
				{
					int num = battlesToAddCount;
					battlesToAddCount = num - 1;
					if (battlesToAddCount <= 0)
					{
						doneCallBack?.Invoke(null);
					}
					continue;
				}
				AddMapFromBattleAsync(battle, customContentList, delegate
				{
					GetCustomContentAsync(battle, WorkshopContentType.Battle, delegate(GenericCustomContentWrapper battleWrapper)
					{
						if (battleWrapper != null)
						{
							customContentList.Add(battleWrapper);
						}
						AddBattleIconAsync(battle, delegate(GenericCustomContentWrapper battleIconWrapper)
						{
							if (battleIconWrapper != null)
							{
								customContentList.Add(battleIconWrapper);
							}
							AddFactionsAsync(battle, customContentList, delegate
							{
								battlesToAddCount--;
								if (battlesToAddCount <= 0)
								{
									doneCallBack?.Invoke(null);
								}
							});
						});
					});
				});
			}
		}
		catch (Exception ex)
		{
			Debug.LogError("Failed to add custom battles with error " + ex.Message);
			doneCallBack?.Invoke(ex);
		}
	}

	private void AddBattleIconAsync(object battleData, Action<GenericCustomContentWrapper> doneCallback)
	{
		TABSCampaignLevelAsset battle = battleData as TABSCampaignLevelAsset;
		string directoryName = Path.GetDirectoryName(battle.FilePath);
		string iconPath = Path.Combine(directoryName, "Picture.png");
		FileIOWrapper service = ServiceLocator.GetService<FileIOWrapper>();
		try
		{
			service.FileExists(iconPath, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(bool success)
			{
				if (success)
				{
					GenericCustomContentWrapper obj = new GenericCustomContentWrapper("Picture", iconPath, battle.Entity.GUID, WorkshopContentType.Battle);
					doneCallback?.Invoke(obj);
				}
				else
				{
					Debug.LogError("Outdated downloaded battle, the Icon was not found at: " + iconPath);
					doneCallback?.Invoke(null);
				}
			});
		}
		catch (Exception ex)
		{
			Debug.LogError("Failed to add Battle Icon with error " + ex.Message);
			doneCallback?.Invoke(null);
		}
	}

	private void AddUnitIconAsync(object unitData, Action<GenericCustomContentWrapper> doneCallback)
	{
		UnitBlueprint unit = unitData as UnitBlueprint;
		string directoryName = Path.GetDirectoryName(unit.FilePath);
		string iconPath = Path.Combine(directoryName, "icon.png");
		FileIOWrapper service = ServiceLocator.GetService<FileIOWrapper>();
		try
		{
			service.FileExists(iconPath, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(bool success)
			{
				if (success)
				{
					GenericCustomContentWrapper obj = new GenericCustomContentWrapper("icon", iconPath, unit.Entity.GUID, WorkshopContentType.Unit);
					doneCallback?.Invoke(obj);
				}
				else
				{
					Debug.LogError("Outdated downloaded unit, the Icon was not found at: " + iconPath);
					doneCallback?.Invoke(null);
				}
			});
		}
		catch (Exception ex)
		{
			Debug.LogError("Failed to add Unit Icon with error " + ex.Message);
			doneCallback?.Invoke(null);
		}
	}

	private List<GenericCustomContentWrapper> RemoveDuplicates(List<GenericCustomContentWrapper> customContent)
	{
		List<GenericCustomContentWrapper> list = new List<GenericCustomContentWrapper>();
		List<string> list2 = new List<string>();
		if (customContent != null)
		{
			foreach (GenericCustomContentWrapper item in customContent)
			{
				if (item != null && !list2.Contains(item.FullFilePath))
				{
					list.Add(item);
					list2.Add(item.FullFilePath);
				}
			}
		}
		return list;
	}

	private List<string> GetTags(object data, WorkshopContentType contentType)
	{
		List<string> selectedTags = m_selectedTags;
		string platformTag = ViewManager.instance.explorerView.platformTag;
		if (!string.IsNullOrEmpty(platformTag))
		{
			selectedTags.Add(platformTag);
		}
		int modVersion = ModManager.ModVersion;
		for (int i = 1; i <= modVersion; i++)
		{
			if (!selectedTags.Contains(i.ToString()))
			{
				selectedTags.Add(i.ToString());
			}
		}
		selectedTags.Insert(0, m_contentType.ToString());
		return selectedTags;
	}

	public void UploadContent(bool isUpdate)
	{
		UploadContentAsync(isUpdate);
	}

	public void UploadContentAsync(bool isUpdate)
	{
		GetAllCustomContentAsync(contentToUpload, m_contentType, delegate(List<GenericCustomContentWrapper> customContent)
		{
			if (customContent != null)
			{
				ContentTypeFilter contentTypeFilter = m_contentType.ToContentTypeFilter();
				if (contentTypeFilter != ContentTypeFilter.Any && contentTypeFilter != ContentTypeFilter.None)
				{
					List<string> tags = GetTags(contentToUpload, m_contentType);
					if (isUpdate)
					{
						SendUpdate(customContent, contentTypeFilter, tags);
					}
					else
					{
						SendUpload(customContent, contentTypeFilter, tags);
					}
				}
			}
		});
	}

	private void SendUpload(List<GenericCustomContentWrapper> customContent, ContentTypeFilter contentTypeFilter, List<string> tags)
	{
		string description = GetDescription(isUpdate: false);
		if (string.IsNullOrEmpty(description))
		{
			m_modalPanel.PopUp("POPUP_EMPTYDESCRIPTION");
			return;
		}
		BattleCreatorSharedCommands.UploadContent(customContent, tags, m_visibility, delegate
		{
			FinishUploadUpdate("POPUP_UPLOADED", 2);
		}, contentTypeFilter, description, m_factionIconAtlas);
	}

	private void SendUpdate(List<GenericCustomContentWrapper> customContent, ContentTypeFilter contentTypeFilter, List<string> tags)
	{
		byte[] logoData = ModIOModCreator.GetModLogoData(customContent, customContent[0].ItemName, contentTypeFilter, m_factionIconAtlas);
		if (logoData == null)
		{
			FinishUploadUpdate("POPUP_UPDATEFAILED_DOWNLOADEDCONTENT");
			return;
		}
		string description = GetDescription(isUpdate: true);
		if (string.IsNullOrEmpty(description))
		{
			m_modalPanel.PopUp("POPUP_EMPTYDESCRIPTION");
			return;
		}
		BattleCreatorSharedCommands.UpdateContent(customContent, modToUpdate, delegate
		{
			APIClient.AddModMedia(modToUpdate.id, new AddModMediaParameters
			{
				logo = BinaryUpload.Create("logo.png", logoData)
			}, delegate(APIMessage addLogoMessage)
			{
				if (addLogoMessage.code != 201)
				{
					FinishUploadUpdate("Update Failed: " + addLogoMessage.message);
				}
				else
				{
					List<string> list = new List<string>(modToUpdate.tagNames);
					List<string> tagsToAdd = tags;
					WorkshopContentType contentType;
					string modTypeTag = GetModTypeTag(modToUpdate, out contentType);
					if (string.IsNullOrEmpty(modTypeTag))
					{
						FinishUploadUpdate("POPUP_UPDATEFAILED_INVALIDCONTENTTYPE");
					}
					else
					{
						tagsToAdd.Add(modTypeTag);
						APIClient.DeleteModTags(modToUpdate.id, new DeleteModTagsParameters
						{
							tagNames = list.ToArray()
						}, delegate
						{
							APIClient.AddModTags(modToUpdate.id, new AddModTagsParameters
							{
								tagNames = tagsToAdd.ToArray()
							}, delegate
							{
								FinishUploadUpdate("POPUP_UPDATED", 3);
							}, delegate(WebRequestError e)
							{
								FinishUploadUpdate("Update Failed: " + e.displayMessage);
							});
						}, delegate(WebRequestError e)
						{
							FinishUploadUpdate("Update Failed: " + e.displayMessage);
						});
					}
				}
			}, delegate(WebRequestError e)
			{
				FinishUploadUpdate("Update Failed: " + e.displayMessage);
			});
		}, contentTypeFilter, description);
	}

	private void FinishUploadUpdate(string message, int backCount = 0)
	{
		ViewManager.instance.explorerView.ClearCacheAndRefresh();
		m_modalPanel.CloseWaitPopup();
		m_modalPanel.PopUp(message, delegate
		{
			if (this == null)
			{
				ReturnFromUploadScreen();
			}
			else
			{
				StartCoroutine(Delay());
			}
		});
		IEnumerator Delay()
		{
			yield return new WaitUntil(() => !m_modalPanel.IsPopupOpen);
			ReturnFromUploadScreen();
		}
		void ReturnFromUploadScreen()
		{
			if (DMWorkshopHandler.uploadData != null)
			{
				DMWorkshopHandler.uploadData = null;
				TABSSceneManager.LoadCustomContentPage();
			}
			else
			{
				m_workshopHandler.Back(backCount);
			}
		}
	}

	private string GetModTypeTag(ModProfile modProfile, out WorkshopContentType contentType)
	{
		for (int i = 0; i < modProfile.tags.Length; i++)
		{
			string text = modProfile.tags[i].name;
			if (Enum.TryParse<WorkshopContentType>(text, out contentType) && contentType != WorkshopContentType.Any)
			{
				return text;
			}
		}
		contentType = WorkshopContentType.Any;
		return null;
	}

	public void ToggleTag(TagContainerItem tagCell)
	{
		if (!m_selectedTags.Contains(tagCell.TagName))
		{
			m_selectedTags.Add(tagCell.TagName);
		}
		else
		{
			m_selectedTags.Remove(tagCell.TagName);
		}
	}

	public void ToggleVisibility(bool enabled)
	{
		m_visibility = (enabled ? ModVisibility.Public : ModVisibility.Hidden);
	}
}

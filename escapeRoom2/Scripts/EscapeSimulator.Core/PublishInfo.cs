using System;
using System.Collections.Generic;
using System.IO;
using Steamworks;
using UnityEngine;
using UnityEngine.UI;

public class PublishInfo : MonoBehaviour
{
	private enum Tab
	{
		BasicInformation = 0,
		Tags = 1
	}

	public Action<ulong> onPublishSucess;

	public Action onPublishStart;

	public Action<bool> onClosePublish;

	public Action onScreenshotButton;

	public PublishInfoUI ui;

	public bool canActivateSaveOnExitModal = true;

	private Menu.WorkshopRoomInfo roomToPublish;

	private List<object> gcDontKill = new List<object>();

	private PineTweenSystemEnableNoHandles tween = new PineTweenSystemEnableNoHandles();

	private ScrollRect[] scrolls;

	private Tags tags = new Tags();

	public void init()
	{
		ui.init();
		scrolls = ui.root.GetComponentsInChildren<ScrollRect>(includeInactive: true);
		PineUI.addButtonClickDelegate(onButtonClick);
	}

	public void update()
	{
		tween.processTweens(Time.deltaTime);
		if (ui.Publish.gameObject.activeInHierarchy)
		{
			updatePublishingInfoDropdown();
		}
		if (ui.PublishingWaiting.gameObject.activeInHierarchy)
		{
			ui.PublishingWaiting_Image.transform.Rotate(0f, 0f, 90f * Time.deltaTime);
		}
	}

	public bool isActive()
	{
		if (!ui.Publish.gameObject.activeInHierarchy)
		{
			return ui.PublishingWaiting.gameObject.activeInHierarchy;
		}
		return true;
	}

	public void open(Menu.WorkshopRoomInfo roomToPublish)
	{
		this.roomToPublish = roomToPublish;
		selectTab(Tab.BasicInformation);
		enableTags(tags.playTimeTagsList);
		enableTags(tags.minNumPlayersTagsList);
		enableTags(tags.maxNumPlayersTagsList);
		enableTags(tags.difficultyTagsList);
		enableTags(tags.themesTagsList);
		enableTags(tags.languagesTagsList);
		enableTags(tags.miscellaneousTagsList);
		enableTags(tags.codeAddedTagsList);
		regenerateUI(ui, tags);
		ui.Publish.gameObject.SetActive(value: true);
		ui.Publish_Description_BasicInformationContent_Layout_Name_InputField.text = roomToPublish.roomTitle;
		ui.Publish_Description_BasicInformationContent_Level_LevelImage.texture = roomToPublish.roomPreviewImage;
		ui.Publish_Description_BasicInformationContent_DescriptionInput.text = roomToPublish.roomDescription;
		ui.Publish_Description_BasicInformationContent_Level_ScreenshotButton.gameObject.SetActive(onScreenshotButton != null);
		if (roomToPublish.roomId != 0L)
		{
			SteamAPICall_t hAPICall = SteamUGC.SendQueryUGCRequest(SteamUGC.CreateQueryUGCDetailsRequest(new PublishedFileId_t[1]
			{
				new PublishedFileId_t(roomToPublish.roomId)
			}, 1u));
			CallResult<SteamUGCQueryCompleted_t> callResult = CallResult<SteamUGCQueryCompleted_t>.Create(onPublishInfoGetDetails);
			callResult.Set(hAPICall);
			gcDontKill.Add(callResult);
		}
		else
		{
			ui.Publish_Footer_VisibilityDropdown_Dropdown.value = (int)getInitialRoomVisibility(roomToPublish.roomTitle);
		}
		ScrollRect[] array = scrolls;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].verticalNormalizedPosition = 1f;
		}
		void enableTags(List<Tags.Tag> tags)
		{
			foreach (Tags.Tag tag in tags)
			{
				tag.isActive = roomToPublish.roomTags.Contains(tag.name);
			}
		}
		static ERemoteStoragePublishedFileVisibility getInitialRoomVisibility(string title)
		{
			title = title.ToLower();
			if (title.Contains("editor basics") || title.Contains("editor advanced") || title.Contains("myroom"))
			{
				return ERemoteStoragePublishedFileVisibility.k_ERemoteStoragePublishedFileVisibilityUnlisted;
			}
			return ERemoteStoragePublishedFileVisibility.k_ERemoteStoragePublishedFileVisibilityPublic;
		}
	}

	public void close(bool canActivateSaveOnExitModal)
	{
		if (canActivateSaveOnExitModal)
		{
			ui.SaveOnExitModal.gameObject.SetActive(value: true);
		}
		else
		{
			ui.Publish.gameObject.SetActive(value: false);
		}
		onClosePublish?.Invoke(obj: true);
	}

	public void destroy()
	{
		foreach (IDisposable item in gcDontKill)
		{
			item.Dispose();
		}
	}

	public void changeLevelImage(Texture2D newImage)
	{
		ui.Publish_Description_BasicInformationContent_Level_LevelImage.texture = newImage;
	}

	public bool getActiveTags(out List<string> output, bool hasWalkthrough)
	{
		output = new List<string>();
		addTagsIfActive(tags.playTimeTagsList, ref output);
		addTagsIfActive(tags.minNumPlayersTagsList, ref output);
		addTagsIfActive(tags.maxNumPlayersTagsList, ref output);
		addTagsIfActive(tags.difficultyTagsList, ref output);
		addTagsIfActive(tags.themesTagsList, ref output);
		addTagsIfActive(tags.languagesTagsList, ref output);
		addTagsIfActive(tags.miscellaneousTagsList, ref output);
		addTagsIfActive(tags.codeAddedTagsList, ref output);
		if (hasWalkthrough)
		{
			output.Add("tagInGameWalkthrough");
		}
		return output.Count > 0;
		static void addTagsIfActive(List<Tags.Tag> tags, ref List<string> reference)
		{
			foreach (Tags.Tag tag in tags)
			{
				if (tag.isActive)
				{
					reference.Add(tag.name);
				}
			}
		}
	}

	public void toggleCodeAddedTags(string tag, bool activate)
	{
		Debug.Log($"Toggle: {tag} to {activate}");
		foreach (Tags.Tag codeAddedTags in tags.codeAddedTagsList)
		{
			if (!(codeAddedTags.name != tag))
			{
				codeAddedTags.isActive = activate;
				break;
			}
		}
	}

	public void forceLayoutRebuild(RectTransform transform)
	{
		LayoutRebuilder.ForceRebuildLayoutImmediate(transform);
	}

	private List<string> getSteamWorkshopTags(bool hasWalkthrough)
	{
		getActiveTags(out var output, hasWalkthrough);
		for (int i = 0; i < output.Count; i++)
		{
			output[i] = Localization.lookupInEnglishDictionary(output[i], output[i]);
		}
		return output;
	}

	public static void regenerateUI(PublishInfoUI ui, Tags tags)
	{
		ui.Publish_Description_BasicInformationContent_Layout_DifficultyDropdown_Dropdown.onValueChanged.RemoveAllListeners();
		ui.Publish_Description_BasicInformationContent_Layout_PlayTimeDropdown_Dropdown.onValueChanged.RemoveAllListeners();
		ui.Publish_Description_BasicInformationContent_Layout_NumOfPlayersDropdownMin_Min.onValueChanged.RemoveAllListeners();
		ui.Publish_Description_BasicInformationContent_Layout_NumOfPlayersDropdownMax_Max.onValueChanged.RemoveAllListeners();
		addDropdownOptions(ui.Publish_Description_BasicInformationContent_Layout_DifficultyDropdown_Dropdown, tags.difficultyTagsList);
		addDropdownOptions(ui.Publish_Description_BasicInformationContent_Layout_PlayTimeDropdown_Dropdown, tags.playTimeTagsList);
		addDropdownOptions(ui.Publish_Description_BasicInformationContent_Layout_NumOfPlayersDropdownMin_Min, tags.minNumPlayersTagsList);
		addDropdownOptions(ui.Publish_Description_BasicInformationContent_Layout_NumOfPlayersDropdownMax_Max, tags.maxNumPlayersTagsList);
		updateTagDropdownsVisibility();
		for (int num = ui.Publish_Description_TagsContent_Background_ScrollView_Viewport_PublishingInfoWorkshopTags.childCount - 1; num >= 0; num--)
		{
			UnityEngine.Object.DestroyImmediate(ui.Publish_Description_TagsContent_Background_ScrollView_Viewport_PublishingInfoWorkshopTags.GetChild(num).gameObject);
		}
		generateTagToggles(tags.themesTagsList, "tagTitleThemes");
		generateTagToggles(tags.miscellaneousTagsList, "tagTitleMiscellaneous");
		generateTagToggles(tags.languagesTagsList, "tagTitleLanguages");
		static void addDropdownOptions(Dropdown dropdown, List<Tags.Tag> tagsList)
		{
			dropdown.onValueChanged.AddListener(delegate
			{
				foreach (Tags.Tag tags2 in tagsList)
				{
					if (tags2 == tagsList[dropdown.value])
					{
						tags2.isActive = true;
					}
					else
					{
						tags2.isActive = false;
					}
				}
			});
			dropdown.ClearOptions();
			bool flag = true;
			for (int num2 = 0; num2 < tagsList.Count; num2++)
			{
				dropdown.options.Add(new Dropdown.OptionData
				{
					text = Localization.lookupInDictionary(tagsList[num2].name, tagsList[num2].name)
				});
				if (tagsList[num2].isActive)
				{
					flag = false;
					dropdown.value = num2;
				}
			}
			if (flag)
			{
				tagsList[dropdown.value].isActive = true;
			}
		}
		void generateTagToggles(List<Tags.Tag> tagsList, string category)
		{
			Text text = UnityEngine.Object.Instantiate(ui.Publish_Description_TagsContent_Background_ScrollView_Viewport_CategorySpacerTemplate, ui.Publish_Description_TagsContent_Background_ScrollView_Viewport_PublishingInfoWorkshopTags);
			text.gameObject.SetActive(value: true);
			text.text = Localization.lookupInDictionary(category, category);
			foreach (Tags.Tag tag in tagsList)
			{
				if (!tag.doNotDisplayInPublish)
				{
					Toggle toggleObject = UnityEngine.Object.Instantiate(ui.Publish_Description_TagsContent_Background_ScrollView_Viewport_ToggleTemplate, ui.Publish_Description_TagsContent_Background_ScrollView_Viewport_PublishingInfoWorkshopTags);
					Text componentInChildren = toggleObject.GetComponentInChildren<Text>();
					componentInChildren.text = Localization.lookupInDictionary(tag.name, tag.name);
					toggleObject.GetComponent<LayoutElement>().preferredWidth = componentInChildren.preferredWidth + 45f;
					toggleObject.gameObject.SetActive(value: true);
					toggleObject.onValueChanged.AddListener(delegate
					{
						tag.isActive = toggleObject.isOn;
					});
					if (tag.isActive)
					{
						toggleObject.isOn = true;
					}
				}
			}
		}
		void updateTagDropdownsVisibility()
		{
			bool active = true;
			ui.Publish_Description_BasicInformationContent_Layout_DifficultyDropdown.gameObject.SetActive(active);
			ui.Publish_Description_BasicInformationContent_Layout_PlayTimeDropdown.gameObject.SetActive(active);
			ui.Publish_Description_BasicInformationContent_Layout_NumOfPlayersDropdownMin_Min.gameObject.SetActive(active);
			ui.Publish_Description_BasicInformationContent_Layout_NumOfPlayersDropdownMax_Max.gameObject.SetActive(active);
			tags.difficultyTagsList[ui.Publish_Description_BasicInformationContent_Layout_DifficultyDropdown_Dropdown.value].isActive = active;
			tags.playTimeTagsList[ui.Publish_Description_BasicInformationContent_Layout_PlayTimeDropdown_Dropdown.value].isActive = active;
		}
	}

	public static string getNumOfPlayersText(Tags tags)
	{
		int num = tags.minNumPlayersTagsList.FindIndex((Tags.Tag x) => x.isActive) + 1;
		int num2 = tags.maxNumPlayersTagsList.FindIndex((Tags.Tag x) => x.isActive) + 1;
		string text = "";
		if (num2 == num)
		{
			if (num2 == 1)
			{
				return Localization.lookupInDictionary("tagSingleplayer", "tagSingleplayer");
			}
			return Localization.lookupInDictionary("nPlayerOnly").Replace("[playerNum]", num2.ToString());
		}
		if (num2 == 8 || num2 < num)
		{
			return num switch
			{
				8 => Localization.lookupInDictionary("nPlayer").Replace("[playerNum]", num.ToString()), 
				1 => Localization.lookupInDictionary("Any", "Any"), 
				_ => Localization.lookupInDictionary("nPlayerPlus").Replace("[playerNum]", num.ToString()), 
			};
		}
		return Localization.lookupInDictionary("fromToPlayers").Replace("[playerMin]", num.ToString()).Replace("[playerMax]", num2.ToString());
	}

	private void updatePublishingInfoDropdown()
	{
		updateText(ui.Publish_Description_BasicInformationContent_Layout_DifficultyDropdown_Dropdown.captionText, null, tags.difficultyTagsList[ui.Publish_Description_BasicInformationContent_Layout_DifficultyDropdown_Dropdown.value].name);
		updateText(ui.Publish_Description_BasicInformationContent_Layout_PlayTimeDropdown_Dropdown.captionText, null, tags.playTimeTagsList[ui.Publish_Description_BasicInformationContent_Layout_PlayTimeDropdown_Dropdown.value].name);
		updateText(ui.Publish_Description_BasicInformationContent_Layout_NumOfPlayersDropdownMin_Min.captionText, null, tags.minNumPlayersTagsList[ui.Publish_Description_BasicInformationContent_Layout_NumOfPlayersDropdownMin_Min.value].name);
		updateText(ui.Publish_Description_BasicInformationContent_Layout_NumOfPlayersDropdownMax_Max.captionText, null, tags.maxNumPlayersTagsList[ui.Publish_Description_BasicInformationContent_Layout_NumOfPlayersDropdownMax_Max.value].name);
		static void updateText(Text text, string prefix, string lookupName)
		{
			if (prefix == null)
			{
				text.text = Localization.lookupInDictionary(lookupName, lookupName);
			}
			else
			{
				string newValue = Localization.lookupInDictionary(lookupName, lookupName);
				string text2 = Localization.lookupInDictionary(prefix, prefix);
				text.text = text2.Replace("[item]", newValue);
			}
		}
	}

	private void publish()
	{
		if (roomToPublish == null)
		{
			return;
		}
		Debug.Log("Publishing " + roomToPublish.roomTitle);
		if (string.IsNullOrEmpty(roomToPublish.roomTitle))
		{
			Debug.Log("Error: Level name is undefined.");
			ui.PublishingWaiting.gameObject.SetActive(value: true);
			ui.PublishingWaiting_Error.gameObject.SetActive(value: true);
			ui.PublishingWaiting_Error_Background_errorLbl.text = Localization.lookupInDictionary("cantPublishNameUndefied");
			return;
		}
		string text = Path.Combine(roomToPublish.roomLocalPath, "Preview.jpg");
		if (!File.Exists(text))
		{
			Debug.Log("Error: Preview image doesn't exist.");
			ui.PublishingWaiting.gameObject.SetActive(value: true);
			ui.PublishingWaiting_Error.gameObject.SetActive(value: true);
			ui.PublishingWaiting_Error_Background_errorLbl.text = Localization.lookupInDictionary("cantPublishNoPreviewImage");
			return;
		}
		if (new FileInfo(text).Length > 1000000)
		{
			Debug.Log("Error: preview image is too big.");
			ui.PublishingWaiting.gameObject.SetActive(value: true);
			ui.PublishingWaiting_Error.gameObject.SetActive(value: true);
			ui.PublishingWaiting_Error_Background_errorLbl.text = Localization.lookupInDictionary("cantPublishImageLarge");
			return;
		}
		Debug.Log("start publishing");
		if (onPublishStart != null)
		{
			onPublishStart();
		}
		saveRoomToPublishToRoomData();
		foreach (MenuLevelUI levelTile in roomToPublish.levelTiles)
		{
			levelTile.title.text = roomToPublish.roomTitle;
		}
		ui.Publish.gameObject.SetActive(value: false);
		ui.PublishingWaiting.gameObject.SetActive(value: true);
		if (roomToPublish.roomId == 0L)
		{
			createLevelInWorkshop();
		}
		else
		{
			publishRoomToWorkshop();
		}
	}

	private void onButtonClick(Button button)
	{
		if (button == ui.Publish_Description_BasicInformationContent_Level_ScreenshotButton)
		{
			onScreenshotButton?.Invoke();
		}
		else if (button == ui.Publish_Footer_CancelButton)
		{
			close(canActivateSaveOnExitModal);
		}
		else if (button == ui.Publish_Footer_PublishButton)
		{
			publish();
		}
		else if (button == ui.SaveOnExitModal_buttons_no)
		{
			ui.SaveOnExitModal.gameObject.SetActive(value: false);
			ui.Publish.gameObject.SetActive(value: false);
		}
		else if (button == ui.SaveOnExitModal_buttons_yes)
		{
			saveRoomToPublishToRoomData();
			ui.SaveOnExitModal.gameObject.SetActive(value: false);
			ui.Publish.gameObject.SetActive(value: false);
		}
		else if (button == ui.Publish_Description_BasicInformationContent_Layout_Name_IDBtn)
		{
			ui.EditID.gameObject.SetActive(value: true);
			ui.EditID_IDInputField.text = roomToPublish.roomId.ToString();
			ui.EditID_Intro.gameObject.SetActive(value: true);
		}
		else if (button == ui.EditID_buttons_cancel)
		{
			ui.EditID.gameObject.SetActive(value: false);
		}
		else if (button == ui.EditID_buttons_save)
		{
			if (ulong.TryParse(ui.EditID_IDInputField.text, out var result))
			{
				Menu.writeIdToDisk(roomToPublish.roomLocalPath, result);
				roomToPublish.roomId = result;
				ui.EditID.gameObject.SetActive(value: false);
				return;
			}
			ui.EditID_Error.gameObject.SetActive(value: true);
			ui.EditID_Intro.gameObject.SetActive(value: false);
			tween.animate((float t) => t > 5f).onComplete = delegate
			{
				ui.EditID_Error.gameObject.SetActive(value: false);
				ui.EditID_Intro.gameObject.SetActive(value: true);
			};
		}
		else if (button == ui.Publish_Description_DescriptionTabs_BasicInformation)
		{
			selectTab(Tab.BasicInformation);
		}
		else if (button == ui.Publish_Description_DescriptionTabs_TagsButton)
		{
			selectTab(Tab.Tags);
		}
	}

	private void selectTab(Tab newTab)
	{
		ui.Publish_Description_BasicInformationContent.gameObject.SetActive(newTab == Tab.BasicInformation);
		ui.Publish_Description_TagsContent.gameObject.SetActive(newTab == Tab.Tags);
		ui.Publish_Description_DescriptionTabs_BasicInformation_Selected.gameObject.SetActive(newTab == Tab.BasicInformation);
		ui.Publish_Description_DescriptionTabs_TagsButton_Selected.gameObject.SetActive(newTab == Tab.Tags);
		setColor(ui.Publish_Description_DescriptionTabs_BasicInformation.image, ui.Publish_Description_DescriptionTabs_BasicInformation_Text, newTab == Tab.BasicInformation);
		setColor(ui.Publish_Description_DescriptionTabs_TagsButton.image, ui.Publish_Description_DescriptionTabs_TagsButton_Text, newTab == Tab.Tags);
		static void setColor(Image image, Text text, bool isSelected)
		{
			Themes.ColorsTabButton tabButtonColors = Menu.getTheme().tabButtonColors;
			text.color = (isSelected ? tabButtonColors.selectedColorText : tabButtonColors.unselectedColorText);
		}
	}

	private void saveRoomToPublishToRoomData()
	{
		roomToPublish.roomTitle = ui.Publish_Description_BasicInformationContent_Layout_Name_InputField.text;
		roomToPublish.roomDescription = ui.Publish_Description_BasicInformationContent_DescriptionInput.text;
		bool hasWalkthrough = roomToPublish.roomWalkthrough.Count > 0;
		if (getActiveTags(out var output, hasWalkthrough))
		{
			roomToPublish.roomTags = output;
		}
		string path = Path.Combine(roomToPublish.roomLocalPath, "Room.room");
		RoomData roomData = JsonUtility.FromJson<RoomData>(File.ReadAllText(path));
		roomData.name = roomToPublish.roomTitle;
		roomData.description = roomToPublish.roomDescription;
		roomData.tags = roomToPublish.roomTags;
		File.WriteAllText(path, JsonUtility.ToJson(roomData, prettyPrint: true));
	}

	private void onPublishInfoGetDetails(SteamUGCQueryCompleted_t data, bool failure)
	{
		if (roomToPublish != null)
		{
			SteamUGC.GetQueryUGCResult(data.m_handle, 0u, out var pDetails);
			ui.Publish_Footer_VisibilityDropdown_Dropdown.value = (int)pDetails.m_eVisibility;
		}
	}

	private void createLevelInWorkshop()
	{
		Debug.Log("UGC: First publish, creating new id.");
		SteamAPICall_t hAPICall = SteamUGC.CreateItem(new AppId_t(Game.STEAM_APP_ID), EWorkshopFileType.k_EWorkshopFileTypeFirst);
		CallResult<CreateItemResult_t> callResult = CallResult<CreateItemResult_t>.Create(createPublishCallback);
		callResult.Set(hAPICall);
		gcDontKill.Add(callResult);
	}

	private void createPublishCallback(CreateItemResult_t callback, bool failure)
	{
		if (failure || callback.m_eResult != EResult.k_EResultOK)
		{
			Debug.Log("UGC: Id failed. " + callback.m_eResult);
			roomToPublish = null;
			ui.PublishingWaiting_Error.gameObject.SetActive(value: true);
			ui.PublishingWaiting_Error_Background_errorLbl.text = "UGC: Id failed. " + callback.m_eResult;
			return;
		}
		Debug.Log("UGC: New id created.");
		roomToPublish.roomId = callback.m_nPublishedFileId.m_PublishedFileId;
		if (callback.m_bUserNeedsToAcceptWorkshopLegalAgreement)
		{
			PublishedFileId_t nPublishedFileId = callback.m_nPublishedFileId;
			SteamFriends.ActivateGameOverlayToWebPage("steam://url/CommunityFilePage/" + nPublishedFileId.ToString(), EActivateGameOverlayToWebPageMode.k_EActivateGameOverlayToWebPageMode_Modal);
			Debug.Log("UGC: m_bUserNeedsToAcceptWorkshopLegalAgreement.");
			roomToPublish = null;
			ui.PublishingWaiting_Error.gameObject.SetActive(value: true);
			ui.PublishingWaiting_Error_Background_errorLbl.text = "UGC: Need to accept workshop legal agreement. ";
		}
		else
		{
			publishRoomToWorkshop();
		}
	}

	private void publishRoomToWorkshop()
	{
		Debug.Log("UGC: Publishing room to workshop.");
		UGCUpdateHandle_t uGCUpdateHandle_t = SteamUGC.StartItemUpdate(new AppId_t(Game.STEAM_APP_ID), new PublishedFileId_t(roomToPublish.roomId));
		SteamUGC.SetItemTitle(uGCUpdateHandle_t, roomToPublish.roomTitle);
		SteamUGC.SetItemDescription(uGCUpdateHandle_t, roomToPublish.roomDescription);
		SteamUGC.SetItemVisibility(uGCUpdateHandle_t, (ERemoteStoragePublishedFileVisibility)ui.Publish_Footer_VisibilityDropdown_Dropdown.value);
		bool hasWalkthrough = roomToPublish.roomWalkthrough.Count > 0;
		SteamUGC.SetItemTags(uGCUpdateHandle_t, getSteamWorkshopTags(hasWalkthrough));
		string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(roomToPublish.roomLocalPath);
		string text = Path.Combine(Directory.GetParent(roomToPublish.roomLocalPath).Parent.FullName, "tempPublish_" + fileNameWithoutExtension);
		if (Directory.Exists(text))
		{
			Directory.Delete(text, recursive: true);
		}
		Directory.CreateDirectory(text);
		Menu.dirCopy(roomToPublish.roomLocalPath, text, roomToPublish.roomLocalPath);
		RoomEditor.clearUnusedFiles(text);
		SteamUGC.SetItemContent(uGCUpdateHandle_t, text);
		if (File.Exists(roomToPublish.roomLocalPath + "/Preview.jpg"))
		{
			SteamUGC.SetItemPreview(uGCUpdateHandle_t, roomToPublish.roomLocalPath + "/Preview.jpg");
		}
		SteamAPICall_t hAPICall = SteamUGC.SubmitItemUpdate(uGCUpdateHandle_t, ui.Publish_Description_BasicInformationContent_PatchInput.text);
		CallResult<SubmitItemUpdateResult_t> callResult = CallResult<SubmitItemUpdateResult_t>.Create(finishPublishCallback);
		callResult.Set(hAPICall);
		gcDontKill.Add(callResult);
	}

	private void finishPublishCallback(SubmitItemUpdateResult_t callback, bool faliure)
	{
		if (callback.m_eResult == EResult.k_EResultOK)
		{
			Debug.Log("UGC: Publishing success");
			ulong roomId = roomToPublish.roomId;
			Menu.writeIdToDisk(roomToPublish.roomLocalPath, roomId);
			ui.PublishingWaiting.gameObject.SetActive(value: false);
			ui.Publish.gameObject.SetActive(value: false);
			onPublishSucess(roomId);
			string text = "http://steamcommunity.com/sharedfiles/filedetails/?id=" + roomToPublish.roomId;
			SteamFriends.ActivateGameOverlayToWebPage(text, EActivateGameOverlayToWebPageMode.k_EActivateGameOverlayToWebPageMode_Modal);
			GUIUtility.systemCopyBuffer = text;
		}
		else if (callback.m_eResult == EResult.k_EResultFileNotFound)
		{
			Menu.writeIdToDisk(roomToPublish.roomLocalPath, 0uL);
			roomToPublish.roomId = 0uL;
			ui.Publish.gameObject.SetActive(value: true);
			ui.PublishingWaiting_Error.gameObject.SetActive(value: true);
			ui.PublishingWaiting_Error_Background_errorLbl.text = "UGC: Failed to find " + roomToPublish.roomTitle + " files or the level was deleted on the Workshop. Check the files and try again. (" + callback.m_eResult.ToString() + ")";
		}
		else
		{
			Debug.Log("UGC: Publishing error");
			ui.Publish.gameObject.SetActive(value: true);
			ui.PublishingWaiting_Error.gameObject.SetActive(value: true);
			if (callback.m_bUserNeedsToAcceptWorkshopLegalAgreement)
			{
				ui.PublishingWaiting_Error_Background_errorLbl.text = "UGC: Publishing error. Need to accept agreement." + callback.m_eResult;
			}
			else
			{
				ui.PublishingWaiting_Error_Background_errorLbl.text = "UGC: Publishing error. " + callback.m_eResult;
			}
		}
		string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(roomToPublish.roomLocalPath);
		string path = Path.Combine(Directory.GetParent(roomToPublish.roomLocalPath).Parent.FullName, "tempPublish_" + fileNameWithoutExtension);
		if (Directory.Exists(path))
		{
			Directory.Delete(path, recursive: true);
		}
		if (callback.m_eResult != EResult.k_EResultFileNotFound)
		{
			roomToPublish = null;
		}
		if (onClosePublish != null)
		{
			onClosePublish(callback.m_eResult == EResult.k_EResultOK);
		}
	}
}

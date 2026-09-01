using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Steamworks.Data;
using Steamworks.Ugc;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Panel_WorkshopSubmitMod : MonoBehaviour
{
	public static ulong m_LastOpenedWorkshopItemID;

	public static string m_LastOpenedWorkshopItemTitle;

	[Header("Header")]
	public TextMeshProUGUI m_HeaderText;

	[Header("Mod Dir")]
	public TextMeshProUGUI m_TextModDirPath;

	[Header("Title")]
	public TMP_InputField m_TitleInputField;

	public Button m_TitleInputFieldGamepadButton;

	[Header("Description")]
	public TMP_InputField m_DescriptionInputField;

	public Scrollbar m_Scrollbar;

	public RectTransform m_ScrollbarRectTransform;

	public Button m_DescriptionInputFieldGamepadButton;

	[Header("Thumbnail")]
	public Texture2D m_DefaultSlotPreview;

	public RawImage m_ThumbnailRawImage;

	[Header("Buttons")]
	public Button m_BrowseDirectoryButton;

	public Button m_CancelButton;

	public Button m_SubmitButton;

	public Button m_BrowseThumbnailButton;

	[Header("Child Panels")]
	public Panel_PickFolder m_PickFolder;

	public GameObject m_Loading;

	private string m_PreviewImagePath;

	private static string[] m_AllowedExtensionsDir = new string[0];

	private static string[] m_AllowedExtensionsThumbnail = new string[3] { "*.jpg", "*.png", "*.gif" };

	private Action<string, string> m_UploadCallback;

	private string m_SourceFullPath;

	private static string m_EditItemID;

	private void Awake()
	{
		m_TitleInputField.characterLimit = Workshop.TITLE_CHAR_LIMIT;
		m_DescriptionInputField.characterLimit = Workshop.DESCRIPTION_CHAR_LIMIT;
		TMP_InputField titleInputField = m_TitleInputField;
		titleInputField.onValidateInput = (TMP_InputField.OnValidateInput)Delegate.Combine(titleInputField.onValidateInput, new TMP_InputField.OnValidateInput(Utils.StripTab));
		TMP_InputField descriptionInputField = m_DescriptionInputField;
		descriptionInputField.onValidateInput = (TMP_InputField.OnValidateInput)Delegate.Combine(descriptionInputField.onValidateInput, new TMP_InputField.OnValidateInput(Utils.StripTab));
	}

	private void Start()
	{
		m_CancelButton.onClick.AddListener(Close);
		m_SubmitButton.onClick.AddListener(TrySubmit);
		m_BrowseThumbnailButton.onClick.AddListener(BrowseForThumbnailImage);
		m_BrowseDirectoryButton.onClick.AddListener(BrowseForModDirectory);
		m_TitleInputFieldGamepadButton.onClick.AddListener(OnTitleInputFieldGamepadButton);
		m_DescriptionInputFieldGamepadButton.onClick.AddListener(OnDescriptionInputFieldGamepadButton);
		m_PickFolder.gameObject.SetActive(value: false);
	}

	private void OnEnable()
	{
		ActivePanels.Add(base.gameObject);
	}

	private void OnDisable()
	{
		ActivePanels.Remove(base.gameObject);
	}

	private void Update()
	{
		ProcessInput();
		UpdateScrollbar();
		UpdatePlaceholderText();
	}

	public void Open(string itemID, string modName, string modFullPath, Action<string, string> callback)
	{
		m_UploadCallback = callback;
		m_SourceFullPath = modFullPath;
		base.gameObject.SetActive(value: true);
		m_Loading.SetActive(value: false);
		UpdateScrollbar();
		m_EditItemID = itemID;
		m_ThumbnailRawImage.texture = m_DefaultSlotPreview;
		Utils.SizeRawImageToParent(m_ThumbnailRawImage);
		if (string.IsNullOrEmpty(itemID))
		{
			m_HeaderText.text = Localize.Get("UI_MODS_CREATE_NEW");
			m_TitleInputField.text = modName;
			m_DescriptionInputField.text = string.Empty;
		}
		else
		{
			m_HeaderText.text = Localize.Get("UI_MODS_EDIT_MOD");
			UpdateFromExistingItem(itemID);
		}
		m_TextModDirPath.text = modFullPath;
		m_PreviewImagePath = string.Empty;
		UpdatePlaceholderText();
	}

	public void UpdateForCurrentDevice()
	{
		m_TitleInputField.interactable = !GamepadVirtualKeyboard.IsSupported();
		m_DescriptionInputField.interactable = !GamepadVirtualKeyboard.IsSupported();
		m_TitleInputFieldGamepadButton.gameObject.SetActive(GamepadVirtualKeyboard.IsSupported());
		m_DescriptionInputFieldGamepadButton.gameObject.SetActive(GamepadVirtualKeyboard.IsSupported());
	}

	private void UploadToWorkshop(string workshopID, string modName, string modDescription, string modFullPath)
	{
		List<string> list = new List<string>();
		list.Add(WorkshopTags.MOD_TAG);
		FileInfo[] luaFilesInMod = Mods.GetLuaFilesInMod(modFullPath);
		if (luaFilesInMod != null && luaFilesInMod.Length != 0)
		{
			if (ModApi.CheckForCheatFunctions(luaFilesInMod))
			{
				list.Add(WorkshopTags.AFFECTS_GAMEPLAY_TAG);
			}
			if (ModApi.CheckForLanguageFunctions(luaFilesInMod))
			{
				list.Add(WorkshopTags.LANGUAGE_TAG);
			}
			if (ModApi.CheckForVehicleUGCFunctions(luaFilesInMod))
			{
				list.Add(WorkshopTags.UGC_VEHICLES_TAG);
			}
			if (ModApi.CheckForZVehicleUGCFunctions(luaFilesInMod))
			{
				list.Add(WorkshopTags.UGC_BOATS_PLANES_TAG);
			}
			if (ModApi.CheckForCustomShapeUGCFunctions(luaFilesInMod))
			{
				list.Add(WorkshopTags.UGC_CUSTOM_SHAPES_TAG);
			}
			if (ModApi.CheckForDecorUGCFunctions(luaFilesInMod))
			{
				list.Add(WorkshopTags.UGC_DECOR_TAG);
			}
			if (ModApi.CheckForWorkshopCampaignFunctions(luaFilesInMod))
			{
				list.Add(WorkshopTags.CAMPAIGN_TAG);
				list.Remove(WorkshopTags.MOD_TAG);
			}
		}
		if (!string.IsNullOrEmpty(workshopID))
		{
			PublishedFileId fileId = default(PublishedFileId);
			if (ulong.TryParse(workshopID, out var result))
			{
				fileId.Value = result;
				SubmitAsync(m_PreviewImagePath, modName, modDescription, modFullPath, list, new Editor(fileId));
			}
		}
		else
		{
			SubmitAsync(m_PreviewImagePath, modName, modDescription, modFullPath, list, Editor.NewCommunityFile);
		}
	}

	public async void SubmitAsync(string previewPath, string modName, string modDescription, string modFullPath, List<string> tags, Editor editor)
	{
		GameUI.m_Instance.m_Status.Open(Localize.Get("UI_STATUS_SUBMITTING_TO_WORKSHOP"));
		foreach (string tag in tags)
		{
			editor.WithTag(tag);
		}
		if (!string.IsNullOrEmpty(previewPath))
		{
			editor.WithPreviewFile(previewPath);
		}
		PublishResult result = await editor.WithTitle(modName).WithDescription(modDescription).WithPublicVisibility()
			.WithContent(modFullPath)
			.SubmitAsync();
		if (!result.Success)
		{
			OnSubmitComplete(success: false, string.Empty);
			return;
		}
		ResultPage? resultPage = await Query.All.WithFileId(result.FileId).GetPageAsync(1);
		if (!resultPage.HasValue)
		{
			OnSubmitComplete(success: false, string.Empty);
			return;
		}
		foreach (Item item in resultPage.Value.Entries)
		{
			await item.Subscribe();
			item.Download();
		}
		if (result.NeedsWorkshopAgreement)
		{
			SteamUtils.OpenWorkshopAgreementOverlay();
		}
		OnSubmitComplete(success: true, result.FileId.Value.ToString());
		Close();
	}

	private void OnSubmitComplete(bool success, string itemId)
	{
		if (!success)
		{
			GameUI.m_Instance.m_Status.Complete(GameManager.IsSteamOffline() ? Localize.Get("UI_STATUS_SUBMIT_WORKSHOP_NETWORK ERROR") : Localize.Get("UI_STATUS_SUBMIT_WORKSHOP_FAILED"));
			m_UploadCallback?.Invoke(string.Empty, m_SourceFullPath);
		}
		else
		{
			GameUI.m_Instance.m_Status.Complete(Localize.Get("UI_STATUS_SUBMIT_WORKSHOP_SUCCESS"));
			m_UploadCallback?.Invoke(itemId, m_SourceFullPath);
		}
	}

	private void TrySubmit()
	{
		if (GameManager.IsSteamOffline())
		{
			PopUpMessage.DisplayErrorOkOnly(Localize.Get("UI_STEAM_OFFLINE"));
		}
		else if (!TitleIsValid())
		{
			PopUpMessage.DisplayWarningOkOnly(string.Format(Localize.Get("UI_MODS_WARN_MIN_TITLE_LEN"), WorkshopSubmit.MIN_CHARS_IN_TITLE));
		}
		else if (string.IsNullOrEmpty(m_DescriptionInputField.text))
		{
			PopUpMessage.DisplayWarningOkOnly(Localize.Get("UI_MODS_UPLOAD_DESC_NOT_FOUND"));
		}
		else if (!Directory.Exists(m_TextModDirPath.text))
		{
			PopUpMessage.DisplayWarningOkOnly(Localize.Get("UI_MODS_UPLOAD_DIR_NOT_FOUND"));
		}
		else if (!AnyLuaFileHasRealCode(m_TextModDirPath.text))
		{
			PopUpMessage.DisplayWarningOkOnly(Localize.Get("UI_MODS_CANNOT_SUBMIT_EMPTY"));
		}
		else if (OnModLoadContainsInvalidCampaignFunctions(m_TextModDirPath.text))
		{
			PopUpMessage.DisplayWarningOkOnly(Localize.Get("UI_MODS_INVALID_CAMPAIGN_MOD"));
		}
		else if (!string.IsNullOrEmpty(Workshop.m_ForceWorkshopID))
		{
			PopUpMessage.DisplayWarning(Workshop.GetForceWorkshopIDWarningMessage(), useYesNoLables: true, DoOverwrite);
		}
		else
		{
			ReallyOnTrySubmit();
		}
	}

	private void ReallyOnTrySubmit()
	{
		if (!string.IsNullOrEmpty(m_EditItemID))
		{
			PopUpTwoChoices.Display(string.Format(Localize.Get("UI_MODS_OVERWRITE_CONFIRM")), Localize.Get("UI_OVERWRITE_MOD"), Localize.Get("UI_UPLOAD_MOS_AS_NEW"), DoOverwrite, TrySubmitAsNew);
		}
		else
		{
			TrySubmitAsNew();
		}
	}

	private void TrySubmitAsNew()
	{
		UploadToWorkshop(string.Empty, m_TitleInputField.text, m_DescriptionInputField.text, m_TextModDirPath.text);
	}

	private void BrowseForThumbnailImage()
	{
		InterfaceAudio.Play("ui_window_open");
		string text = Mods.GetLocalTestModsDirectoryPath();
		if (!Utils.DirectoryExists(text))
		{
			text = Application.persistentDataPath;
		}
		m_PickFolder.Open(text, m_AllowedExtensionsThumbnail, Localize.Get("UI_MODS_SELECT_PREVIEW_IMAGE"), PickedThumbnailImage);
	}

	private void BrowseForModDirectory()
	{
		InterfaceAudio.Play("ui_window_open");
		string text = Mods.GetLocalTestModsDirectoryPath();
		if (!Utils.DirectoryExists(text))
		{
			text = Application.persistentDataPath;
		}
		m_PickFolder.Open(text, m_AllowedExtensionsDir, Localize.Get("UI_MODS_SELECT_MOD_DIR"), PickedFolderCallback);
	}

	private async void PickedThumbnailImage(string fullPath)
	{
		bool flag = false;
		string[] allowedExtensionsThumbnail = m_AllowedExtensionsThumbnail;
		foreach (string text in allowedExtensionsThumbnail)
		{
			if (fullPath.EndsWith(text.Substring(text.Length - 3)))
			{
				flag = true;
			}
		}
		if (!flag)
		{
			PopUpMessage.DisplayWarningOkOnly(Localize.Get("UI_MODS_IMAGE_WRONG_TYPE"));
			return;
		}
		if (!Utils.FileExists(fullPath))
		{
			PopUpMessage.DisplayWarningOkOnly(Localize.Get("UI_MODS_IMAGE_NOT_FOUND", fullPath));
			return;
		}
		if (Utils.GetFileLengthInBytes(fullPath) > 1048576)
		{
			PopUpMessage.DisplayWarningOkOnly(Localize.Get("UI_MODS_IMAGE_TOO_BIG"));
			return;
		}
		m_PreviewImagePath = fullPath;
		await GetTexture(fullPath);
	}

	private async Task GetTexture(string url)
	{
		UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
		www.SendWebRequest();
		int loopBreaker = 0;
		while (www.result == UnityWebRequest.Result.InProgress && loopBreaker < 100)
		{
			loopBreaker++;
			await Task.Delay(100);
		}
		if (www.result == UnityWebRequest.Result.Success)
		{
			m_ThumbnailRawImage.texture = DownloadHandlerTexture.GetContent(www);
			Utils.SizeRawImageToParent(m_ThumbnailRawImage);
		}
	}

	private void PickedFolderCallback(string fullpath)
	{
		m_TextModDirPath.text = fullpath;
	}

	private void DoOverwrite()
	{
		string workshopID = ((!string.IsNullOrEmpty(Workshop.m_ForceWorkshopID)) ? Workshop.m_ForceWorkshopID : m_EditItemID);
		UploadToWorkshop(workshopID, m_TitleInputField.text, m_DescriptionInputField.text, m_TextModDirPath.text);
	}

	private void UpdateScrollbar()
	{
		if (Mathf.Approximately(m_Scrollbar.size, 1f))
		{
			m_ScrollbarRectTransform.anchoredPosition = new Vector2(5000f, m_ScrollbarRectTransform.anchoredPosition.y);
		}
		else
		{
			m_ScrollbarRectTransform.anchoredPosition = new Vector2(15f, m_ScrollbarRectTransform.anchoredPosition.y);
		}
	}

	private void Close()
	{
		InterfaceAudio.Play("ui_menubar_gen_off");
		base.gameObject.SetActive(value: false);
	}

	private bool TitleIsValid()
	{
		return m_TitleInputField.text.Trim().Length >= WorkshopSubmit.MIN_CHARS_IN_TITLE;
	}

	private void ProcessInput()
	{
		if (GameStateCommonInput.IgnoreKeyboardInputForPanel(base.gameObject))
		{
			return;
		}
		if (Input.GetKeyDown(KeyCode.Escape) || GamepadManager.ButtonJustPressed(GamepadButtonType.EAST))
		{
			Close();
		}
		else if (Input.GetKeyDown(KeyCode.Tab))
		{
			if (m_TitleInputField.isFocused)
			{
				m_DescriptionInputField.ActivateInputField();
				m_DescriptionInputField.Select();
			}
			else if (m_DescriptionInputField.isFocused)
			{
				m_TitleInputField.ActivateInputField();
				m_TitleInputField.Select();
			}
		}
	}

	private void UpdateFromExistingItem(string itemID)
	{
		WorkshopItem item = WorkshopCaches.GetItem(WorkshopTab.MODS, itemID);
		if (item != null)
		{
			if (item.m_PreviewTexture != null)
			{
				m_ThumbnailRawImage.texture = item.m_PreviewTexture;
				m_ThumbnailRawImage.gameObject.SetActive(value: true);
				Utils.SizeRawImageToParent(m_ThumbnailRawImage);
			}
			m_TitleInputField.text = item.GetTitle();
			m_DescriptionInputField.text = item.GetDescription();
		}
		else
		{
			SteamItemInfo subscibedItem = Workshop.GetSubscibedItem(itemID);
			if (subscibedItem != null)
			{
				m_TitleInputField.text = subscibedItem.m_Title;
				m_DescriptionInputField.text = subscibedItem.m_Description;
				m_Loading.SetActive(value: true);
				AsyncLoadPreviewTexture(subscibedItem.m_PreviewImageUrl);
			}
			else
			{
				DownloadItem(itemID, DownloadComplete);
			}
		}
	}

	private void DownloadComplete(Item item)
	{
		m_TitleInputField.text = item.Title;
		m_DescriptionInputField.text = item.Description;
		AsyncLoadPreviewTexture(item.PreviewImageUrl);
	}

	private async void DownloadItem(string itemID, Action<Item> callback)
	{
		PublishedFileId[] array = new PublishedFileId[1] { default(PublishedFileId) };
		if (ulong.TryParse(itemID, out var result))
		{
			array[0].Value = result;
			ResultPage? resultPage = await Query.All.WithFileId(array).GetPageAsync(1);
			if (resultPage.HasValue)
			{
				using IEnumerator<Item> enumerator = resultPage.Value.Entries.GetEnumerator();
				if (enumerator.MoveNext())
				{
					Item current = enumerator.Current;
					callback?.Invoke(current);
					return;
				}
			}
		}
		m_Loading.SetActive(value: false);
		m_TitleInputField.text = string.Empty;
		m_DescriptionInputField.text = string.Empty;
	}

	private void UpdatePlaceholderText()
	{
		m_TitleInputField.placeholder.GetComponent<TextMeshProUGUI>().text = Localize.Get("UI_SANDBOX_ENTER_TITLE");
		m_DescriptionInputField.placeholder.GetComponent<TextMeshProUGUI>().text = Localize.Get("UI_SANDBOX_ENTER_DESCRIPTION");
	}

	private void AsyncLoadPreviewTexture(string url)
	{
		WebRequest.GetTexture(url).SendWebRequest().completed += OnLoadPreviewComplete;
	}

	private void OnLoadPreviewComplete(AsyncOperation asyncOperation)
	{
		UnityWebRequestAsyncOperation unityWebRequestAsyncOperation = (UnityWebRequestAsyncOperation)asyncOperation;
		if (unityWebRequestAsyncOperation.webRequest.result == UnityWebRequest.Result.ConnectionError || unityWebRequestAsyncOperation.webRequest.result == UnityWebRequest.Result.ProtocolError)
		{
			string errorMessage = WebRequest.GetErrorMessage(unityWebRequestAsyncOperation.webRequest);
			Debug.LogWarning("Load slot preview failed: " + errorMessage);
		}
		else
		{
			m_ThumbnailRawImage.texture = DownloadHandlerTexture.GetContent(unityWebRequestAsyncOperation.webRequest);
			m_ThumbnailRawImage.gameObject.SetActive(value: true);
			Utils.SizeRawImageToParent(m_ThumbnailRawImage);
		}
		m_Loading.SetActive(value: false);
	}

	private bool AnyLuaFileHasRealCode(string modDirectory)
	{
		if (LuaFileHasRealCode(Path.Combine(modDirectory, Mods.MOD_LOAD_FILENAME)))
		{
			return true;
		}
		if (LuaFileHasRealCode(Path.Combine(modDirectory, Mods.MOD_UPDATE_FILENAME)))
		{
			return true;
		}
		if (LuaFileHasRealCode(Path.Combine(modDirectory, Mods.MOD_FIXED_UPDATE_FILENAME)))
		{
			return true;
		}
		return false;
	}

	private bool OnModLoadContainsInvalidCampaignFunctions(string modDirectory)
	{
		string path = Path.Combine(modDirectory, Mods.MOD_LOAD_FILENAME);
		if (File.Exists(path))
		{
			string[] array = File.ReadAllLines(path);
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].Trim() == "WorkshopCampaignAddLevelToWorld(\"WORLD_NAME\", \"LEVEL_ID\")")
				{
					return true;
				}
			}
		}
		return false;
	}

	private bool LuaFileHasRealCode(string luaFilePath)
	{
		if (File.Exists(luaFilePath))
		{
			string[] array = File.ReadAllLines(luaFilePath);
			for (int i = 0; i < array.Length; i++)
			{
				string text = array[i].Trim();
				if (text.Length > 0 && !text.StartsWith("--"))
				{
					return true;
				}
			}
		}
		return false;
	}

	private void OnTitleInputFieldGamepadButton()
	{
		GamepadVirtualKeyboard.MaybeOpenVirtualKeyboard(m_TitleInputField.text, m_TitleInputField.characterLimit, Localize.Get("UI_TITLE"), multiline: false, OnTitleEntered);
	}

	private void OnDescriptionInputFieldGamepadButton()
	{
		GamepadVirtualKeyboard.MaybeOpenVirtualKeyboard(m_DescriptionInputField.text, m_DescriptionInputField.characterLimit, Localize.Get("UI_DESCRIPTION"), multiline: true, OnDescriptionEntered);
	}

	private void OnTitleEntered(string title)
	{
		if (title != null)
		{
			m_TitleInputField.text = title;
		}
	}

	private void OnDescriptionEntered(string description)
	{
		if (description != null)
		{
			m_DescriptionInputField.text = description;
		}
	}
}

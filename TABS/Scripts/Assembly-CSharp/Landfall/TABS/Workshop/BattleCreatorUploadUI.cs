using System;
using System.Collections.Generic;
using Landfall.TABS_Input;
using ModIO;
using TFBGames;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Landfall.TABS.Workshop
{
	public class BattleCreatorUploadUI : MonoBehaviour, IBattleCreatorMenu
	{
		[SerializeField]
		private TMP_InputField m_DescriptionContent;

		[SerializeField]
		private LocalizeText m_HeaderText;

		[SerializeField]
		private GameObject m_TagCell;

		[SerializeField]
		private Transform m_TagList;

		[SerializeField]
		private GameObject m_onScreenButtons;

		[SerializeField]
		private GameObject m_buttonPrompts;

		[SerializeField]
		private LocalizeText m_uploadButtonLocalizer;

		[SerializeField]
		private Button m_UploadButton;

		[SerializeField]
		private Button m_UpdateButton;

		[SerializeField]
		private Selectable m_contentDescriptionInputField;

		private BattleCreatorState m_CurrentState;

		private State m_state;

		private BattleCreatorTabsUIHandler m_UIHandler;

		private List<GenericCustomContentWrapper> m_AllContentToUpload;

		private BattleCreatorAssetUICellBase m_ContentToUpload;

		private UpdateableWorkshopContentPack m_contentToUpdate;

		private List<string> m_SelectedTags;

		private Color m_ActiveColor;

		private Color m_PassiveColor;

		private ContentTypeFilter m_currentContentFilter = ContentTypeFilter.Battles;

		private InputService m_inputService;

		private ModalPanel m_modalPanel;

		public bool AllowPageChange => true;

		private void InitReferences()
		{
			m_SelectedTags = new List<string>();
			BattleCreatorDataHolder dataHolder = BattleCreatorDataHolder.GetDataHolder();
			m_ActiveColor = dataHolder.GetTabColor(active: false);
			m_PassiveColor = dataHolder.GetTabColor(active: true);
		}

		private void Clear()
		{
			for (int num = m_TagList.childCount - 1; num >= 0; num--)
			{
				UnityEngine.Object.Destroy(m_TagList.GetChild(num).gameObject);
			}
		}

		private void UpdateContent()
		{
			m_modalPanel.Choice("Upload to existing mod?", "Update existing mod: " + m_contentToUpdate.SelectedContent.ContentName + "?", delegate
			{
				ScenarioEditorUI.instance.GoToPreviousScreen();
				string desc = ValidateUpdatedDescription();
				BattleCreatorSharedCommands.UpdateContent(m_contentToUpdate, m_contentToUpdate.SelectedContent, null, desc);
			}, null, new string[2] { "UPDATE", "CANCEL" });
		}

		private string ValidateUpdatedDescription()
		{
			if (string.IsNullOrEmpty(m_DescriptionContent.text))
			{
				return m_contentToUpdate.ContentType.ToWorkshopTypeFilter().ToString() + " Uploaded By: " + CustomContentLoaderModIO.LocalModIOUser.nameId;
			}
			return m_DescriptionContent.text;
		}

		private void UploadContent()
		{
			ScenarioEditorUI.instance.GoToPreviousScreen();
			ValidateDescription();
		}

		private string ValidateDescription()
		{
			if (string.IsNullOrEmpty(m_DescriptionContent.text))
			{
				return m_ContentToUpload.ContentType.ToWorkshopTypeFilter().ToString() + " Uploaded By: " + CustomContentLoaderModIO.LocalModIOUser.nameId;
			}
			return m_DescriptionContent.text;
		}

		private void PopulateTags(ModTag[] tagsSelected = null)
		{
			Clear();
			switch ((m_ContentToUpload ?? m_contentToUpdate.SelectedContent).ContentType)
			{
			case ContentTypeFilter.Battles:
				m_SelectedTags.Add(WorkshopContentType.Battle.ToString());
				break;
			case ContentTypeFilter.Campaigns:
				m_SelectedTags.Add(WorkshopContentType.Campaign.ToString());
				break;
			case ContentTypeFilter.Units:
				m_SelectedTags.Add(WorkshopContentType.Unit.ToString());
				break;
			case ContentTypeFilter.Factions:
				m_SelectedTags.Add(WorkshopContentType.Faction.ToString());
				break;
			}
			ModTagCategory[] tagCategories = CustomContentLoaderModIO.ModIOGameProfile.tagCategories;
			List<Button> list = new List<Button>();
			ModTagCategory[] array = tagCategories;
			foreach (ModTagCategory modTagCategory in array)
			{
				if (modTagCategory.name.ToUpper() == "TYPE")
				{
					continue;
				}
				string[] tags = modTagCategory.tags;
				foreach (string text in tags)
				{
					string tagText = text;
					GameObject gameObject = UnityEngine.Object.Instantiate(m_TagCell);
					gameObject.transform.SetParent(m_TagList, worldPositionStays: false);
					gameObject.transform.localScale = Vector3.one;
					GameObject tagCell = gameObject;
					tagCell.GetComponent<Image>().color = m_ActiveColor;
					TextMeshProUGUI componentInChildren = gameObject.GetComponentInChildren<TextMeshProUGUI>();
					componentInChildren.color = Color.white;
					componentInChildren.text = tagText;
					Button component = gameObject.GetComponent<Button>();
					component.onClick.AddListener(delegate
					{
						OnTagClicked(tagCell, tagText);
					});
					list.Add(component);
					gameObject.SetActive(value: true);
					if (tagsSelected == null)
					{
						continue;
					}
					foreach (ModTag modTag in tagsSelected)
					{
						if (text == modTag.name)
						{
							OnTagClicked(tagCell, tagText);
							break;
						}
					}
				}
			}
			CreateNavigation(list);
		}

		private void OnTagClicked(GameObject tagCell, string tag)
		{
			Debug.Log("ClickedTag: " + tag);
			if (m_SelectedTags.Contains(tag))
			{
				RemoveTag(tagCell, tag);
			}
			else
			{
				AddTag(tagCell, tag);
			}
		}

		private void RemoveTag(GameObject tagCell, string tag)
		{
			m_SelectedTags.Remove(tag);
			tagCell.GetComponent<Image>().color = m_ActiveColor;
			tagCell.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
		}

		private void AddTag(GameObject tagCell, string tag)
		{
			m_SelectedTags.Add(tag);
			tagCell.GetComponent<Image>().color = m_PassiveColor;
			tagCell.GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
		}

		private void CheckCurrentBattleCreatorState()
		{
			m_AllContentToUpload = new List<GenericCustomContentWrapper>();
			BattleCreatorAssetUICellBase battleCreatorAssetUICellBase = m_ContentToUpload ?? m_contentToUpdate.SelectedContent;
			switch (battleCreatorAssetUICellBase.ContentType)
			{
			case ContentTypeFilter.Battles:
				m_AllContentToUpload.Add(new GenericCustomContentWrapper(battleCreatorAssetUICellBase.LevelAsset.Entity.Name, battleCreatorAssetUICellBase.FullPath, battleCreatorAssetUICellBase.LevelAsset.Entity.GUID, WorkshopContentType.Battle));
				break;
			case ContentTypeFilter.Campaigns:
			{
				m_AllContentToUpload.Add(new GenericCustomContentWrapper(battleCreatorAssetUICellBase.ContentName, battleCreatorAssetUICellBase.FullPath, battleCreatorAssetUICellBase.CampaignAsset.Entity.GUID, WorkshopContentType.Campaign));
				TABSCampaignLevelAsset[] levelsInCampaign = battleCreatorAssetUICellBase.CampaignAsset.LevelsInCampaign;
				foreach (TABSCampaignLevelAsset tABSCampaignLevelAsset in levelsInCampaign)
				{
					m_AllContentToUpload.Add(new GenericCustomContentWrapper(tABSCampaignLevelAsset.Entity.Name, tABSCampaignLevelAsset.FilePath, tABSCampaignLevelAsset.Entity.GUID, WorkshopContentType.Battle));
				}
				break;
			}
			case ContentTypeFilter.None:
			case ContentTypeFilter.Battles | ContentTypeFilter.Campaigns:
			case ContentTypeFilter.Units:
			case ContentTypeFilter.Battles | ContentTypeFilter.Units:
			case ContentTypeFilter.Campaigns | ContentTypeFilter.Units:
			case ContentTypeFilter.Battles | ContentTypeFilter.Campaigns | ContentTypeFilter.Units:
			case ContentTypeFilter.Factions:
				break;
			}
		}

		public void Close()
		{
			m_inputService.OnUIClose();
			base.gameObject.SetActive(value: false);
			m_state = State.Closing;
			InputService service = ServiceLocator.GetService<InputService>();
			if (service != null)
			{
				service.InputChanged -= OnInputSourceChanged;
			}
		}

		public bool IsOpen()
		{
			return m_state == State.Opening;
		}

		public void Open(BattleCreatorState state, object data = null)
		{
			InitReferences();
			m_inputService.OnUIOpen();
			base.gameObject.SetActive(value: true);
			m_CurrentState = state;
			if (m_CurrentState == BattleCreatorState.Upload)
			{
				m_contentToUpdate = null;
				m_ContentToUpload = (BattleCreatorAssetUICellBase)data;
				Debug.Log("Content To Upload: " + m_ContentToUpload.ContentType.ToString() + " Name: " + m_ContentToUpload.ContentName);
				m_state = State.Opening;
				m_CurrentState = state;
				m_DescriptionContent.text = string.Empty;
				CheckCurrentBattleCreatorState();
				Debug.Log(string.Concat("Opening With State: ", state, " : ", m_ContentToUpload.ContentName));
				m_HeaderText.Args = new string[2]
				{
					m_ContentToUpload.ContentName,
					m_ContentToUpload.ContentType.ToString()
				};
				m_HeaderText.LocaleID = "LABEL_UPLOAD_TITLE";
				m_uploadButtonLocalizer.LocaleID = "BUTTON_UPLOAD";
				PopulateTags();
			}
			else if (m_CurrentState == BattleCreatorState.Update)
			{
				m_contentToUpdate = (UpdateableWorkshopContentPack)data;
				CheckCurrentBattleCreatorState();
				m_contentToUpdate.CustomContent = m_AllContentToUpload;
				string contentName = m_contentToUpdate.AssetCellUI.ContentName;
				ContentTypeFilter contentType = m_contentToUpdate.ContentType;
				Debug.Log("Content To Update: " + m_contentToUpdate.ContentType.ToString() + " Name: " + contentName);
				m_state = State.Opening;
				m_CurrentState = state;
				m_DescriptionContent.text = m_contentToUpdate.SelectedContent.Description;
				Debug.Log(string.Concat("Opening With State: ", state, " : ", contentName));
				m_HeaderText.Args = new string[2]
				{
					contentName,
					contentType.ToString()
				};
				m_HeaderText.LocaleID = "LABEL_UPDATE_TITLE";
				m_uploadButtonLocalizer.LocaleID = "BUTTON_UPDATE";
				PopulateTags(m_contentToUpdate.SelectedContent.ModProfile.tags);
			}
			InputService service = ServiceLocator.GetService<InputService>();
			if (service != null)
			{
				service.InputChanged += OnInputSourceChanged;
			}
			OnInputSourceChanged(PlayerActions.Instance.InputType);
		}

		public void Init(BattleCreatorTabsUIHandler uiHandler)
		{
			m_UIHandler = uiHandler;
			m_inputService = ServiceLocator.GetService<InputService>();
			m_modalPanel = ServiceLocator.GetService<ModalPanel>();
		}

		public void Toggle(BattleCreatorState state)
		{
			if (m_state == State.Closing)
			{
				Open(state);
			}
			else
			{
				Close();
			}
		}

		public void Init(CustomContentOverlaysManager overlay)
		{
			throw new NotImplementedException();
		}

		public bool NavigateUIWithController(PlayerActions playerActions)
		{
			if (playerActions.m_upload.WasPressed)
			{
				UploadUpdateContent();
			}
			if (playerActions.m_back.WasPressed && !m_inputService.IsTextInputCurrentlySelected())
			{
				ScenarioEditorUI.instance.GoToPreviousScreen();
			}
			return false;
		}

		public void UploadUpdateContent()
		{
			switch (m_CurrentState)
			{
			case BattleCreatorState.Upload:
				UploadContent();
				break;
			case BattleCreatorState.Update:
				UpdateContent();
				break;
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		public void GoBack()
		{
			switch (m_CurrentState)
			{
			case BattleCreatorState.Upload:
				m_UIHandler.OpenUploadScreen();
				break;
			default:
				throw new ArgumentOutOfRangeException();
			case BattleCreatorState.Update:
				break;
			}
		}

		private void OnInputSourceChanged(InputType type)
		{
			switch (type)
			{
			case InputType.Controller:
				m_contentDescriptionInputField.Select();
				m_onScreenButtons.SetActive(value: false);
				m_buttonPrompts.SetActive(value: true);
				break;
			case InputType.Keyboard:
			case InputType.Any:
				m_onScreenButtons.SetActive(value: true);
				m_buttonPrompts.SetActive(value: false);
				EventSystem.current.SetSelectedGameObject(null);
				break;
			default:
				throw new ArgumentOutOfRangeException("type", type, null);
			}
		}

		private void CreateNavigation(List<Button> tagButtons)
		{
			UIHelpers.CreateExplicitLinearNavigation(tagButtons.ToArray(), horizontal: false);
			Button button = tagButtons[0];
			Button button2 = tagButtons[tagButtons.Count - 1];
			Navigation navigation = button.navigation;
			navigation.selectOnUp = m_contentDescriptionInputField;
			button.navigation = navigation;
			navigation = button2.navigation;
			navigation.selectOnDown = ((m_currentContentFilter == ContentTypeFilter.Battles) ? m_UpdateButton : m_UploadButton);
			button2.navigation = navigation;
			navigation = m_contentDescriptionInputField.navigation;
			navigation.selectOnDown = button;
			navigation.selectOnUp = ((m_currentContentFilter == ContentTypeFilter.Battles) ? m_UpdateButton : m_UploadButton);
			m_contentDescriptionInputField.navigation = navigation;
		}
	}
}

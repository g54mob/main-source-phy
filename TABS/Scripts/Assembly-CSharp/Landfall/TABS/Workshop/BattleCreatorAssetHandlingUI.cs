using System;
using System.Collections.Generic;
using DM;
using Landfall.TABS_Input;
using ModIO;
using TFBGames;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Landfall.TABS.Workshop
{
	public class BattleCreatorAssetHandlingUI : MonoBehaviour, IBattleCreatorMenu
	{
		[SerializeField]
		private Transform m_Grid;

		[SerializeField]
		private GameObject m_ContentCell;

		[SerializeField]
		private TMP_Text m_header;

		[Space]
		[SerializeField]
		private GameObject[] controlPromptLabels;

		[SerializeField]
		private TextMeshProUGUI m_acceptGlyphText;

		[SerializeField]
		private ActionGlyphText m_toggleGlyphText;

		[SerializeField]
		private BattleCreatorCampaignCreatorUI m_campaignCreatorUIManager;

		[SerializeField]
		private GameObject m_LoginModIODialogObject;

		[HideInInspector]
		public BattleCreatorState m_CurrentState;

		private GridLayoutHandler m_GridHandler;

		private BattleCreatorOptionMenuUI m_OptionMenu;

		private BattleCreatorTabsUIHandler m_UIHandler;

		private BattleCreatorAssetUICellBase m_LastClickedLevel;

		private UpdateableWorkshopContentPack m_CustomContentToUpload;

		private State m_state;

		private Action<BattleCreatorAssetUICellBase> m_OnAssetClicked;

		private ContentTypeFilter m_ContentFilter;

		private WorkshopTypeFilter m_WorkshopFilter;

		private InputService m_inputService;

		private ModalPanel m_ModalPanel;

		public bool AllowPageChange => true;

		private void InitReferences()
		{
			m_GridHandler = m_Grid.GetComponent<GridLayoutHandler>();
			m_ModalPanel = ServiceLocator.GetService<ModalPanel>();
		}

		public void OnSwitchedContentFilter(WorkshopTypeFilter newFilter)
		{
			if (m_ContentFilter == ContentTypeFilter.Battles)
			{
				switch (newFilter)
				{
				case WorkshopTypeFilter.Local:
					m_header.text = "MY BATTLES";
					break;
				case WorkshopTypeFilter.Workshop:
					m_header.text = "COMMUNITY BATTLES";
					break;
				}
			}
			else if (m_ContentFilter == ContentTypeFilter.Campaigns)
			{
				switch (newFilter)
				{
				case WorkshopTypeFilter.Local:
					m_header.text = "MY CAMPAIGNS";
					break;
				case WorkshopTypeFilter.Workshop:
					m_header.text = "COMMUNITY CAMPAIGNS";
					break;
				}
			}
			if (m_WorkshopFilter != newFilter)
			{
				m_WorkshopFilter = newFilter;
				Populate();
			}
		}

		public void OnSwitchedContentFilter(ContentTypeFilter newFilter)
		{
			if (m_ContentFilter != newFilter)
			{
				m_ContentFilter = newFilter;
				Populate();
			}
		}

		private void CheckCurrentBattleCreatorState()
		{
			switch (m_CurrentState)
			{
			case BattleCreatorState.Load:
				m_ContentFilter = ContentTypeFilter.Battles;
				m_WorkshopFilter = WorkshopTypeFilter.Local;
				m_OnAssetClicked = OnLevelCogClicked;
				break;
			case BattleCreatorState.CampaignCreator:
				m_ContentFilter = ContentTypeFilter.Campaigns;
				m_WorkshopFilter = WorkshopTypeFilter.Local;
				m_OnAssetClicked = OnCampaignCellClicked;
				break;
			}
		}

		private void Populate()
		{
			Clear();
			List<GridLayoutHandler.GridDataWrapper> list = new List<GridLayoutHandler.GridDataWrapper>();
			if (m_ContentFilter == ContentTypeFilter.Battles)
			{
				if (m_WorkshopFilter == WorkshopTypeFilter.WorkshopSelf)
				{
					ModProfile[] userModsOfType = CustomContentLoaderModIO.GetUserModsOfType(WorkshopContentType.Battle);
					foreach (ModProfile modProfile in userModsOfType)
					{
						if (modProfile.status == ModStatus.Accepted)
						{
							list.Add(SpawnUserModCell(modProfile));
						}
					}
				}
				else
				{
					foreach (TABSCampaignLevelAsset item in ContentDatabase.Instance().GetUserCampaignLevelsByFilter(new Filter
					{
						WorkshopTypeFilter = m_WorkshopFilter
					}))
					{
						list.Add(SpawnLevelCell(item));
					}
				}
			}
			if (m_ContentFilter == ContentTypeFilter.Campaigns)
			{
				if (m_WorkshopFilter == WorkshopTypeFilter.WorkshopSelf)
				{
					ModProfile[] userModsOfType = CustomContentLoaderModIO.GetUserModsOfType(WorkshopContentType.Campaign);
					foreach (ModProfile modProfile2 in userModsOfType)
					{
						if (modProfile2.status == ModStatus.Accepted)
						{
							list.Add(SpawnUserModCell(modProfile2));
						}
					}
				}
				else
				{
					foreach (TABSCampaignAsset item2 in ContentDatabase.Instance().GetUserCampaignsByFilter(new Filter
					{
						WorkshopTypeFilter = m_WorkshopFilter
					}))
					{
						list.Add(SpawnCampaignCell(item2));
					}
				}
			}
			bool withSaveButton = false;
			m_GridHandler.Feed(list.ToArray(), withSaveButton);
		}

		private bool CreateAddButtonToMapGrid(out SaveButtonInfo saveButtonInfo)
		{
			bool result = (m_CurrentState == BattleCreatorState.Load || m_CurrentState == BattleCreatorState.CampaignCreator) && m_WorkshopFilter == WorkshopTypeFilter.Local;
			switch (m_CurrentState)
			{
			case BattleCreatorState.Load:
				saveButtonInfo.Title = "BUTTON_SAVEBATTLE";
				saveButtonInfo.OnPressAction = delegate
				{
					m_UIHandler.OpenNewScreen(BattleCreatorScreenState.Save, BattleCreatorState.Save, null, closeIfAlreadyOpen: false);
				};
				break;
			case BattleCreatorState.CampaignCreator:
				saveButtonInfo.Title = "BUTTON_SAVECAMPAIGN";
				saveButtonInfo.OnPressAction = delegate
				{
					m_UIHandler.OpenNewScreen(BattleCreatorScreenState.TwoList, BattleCreatorState.CampaignCreator);
				};
				break;
			default:
				saveButtonInfo = default(SaveButtonInfo);
				break;
			}
			return result;
		}

		private void Clear()
		{
			for (int num = m_Grid.childCount - 1; num >= 0; num--)
			{
				UnityEngine.Object.Destroy(m_Grid.GetChild(num).gameObject);
			}
		}

		private GridLayoutHandler.GridDataWrapper SpawnCampaignCell(TABSCampaignAsset campaign)
		{
			return new GridLayoutHandler.GridDataWrapper(new BattleCreatorAssetUICellBase.CampaignData(campaign.Entity.Name, campaign, m_OnAssetClicked, OnLevelDeleteClicked, OnLevelCogClicked, OnContentUploadClicked, OnLoadClicked, m_ContentFilter, m_CurrentState), BattleCreatorAssetUICellBase.CellType.CampaignContent);
		}

		private GridLayoutHandler.GridDataWrapper SpawnLevelCell(TABSCampaignLevelAsset level)
		{
			GameObject obj = UnityEngine.Object.Instantiate(m_ContentCell);
			UnityEngine.Object.Destroy(obj.GetComponent<BattleCreatorAssetUICellBase>().GetComponent<DraggableUIElement>());
			string levelName = level.Entity.Name;
			obj.transform.SetParent(m_Grid, worldPositionStays: true);
			obj.transform.localScale = Vector3.one;
			obj.SetActive(value: true);
			return new GridLayoutHandler.GridDataWrapper(new BattleCreatorAssetUICellBase.CampaignLevelData(levelName, level, m_OnAssetClicked, OnLevelDeleteClicked, OnLevelCogClicked, OnContentUploadClicked, OnLoadClicked, m_ContentFilter, m_CurrentState), BattleCreatorAssetUICellBase.CellType.LevelContent);
		}

		private GridLayoutHandler.GridDataWrapper SpawnUserModCell(ModProfile modProfile)
		{
			return new GridLayoutHandler.GridDataWrapper(new BattleCreatorAssetUICellBase.UpdateContentData(modProfile.name, modProfile, m_OnAssetClicked, null, null, m_ContentFilter, m_CurrentState), BattleCreatorAssetUICellBase.CellType.UpdateContent);
		}

		private void OnLevelCogClicked(BattleCreatorAssetUICellBase cellUI)
		{
			m_UIHandler.OpenNewScreen(BattleCreatorScreenState.Save, BattleCreatorState.Save, cellUI);
		}

		private void OnLevelDeleteClicked(BattleCreatorAssetUICellBase cellUI)
		{
			BattleCreatorSharedCommands.DeleteContent(cellUI, Populate);
		}

		private void OnLevelLoadClicked(BattleCreatorAssetUICellBase levelCell)
		{
			BattleCreatorSharedCommands.LoadContent(levelCell, delegate
			{
				if (CampaignHandler.LastLoadedLevel != null)
				{
					CampaignHandler.LastLoadedLevel.SetCustomIDButOnlySometimes(levelCell.LevelAsset.ModID);
				}
				if (m_UIHandler != null)
				{
					m_UIHandler.Close();
				}
			});
		}

		private void OnUpdateContentClicked(BattleCreatorAssetUICellBase contentCell)
		{
			ScenarioEditorUI.instance.WaitForToken(delegate
			{
				m_CustomContentToUpload.SelectedContent = contentCell;
				BattleCreatorSharedCommands.OpenUpdateScreen(m_CustomContentToUpload);
			});
		}

		private void OnCampaignCellClicked(BattleCreatorAssetUICellBase campaignCell)
		{
			BattleCreatorSharedCommands.LoadContent(campaignCell);
			m_campaignCreatorUIManager.ChangeUIControllerState(CampaignCreatorUIMode.Designing);
		}

		private void OnContentUploadClicked(BattleCreatorAssetUICellBase contentCell)
		{
			ScenarioEditorUI.instance.WaitForToken(delegate
			{
				ModManager.FetchAuthenticatedUserMods(delegate(List<ModProfile> profiles)
				{
					foreach (ModProfile profile in profiles)
					{
						if (contentCell.ContentName == profile.name && profile.status == ModStatus.Accepted)
						{
							contentCell.Init(new BattleCreatorAssetUICellBase.UpdateContentData(contentCell.ContentName, profile, null, null, null, contentCell.ContentType, BattleCreatorState.Update));
							BattleCreatorSharedCommands.OpenUpdateScreen(new UpdateableWorkshopContentPack
							{
								AssetCellUI = contentCell,
								SelectedContent = contentCell
							});
							return;
						}
					}
					BattleCreatorSharedCommands.OpenUploadScreen(contentCell);
				}, WebRequestError.LogAsWarning);
			});
		}

		private void OnLoadClicked(BattleCreatorAssetUICellBase contentCell)
		{
			if (contentCell.ContentType == ContentTypeFilter.Campaigns)
			{
				CampaignPlayerDataHolder.StartedPlayingNewCampaign(contentCell.CampaignAsset, 0);
				TABSSceneManager.LoadCampaign();
			}
			else if (contentCell.ContentType == ContentTypeFilter.Battles)
			{
				CampaignHandler.LoadLayoutFromDisk(contentCell.LevelAsset.FilePath, null);
				ScenarioEditorUI.instance.CloseScenarioEditor();
			}
		}

		public void Close()
		{
			m_inputService.OnUIClose();
			m_state = State.Closing;
			InputService service = ServiceLocator.GetService<InputService>();
			if (service != null)
			{
				service.InputChanged -= OnInputSourceChanged;
			}
			m_GridHandler.OnGridItemsFinishedSpawning -= DoneSpawningGridItems;
			base.gameObject.SetActive(value: false);
		}

		public bool IsOpen()
		{
			return m_state == State.Opening;
		}

		public void Open(BattleCreatorState state, object data = null)
		{
			m_inputService.OnUIOpen();
			m_state = State.Opening;
			m_CurrentState = state;
			if (m_CurrentState == BattleCreatorState.Update)
			{
				m_CustomContentToUpload = (UpdateableWorkshopContentPack)data;
			}
			CheckCurrentBattleCreatorState();
			InputService service = ServiceLocator.GetService<InputService>();
			OnInputSourceChanged(PlayerActions.Instance.InputType);
			if (service != null)
			{
				service.InputChanged += OnInputSourceChanged;
			}
			m_GridHandler.OnGridItemsFinishedSpawning += DoneSpawningGridItems;
			Populate();
			base.gameObject.SetActive(value: true);
		}

		private void DoneSpawningGridItems()
		{
			if (PlayerActions.Instance.InputType == InputType.Controller)
			{
				SelectFirstGridButton();
			}
		}

		public void Init(BattleCreatorTabsUIHandler uiHandler)
		{
			m_UIHandler = uiHandler;
			m_inputService = ServiceLocator.GetService<InputService>();
			InitReferences();
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
			NavigateGridLayout(playerActions);
			if (playerActions.m_back.WasPressed)
			{
				ScenarioEditorUI.instance.GoToPreviousScreen();
			}
			return playerActions.m_back.WasPressed;
		}

		public void NavigateGridLayout(PlayerActions playerActions)
		{
			if (playerActions.m_pageRight.WasPressed)
			{
				m_GridHandler.PageRight();
			}
			if (playerActions.m_pageLeft.WasPressed)
			{
				m_GridHandler.PageLeft();
			}
			if (playerActions.m_accept.WasPressed)
			{
				m_GridHandler.PressLoadOfSelectedButton();
			}
			if (playerActions.m_cogGridButton.WasPressed)
			{
				m_GridHandler.PressContextofSelectedButton();
			}
			if (playerActions.m_upload.WasPressed)
			{
				m_GridHandler.PressDeleteOfSelectedButton();
			}
		}

		public void SelectFirstGridButton()
		{
			m_GridHandler.SelectFirstButton();
		}

		private void OnInputSourceChanged(InputType type)
		{
			switch (type)
			{
			case InputType.Controller:
				SelectFirstGridButton();
				EnableControllerPromptLabels(enable: true);
				break;
			case InputType.Keyboard:
			case InputType.Any:
				EventSystem.current.SetSelectedGameObject(null);
				EnableControllerPromptLabels(enable: false);
				break;
			default:
				throw new ArgumentOutOfRangeException("type", type, null);
			}
		}

		private void EnableControllerPromptLabels(bool enable)
		{
			GameObject[] array = controlPromptLabels;
			foreach (GameObject gameObject in array)
			{
				if (gameObject != null)
				{
					gameObject.SetActive(enable);
				}
			}
		}
	}
}

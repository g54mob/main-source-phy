using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DM;
using Landfall.TABS.Workshop;
using Landfall.TABS_Input;
using ModIO;
using TFBGames;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Landfall.TABS
{
	public class CampaignLoaderUI : Paginator
	{
		[SerializeField]
		private PageCounter m_PageCounter;

		[SerializeField]
		private GameObject m_CampaignCell;

		[SerializeField]
		private GameObject m_WorkshopButton;

		[SerializeField]
		private GameObject m_WorkshopCanvas;

		[SerializeField]
		private Transform m_Grid;

		[SerializeField]
		private Toggle m_tabLandfall;

		[SerializeField]
		private Toggle m_tabLocal;

		[SerializeField]
		private Toggle m_tabCustom;

		[SerializeField]
		private Toggle m_tabBattles;

		[SerializeField]
		private MainMenuUIHandler m_mainMenuUIHandler;

		[SerializeField]
		private TextMeshProUGUI m_permissionsMessage;

		[SerializeField]
		private GameObject m_CampaignTabBar;

		private int m_tabIndex;

		private List<TABSCampaignAsset> m_LandfallCampaigns;

		private TABSCampaignAsset[] m_LoadedCustomCampaigns;

		private TABSCampaignLevelAsset[] m_LoadedCustomLevels;

		private CampaignPlayCellUI m_SelectedCampaign;

		private TABSCampaignAsset m_CustomBattlesCampaign;

		private List<Button> m_campaignButtons;

		private PlayerActions m_playerActions;

		private Toggle[] m_campaignTabs;

		private int m_campaignTabIndex;

		private bool m_selectFirstCampaign;

		private IAccountPermissions m_accountPermissions;

		private List<GameObject> cells;

		private int frameSpawnedButtons;

		private bool selectButton;

		private const int FRAMES_TO_WAIT_BEFORE_SELECTION = 3;

		protected override void Awake()
		{
			base.Awake();
			m_campaignButtons = new List<Button>();
			m_playerActions = PlayerActions.Instance;
			if (m_WorkshopButton != null)
			{
				m_WorkshopButton.GetComponent<Button>().onClick.AddListener(OnWorkshopClicked);
			}
		}

		protected override void Start()
		{
			base.Start();
			m_tabLandfall.onValueChanged.AddListener(delegate(bool isOn)
			{
				if (isOn)
				{
					UpdateFilter(0);
				}
			});
			m_tabLocal.onValueChanged.AddListener(delegate(bool isOn)
			{
				if (isOn)
				{
					UpdateFilter(1);
				}
			});
			m_tabCustom.onValueChanged.AddListener(delegate(bool isOn)
			{
				if (isOn)
				{
					UpdateFilter(2);
				}
			});
			m_tabBattles.onValueChanged.AddListener(delegate(bool isOn)
			{
				if (isOn)
				{
					UpdateFilter(3);
				}
			});
			m_campaignTabs = new Toggle[4] { m_tabLandfall, m_tabLocal, m_tabCustom, m_tabBattles };
			InputService service = ServiceLocator.GetService<InputService>();
			if (service != null)
			{
				service.InputChanged += OnInputSourceChanged;
			}
			m_accountPermissions = ServiceLocator.GetService<IAccountPermissions>();
			ShowPermissionsMessage(visible: false);
		}

		protected override void Update()
		{
			base.Update();
			if (m_mainMenuUIHandler.currentMenuState != MenuState.CampaignSelect)
			{
				return;
			}
			if (base.IsOpen)
			{
				if (PlayerActions.Instance.m_cycleUnitsUp.WasPressed)
				{
					NextPage();
				}
				if (PlayerActions.Instance.m_cycleUnitsDown.WasPressed)
				{
					PreviousPage();
				}
				if (m_playerActions.m_cycleTabs.WasPressed)
				{
					int num = m_campaignTabs.Length;
					int num2 = Mathf.RoundToInt(m_playerActions.m_cycleTabs.Value);
					m_campaignTabIndex = (m_campaignTabIndex + num2) % num;
					m_campaignTabIndex = ((m_campaignTabIndex < 0) ? (num - 1) : m_campaignTabIndex);
					m_campaignTabs[m_campaignTabIndex].isOn = true;
				}
			}
			if (Time.frameCount - frameSpawnedButtons > 3 && selectButton)
			{
				SelectFirstSelectable();
			}
		}

		private void OnInputSourceChanged(InputType type)
		{
			if (m_mainMenuUIHandler.currentMenuState == MenuState.CampaignSelect)
			{
				switch (type)
				{
				case InputType.Controller:
					SelectFirstSelectable();
					break;
				default:
					throw new ArgumentOutOfRangeException("type", type, null);
				case InputType.Keyboard:
				case InputType.Any:
					break;
				}
			}
		}

		private void UpdateFilter(int tabIndex)
		{
			m_tabIndex = tabIndex;
			Populate();
		}

		public void Go()
		{
			OpenPage();
		}

		public override void OpenPage()
		{
			ContentDatabase contentDatabase = ContentDatabase.Instance();
			if (contentDatabase != null && m_LandfallCampaigns == null)
			{
				m_LandfallCampaigns = contentDatabase.GetAllCampaigns().ToList();
			}
			m_LoadedCustomCampaigns = contentDatabase.GetUserCampaigns().ToArray();
			m_LoadedCustomLevels = contentDatabase.GetUserCampaignLevels().ToArray();
			base.OpenPage();
		}

		private void OnWorkshopClicked()
		{
			m_WorkshopCanvas.SetActive(value: true);
		}

		protected override void Clear()
		{
			base.Clear();
			m_campaignButtons.Clear();
			for (int num = m_Grid.childCount - 1; num >= 0; num--)
			{
				UnityEngine.Object.Destroy(m_Grid.GetChild(num).gameObject);
			}
		}

		protected override void Populate(int newPage = 0)
		{
			base.Populate(newPage);
			Clear();
			ShowPermissionsMessage(visible: false);
			switch (m_tabIndex)
			{
			case 0:
				SpawnCampaignCells(m_LandfallCampaigns.ToArray());
				break;
			case 1:
				SpawnCampaignCells(m_LoadedCustomCampaigns);
				break;
			case 2:
				if (!m_accountPermissions.IsSignedIn)
				{
					ShowPermissionsMessage(visible: true, "POPUP_NOT_SIGNED_IN_TO_VIEW");
				}
				else
				{
					SpawnCampaignCells(m_LoadedCustomCampaigns, onlyModCampaigns: true);
				}
				break;
			case 3:
				if (!m_accountPermissions.IsSignedIn)
				{
					ShowPermissionsMessage(visible: true, "POPUP_NOT_SIGNED_IN_TO_VIEW");
				}
				else
				{
					MakeCustomCampaign();
				}
				break;
			}
			frameSpawnedButtons = Time.frameCount;
			selectButton = true;
			SetUpNavigation();
		}

		private void MakeCustomCampaign()
		{
			if (m_LoadedCustomLevels.Length != 0)
			{
				m_CustomBattlesCampaign = ScriptableObject.CreateInstance<TABSCampaignAsset>();
				m_CustomBattlesCampaign.SetData("LABEL_CUSTOM_BATTLES_CAMPAIGN_TITLE", m_LoadedCustomLevels);
				totalPages = Mathf.CeilToInt((float)m_LoadedCustomLevels.Length / 12f);
				int num = m_LoadedCustomLevels.Length - 12 * currentPage;
				if ((bool)m_PageCounter)
				{
					m_PageCounter.Set(currentPage + 1, totalPages);
				}
				for (int i = 0; i < Mathf.Min(12, num); i++)
				{
					int num2 = m_LoadedCustomLevels.Length - (num - i);
					SpawnLevelCell(num2, m_LoadedCustomLevels[num2], unlocked: true, newLevel: false);
				}
			}
		}

		private void SpawnCampaignCells(TABSCampaignAsset[] campaigns, bool onlyModCampaigns = false)
		{
			totalPages = Mathf.CeilToInt((float)campaigns.Length / 12f);
			int num = campaigns.Length - 12 * currentPage;
			if (m_PageCounter != null)
			{
				m_PageCounter.Set(currentPage + 1, totalPages);
			}
			for (int i = 0; i < Mathf.Min(12, num); i++)
			{
				int num2 = campaigns.Length - (num - i);
				if (campaigns[num2].IsModCampaign && onlyModCampaigns)
				{
					SpawnCampaignCell(campaigns[num2]);
				}
				else if (!campaigns[num2].IsModCampaign && !onlyModCampaigns)
				{
					SpawnCampaignCell(campaigns[num2]);
				}
			}
		}

		private void SpawnCampaignCell(TABSCampaignAsset campaign)
		{
			GameObject cell = UnityEngine.Object.Instantiate(m_CampaignCell, m_Grid, worldPositionStays: false);
			CampaignPlayCellUI cellUI = cell.FetchComponent<CampaignPlayCellUI>();
			MapGrid mapGrid = cell.GetComponent<MapGrid>();
			campaign.Entity.GetSpriteIconAsync(delegate(Sprite sprite)
			{
				if (mapGrid != null)
				{
					mapGrid.Setup(sprite, campaign.Entity.Name, !campaign.IsCustomCampaign, campaign.CampaignInfo.Description);
				}
			});
			mapGrid.CheckCamapaignWinStreak(campaign.Entity.GUID);
			cellUI.Init(campaign, delegate
			{
				OnCampaignClicked(cellUI);
			});
			cell.transform.localScale = Vector3.one;
			cell.SetActive(value: true);
			m_campaignButtons.Add(cell.GetComponent<Button>());
			if (campaign.IsModCampaign)
			{
				ModManager.GetModLogo(campaign.ModProfile, LogoSize.Thumbnail_320x180, delegate(Texture2D texture2D)
				{
					cell.GetComponent<Image>().sprite = Sprite.Create(texture2D, new Rect(0f, 0f, texture2D.width, texture2D.height), Vector2.zero);
				}, delegate(WebRequestError error)
				{
					Debug.LogError("Could not get mod image: " + error.errorMessage);
				});
			}
			else
			{
				if (!campaign.IsCustomCampaign)
				{
					return;
				}
				FileInfo fileInfo = new FileInfo(Path.Combine(new FileInfo(campaign.FilePath).Directory.FullName, "Picture.png"));
				string path = fileInfo.FullName;
				FileIOWrapper fileIO = ServiceLocator.GetService<FileIOWrapper>();
				try
				{
					fileIO.FileExists(path, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(bool exists)
					{
						if (exists)
						{
							fileIO.ReadAllBytes(path, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(byte[] bytes, Exception exception)
							{
								Texture2D texture2D = new Texture2D(2, 2);
								if (exception == null && bytes != null)
								{
									texture2D.LoadImage(bytes);
								}
								cell.GetComponent<Image>().sprite = Sprite.Create(texture2D, new Rect(0f, 0f, texture2D.width, texture2D.height), Vector2.zero);
							});
						}
					});
				}
				catch (Exception ex)
				{
					Debug.LogError("Could not read campaign cell data at path " + path + " with error: " + ex.Message);
				}
			}
		}

		private void OnCampaignClicked(CampaignPlayCellUI cellUI)
		{
			m_SelectedCampaign = cellUI;
			LoadCampaign();
		}

		private void LoadCampaign()
		{
			MainMenuStateHandler.Instance.OnCampaignSelected(m_SelectedCampaign.CurrentCampaign);
		}

		private void SetUpNavigation()
		{
			UIHelpers.CreateAutomaticNavigation(m_Grid.GetSelectableChildren());
		}

		public void SelectFirstSelectable()
		{
			if (m_campaignButtons.Count != 0)
			{
				Button button = m_campaignButtons[0];
				LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)button.transform.parent);
				if (m_playerActions.InputType == InputType.Controller)
				{
					button.Select();
				}
				m_selectFirstCampaign = false;
				selectButton = false;
			}
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			InputService service = ServiceLocator.GetService<InputService>();
			if (service != null)
			{
				service.InputChanged -= OnInputSourceChanged;
			}
		}

		private void ShowPermissionsMessage(bool visible, string newMessage = null)
		{
			if (m_permissionsMessage.gameObject.activeSelf != visible)
			{
				m_permissionsMessage.gameObject.SetActive(visible);
			}
			if (!string.IsNullOrEmpty(newMessage))
			{
				m_permissionsMessage.text = newMessage;
			}
		}

		private void SpawnLevelCell(int index, TABSCampaignLevelAsset reference, bool unlocked, bool newLevel)
		{
			GameObject cell = UnityEngine.Object.Instantiate(m_CampaignCell, m_Grid, worldPositionStays: false);
			reference.Entity.GetSpriteIconAsync(delegate(Sprite sprite)
			{
				if (cell != null)
				{
					cell.GetComponent<MapGrid>().Setup(sprite, reference.Entity.Name, !reference.IsCustomCampaignLevel, reference.CampaignInfo.Description);
				}
			});
			CampaignSelectedCellUI cellUI = cell.FetchComponent<CampaignSelectedCellUI>();
			cellUI.Init(index, reference, delegate
			{
				OnLevelClicked(cellUI);
			}, unlocked, newLevel);
			cell.transform.localScale = Vector3.one;
			cell.SetActive(value: true);
			if (reference.IsModIOLevel)
			{
				ModManager.GetModLogo(reference.ModProfile, LogoSize.Thumbnail_320x180, delegate(Texture2D texture2D)
				{
					cell.GetComponent<Image>().sprite = Sprite.Create(texture2D, new Rect(0f, 0f, texture2D.width, texture2D.height), Vector2.zero);
				}, delegate(WebRequestError error)
				{
					Debug.LogError("Could not get mod image: " + error.errorMessage);
				});
			}
			else
			{
				if (!reference.IsCustomCampaignLevel)
				{
					return;
				}
				FileInfo fileInfo = new FileInfo(Path.Combine(new FileInfo(reference.FilePath).Directory.FullName, "Picture.png"));
				string path = fileInfo.FullName;
				FileIOWrapper fileIO = ServiceLocator.GetService<FileIOWrapper>();
				try
				{
					fileIO.FileExists(path, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(bool exists)
					{
						if (exists)
						{
							fileIO.ReadAllBytes(path, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(byte[] bytes, Exception exception)
							{
								Texture2D texture2D = new Texture2D(2, 2);
								if (exception == null && bytes != null)
								{
									texture2D.LoadImage(bytes);
								}
								cell.GetComponent<Image>().sprite = Sprite.Create(texture2D, new Rect(0f, 0f, texture2D.width, texture2D.height), Vector2.zero);
							});
						}
					});
				}
				catch (Exception ex)
				{
					Debug.LogError("Could not read level cell data at path " + path + " with error: " + ex.Message);
				}
			}
		}

		private void OnLevelClicked(CampaignSelectedCellUI cellUI)
		{
			Load(cellUI);
		}

		private void Load(CampaignSelectedCellUI selectedCellUI)
		{
			CampaignPlayerDataHolder.StartedPlayingNewCampaign(m_CustomBattlesCampaign, selectedCellUI.CampaignIndex, autoLoadNextLevel: false, saveGameWhenCompleteLevel: false);
			Debug.Log("Loading campaign Level: " + selectedCellUI.CampaignLevelReference.Entity.Name);
			TABSSceneManager.LoadCampaign();
		}
	}
}

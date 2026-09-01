using System;
using System.Collections.Generic;
using Landfall.TABS.Workshop;
using Landfall.TABS_Input;
using TFBGames;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Landfall.TABS
{
	public class MainMenuSelectedCampaignUI : Paginator
	{
		[SerializeField]
		private TextMeshProUGUI m_CampaignNameText;

		[SerializeField]
		private GameObject m_CampaignLevelCell;

		[SerializeField]
		private Transform m_Grid;

		[SerializeField]
		private CampaignSelectorPopUpUIHandler m_PopUpHandler;

		[SerializeField]
		private MainMenuUIHandler m_mainMenuUIHandler;

		private TABSCampaignAsset m_SelectedCampaign;

		private List<TABSCampaignLevelAsset> m_CorruptMaps;

		private List<Button> m_CampaignLevelButtons = new List<Button>();

		private List<GameObject> spawnedObjects = new List<GameObject>();

		public PageCounter m_PageCounter;

		protected override void Update()
		{
			base.Update();
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
			}
		}

		private void OnInputSourceChanged(InputType type)
		{
			if (m_mainMenuUIHandler.currentMenuState == MenuState.CampaignLevels)
			{
				switch (type)
				{
				case InputType.Controller:
					MakeSelection();
					break;
				default:
					throw new ArgumentOutOfRangeException("type", type, null);
				case InputType.Keyboard:
				case InputType.Any:
					break;
				}
			}
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			InputService service = ServiceLocator.GetService<InputService>();
			if (service != null)
			{
				service.InputChanged += OnInputSourceChanged;
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

		public void Go()
		{
			OpenPage();
		}

		public override void OpenPage()
		{
			m_CorruptMaps = new List<TABSCampaignLevelAsset>();
			m_SelectedCampaign = MainMenuStateHandler.Instance.SelectedCampaign;
			if (m_SelectedCampaign == null)
			{
				Debug.LogError("Selected Campaign is null!==!!=!==!");
			}
			m_CampaignNameText.text = m_SelectedCampaign.Entity.Name;
			base.OpenPage();
		}

		protected override void Clear()
		{
			base.Clear();
			m_Grid.DestroyAllChildren();
			m_CampaignLevelButtons.Clear();
			for (int i = 0; i < spawnedObjects.Count; i++)
			{
				UnityEngine.Object.Destroy(spawnedObjects[i]);
			}
			spawnedObjects.Clear();
		}

		protected override void Populate(int newPage = 0)
		{
			base.Populate(newPage);
			bool flag = false;
			TABSCampaignLevelAsset[] levelsInCampaign = m_SelectedCampaign.LevelsInCampaign;
			totalPages = Mathf.CeilToInt((float)levelsInCampaign.Length / 12f);
			int num = levelsInCampaign.Length - 12 * currentPage;
			if (m_PageCounter != null)
			{
				m_PageCounter.Set(currentPage + 1, totalPages);
			}
			Clear();
			for (int i = 0; i < Mathf.Min(12, num); i++)
			{
				int num2 = levelsInCampaign.Length - (num - i);
				if (levelsInCampaign[num2] == null)
				{
					m_CorruptMaps.Add(levelsInCampaign[num2]);
					continue;
				}
				bool flag2 = ServiceLocator.GetService<ISaveLoaderService>().HasBeatenLevel(levelsInCampaign[num2].Entity.GUID, m_SelectedCampaign.Entity.GUID);
				bool flag3 = ServiceLocator.GetService<DebugService>() != null && ServiceLocator.GetService<DebugService>().HasUnlockedProgress;
				bool flag4 = m_Grid.childCount == 0 || num2 == 0;
				bool flag5 = flag2 || flag4 || flag || flag3;
				bool newLevel = !flag3 && !flag2 && flag5;
				CampaignSelectedCellUI campaignSelectedCellUI = SpawnLevelCell(num2, levelsInCampaign[num2], flag5, newLevel);
				m_CampaignLevelButtons.Add(campaignSelectedCellUI.GetComponent<Button>());
				spawnedObjects.Add(campaignSelectedCellUI.gameObject);
				flag = flag2;
			}
			if (m_CorruptMaps.Count > 0)
			{
				string empty = string.Empty;
				foreach (TABSCampaignLevelAsset corruptMap in m_CorruptMaps)
				{
					_ = corruptMap;
				}
				ServiceLocator.GetService<ModalPanel>().PopUp("POPUP_INVALIDLEVELS", empty, "\n");
			}
			else if (PlayerActions.Instance.InputType == InputType.Controller)
			{
				MakeSelection();
			}
		}

		private CampaignSelectedCellUI SpawnLevelCell(int index, TABSCampaignLevelAsset reference, bool unlocked, bool newLevel)
		{
			GameObject cell = UnityEngine.Object.Instantiate(m_CampaignLevelCell, m_Grid, worldPositionStays: false);
			MapGrid mapGrid = cell.GetComponent<MapGrid>();
			reference.Entity.GetSpriteIconAsync(delegate(Sprite sprite)
			{
				if (mapGrid != null)
				{
					mapGrid.Setup(sprite, reference.Entity.Name, !reference.IsCustomCampaignLevel, reference.CampaignInfo.Description);
				}
			});
			mapGrid.CheckLevelWinStreak(reference.Entity.GUID, CampaignPlayerDataHolder.GetCurrentCampaignID);
			CampaignSelectedCellUI cellUI = cell.FetchComponent<CampaignSelectedCellUI>();
			cellUI.Init(index, reference, delegate
			{
				OnLevelClicked(cellUI);
			}, unlocked, newLevel);
			cell.transform.localScale = Vector3.one;
			cell.SetActive(value: true);
			if (reference.IsModIOLevel || reference.IsCustomCampaignLevel)
			{
				CampaignHandler.GetBattleSprite(reference, delegate(Sprite sprite)
				{
					if (cell != null)
					{
						cell.GetComponent<Image>().sprite = sprite;
					}
				});
			}
			else
			{
				reference.Entity.GetSpriteIconAsync(delegate(Sprite sprite)
				{
					if (sprite != null && cell != null)
					{
						cell.GetComponent<Image>().sprite = sprite;
					}
				});
			}
			return cellUI;
		}

		private void OnLevelClicked(CampaignSelectedCellUI cellUI)
		{
			Load(cellUI);
		}

		private void Load(CampaignSelectedCellUI selectedCellUI)
		{
			CampaignPlayerDataHolder.StartedPlayingNewCampaign(m_SelectedCampaign, selectedCellUI.CampaignIndex);
			Debug.Log("Loading campaign Level: " + selectedCellUI.CampaignLevelReference.Entity.Name);
			TABSSceneManager.LoadCampaign();
		}

		private void MakeSelection()
		{
			if (m_CampaignLevelButtons != null && m_CampaignLevelButtons.Count > 0)
			{
				m_CampaignLevelButtons[0].Select();
				UIHelpers.CreateAutomaticNavigation(m_CampaignLevelButtons.ToArray());
			}
		}
	}
}

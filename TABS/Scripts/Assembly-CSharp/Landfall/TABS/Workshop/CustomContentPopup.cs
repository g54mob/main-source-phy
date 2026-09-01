using System.Collections;
using DM;
using GamepadUI.StateManager.Core;
using Landfall.TABS_Input;
using TMPro;
using UIStateManager;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Landfall.TABS.Workshop
{
	public class CustomContentPopup : MonoBehaviour
	{
		public InterfaceStateManager interfaceManager;

		public CustomContentSideBar sideBar;

		public UnitCreatorFactionBrowser browser;

		public GameObject renameUnitPanel;

		public GameObject renameFactionPanel;

		public GameObject renameBattlePanel;

		public GameObject renameCampaignPanel;

		[SerializeField]
		private UnitButtonBase unitButton;

		[SerializeField]
		private TMP_InputField unitRenameInputField;

		[SerializeField]
		private Image factionIcon;

		[SerializeField]
		private TMP_InputField factionRenameInputField;

		[SerializeField]
		private Image battleIcon;

		[SerializeField]
		private TMP_InputField battleRenameInputField;

		[SerializeField]
		private Image campaignIcon;

		[SerializeField]
		private TMP_InputField campaignRenameInputField;

		public bool isOpen;

		private CanvasGroup canvasGroup;

		private GameObject lastOpenedPannel;

		private GameObject panelParent;

		private float currentAlpha;

		private float targetAlpha;

		private UnitBlueprint currentlyRenaming;

		private Faction currentlyRenamingFaction;

		private TABSCampaignLevelAsset currentlyRenamingBattle;

		private TABSCampaignAsset currentlyRenamingCampaign;

		private void Awake()
		{
			panelParent = base.transform.GetChild(0).gameObject;
			canvasGroup = GetComponent<CanvasGroup>();
		}

		public void Hide()
		{
			isOpen = false;
			UIComponentMainMenu component = browser.GetComponent<UIComponentMainMenu>();
			interfaceManager.OpenUIComponent(component);
			component.OpenSubMenu(sideBar.GetComponent<UISubMenu>());
			currentlyRenaming = null;
			targetAlpha = 0f;
			canvasGroup.blocksRaycasts = false;
			StartCoroutine(CloseCorutine());
		}

		private void Show(GameObject newPanel)
		{
			isOpen = true;
			interfaceManager.OpenUIComponent(GetComponent<UIComponentMainMenu>(), backgroundPrevious: true);
			if (lastOpenedPannel != null)
			{
				lastOpenedPannel.SetActive(value: false);
			}
			lastOpenedPannel = newPanel;
			newPanel.SetActive(value: true);
			panelParent.SetActive(value: true);
			targetAlpha = 1f;
			canvasGroup.blocksRaycasts = true;
		}

		private IEnumerator CloseCorutine()
		{
			yield return new WaitForSeconds(0.25f);
			panelParent.SetActive(value: false);
		}

		private void Update()
		{
			currentAlpha = Mathf.MoveTowards(currentAlpha, targetAlpha, 10f * Time.unscaledDeltaTime);
			canvasGroup.alpha = currentAlpha;
			if (PlayerActions.Instance.m_back.WasPressed && isOpen)
			{
				Hide();
			}
		}

		private void SetSelection(GameObject selection)
		{
			EventSystem.current.SetSelectedGameObject(selection);
		}

		public void RenameUnit(UnitBlueprint unit)
		{
			Show(renameUnitPanel);
			SetupRename(unit);
		}

		private void SetupRename(UnitBlueprint unit)
		{
			unitButton.Setup(unit);
			unitRenameInputField.SetTextWithoutNotify(unit.Entity.Name);
			currentlyRenaming = unit;
			SetSelection(unitRenameInputField.gameObject);
		}

		public void FinishRename()
		{
			DatabaseID gUID = currentlyRenaming.Entity.GUID;
			currentlyRenaming.Entity.Name = unitRenameInputField.text;
			CustomUnitHandler.SaveUnit(currentlyRenaming, gUID);
			browser.QuickRefresh(WorkshopContentType.Unit);
			sideBar.ShowUnit(ContentDatabase.Instance().GetUnitBlueprint(gUID), playAnimation: false);
			Invoke("Hide", 0.05f);
		}

		public void RenameFaction(Faction faction)
		{
			Show(renameFactionPanel);
			SetupFaction(faction);
		}

		private void SetupFaction(Faction faction)
		{
			faction.Entity.GetSpriteIconAsync(delegate(Sprite sprite)
			{
				if (sprite != null && factionIcon != null)
				{
					factionIcon.sprite = sprite;
				}
			});
			factionRenameInputField.text = faction.Entity.Name;
			currentlyRenamingFaction = faction;
			SetSelection(factionRenameInputField.gameObject);
		}

		public void FinishRenameFaction()
		{
			DatabaseID gUID = currentlyRenamingFaction.Entity.GUID;
			currentlyRenamingFaction.Entity.Name = factionRenameInputField.text;
			CustomFactionHandler.SaveFaction(currentlyRenamingFaction, gUID);
			browser.QuickRefresh(WorkshopContentType.Faction);
			sideBar.ShowFaction(ContentDatabase.Instance().GetFaction(gUID), playAnimation: false);
			Invoke("Hide", 0.05f);
		}

		public void RenameBattle(TABSCampaignLevelAsset battle)
		{
			Show(renameBattlePanel);
			SetupBattle(battle);
		}

		private void SetupBattle(TABSCampaignLevelAsset battle)
		{
			CampaignHandler.GetBattleSprite(battle, delegate(Sprite sprite)
			{
				if (battleIcon != null)
				{
					battleIcon.sprite = sprite;
				}
			});
			battleRenameInputField.text = battle.Entity.Name;
			currentlyRenamingBattle = battle;
			SetSelection(battleRenameInputField.gameObject);
		}

		public void FinishRenameBattle()
		{
			DatabaseID gUID = currentlyRenamingBattle.Entity.GUID;
			currentlyRenamingBattle.Entity.Name = battleRenameInputField.text;
			CampaignHandler.OverwriteLayout(currentlyRenamingBattle, null);
			browser.QuickRefresh(WorkshopContentType.Battle);
			sideBar.ShowBattle(ContentDatabase.Instance().GetCampaignLevel(gUID), playAnimation: false);
			Invoke("Hide", 0.05f);
		}

		public void RenameCampaign(TABSCampaignAsset campaign)
		{
			Show(renameCampaignPanel);
			SetupCampaign(campaign);
		}

		private void SetupCampaign(TABSCampaignAsset campaign)
		{
			campaign.Entity.GetSpriteIconAsync(delegate(Sprite sprite)
			{
				if (sprite != null && campaignIcon != null)
				{
					campaignIcon.sprite = sprite;
				}
			});
			CampaignHandler.GetCampaignSprite(campaign, delegate(Sprite sprite)
			{
				campaignIcon.sprite = sprite;
			});
			campaignRenameInputField.text = campaign.Entity.Name;
			currentlyRenamingCampaign = campaign;
			SetSelection(campaignRenameInputField.gameObject);
		}

		public void FinishRenameCampaign()
		{
			DatabaseID gUID = currentlyRenamingCampaign.Entity.GUID;
			currentlyRenamingCampaign.Entity.Name = campaignRenameInputField.text;
			CampaignHandler.OverwriteCampaign(currentlyRenamingCampaign, null);
			browser.QuickRefresh(WorkshopContentType.Campaign);
			sideBar.ShowCampaign(ContentDatabase.Instance().GetCampaign(gUID), playAnimation: false);
			Invoke("Hide", 0.05f);
		}
	}
}

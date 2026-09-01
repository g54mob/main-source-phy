using System;
using System.Linq;
using InternalModding.Loading;
using InternalModding.Mods;
using Localisation;
using Steamworks;
using UnityEngine;
using UnityEngine.UI;

namespace InternalModding.UI
{
	public class ModMismatchEntry : MonoBehaviour
	{
		public Text NameText;

		public Button InstallButton;

		public Button EnableButton;

		public Button DisableButton;

		public GameObject TickObject;

		public GameObject CrossObject;

		public RawImage Icon;

		public Color WarningColor;

		private ModList.Mod mod;

		private ModContainer container;

		public void Awake()
		{
			InstallButton.onClick.AddListener(OnInstallClicked);
			EnableButton.onClick.AddListener(OnEnableClicked);
			DisableButton.onClick.AddListener(OnDisableClicked);
		}

		private void OnInstallClicked()
		{
			string text = "https://steamcommunity.com/sharedfiles/filedetails/?id=" + mod.WorkshopId;
			if (SteamManager.Initialized && !Application.isEditor)
			{
				SteamFriends.ActivateGameOverlayToWebPage(text);
			}
			else
			{
				Application.OpenURL(text);
			}
		}

		private void OnEnableClicked()
		{
			if (container != null)
			{
				ModStatus.EnableMod(container);
				UpdateSuccessState();
			}
		}

		private void OnDisableClicked()
		{
			if (container != null)
			{
				if (!ModStatus.DisableMod(container))
				{
					SingleInstanceFindOnly<GenericUIPopup>.Instance.Show(LocalisationManager.GetTranslation(3570), 10f, GenericUIPopup.PopupPosition.Bottom);
					DisableButton.GetComponent<Image>().color = WarningColor;
				}
				UpdateSuccessState();
			}
		}

		public void Init(ModList.Mod mod)
		{
			this.mod = mod;
			container = ModIds.GetModById(mod.Id, true);
			bool flag = container != null;
			string translation;
			switch (mod.Mismatch)
			{
			case ModList.MismatchType.MissingLocally:
				translation = LocalisationManager.GetTranslation(3571);
				break;
			case ModList.MismatchType.MissingOnServer:
				translation = LocalisationManager.GetTranslation(3572);
				break;
			case ModList.MismatchType.VersionDoesntMatch:
				translation = LocalisationManager.GetTranslation(3573);
				break;
			default:
				throw new Exception("Unknown mismatch type!");
			}
			InstallButton.gameObject.SetActive(mod.Mismatch == ModList.MismatchType.MissingLocally && !flag && mod.Workshop);
			EnableButton.gameObject.SetActive(mod.Mismatch == ModList.MismatchType.MissingLocally && flag);
			DisableButton.gameObject.SetActive(mod.Mismatch == ModList.MismatchType.MissingOnServer);
			NameText.text = ReferenceMaster.CamelCaseToSpaces(mod.Name).ToUpper() + ": " + translation;
			if (!mod.Workshop)
			{
				InstallButton.GetComponent<Image>().color = WarningColor;
			}
			if (container != null && container.IsActive && !container.IsEnabled)
			{
				DisableButton.GetComponent<Image>().color = WarningColor;
			}
			if (container != null)
			{
				Icon.texture = (Texture2D)container.Info.Icon;
			}
			else
			{
				Icon.texture = mod.Icon;
			}
			UpdateSuccessState();
		}

		public bool GetSuccessState()
		{
			switch (mod.Mismatch)
			{
			case ModList.MismatchType.MissingLocally:
				return ModManager.Mods.Any((ModContainer m) => m.Info.Id == mod.Id && m.IsEnabled);
			case ModList.MismatchType.MissingOnServer:
				return !container.IsEnabled && !container.IsActive;
			case ModList.MismatchType.VersionDoesntMatch:
				return false;
			default:
				throw new Exception("Unknown mismatch type!");
			}
		}

		public void UpdateSuccessState(ModContainer newlyLoadedMod = null)
		{
			if (mod.Mismatch == ModList.MismatchType.MissingLocally && newlyLoadedMod != null && newlyLoadedMod.Info.Id == mod.Id && !newlyLoadedMod.IsEnabled)
			{
				ModStatus.EnableMod(newlyLoadedMod);
			}
			bool successState = GetSuccessState();
			TickObject.SetActive(successState);
			CrossObject.SetActive(!successState);
			if (successState)
			{
				InstallButton.gameObject.SetActive(false);
				EnableButton.gameObject.SetActive(false);
				DisableButton.gameObject.SetActive(false);
				NameText.text = mod.Name;
				ModMismatchUI.UpdateStateUI();
			}
		}
	}
}

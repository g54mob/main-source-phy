using Landfall.TABS;
using Landfall.TABS.Workshop;
using Landfall.TABS_Input;
using TFBGames;
using UIStateManager;
using UnityEngine;
using UnityEngine.UI;

public class WorkshopCustomContentLoadingScreen : UIComponentMainMenu
{
	[SerializeField]
	protected InterfaceStateManager interfaceStateManager;

	[SerializeField]
	protected UIComponentMainMenu workshopUIComponentMainMenu;

	[SerializeField]
	protected UIComponentMainMenu mainMenuButtonsComponents;

	[SerializeField]
	protected LevelSelection levelSelector;

	[SerializeField]
	protected Button backButton;

	private PlayerActions playerActions;

	private IAccountPermissions accountPermissions;

	private PermissionsHelper permissionsHelper;

	private IUserProfileUI userProfileUI;

	private IPlayerPrefsPlatform m_PlayerPrefs;

	private ModalPanel modalPanel;

	private bool didClickBackButton;

	private CustomContentLoaderModIO customContentLoader;

	private static bool didCheckPermissionToLoadMods;

	protected override void Awake()
	{
		base.Awake();
		permissionsHelper = ServiceLocator.GetService<PermissionsHelper>();
		accountPermissions = ServiceLocator.GetService<IAccountPermissions>();
		userProfileUI = ServiceLocator.GetService<IUserProfileUI>();
		modalPanel = ServiceLocator.GetService<ModalPanel>();
		m_PlayerPrefs = ServiceLocator.GetService<IPlayerPrefsPlatform>();
		playerActions = PlayerActions.Instance;
		customContentLoader = ServiceLocator.GetService<CustomContentLoaderModIO>();
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		backButton.onClick.AddListener(OnBackButtonClicked);
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		if (backButton != null)
		{
			backButton.onClick.RemoveListener(OnBackButtonClicked);
		}
	}

	protected override void Update()
	{
		if (playerActions != null && playerActions.m_back.WasPressed)
		{
			OnBackButtonClicked();
		}
	}

	public void OnBackButtonClicked()
	{
		didClickBackButton = true;
		LoadUIComponentMainMenu(mainMenuButtonsComponents);
	}

	public void CheckUserProfileAndLoadWorkShopUI()
	{
		didClickBackButton = false;
		HideUserProfile();
		permissionsHelper.CheckWorkshopPermissions(DidClickBackButton, delegate(PermissionsHelperResult result)
		{
			switch (result)
			{
			case PermissionsHelperResult.Cancelled:
				ShowUserProfile();
				break;
			case PermissionsHelperResult.Failed:
				ShowUserProfile();
				LoadUIComponentMainMenu(mainMenuButtonsComponents);
				break;
			case PermissionsHelperResult.Succeeded:
				LoadUIComponentMainMenu(workshopUIComponentMainMenu);
				break;
			}
		});
	}

	public void CheckUserProfileAndLoadCustomContentScene()
	{
		if (!accountPermissions.IsSignedIn)
		{
			HideUserProfile();
			ServiceLocator.GetService<ModalPanel>().PopUp("POPUP_NOT_SIGNED_IN_TO_CREATE", ShowUserProfile);
			LoadUIComponentMainMenu(mainMenuButtonsComponents);
			return;
		}
		if (m_PlayerPrefs.GetInt("ALLOW_UGC") != 0)
		{
			permissionsHelper.CanViewDownloadTabs = false;
			TABSSceneManager.LoadCustomContentPage();
			return;
		}
		accountPermissions.CanViewAndDownloadUgcAsync(showPopup: false, "POPUP_NOT_ALLOWED_TO_VIEW_UGC", delegate(bool permitted)
		{
			permissionsHelper.CanViewDownloadTabs = permitted && customContentLoader.DidGivePermissionToLoadMods;
			if (!permitted || didCheckPermissionToLoadMods)
			{
				TABSSceneManager.LoadCustomContentPage();
			}
			else
			{
				didCheckPermissionToLoadMods = true;
				customContentLoader.CheckPermissionToLoadMods(refresh: true, delegate(bool didGivePermissionToLoadMods)
				{
					permissionsHelper.CanViewDownloadTabs = didGivePermissionToLoadMods;
					TABSSceneManager.LoadCustomContentPage();
				});
			}
		});
	}

	public void CheckUserProfileAndLoadCampaignLevelSelector()
	{
		if (levelSelector == null)
		{
			Debug.LogError("levelSelector is null");
			return;
		}
		if (m_PlayerPrefs.GetInt("ALLOW_UGC") != 0)
		{
			permissionsHelper.CanViewDownloadTabs = false;
			interfaceStateManager.OpenUIComponent(levelSelector);
			levelSelector.SetForCampaign();
			return;
		}
		accountPermissions.CanViewAndDownloadUgcAsync(showPopup: false, "POPUP_NOT_ALLOWED_TO_VIEW_UGC", delegate(bool permitted)
		{
			permissionsHelper.CanViewDownloadTabs = permitted && customContentLoader.DidGivePermissionToLoadMods;
			interfaceStateManager.OpenUIComponent(levelSelector);
			levelSelector.SetForCampaign();
		});
	}

	private void LoadUIComponentMainMenu(UIComponentMainMenu uiComponent)
	{
		if (interfaceStateManager != null || uiComponent != null)
		{
			interfaceStateManager.OpenUIComponent(uiComponent);
		}
	}

	private void ShowUserProfile()
	{
		if (userProfileUI != null)
		{
			userProfileUI.Show(userProfileUI.CanChangeProfile);
		}
	}

	private void HideUserProfile()
	{
		if (userProfileUI != null)
		{
			userProfileUI.Hide();
		}
	}

	private bool DidClickBackButton()
	{
		return didClickBackButton;
	}
}

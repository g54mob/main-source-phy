using System.Collections.Generic;
using BitCode.Networking;
using GamepadUI.StateManager.Core;
using TFBGames;
using UnityEngine;

public class UIComponentMainMenu : UIComponent
{
	[SerializeField]
	[Tooltip("Optional. If this is null, it will fall back to using UIComponent's Canvas toggle logic.")]
	protected CodeAnimation stateCodeAnimation;

	protected bool allowAnimation = true;

	protected UISubMenu currentSubMenu;

	protected SocialProfileService socialService;

	private Stack<UISubMenu> menuNavigationStack;

	protected int StackCount
	{
		get
		{
			if (menuNavigationStack == null)
			{
				return 0;
			}
			return menuNavigationStack.Count;
		}
	}

	protected override void Awake()
	{
		base.Awake();
		if (stateCodeAnimation == null)
		{
			stateCodeAnimation = GetComponent<CodeAnimation>();
		}
		socialService = ServiceLocator.GetService<SocialProfileService>();
		if (socialService != null)
		{
			socialService.ReceivedInvitation += OnReceivedInvitation;
		}
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		if (socialService != null)
		{
			socialService.ReceivedInvitation -= OnReceivedInvitation;
		}
	}

	protected virtual void OnReceivedInvitation(IGameInvitation invite)
	{
		if (base.IsActive)
		{
			base.Close();
		}
	}

	protected override void OnOpen()
	{
		base.OnOpen();
		if (stateCodeAnimation != null && allowAnimation)
		{
			stateCodeAnimation.PlayIn();
		}
	}

	protected override void OnClose()
	{
		base.OnClose();
		if (currentSubMenu != null)
		{
			currentSubMenu.OnParentClose();
			SubscribeToSubMenuEvents(currentSubMenu, subscribe: false);
		}
		if (stateCodeAnimation != null && allowAnimation)
		{
			stateCodeAnimation.PlayOut();
		}
	}

	public virtual void OpenSubMenu(UISubMenu menu)
	{
		if (menuNavigationStack == null)
		{
			menuNavigationStack = new Stack<UISubMenu>();
		}
		if (!menu.gameObject.activeSelf)
		{
			menu.gameObject.SetActive(value: true);
		}
		if (currentSubMenu == null)
		{
			currentSubMenu = menu;
			SubscribeToSubMenuEvents(currentSubMenu, subscribe: true);
			currentSubMenu.Open();
		}
		else if (currentSubMenu != menu)
		{
			SubscribeToSubMenuEvents(currentSubMenu, subscribe: false);
			currentSubMenu.Close();
			currentSubMenu = menu;
			SubscribeToSubMenuEvents(currentSubMenu, subscribe: true);
			currentSubMenu.Open();
		}
		else
		{
			SubscribeToSubMenuEvents(currentSubMenu, subscribe: true);
			currentSubMenu.OnGainedFocus();
		}
		if (!menuNavigationStack.Contains(menu))
		{
			menuNavigationStack.Push(menu);
		}
	}

	public virtual void CloseSubMenu(UISubMenu menu, bool removeFromStack = false)
	{
		if (currentSubMenu != null && currentSubMenu == menu)
		{
			SubscribeToSubMenuEvents(currentSubMenu, subscribe: false);
			currentSubMenu.Close();
			currentSubMenu = null;
			if (removeFromStack && menuNavigationStack.Peek() == menu)
			{
				menuNavigationStack.Pop();
			}
		}
	}

	public bool OnBackPressed(int stackMinCount = 1)
	{
		bool result = true;
		if (currentSubMenu == null)
		{
			return true;
		}
		if (menuNavigationStack == null || menuNavigationStack.Count <= stackMinCount)
		{
			return true;
		}
		if (currentSubMenu.OverrideBackAction)
		{
			currentSubMenu.OnBackPressed();
		}
		else
		{
			CloseSubMenu(currentSubMenu);
			menuNavigationStack.Pop();
			if (menuNavigationStack.Count <= 0)
			{
				EnableNavigation();
				return true;
			}
			OpenSubMenu(menuNavigationStack.Peek());
			result = false;
		}
		return result;
	}

	public void ClearStack()
	{
		menuNavigationStack.Clear();
	}

	public void OnBackPressedVoid()
	{
		OnBackPressed();
	}

	protected virtual void CloseSubMenuAndClearStack()
	{
		CloseSubMenu(currentSubMenu);
		if (menuNavigationStack != null)
		{
			menuNavigationStack.Clear();
		}
	}

	protected virtual void OnSubMenuPressedBackButton(UISubMenu menu)
	{
	}

	protected virtual void SubscribeToSubMenuEvents(UISubMenu menu, bool subscribe)
	{
		if (!(menu == null))
		{
			menu.PressedBackButton -= OnSubMenuPressedBackButton;
			if (subscribe)
			{
				menu.PressedBackButton += OnSubMenuPressedBackButton;
			}
		}
	}

	public void ClearSubmenuStack()
	{
		menuNavigationStack?.Clear();
	}
}

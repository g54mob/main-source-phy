using System;
using BitCode.Extensions;
using Landfall.TABS_Input;
using TFBGames;
using UIStateManager;
using UnityEngine;
using UnityEngine.Events;

namespace GamepadUI.StateManager.Core
{
	public class UISubMenu : MonoBehaviour, IMenuWithEvents
	{
		[SerializeField]
		[Tooltip("Optional title used for display.")]
		protected string submenuTitle = "";

		[SerializeField]
		private bool overrideBackAction;

		[SerializeField]
		[Tooltip("If we are returning from a pop up via a button press with gamepad, this flag can be set before the UINavigation group is opened, this prevents the auto select on open")]
		protected bool blockAutoSelectOnUINavEnable;

		[SerializeField]
		private UnityEvent backAction;

		private CodeAnimation transitionAnimation;

		[Header("UI Navigation")]
		[SerializeField]
		[Tooltip("Disable other navigation groups when this sub-menu is shown.")]
		private bool disableOtherGroups = true;

		[SerializeField]
		[Tooltip("Select the first selectable child whenever the sub-menu is shown. (If no other child is selected.)")]
		private bool selectFirstChildAlways = true;

		[SerializeField]
		[Tooltip("Select the first selectable child when the sub-menu is shown the first time. (If no other child is selected.)")]
		private bool selectFirstChildOnce;

		protected PlayerActions playerActions;

		protected UINavigationGroup navigationGroup;

		protected InterfaceStateManager stateManager;

		private bool didJustOpenOrGainFocus;

		private bool didSelectFirstChildOnce;

		public string Title => submenuTitle;

		public bool OverrideBackAction => overrideBackAction;

		public bool IsOpen { get; private set; }

		public bool HasFocus { get; private set; }

		public int EnableWithMenusParameter { get; private set; }

		public bool IsAnimationOpenAndPlaying
		{
			get
			{
				if (transitionAnimation != null)
				{
					return transitionAnimation.IsInAndPlaying;
				}
				return false;
			}
		}

		public event Action<IMenuWithEvents> MenuOpened;

		public event Action<IMenuWithEvents> MenuClosed;

		public event Action MenuDestroyed;

		public event Action<IMenuWithEvents, int> EnableWithMenusParameterChanged;

		public event Action<UISubMenu> PressedBackButton;

		protected virtual void Awake()
		{
			playerActions = PlayerActions.Instance;
			transitionAnimation = GetComponent<CodeAnimation>();
		}

		protected virtual void OnDestroy()
		{
			this.MenuDestroyed?.Invoke();
		}

		public virtual void Init(InterfaceStateManager interfaceStateManager)
		{
			stateManager = interfaceStateManager;
			navigationGroup = base.gameObject.GetOrAddComponent<UINavigationGroup>();
			navigationGroup.AllowAutoSelectableDelay = blockAutoSelectOnUINavEnable;
			stateManager.AddGroup(navigationGroup);
		}

		protected virtual void Update()
		{
			if (!IsOpen || !HasFocus)
			{
				return;
			}
			if (didJustOpenOrGainFocus)
			{
				didJustOpenOrGainFocus = false;
				if (!navigationGroup.NavigationEnabled)
				{
					navigationGroup.AllowAutoSelectableDelay = blockAutoSelectOnUINavEnable;
					stateManager.EnableGroup(navigationGroup, disableOtherGroups);
				}
				if (selectFirstChildAlways || (!didSelectFirstChildOnce && selectFirstChildOnce))
				{
					didSelectFirstChildOnce = true;
					SelectFirstChild(ignoreIfAlreadyHasSelected: true);
				}
			}
			else
			{
				UpdateGamepads();
			}
		}

		public virtual void Open()
		{
			IsOpen = true;
			HasFocus = true;
			if (transitionAnimation != null)
			{
				transitionAnimation.PlayIn();
			}
			didJustOpenOrGainFocus = true;
			this.MenuOpened?.Invoke(this);
		}

		public virtual void Close()
		{
			IsOpen = false;
			HasFocus = false;
			if (transitionAnimation != null)
			{
				transitionAnimation.PlayOut();
			}
			didJustOpenOrGainFocus = false;
			stateManager.DisableGroup(navigationGroup);
			this.MenuClosed?.Invoke(this);
		}

		public virtual void OnParentClose()
		{
		}

		public virtual void OnGainedFocus()
		{
			didJustOpenOrGainFocus = IsOpen;
			HasFocus = IsOpen;
		}

		public virtual void OnBackPressed()
		{
			if (overrideBackAction)
			{
				backAction?.Invoke();
			}
		}

		public virtual void SetFocus(bool focus)
		{
			HasFocus = focus;
		}

		protected virtual void UpdateGamepads()
		{
			if (playerActions.m_back.WasPressed)
			{
				this.PressedBackButton?.Invoke(this);
			}
		}

		protected void SetEnableWithMenusParameter(int parameter)
		{
			if (EnableWithMenusParameter != parameter)
			{
				EnableWithMenusParameter = parameter;
				this.EnableWithMenusParameterChanged?.Invoke(this, parameter);
			}
		}

		protected void SelectFirstChild(bool ignoreIfAlreadyHasSelected)
		{
			navigationGroup.SelectFirstChild(ignoreIfAlreadyHasSelected);
		}
	}
}

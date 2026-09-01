using System;
using System.Collections.Generic;
using GamepadUI.StateManager.Core;
using Landfall.TABS;
using Landfall.TABS.GameMode;
using Landfall.TABS.Services;
using Landfall.TABS_Input;
using TFBGames;
using UnityEngine;

namespace UIStateManager
{
	public class InterfaceStateManager : UINavigationGroupManager
	{
		[Space(20f)]
		[SerializeField]
		protected List<UIComponentContainer> interfaceComponents;

		private UIComponentContainer currentState;

		private Stack<UIComponentContainer> stackedStates;

		private ModalPanel modalPanel;

		private BaseGameMode baseGameMode;

		private bool timeLockedBeforePaused;

		private float timeScaleBeforePaused = 1f;

		private IApplicationStateTracker stateTracker;

		private bool canUIPauseGame;

		public bool IsDefaultState { get; private set; }

		public bool OpenUIComponent(UIComponent component, bool backgroundPrevious = false)
		{
			if (!CheckUICanOpen())
			{
				return false;
			}
			foreach (UIComponentContainer interfaceComponent in interfaceComponents)
			{
				if (interfaceComponent.Component.State == UIState.Open)
				{
					CloseUIComponent(interfaceComponent, backgroundPrevious);
					if (!interfaceComponent.IsStackable)
					{
						stackedStates.Pop();
					}
				}
			}
			component.Open?.Invoke();
			EnableGroup(component, disableOthers: true);
			currentState = GetComponentContainer(component);
			switch (currentState.PauseState)
			{
			case UIPauseState.Pause:
				Pause();
				break;
			case UIPauseState.Unpause:
				Unpause();
				break;
			default:
				throw new ArgumentOutOfRangeException();
			case UIPauseState.None:
				break;
			}
			IsDefaultState = currentState.DefaultState;
			if (currentState.IsStackable)
			{
				stackedStates.Push(currentState);
				if (currentState.DefaultState)
				{
					PlayerActions.Instance.ClearInputState();
				}
			}
			return true;
		}

		public T GetUIComponent<T>() where T : UIComponentBase
		{
			if (interfaceComponents == null || interfaceComponents.Count <= 0)
			{
				return null;
			}
			foreach (UIComponentContainer interfaceComponent in interfaceComponents)
			{
				if (interfaceComponent.Component is T result)
				{
					return result;
				}
			}
			return null;
		}

		protected override void Awake()
		{
			base.Awake();
			foreach (UIComponentContainer interfaceComponent in interfaceComponents)
			{
				if (interfaceComponent.EnableOnAwake && interfaceComponent.ContainerObject != null)
				{
					interfaceComponent.ContainerObject.SetActive(value: true);
				}
			}
			stackedStates = new Stack<UIComponentContainer>();
			modalPanel = ServiceLocator.GetService<ModalPanel>();
			baseGameMode = ServiceLocator.GetService<GameModeService>().CurrentGameMode;
			stateTracker = ServiceLocator.GetService<IApplicationStateTracker>();
			canUIPauseGame = !(baseGameMode is OnlineMultiplayerGameMode);
		}

		private void OnEnable()
		{
			if (stateTracker != null)
			{
				stateTracker.OnApplicationSuspended += OpenEscapeMenu;
			}
		}

		private void OnDisable()
		{
			if (stateTracker != null)
			{
				stateTracker.OnApplicationSuspended -= OpenEscapeMenu;
			}
		}

		protected virtual void Start()
		{
			for (int num = interfaceComponents.Count - 1; num > -1; num--)
			{
				UIComponentContainer uIComponentContainer = interfaceComponents[num];
				if (uIComponentContainer.ContainerObject == null)
				{
					interfaceComponents.RemoveAt(num);
				}
				else
				{
					uIComponentContainer.ChangeStateEvents.RemoveAll((ChangeStateEvent e) => e.ComponentToOpen == null);
				}
			}
			Init();
		}

		protected void Update()
		{
			if ((modalPanel != null && modalPanel.IsPopupOpen) || currentState == null)
			{
				return;
			}
			PopStackOnAllComponentsClosed();
			foreach (ChangeStateEvent changeStateEvent in currentState.ChangeStateEvents)
			{
				if (changeStateEvent.PlayerAction != null && changeStateEvent.PlayerAction.WasPressed)
				{
					UserOpenUIComponent(changeStateEvent);
				}
			}
		}

		private ChangeStateEvent GetEscapeMenuChangeStateEvent()
		{
			if (currentState == null)
			{
				return null;
			}
			foreach (ChangeStateEvent changeStateEvent in currentState.ChangeStateEvents)
			{
				if (changeStateEvent.ComponentToOpen is EscapeMenuComponent)
				{
					return changeStateEvent;
				}
			}
			return null;
		}

		private void OpenEscapeMenu()
		{
			ChangeStateEvent escapeMenuChangeStateEvent = GetEscapeMenuChangeStateEvent();
			if (escapeMenuChangeStateEvent != null)
			{
				UserOpenUIComponent(escapeMenuChangeStateEvent);
			}
		}

		private void Init()
		{
			foreach (UIComponentContainer interfaceComponent in interfaceComponents)
			{
				interfaceComponent.Component.Init(this);
				CreateNavigationGroup(interfaceComponent.Component);
				UISubMenu[] componentsInChildren = interfaceComponent.Component.GetComponentsInChildren<UISubMenu>(includeInactive: true);
				if (componentsInChildren != null)
				{
					UISubMenu[] array = componentsInChildren;
					for (int i = 0; i < array.Length; i++)
					{
						array[i].Init(this);
					}
				}
				MapButtonEvents(interfaceComponent.ChangeStateEvents);
			}
			foreach (UIComponentContainer interfaceComponent2 in interfaceComponents)
			{
				if (interfaceComponent2.DefaultState)
				{
					OpenUIComponent(interfaceComponent2.Component);
				}
			}
		}

		private void MapButtonEvents(List<ChangeStateEvent> buttons)
		{
			foreach (ChangeStateEvent buttonEvent in buttons)
			{
				buttonEvent.SetPlayerAction(PlayerActions.Instance);
				if (buttonEvent.Button != null)
				{
					buttonEvent.Button.onClick.AddListener(delegate
					{
						UserOpenUIComponent(buttonEvent);
					});
				}
			}
		}

		private void CreateNavigationGroup(UIComponentBase component)
		{
			base.AddGroup(component);
		}

		private void UserOpenUIComponent(ChangeStateEvent changeStateEvent)
		{
			if (!modalPanel.IsPopupOpen && !TABSSceneManager.IsLoading && OpenUIComponent(changeStateEvent.ComponentToOpen, changeStateEvent.BackgroundWhenOpened))
			{
				changeStateEvent.OnPressedEvent.Invoke();
			}
		}

		private UIComponentContainer GetComponentContainer(UIComponent component)
		{
			foreach (UIComponentContainer interfaceComponent in interfaceComponents)
			{
				if (interfaceComponent.Component == component)
				{
					return interfaceComponent;
				}
			}
			return null;
		}

		private bool CheckUICanOpen()
		{
			if (currentState == null)
			{
				return true;
			}
			bool num = currentState.Component.State == UIState.Open && currentState.IsStackable;
			bool flag = currentState.Component.State == UIState.Closed;
			return num || flag;
		}

		private void PopStackOnAllComponentsClosed()
		{
			if (!AllComponentsClosed() || stackedStates == null || stackedStates.Count <= 0)
			{
				return;
			}
			UIComponentContainer uIComponentContainer = stackedStates.Pop();
			if (stackedStates.Count == 0 || !uIComponentContainer.HasStackParent)
			{
				foreach (UIComponentContainer interfaceComponent in interfaceComponents)
				{
					if (interfaceComponent.DefaultState)
					{
						OpenUIComponent(interfaceComponent.Component);
						break;
					}
				}
				return;
			}
			OpenUIComponent(uIComponentContainer.Component);
		}

		private bool AllComponentsClosed()
		{
			foreach (UIComponentContainer interfaceComponent in interfaceComponents)
			{
				if (interfaceComponent.Component.State == UIState.Open)
				{
					return false;
				}
			}
			return true;
		}

		private void CloseUIComponent(UIComponentContainer container, bool background = false)
		{
			UIComponent component = container.Component;
			if (component.State != UIState.Closed)
			{
				if (background)
				{
					component.Background();
				}
				else
				{
					component.Close();
				}
				if (container.PauseState == UIPauseState.Pause)
				{
					Unpause();
				}
			}
		}

		private void Pause()
		{
			if ((baseGameMode == null || !baseGameMode.MatchOver) && canUIPauseGame)
			{
				UIScreenInputBlocker.DoBlockInput(open: true);
				ITimeService service = ServiceLocator.GetService<ITimeService>();
				timeLockedBeforePaused = service.IsLocked();
				if (!timeLockedBeforePaused)
				{
					timeScaleBeforePaused = service.CurrentTargetTimeScale;
				}
				service.Unlock();
				service.SetState(0f, 0f);
				service.Pause();
				service.Lock();
			}
		}

		private void Unpause()
		{
			if ((baseGameMode == null || !baseGameMode.MatchOver) && canUIPauseGame)
			{
				UIScreenInputBlocker.DoBlockInput(open: false);
				ITimeService service = ServiceLocator.GetService<ITimeService>();
				service.Unlock();
				service.UnPause();
				service.SetState(timeScaleBeforePaused, 0f);
				if (timeLockedBeforePaused && !currentState.DefaultState)
				{
					service.Lock();
				}
			}
		}
	}
}

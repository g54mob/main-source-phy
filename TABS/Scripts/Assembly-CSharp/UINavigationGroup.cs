using System;
using System.Collections.Generic;
using BitCode.Extensions;
using Landfall.TABS_Input;
using TFBGames;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UINavigationGroup : MonoBehaviour
{
	[SerializeField]
	[Tooltip("Enable/disable this component when the group is enabled/disabled.")]
	protected bool autoEnableComponent = true;

	[SerializeField]
	[Tooltip("Keep a history of children that were selected, up to this amount. Must be at least 1.")]
	protected int historySize = 10;

	[SerializeField]
	[Tooltip("Set the interactable state of a CanvasGroup when the group is enabled/disabled.\nNOTE: If used this will add a CanvasGroup if none is attached.")]
	protected bool useCanvasGroup;

	[SerializeField]
	[Tooltip("Select the first valid game object in this list when this component starts up.")]
	protected GameObject[] defaultObjects;

	[SerializeField]
	[Tooltip("When this group is enabled and it does not have any selected children, then deselect the current selection if the selection belongs to a disabled group (or to no group).")]
	protected bool deselectWhenOtherDisabledGroupHasFocus = true;

	[SerializeField]
	[Tooltip("If the UINavigationGroup should auto select the previous button when returning to an active state.")]
	protected bool autoHighlightSelectableWhenActive = true;

	[SerializeField]
	[Tooltip("If using UISubMenus, keep this NavigationGroup from being disabled when enabling those SubMenu groups. Useful for TabbedUIComponents.")]
	protected bool keepEnabledWhenNavigatingSubMenus;

	[SerializeField]
	[Tooltip("Insert Selectable UI Game Objects here when they should be ignored from the history of previously selected children.")]
	private List<GameObject> ignoreFromHistory;

	private Transform cachedTransform;

	private Canvas canvas;

	private CanvasGroup canvasGroup;

	private bool pendingDeselect;

	private DelaySelectable delaySelectable = new DelaySelectable();

	private static List<UINavigationGroup> activeGroups = new List<UINavigationGroup>();

	private static UINavigationGroup modalGroup;

	private List<GameObject> history = new List<GameObject>();

	public bool AllowAutoSelectableDelay { get; set; }

	public bool NavigationEnabled { get; private set; }

	public bool KeepEnabledWhenNavigatingSubMenus => keepEnabledWhenNavigatingSubMenus;

	public bool CanvasEnabled
	{
		get
		{
			if (canvas != null && canvas.enabled)
			{
				return canvas.gameObject.activeInHierarchy;
			}
			return false;
		}
	}

	public bool IsModal => this == modalGroup;

	public event Action Enabled;

	public event Action Disabled;

	protected virtual void Awake()
	{
		cachedTransform = base.transform;
		if (useCanvasGroup)
		{
			canvasGroup = base.gameObject.GetOrAddComponent<CanvasGroup>();
		}
		canvas = GetComponentInParent<Canvas>();
		UpdateCanvasGroup();
	}

	protected virtual void Start()
	{
		SelectDefault();
		UpdateCanvasGroup();
	}

	protected virtual void OnDestroy()
	{
		RemoveGroup(this);
	}

	protected virtual void OnEnable()
	{
		if (autoEnableComponent && !NavigationEnabled)
		{
			EnableNavigation();
		}
	}

	protected virtual void OnDisable()
	{
		if (autoEnableComponent && NavigationEnabled)
		{
			DisableNavigation();
		}
	}

	protected virtual void Update()
	{
		if (pendingDeselect)
		{
			pendingDeselect = false;
			EventSystem current = EventSystem.current;
			if (deselectWhenOtherDisabledGroupHasFocus && current != null && current.currentSelectedGameObject != null)
			{
				UINavigationGroup componentInParent = current.currentSelectedGameObject.GetComponentInParent<UINavigationGroup>();
				if (componentInParent != null && componentInParent != this && !componentInParent.NavigationEnabled)
				{
					current.SetSelectedGameObject(null);
				}
			}
		}
		if (autoHighlightSelectableWhenActive)
		{
			UpdateSelection();
		}
		if (delaySelectable.IsDelayed && Time.frameCount > delaySelectable.FrameDelay)
		{
			EventSystem current2 = EventSystem.current;
			if (current2 != null && delaySelectable.GameObjectToSelect != null)
			{
				current2.SetSelectedGameObject(delaySelectable.GameObjectToSelect);
				delaySelectable.GameObjectToSelect = null;
				autoHighlightSelectableWhenActive = true;
			}
			delaySelectable.IsDelayed = false;
			AllowAutoSelectableDelay = false;
		}
	}

	public virtual void EnableNavigation()
	{
		NavigationEnabled = true;
		if (autoEnableComponent)
		{
			base.enabled = NavigationEnabled;
		}
		AddGroup(this);
		UpdateCanvasGroup();
		UpdateSelection();
		pendingDeselect = deselectWhenOtherDisabledGroupHasFocus;
		this.Enabled?.Invoke();
	}

	public virtual void DisableNavigation()
	{
		if (IsModal)
		{
			modalGroup = null;
		}
		NavigationEnabled = false;
		if (autoEnableComponent)
		{
			base.enabled = NavigationEnabled;
		}
		RemoveGroup(this);
		UpdateCanvasGroup();
		pendingDeselect = false;
		this.Disabled?.Invoke();
	}

	public void MakeModal()
	{
		modalGroup = this;
	}

	public void SetAutoEnableComponent(bool enable)
	{
		autoEnableComponent = enable;
	}

	public void SelectFirstChild(bool ignoreIfAlreadyHasSelected)
	{
		EventSystem current = EventSystem.current;
		if (current == null || (ignoreIfAlreadyHasSelected && current.currentSelectedGameObject != null && IsSelectableInGroup(this, current.currentSelectedGameObject)))
		{
			return;
		}
		Selectable[] componentsInChildren = GetComponentsInChildren<Selectable>();
		if (componentsInChildren.Length == 0)
		{
			return;
		}
		int i = 0;
		for (int num = componentsInChildren.Length; i < num; i++)
		{
			Selectable selectable = componentsInChildren[i];
			if (IsSelectableInGroup(this, selectable.gameObject))
			{
				if (AllowAutoSelectableDelay)
				{
					QueueNextDelayedSelectable(selectable.gameObject);
				}
				else
				{
					current.SetSelectedGameObject(selectable.gameObject);
				}
				break;
			}
		}
	}

	public void SetAutoSelect(bool autoSelect)
	{
		autoHighlightSelectableWhenActive = autoSelect;
		AllowAutoSelectableDelay = !autoSelect;
	}

	private void QueueNextDelayedSelectable(GameObject selecGameObject)
	{
		autoHighlightSelectableWhenActive = false;
		delaySelectable.SetDelay(selecGameObject);
	}

	private static void AddGroup(UINavigationGroup navigationGroup)
	{
		if (activeGroups != null && !activeGroups.Contains(navigationGroup))
		{
			activeGroups.Add(navigationGroup);
		}
	}

	private static void RemoveGroup(UINavigationGroup navigationGroup)
	{
		if (activeGroups != null && activeGroups.Contains(navigationGroup))
		{
			activeGroups.Remove(navigationGroup);
		}
	}

	private void UpdateCanvasGroup()
	{
		if (useCanvasGroup && canvasGroup != null)
		{
			canvasGroup.interactable = NavigationEnabled;
		}
	}

	private void UpdateSelection()
	{
		if (!NavigationEnabled || !autoHighlightSelectableWhenActive || AllowAutoSelectableDelay)
		{
			return;
		}
		EventSystem current = EventSystem.current;
		if (current == null)
		{
			return;
		}
		GameObject currentSelectedGameObject = current.currentSelectedGameObject;
		if (!IsSelectable(currentSelectedGameObject))
		{
			SelectPrevious();
			return;
		}
		bool flag = false;
		if (ignoreFromHistory != null)
		{
			flag = ignoreFromHistory.Contains(currentSelectedGameObject);
		}
		if (IsChildOfGroup(this, currentSelectedGameObject) && !flag)
		{
			AddToHistory(currentSelectedGameObject);
		}
	}

	private bool IsSelectable(GameObject objectToTest)
	{
		if (activeGroups == null || activeGroups.Count <= 0 || IsModal)
		{
			return IsSelectableInGroup(this, objectToTest);
		}
		bool flag = modalGroup != null;
		int i = 0;
		for (int count = activeGroups.Count; i < count; i++)
		{
			UINavigationGroup uINavigationGroup = activeGroups[i];
			if ((!flag || uINavigationGroup.IsModal) && IsSelectableInGroup(uINavigationGroup, objectToTest))
			{
				return true;
			}
		}
		return false;
	}

	private bool IsSelectableInGroup(UINavigationGroup navigationGroup, GameObject objectToTest)
	{
		if (objectToTest == null || navigationGroup == null)
		{
			return false;
		}
		if (objectToTest == null || !objectToTest.activeInHierarchy || !navigationGroup.NavigationEnabled || !IsChildOfGroup(navigationGroup, objectToTest) || !navigationGroup.CanvasEnabled)
		{
			return false;
		}
		Selectable component = objectToTest.GetComponent<Selectable>();
		if (component == null || !component.enabled || !component.interactable)
		{
			return false;
		}
		return true;
	}

	private bool IsChildOfGroup(UINavigationGroup navigationGroup, GameObject objectToTest)
	{
		if (objectToTest != null && navigationGroup != null)
		{
			return objectToTest.transform.IsChildOf(navigationGroup.transform);
		}
		return false;
	}

	private void AddToHistory(GameObject objectToAdd)
	{
		int count = history.Count;
		if (count <= 0)
		{
			history.Add(objectToAdd);
		}
		else if (!(history[count - 1] == objectToAdd))
		{
			int num = history.IndexOf(objectToAdd);
			if (num < 0)
			{
				history.Add(objectToAdd);
				return;
			}
			history.RemoveAt(num);
			history.Add(objectToAdd);
		}
	}

	protected void SelectPrevious()
	{
		EventSystem current = EventSystem.current;
		if (current == null || PlayerActions.Instance.InputType != InputType.Controller || history == null)
		{
			return;
		}
		int count = history.Count;
		if (count <= 0)
		{
			return;
		}
		for (int num = count - 1; num >= 0; num--)
		{
			GameObject gameObject = history[num];
			if (IsSelectable(gameObject))
			{
				current.SetSelectedGameObject(gameObject);
				break;
			}
		}
	}

	protected bool SelectDefault()
	{
		EventSystem current = EventSystem.current;
		if (current == null || PlayerActions.Instance.InputType != InputType.Controller)
		{
			return false;
		}
		if (cachedTransform == null)
		{
			cachedTransform = base.transform;
		}
		if (defaultObjects == null || defaultObjects.Length == 0)
		{
			return false;
		}
		int i = 0;
		for (int num = defaultObjects.Length; i < num; i++)
		{
			GameObject gameObject = defaultObjects[i];
			if (IsSelectable(gameObject))
			{
				current.SetSelectedGameObject(gameObject);
				return true;
			}
		}
		return false;
	}
}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ModIO.UI
{
	public class NavigationManager : MonoBehaviour
	{
		private static NavigationManager _instance;

		public CanvasGroup menuBar;

		public bool isMouseMode = true;

		private Dictionary<IBrowserView, GameObject> m_lastViewSelection = new Dictionary<IBrowserView, GameObject>();

		private Selectable m_currentHoverSelectable;

		private Dictionary<string, float> m_lastAxisValues = new Dictionary<string, float>();

		public static NavigationManager instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = UIUtilities.FindComponentInAllScenes<NavigationManager>(includeInactive: true);
					if (_instance == null)
					{
						_instance = new GameObject("Navigation Manager").AddComponent<NavigationManager>();
					}
				}
				return _instance;
			}
		}

		private void Awake()
		{
			if (_instance == null)
			{
				_instance = this;
			}
		}

		private void Start()
		{
			ViewManager.instance.onBeforeDefocusView.AddListener(OnDefocusView);
			ViewManager.instance.onAfterFocusView.AddListener(OnFocusView);
		}

		private void Update()
		{
			if (ViewManager.instance.currentFocus != null)
			{
				UpdateInputMethod();
				ProcessViewInputs(ViewManager.instance.currentFocus);
			}
		}

		public void UpdateInputMethod()
		{
			bool flag = Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f || Input.GetButton("Submit") || Input.GetButton("Cancel");
			bool flag2 = Input.GetAxis("Mouse X") != 0f || Input.GetAxis("Mouse Y") != 0f || Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetMouseButton(2);
			if (flag && isMouseMode)
			{
				isMouseMode = false;
				if (m_currentHoverSelectable != null)
				{
					ExecuteEvents.Execute(m_currentHoverSelectable.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerExitHandler);
					m_currentHoverSelectable = null;
				}
			}
			else if (!isMouseMode && flag2 && !flag)
			{
				isMouseMode = true;
				EventSystem.current.SetSelectedGameObject(null);
				m_currentHoverSelectable = GetHoveredSelectable();
				if (m_currentHoverSelectable != null)
				{
					ExecuteEvents.Execute(m_currentHoverSelectable.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerEnterHandler);
				}
			}
		}

		public void ProcessViewInputs(IBrowserView view)
		{
			ViewControlBindings component = view.gameObject.GetComponent<ViewControlBindings>();
			if (!(component != null))
			{
				return;
			}
			foreach (ViewControlBindings.ButtonBinding buttonBinding in component.buttonBindings)
			{
				ViewControlBindings.ButtonTriggerCondition buttonTriggerCondition = ViewControlBindings.ButtonTriggerCondition.OnDown;
				if ((buttonBinding.condition & buttonTriggerCondition) == buttonTriggerCondition && Input.GetButtonDown(buttonBinding.inputName))
				{
					buttonBinding.actions.Invoke();
				}
				buttonTriggerCondition = ViewControlBindings.ButtonTriggerCondition.OnHeld;
				if ((buttonBinding.condition & buttonTriggerCondition) == buttonTriggerCondition && Input.GetButton(buttonBinding.inputName))
				{
					buttonBinding.actions.Invoke();
				}
				buttonTriggerCondition = ViewControlBindings.ButtonTriggerCondition.OnUp;
				if ((buttonBinding.condition & buttonTriggerCondition) == buttonTriggerCondition && Input.GetButtonUp(buttonBinding.inputName))
				{
					buttonBinding.actions.Invoke();
				}
			}
			foreach (ViewControlBindings.KeyCodeBinding keyCodeBinding in component.keyCodeBindings)
			{
				ViewControlBindings.ButtonTriggerCondition buttonTriggerCondition2 = ViewControlBindings.ButtonTriggerCondition.OnDown;
				if ((keyCodeBinding.condition & buttonTriggerCondition2) == buttonTriggerCondition2 && Input.GetKeyDown(keyCodeBinding.keyCode))
				{
					keyCodeBinding.actions.Invoke();
				}
				buttonTriggerCondition2 = ViewControlBindings.ButtonTriggerCondition.OnHeld;
				if ((keyCodeBinding.condition & buttonTriggerCondition2) == buttonTriggerCondition2 && Input.GetKey(keyCodeBinding.keyCode))
				{
					keyCodeBinding.actions.Invoke();
				}
				buttonTriggerCondition2 = ViewControlBindings.ButtonTriggerCondition.OnUp;
				if ((keyCodeBinding.condition & buttonTriggerCondition2) == buttonTriggerCondition2 && Input.GetKeyUp(keyCodeBinding.keyCode))
				{
					keyCodeBinding.actions.Invoke();
				}
			}
			foreach (ViewControlBindings.AxisBinding axisBinding in component.axisBindings)
			{
				float axisRaw = Input.GetAxisRaw(axisBinding.inputName);
				float value = 0f;
				if (!m_lastAxisValues.TryGetValue(axisBinding.inputName, out value))
				{
					value = axisRaw;
				}
				m_lastAxisValues[axisBinding.inputName] = axisRaw;
				bool flag = axisRaw > axisBinding.thresholdValue;
				bool flag2 = value > axisBinding.thresholdValue;
				bool flag3 = axisRaw < axisBinding.thresholdValue;
				bool flag4 = value < axisBinding.thresholdValue;
				bool flag5 = !flag && !flag3;
				bool flag6 = !flag2 && !flag4;
				ViewControlBindings.AxisTriggerCondition axisTriggerCondition = ViewControlBindings.AxisTriggerCondition.BecameGreaterThan;
				if ((axisBinding.condition & axisTriggerCondition) == axisTriggerCondition && flag && !flag2)
				{
					axisBinding.actions.Invoke(axisRaw);
				}
				axisTriggerCondition = ViewControlBindings.AxisTriggerCondition.BecameLessThan;
				if ((axisBinding.condition & axisTriggerCondition) == axisTriggerCondition && flag3 && !flag4)
				{
					axisBinding.actions.Invoke(axisRaw);
				}
				axisTriggerCondition = ViewControlBindings.AxisTriggerCondition.BecameEqualTo;
				if ((axisBinding.condition & axisTriggerCondition) == axisTriggerCondition && flag5 && !flag6)
				{
					axisBinding.actions.Invoke(axisRaw);
				}
				axisTriggerCondition = ViewControlBindings.AxisTriggerCondition.IsGreaterThan;
				if ((axisBinding.condition & axisTriggerCondition) == axisTriggerCondition && flag)
				{
					axisBinding.actions.Invoke(axisRaw);
				}
				axisTriggerCondition = ViewControlBindings.AxisTriggerCondition.IsLessThan;
				if ((axisBinding.condition & axisTriggerCondition) == axisTriggerCondition && flag3)
				{
					axisBinding.actions.Invoke(axisRaw);
				}
				axisTriggerCondition = ViewControlBindings.AxisTriggerCondition.IsEqualTo;
				if ((axisBinding.condition & axisTriggerCondition) == axisTriggerCondition && flag5)
				{
					axisBinding.actions.Invoke(axisRaw);
				}
			}
		}

		private void LateUpdate()
		{
			IBrowserView currentFocus = ViewManager.instance.currentFocus;
			if (currentFocus == null)
			{
				return;
			}
			GameObject gameObject = EventSystem.current.currentSelectedGameObject;
			if (isMouseMode)
			{
				m_currentHoverSelectable = GetHoveredSelectable();
				if (m_currentHoverSelectable != null && IsValidSelection(m_currentHoverSelectable.gameObject) && m_currentHoverSelectable.navigation.mode != Navigation.Mode.None)
				{
					gameObject = m_currentHoverSelectable.gameObject;
				}
			}
			else if (!IsValidSelection(gameObject))
			{
				gameObject = ReacquireSelectionForView(currentFocus);
				EventSystem.current.SetSelectedGameObject(gameObject);
			}
			if (gameObject != null)
			{
				m_lastViewSelection[ViewManager.instance.currentFocus] = gameObject;
			}
		}

		public void OnDefocusView(IBrowserView view)
		{
			if (EventSystem.current.currentSelectedGameObject != null)
			{
				EventSystem.current.SetSelectedGameObject(null);
			}
			if (isMouseMode && m_currentHoverSelectable != null)
			{
				ExecuteEvents.Execute(m_currentHoverSelectable.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerExitHandler);
			}
			view.canvasGroup.interactable = false;
		}

		public void OnFocusView(IBrowserView view)
		{
			view.canvasGroup.interactable = true;
			if (menuBar != null)
			{
				menuBar.interactable = view.isRootView;
			}
			GameObject gameObject = EventSystem.current.currentSelectedGameObject;
			if (isMouseMode)
			{
				m_currentHoverSelectable = GetHoveredSelectable();
				if (m_currentHoverSelectable != null)
				{
					ExecuteEvents.Execute(m_currentHoverSelectable.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerEnterHandler);
				}
				gameObject = null;
			}
			else if (!IsValidSelection(gameObject))
			{
				gameObject = ReacquireSelectionForView(view);
			}
			if (gameObject != EventSystem.current.currentSelectedGameObject)
			{
				EventSystem.current.SetSelectedGameObject(gameObject);
			}
		}

		public GameObject ReacquireSelectionForView(IBrowserView view)
		{
			GameObject value = null;
			if (m_lastViewSelection.TryGetValue(view, out value) && IsValidSelection(value))
			{
				return value;
			}
			foreach (Selectable item in view.onFocusPriority)
			{
				if (IsValidSelection(item.gameObject))
				{
					return item.gameObject;
				}
			}
			Selectable[] componentsInChildren = view.gameObject.GetComponentsInChildren<Selectable>();
			foreach (Selectable selectable in componentsInChildren)
			{
				if (selectable.IsActive() && selectable.interactable)
				{
					return selectable.gameObject;
				}
			}
			return null;
		}

		public static Selectable GetHoveredSelectable()
		{
			PointerEventData pointerEventData = new PointerEventData(EventSystem.current)
			{
				pointerId = 0
			};
			pointerEventData.position = Input.mousePosition;
			List<RaycastResult> list = new List<RaycastResult>();
			EventSystem.current.RaycastAll(pointerEventData, list);
			GameObject gameObject = null;
			foreach (RaycastResult item in list)
			{
				if (item.gameObject != null)
				{
					gameObject = item.gameObject;
					break;
				}
			}
			if (gameObject != null)
			{
				Transform parent = gameObject.transform;
				while (parent != null)
				{
					Selectable component = parent.GetComponent<Selectable>();
					if (component != null && component.IsActive())
					{
						return component;
					}
					parent = parent.parent;
				}
			}
			return null;
		}

		private static bool IsValidSelection(GameObject selectionObject)
		{
			if (selectionObject == null)
			{
				return false;
			}
			Selectable component = selectionObject.GetComponent<Selectable>();
			if (selectionObject.activeInHierarchy && component != null && component.interactable)
			{
				return component.IsActive();
			}
			return false;
		}
	}
}

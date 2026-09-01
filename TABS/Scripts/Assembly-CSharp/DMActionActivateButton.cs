using GamepadUI.StateManager.Core;
using InControl;
using Landfall.TABS_Input;
using UnityEngine;
using UnityEngine.UI;

public class DMActionActivateButton : MonoBehaviour
{
	[SerializeField]
	private string actionName;

	[SerializeField]
	private CanvasGroup contextGroup;

	private UIComponentBase uiComponentContext;

	private PlayerAction action;

	private Button button;

	private bool canInvoke
	{
		get
		{
			if (contextGroup == null)
			{
				if (uiComponentContext == null)
				{
					uiComponentContext = GetComponentInParent<UIComponentBase>();
				}
				return uiComponentContext.IsActive;
			}
			return contextGroup.interactable;
		}
	}

	private void Start()
	{
		action = PlayerActions.Instance.GetPlayerActionByName(actionName);
		button = GetComponent<Button>();
	}

	private void Update()
	{
		if (action != null && action.WasPressed && canInvoke && button != null)
		{
			button.onClick?.Invoke();
		}
	}
}

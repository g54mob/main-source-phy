using System;
using System.Collections.Generic;
using Landfall.TABS_Input;
using TFBGames;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WorkshopCursor : MonoBehaviour
{
	public float movementSpeed;

	[SerializeField]
	private Button leftPageButton;

	[SerializeField]
	private Button rightPageButton;

	[SerializeField]
	private GameObject explorerView;

	[SerializeField]
	private Image nameInputImage;

	private PlayerActions playerActions;

	private Selectable lastSelected;

	private List<RaycastResult> raycastResults;

	private bool inControllerMode;

	private Image cursorImage;

	private CursorController cursorController;

	private InputService inputService;

	private BaseEventData eventData;

	private void Start()
	{
		playerActions = PlayerActions.Instance;
		cursorImage = GetComponent<Image>();
		raycastResults = new List<RaycastResult>();
		inControllerMode = playerActions.InputType == InputType.Controller;
		cursorController = ServiceLocator.GetService<CursorController>();
		inputService = ServiceLocator.GetService<InputService>();
		inputService.InputChanged += OnInputChange;
		eventData = new BaseEventData(EventSystem.current);
		OnInputChange(playerActions.InputType);
	}

	private void Update()
	{
		if (!inControllerMode)
		{
			return;
		}
		cursorController.AllowCursorMovement(inControllerMode);
		base.transform.position = cursorController.CursorPosition;
		raycastResults.Clear();
		cursorController.GetObjectsBeneathPointer(raycastResults);
		bool flag = false;
		bool flag2 = lastSelected == null;
		if (raycastResults.Count > 0)
		{
			foreach (RaycastResult raycastResult in raycastResults)
			{
				if (!flag2 && lastSelected.gameObject == raycastResult.gameObject && lastSelected.gameObject == EventSystem.current.currentSelectedGameObject)
				{
					flag = true;
					break;
				}
				Selectable component = raycastResult.gameObject.GetComponent<Selectable>();
				bool flag3 = component == null;
				if (flag3)
				{
					component = raycastResult.gameObject.transform.parent.GetComponent<Selectable>();
					flag3 = component == null;
				}
				if (!flag3)
				{
					lastSelected = component;
					lastSelected.Select();
					flag = true;
					break;
				}
			}
		}
		if (!flag)
		{
			if (lastSelected != null)
			{
				lastSelected.OnDeselect(eventData);
			}
			lastSelected = null;
			EventSystem.current.SetSelectedGameObject(null);
		}
		if (explorerView.activeInHierarchy)
		{
			if (playerActions.m_pageRight.WasPressed)
			{
				rightPageButton.onClick.Invoke();
			}
			if (playerActions.m_pageLeft.WasPressed)
			{
				leftPageButton.onClick.Invoke();
			}
		}
	}

	private void OnInputChange(InputType inputType)
	{
		switch (inputType)
		{
		case InputType.Controller:
			inControllerMode = true;
			cursorImage.enabled = true;
			nameInputImage.enabled = true;
			break;
		case InputType.Keyboard:
			inControllerMode = false;
			cursorImage.enabled = false;
			nameInputImage.enabled = false;
			break;
		default:
			throw new ArgumentOutOfRangeException();
		case InputType.Any:
			break;
		}
	}

	private void OnDestroy()
	{
		if (inputService != null)
		{
			inputService.InputChanged -= OnInputChange;
		}
	}
}

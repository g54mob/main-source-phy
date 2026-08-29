using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ES2FrameController : MonoBehaviour
{
	public ES2FrameUI ui;

	public Transform overrideBotRight;

	public Transform overrideBotLeft;

	public bool customUpdate;

	public VisualController visualController = new VisualController();

	public bool interactable;

	private InputDispatcher inputDispatcher;

	private CanvasGroup canvasGroup;

	public void setup(InputDispatcher inputDispatcher, Action<string> onDown, Action<string> onUp, Action<string> onClick)
	{
		this.inputDispatcher = inputDispatcher;
		visualController.setup(onDown, onUp, onClick);
		canvasGroup = GetComponent<CanvasGroup>();
	}

	public void Update()
	{
		if (!customUpdate)
		{
			doUpdate(inputDispatcher);
		}
		if (canvasGroup != null)
		{
			canvasGroup.interactable = interactable;
		}
	}

	public void doUpdate(InputDispatcher inputDispatcher)
	{
		visualController.update(inputDispatcher);
		LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)ui.BotLeft);
		LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)ui.BotCenter);
		LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)ui.BotRight);
		ui.Tabs_Parent_goLeft_image.transform.parent.gameObject.SetActive(Controller.isActive());
		ui.Tabs_Parent_goRight_image.transform.parent.gameObject.SetActive(Controller.isActive());
	}

	public void setHint(string hint)
	{
	}

	public void addOrUpdateControl(List<VisualControl> controls)
	{
		foreach (VisualControl control in controls)
		{
			addOrUpdateControl(control);
		}
	}

	public void addOrUpdateControl(VisualControl newControl)
	{
		Transform parent = newControl.position switch
		{
			VisualControl.Position.BotLeft => (overrideBotLeft == null) ? ui.BotLeft : overrideBotLeft, 
			VisualControl.Position.BotCenter => ui.BotCenter, 
			VisualControl.Position.BotRight => (overrideBotRight == null) ? ui.BotRight : overrideBotRight, 
			_ => ui.BotRight, 
		};
		visualController?.addOrUpdateControl(newControl, ui.FrameControl, parent);
	}
}

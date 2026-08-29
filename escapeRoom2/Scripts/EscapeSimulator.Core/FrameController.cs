using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrameController : MonoBehaviour
{
	public class VisualControl
	{
		public string id;

		public ControllerButtonActionType controllerHint;

		public string text;

		public Position position;

		public VisualControlType type;

		public VisualControlUI ui;

		public VisualControl(string id, ControllerButtonActionType controllerHint, string text, VisualControlType type, Position position)
		{
			this.id = id;
			this.controllerHint = controllerHint;
			this.text = text;
			this.position = position;
			this.type = type;
		}
	}

	public enum VisualControlType
	{
		Button = 0,
		Text = 1,
		Gear = 2
	}

	public enum Position
	{
		TopLeft = 0,
		TopRight = 1,
		BotLeft = 2,
		BotRight = 3
	}

	private FrameUI ui;

	private Action<string> onClick;

	private List<VisualControl> currentControls = new List<VisualControl>();

	public void init(Action<string> onClick)
	{
		this.onClick = onClick;
		ui = GetComponent<FrameUI>();
		PineUI.addButtonClickDelegate(onButtonClick);
	}

	public void show(string title, params VisualControl[] visualControls)
	{
		foreach (VisualControl currentControl in currentControls)
		{
			UnityEngine.Object.Destroy(currentControl.ui.gameObject);
		}
		currentControls.Clear();
		ui.Title.text = title;
		if (title.StartsWith("%"))
		{
			Localization.translateObject(ui.Title.transform, forceNewText: true);
		}
		foreach (VisualControl visualControl in visualControls)
		{
			addNewControl(visualControl, doSyncControls: false);
		}
		syncControls();
	}

	public void addNewControl(VisualControl visualControl, bool doSyncControls)
	{
		Transform parent = visualControl.position switch
		{
			Position.TopLeft => ui.TopLeft, 
			Position.TopRight => ui.TopRight, 
			Position.BotLeft => ui.BotLeft, 
			Position.BotRight => ui.BotRight, 
			_ => ui.BotRight, 
		};
		VisualControlUI visualControlUI = UnityEngine.Object.Instantiate(ui.FrameControl, parent);
		visualControlUI.gameObject.SetActive(value: true);
		visualControl.ui = visualControlUI;
		currentControls.Add(visualControl);
		if (doSyncControls)
		{
			syncControls();
		}
	}

	public void setHint(string hint)
	{
		if (string.IsNullOrEmpty(hint))
		{
			ui.Hint.text = "";
		}
		else
		{
			ui.Hint.text = hint;
		}
	}

	public void removeControl(string id)
	{
		VisualControl visualControl = currentControls.Find((VisualControl x) => x.id == id);
		if (visualControl != null)
		{
			UnityEngine.Object.Destroy(visualControl.ui.gameObject);
			currentControls.Remove(visualControl);
		}
	}

	public void removeAllInPosition(Position position)
	{
		for (int num = currentControls.Count - 1; num >= 0; num--)
		{
			VisualControl visualControl = currentControls[num];
			if (visualControl.position == position)
			{
				removeControl(visualControl.id);
			}
		}
	}

	public void removeAll()
	{
		for (int num = currentControls.Count - 1; num >= 0; num--)
		{
			UnityEngine.Object.Destroy(currentControls[num].ui.gameObject);
		}
		ui.Hint.text = "";
		currentControls.Clear();
		base.gameObject.SetActive(value: false);
	}

	private void syncControls()
	{
		foreach (VisualControl currentControl in currentControls)
		{
			VisualControl visualControl = currentControl;
			if (visualControl.type == VisualControlType.Text)
			{
				visualControl.ui.Button.gameObject.SetActive(value: false);
				visualControl.ui.ControllerHint.gameObject.SetActive(value: false);
				visualControl.ui.Text.gameObject.SetActive(value: true);
				visualControl.ui.Spinner.gameObject.SetActive(value: false);
				visualControl.ui.Text.text = currentControl.text;
				Localization.translateObject(visualControl.ui.Text.transform, forceNewText: true);
			}
			else if (visualControl.type == VisualControlType.Gear)
			{
				visualControl.ui.Button.gameObject.SetActive(value: false);
				visualControl.ui.ControllerHint.gameObject.SetActive(value: false);
				visualControl.ui.Text.gameObject.SetActive(value: false);
				visualControl.ui.Spinner.gameObject.SetActive(value: true);
			}
			else if (Controller.isActive())
			{
				visualControl.ui.Button.gameObject.SetActive(value: false);
				visualControl.ui.ControllerHint.gameObject.SetActive(value: true);
				visualControl.ui.Text.gameObject.SetActive(value: true);
				visualControl.ui.Spinner.gameObject.SetActive(value: false);
				visualControl.ui.ControllerHint_ControllerHint.controllerButton = currentControl.controllerHint;
				visualControl.ui.ControllerHint_ControllerHint.sync();
				visualControl.ui.Text.text = currentControl.text;
				Localization.translateObject(visualControl.ui.Text.transform, forceNewText: true);
			}
			else
			{
				visualControl.ui.Button.gameObject.SetActive(value: true);
				visualControl.ui.ControllerHint.gameObject.SetActive(value: false);
				visualControl.ui.Text.gameObject.SetActive(value: false);
				visualControl.ui.Spinner.gameObject.SetActive(value: false);
				visualControl.ui.Button_Text.text = currentControl.text;
				Localization.translateObject(visualControl.ui.Button_Text.transform, forceNewText: true);
			}
			LayoutRebuilder.ForceRebuildLayoutImmediate(currentControl.ui.root);
		}
		LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)ui.TopLeft);
		LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)ui.TopRight);
		LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)ui.BotLeft);
		LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)ui.BotRight);
	}

	private void onButtonClick(Button button)
	{
		foreach (VisualControl currentControl in currentControls)
		{
			if (currentControl.ui.Button == button)
			{
				onClick(currentControl.id);
				break;
			}
		}
	}

	private void Update()
	{
		if (Controller.controllerModeChanged)
		{
			syncControls();
		}
		if (!Controller.isActive())
		{
			return;
		}
		for (int num = currentControls.Count - 1; num >= 0; num--)
		{
			VisualControl visualControl = currentControls[num];
			if (Controller.getButtonDown(visualControl.controllerHint))
			{
				onClick(visualControl.id);
				break;
			}
		}
	}
}

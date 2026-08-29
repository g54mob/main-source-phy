using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisualController
{
	private List<VisualControl> currentControls = new List<VisualControl>();

	private Action<string> onDown;

	private Action<string> onClick;

	private Action<string> onUp;

	private bool didSetup;

	private List<VisualControl> cacheToProcess = new List<VisualControl>(16);

	public void setup(Action<string> onDown, Action<string> onUp, Action<string> onClick)
	{
		if (didSetup)
		{
			Debug.Log("Setup already done, skipping.");
		}
		didSetup = true;
		this.onDown = onDown;
		PineUI.addButtonDownDelegate(onButtonDown);
		this.onUp = onUp;
		PineUI.addButtonUpDelegate(onButtonUp);
		this.onClick = onClick;
		PineUI.addButtonClickDelegate(onButtonClick);
	}

	public void update(InputDispatcher inputDispatcher)
	{
		if (Controller.isActive())
		{
			processNonTimedInput((VisualControl item) => inputDispatcher.getDown(item.controllerHint), (VisualControl item) => inputDispatcher.getUp(item.controllerHint), (VisualControl item) => inputDispatcher.getDown(item.controllerHint));
		}
		else
		{
			processNonTimedInput((VisualControl item) => inputDispatcher.getDown(item.keyboardHint), (VisualControl item) => inputDispatcher.getUp(item.keyboardHint), (VisualControl item) => inputDispatcher.getDown(item.keyboardHint));
			processNonTimedInput((VisualControl item) => inputDispatcher.getDown(item.keybindingAction), (VisualControl item) => inputDispatcher.getUp(item.keybindingAction), (VisualControl item) => inputDispatcher.getDown(item.keybindingAction));
		}
		processTimedInput();
		updateVisuals();
	}

	public void updateVisuals()
	{
		foreach (VisualControl currentControl in currentControls)
		{
			VisualControl visualControl = currentControl;
			if (visualControl.type == VisualControl.ControlType.Text)
			{
				visualControl.ui.Button.gameObject.SetActive(value: false);
				visualControl.ui.ControllerHint.gameObject.SetActive(value: false);
				visualControl.ui.Text.gameObject.SetActive(value: true);
				visualControl.ui.Spinner.gameObject.SetActive(value: false);
				visualControl.ui.Text.text = currentControl.text;
				Localization.translateObject(visualControl.ui.Text.transform, forceNewText: true);
			}
			else if (visualControl.type == VisualControl.ControlType.Gear)
			{
				visualControl.ui.Button.gameObject.SetActive(value: false);
				visualControl.ui.ControllerHint.gameObject.SetActive(value: false);
				visualControl.ui.Text.gameObject.SetActive(value: false);
				visualControl.ui.Spinner.gameObject.SetActive(value: true);
			}
			else if (Controller.isActive())
			{
				visualControl.ui.root.gameObject.SetActive(!visualControl.hideOnController);
				visualControl.ui.Button.gameObject.SetActive(value: false);
				visualControl.ui.ControllerHint.gameObject.SetActive(value: true);
				visualControl.ui.Text.gameObject.SetActive(value: false);
				visualControl.ui.Spinner.gameObject.SetActive(value: false);
				visualControl.ui.ControllerHint_ControllerHint.controllerButton = currentControl.controllerHint;
				visualControl.ui.ControllerHint_ControllerHint.sync();
				visualControl.ui.ControllerHint_Text.text = currentControl.text;
				if (visualControl.isTimed)
				{
					float t = ((visualControl.currentDownTimestamp == 0f) ? 0f : UnityUtils.map(visualControl.currentDownTimestamp, visualControl.currentDownTimestamp + visualControl.timeToClick, 0f, 1f, Time.time));
					t = Mathf.SmoothStep(0f, 1f, t);
					visualControl.ui.ControllerHint_Fill.fillAmount = t;
				}
				Localization.translateObject(visualControl.ui.ControllerHint_Text.transform, forceNewText: true);
			}
			else
			{
				visualControl.ui.root.gameObject.SetActive(!visualControl.hideOnMouseAndKeyboard);
				visualControl.ui.Button.gameObject.SetActive(value: true);
				visualControl.ui.ControllerHint.gameObject.SetActive(value: false);
				visualControl.ui.Text.gameObject.SetActive(value: false);
				visualControl.ui.Spinner.gameObject.SetActive(value: false);
				bool flag = false;
				if (!currentControl.hiddenEscape && currentControl.keyboardHint == KeyCode.Escape)
				{
					visualControl.ui.Button_KeyboardHint.gameObject.SetActive(value: true);
					flag = true;
				}
				else if (currentControl.keyboardHintGlyph != null)
				{
					visualControl.ui.Button_KeyboardHint.gameObject.SetActive(value: true);
					visualControl.ui.Button_KeyboardHint.sprite = currentControl.keyboardHintGlyph;
					flag = true;
				}
				else
				{
					visualControl.ui.Button_KeyboardHint.gameObject.SetActive(value: false);
				}
				HorizontalLayoutGroup component = visualControl.ui.Button.GetComponent<HorizontalLayoutGroup>();
				RectOffset padding = component.padding;
				padding.left = (flag ? 2 : 10);
				component.padding = padding;
				visualControl.ui.Button_Text.text = currentControl.text;
				if (visualControl.isTimed)
				{
					float t2 = ((visualControl.currentDownTimestamp == 0f) ? 0f : UnityUtils.map(visualControl.currentDownTimestamp, visualControl.currentDownTimestamp + visualControl.timeToClick, 0f, 1f, Time.time));
					t2 = Mathf.SmoothStep(0f, 1f, t2);
					visualControl.ui.Button_Fill.fillAmount = t2;
				}
				Localization.translateObject(visualControl.ui.Button_Text.transform, forceNewText: true);
			}
			LayoutRebuilder.ForceRebuildLayoutImmediate(currentControl.ui.root);
		}
	}

	private void processNonTimedInput(Func<VisualControl, bool> onDownCondition, Func<VisualControl, bool> onUpCondition, Func<VisualControl, bool> onClickCondition)
	{
		cacheToProcess.Clear();
		cacheToProcess.AddRange(currentControls);
		for (int num = cacheToProcess.Count - 1; num >= 0; num--)
		{
			VisualControl visualControl = cacheToProcess[num];
			if (!(visualControl.ui == null) && visualControl.ui.gameObject.activeInHierarchy)
			{
				if (onDownCondition != null && onDownCondition(visualControl))
				{
					onDown?.Invoke(visualControl.id);
					if (visualControl.isTimed && !visualControl.isTimedExternallyControlled)
					{
						visualControl.currentDownTimestamp = Time.time;
					}
				}
				if (onUpCondition != null && onUpCondition(visualControl))
				{
					onUp?.Invoke(visualControl.id);
					if (visualControl.isTimed)
					{
						if (!visualControl.isTimedExternallyControlled)
						{
							visualControl.currentDownTimestamp = 0f;
						}
						visualControl.didTimedClick = false;
					}
				}
				if (onClickCondition != null && onClickCondition(visualControl) && !visualControl.isTimed)
				{
					onClick?.Invoke(visualControl.id);
				}
			}
		}
	}

	private void processTimedInput()
	{
		for (int num = currentControls.Count - 1; num >= 0; num--)
		{
			VisualControl visualControl = currentControls[num];
			if (visualControl.ui.gameObject.activeInHierarchy && visualControl.isTimed && visualControl.currentDownTimestamp != 0f && Time.time >= visualControl.currentDownTimestamp + visualControl.timeToClick && !visualControl.didTimedClick)
			{
				Debug.Log(visualControl.currentDownTimestamp);
				onClick?.Invoke(visualControl.id);
				visualControl.didTimedClick = true;
			}
		}
	}

	private void onButtonClick(Button button)
	{
		processNonTimedInput(null, null, (VisualControl item) => item.ui.Button == button);
	}

	private void onButtonDown(Button button)
	{
		processNonTimedInput((VisualControl item) => item.ui.Button == button, null, null);
	}

	private void onButtonUp(Button button)
	{
		processNonTimedInput(null, (VisualControl item) => item.ui.Button == button, null);
	}

	public void addOrUpdateControl(VisualControl[] controls, VisualControlUI template, Transform parent)
	{
		foreach (VisualControl newControl in controls)
		{
			addOrUpdateControl(newControl, template, parent);
		}
	}

	public void addOrUpdateControl(VisualControl newControl, VisualControlUI template, Transform parent)
	{
		VisualControl visualControl = currentControls.Find((VisualControl x) => x.id == newControl.id);
		if (visualControl == null)
		{
			if (template == null || parent == null)
			{
				Debug.LogError("Visual Control is not inited correctly: " + newControl.id);
				return;
			}
			VisualControlUI visualControlUI = UnityEngine.Object.Instantiate(template, parent);
			visualControlUI.gameObject.SetActive(value: true);
			if (newControl.isTimed)
			{
				SpriteState spriteState = visualControlUI.Button.spriteState;
				spriteState.pressedSprite = ((Image)visualControlUI.Button.targetGraphic).sprite;
				visualControlUI.Button.spriteState = spriteState;
			}
			newControl.ui = visualControlUI;
			currentControls.Add(newControl);
			LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)parent);
		}
		else
		{
			visualControl.text = newControl.text;
			visualControl.type = newControl.type;
			visualControl.controllerHint = newControl.controllerHint;
			visualControl.position = newControl.position;
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

	public void removeAllInPosition(VisualControl.Position position)
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
		currentControls.Clear();
	}
}
